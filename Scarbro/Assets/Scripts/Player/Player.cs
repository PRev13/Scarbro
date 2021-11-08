using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player control")]
    public float speed;
    public float jumpForce;
    public float gravityChangeCooldown;


    [Header("--Move--")]
    [SerializeField] Transform checkGroundTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    Vector2 movVector;
    bool isGravityInverse = false;
    bool canChangeGravity = true;

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

        //Backups
        checkGroundOriginalPosY = checkGroundTransform.localPosition.y;
        rigibodyOriginalGravityScale = rigi.gravityScale;
    }

    void Update()
    {
        movVector.x = inputMap.LeftDirectional_Horizontal * speed; //Get X axis

        InputJump();
        InputGravityChange();

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

            if (isGrounding)
            {
                rigi.AddForce(Vector2.up * (isGravityInverse ? -jumpForce : jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void InputGravityChange()
    {
        if (canChangeGravity == false) return;

        if (Input.GetKeyDown(KeyCode.Q) || inputMap.B_button_down)
        {
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
        if(isGravityInverse)
        {
            return -0.476f;
        }
        else
        {
            return 0.274f;
        }
    }
}
