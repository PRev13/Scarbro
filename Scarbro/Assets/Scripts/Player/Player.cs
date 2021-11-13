using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Player control")]
    public float speed;
    public float jumpForce;
    public float gravityChangeCooldown;
    public Vector3 spawnLocation;

    [Header("--Move--")]
    [SerializeField] Transform checkGroundTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    Animator anim;
    Vector2 movVector;
    bool isGravityInverse = false;
    bool canChangeGravity = true;

    //Lifes
    int lives = 3;

    //References
    Rigidbody2D rigi;

    //Config backups
    float checkGroundOriginalPosY;
    float rigibodyOriginalGravityScale;

    //GameControllerInputs manager. This help to make more easy use control
    GameControllerInputs inputMap;

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

    }

    void Update()
    {
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
        rigi.velocity = movVector;//Update velocity
    }

    void InputJump()
    {
        if (inputMap.A_button_down)
        {
            bool isGrounding = Physics2D.OverlapPoint(checkGroundTransform.position) != null; //Is touching ground?

            PlayAnimation("Robot_Jump");//Play Jump Animation
            
            if (isGrounding)
            {
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

            isGravityInverse = !isGravityInverse; //Inverse gravity
            rigi.gravityScale = isGravityInverse ? -rigibodyOriginalGravityScale : rigibodyOriginalGravityScale; //Update gravity scale
            //We update ground check postion
            Vector3 groundCheckPos = checkGroundTransform.localPosition;
            groundCheckPos.y = isGravityInverse ? -checkGroundOriginalPosY : checkGroundOriginalPosY;
            checkGroundTransform.localPosition = groundCheckPos;

            //Move spriteRenderer
            spriteRenderer.flipY = isGravityInverse;
            spriteRenderer.transform.localPosition = new Vector3(0f, GetSpritePosAtFlip(), 0f);


            //Cooldown
            canChangeGravity = false;
            Invoke(nameof(EnableGravityChange), gravityChangeCooldown);
        }
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
        lives--;
        if(lives > 0) //If we still have lives, just respawn to last door
        {
            transform.position = spawnLocation;
        }
        else //we dont have more lives, we need to reset level
        {
            print("Reset level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //function to update spawn of player
    //public void UpdateSpawn(Vector3 spawn)
    //{
        //spawnLocation = spawn;
    //}
}
