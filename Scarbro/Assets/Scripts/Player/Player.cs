using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SelectionBase]
public class Player : MonoBehaviour
{

    [Header("Player control")]
    public float speed;
    public float jumpForce;
    public float gravityChangeCooldown;
    Vector3 spawnLocation;
    bool spawnGravityInveser = false;
    int peopleSaved;     //People saved

    [Header("--Move--")]
    [SerializeField] Transform checkGroundTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    Animator anim;
    Vector2 movVector;
    bool isGravityInverse = false;
    bool canChangeGravity = true;
    bool isAbleToMove = true;
    public bool IsAbleToMove
    {
        set
        {
            isAbleToMove = value;
            if (value)
                rigi.gravityScale = isGravityInverse ? -rigibodyOriginalGravityScale : rigibodyOriginalGravityScale;
            else
            {
                rigi.gravityScale = 0f;
                rigi.velocity = Vector2.zero;
            }
        }
        get
        {
            return isAbleToMove;
        }
    }

    //Lifes
    int lives = 3;
    float livesCoolDown;

    //References
    Rigidbody2D rigi;

    //Config backups
    float checkGroundOriginalPosY;
    float rigibodyOriginalGravityScale;

    //GameControllerInputs manager. This help to make more easy use control
    GameControllerInputs inputMap;

    Dialogues dialogues;

    void Start()
    {
        //Get references
        inputMap = GameControllerInputs.GetIstance();
        rigi = GetComponent<Rigidbody2D>();
        anim = spriteRenderer.GetComponent<Animator>();

        //Backups
        checkGroundOriginalPosY = checkGroundTransform.localPosition.y;
        rigibodyOriginalGravityScale = rigi.gravityScale;

        //Default
        spawnLocation = transform.position;

        //player animator
        //anim = GetComponent<Animator>();
        dialogues = GameManager.Instance.dialogues;
    }

    void Update()
    {   
        if(isAbleToMove == false)
        {
            movVector.x = 0f;
            return;
        }

            
        movVector.x = inputMap.LeftDirectional_Horizontal * speed; //Get X axis

        InputJump();
        InputGravityChange();

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            PlayAnimation("Robot_Walk");//Play Walk Animation Right side
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            PlayAnimation("Robot_Walk");//Play Walk Animation Left side
        }
        
    }

    private void FixedUpdate()
    {
      
        movVector.y = rigi.velocity.y; //Keep gravity force
        rigi.velocity = movVector; //Update velocity
         
    }

    void InputJump()
    {
        if (inputMap.A_button_down)
        {
            bool isGrounding = Physics2D.OverlapPoint(checkGroundTransform.position) != null; //Is touching ground?
            
            if (isGrounding)
            {
                PlayAnimation("Robot_Jump");//Play Jump Animation
                SoundManager.PlaySound("Jump");// Play Jump Sound
                rigi.AddForce(Vector2.up * (isGravityInverse ? -jumpForce : jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void InputGravityChange()
    {
        if (canChangeGravity == false) return;

        if (Input.GetKeyDown(KeyCode.Q) || inputMap.B_button_down)
        {
            SoundManager.PlaySound("Inverse");//Play Inverse sound when gravity is inversed.

            InverseGravity(!isGravityInverse);

            //Cooldown
            canChangeGravity = false;
            Invoke(nameof(EnableGravityChange), gravityChangeCooldown);
        }
    }

    void InverseGravity(bool _isGravityInverse)
    {
        isGravityInverse = _isGravityInverse; //Inverse gravity
        rigi.gravityScale = isGravityInverse ? -rigibodyOriginalGravityScale : rigibodyOriginalGravityScale; //Update gravity scale
                                                                                                             //We update ground check postion
        Vector3 groundCheckPos = checkGroundTransform.localPosition;
        groundCheckPos.y = isGravityInverse ? -checkGroundOriginalPosY : checkGroundOriginalPosY;
        checkGroundTransform.localPosition = groundCheckPos;

        //Move spriteRenderer
        spriteRenderer.flipY = isGravityInverse;
        spriteRenderer.transform.localPosition = new Vector3(0f, GetSpritePosAtFlip(), 0f);
    }

    void EnableGravityChange()
    {
        canChangeGravity = true;
    }


    //THis function return where we need to move the sprite on gravity changes
    float GetSpritePosAtFlip()
    {
        return -0.1f;
    }

    void PlayAnimation (string animName)
    {
        anim.Play(animName);//Play "animName" Animation
    }

    public void Die()
    {
        if(livesCoolDown + 0.2f >= Time.time)
        {
            return;
        }

        livesCoolDown = Time.time;

        lives--;
        if(lives > 0) //If we still have lives, just respawn to last door
        {
            rigi.position = spawnLocation;
            InverseGravity(spawnGravityInveser);
            GameManager.Instance.ui.LivesUpdate(lives);
        }
        else //we dont have more lives, we need to reset level
        {
            print("Reset level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void UpdateSpawnPosition(Vector3 _pos)
    {
        spawnLocation = _pos;
        spawnGravityInveser = isGravityInverse;
    }

    public void UpdatePeopleRescueAdd1()
    {
        peopleSaved++;
        GameManager.Instance.ui.PeopleSavedUpdate(peopleSaved);
    }

    public int GetPeopleRescue()
    {
        return peopleSaved;
    }

    //function to update spawn of player
    //public void UpdateSpawn(Vector3 spawn)
    //{
        //spawnLocation = spawn;
    //}
}
