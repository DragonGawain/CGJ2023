using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    PlayerInputs inputs;
    Vector2 rabbitMove,
        desiredVelocity,
        velocity;
    bool isGrounded,
        isJumping = false;
    Ground ground;
    Rigidbody2D body;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 100f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 12f;

    [SerializeField, Range(0f, 500)]
    float jumpForce = 80;
    float maxSpeedChange;

    [SerializeField]
    GameObject lineObject;
    Line line;
    int hasJumps = 2;
    const int jumpTimerReset = 25;
    int jumpTimer = 0;

    private Animator animatorController;
    private SpriteRenderer sprite;

    const int coyoteTimerReset = 5;
    int coyoteTimer = 0;

    const int fallingJumpTimerReset = 500;
    int fallingJumpTimer = 0;

    [SerializeField]
    private GameObject jumpEffect;
    [SerializeField]
    private Transform effectContainer;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        line = lineObject.GetComponent<Line>();

        animatorController = this.gameObject.GetComponent<Animator>();

        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetIsRabbitGrounded();
        rabbitMove = inputs.Player.MoveRabbit.ReadValue<Vector2>();

        if (isJumping && !isGrounded && fallingJumpTimer > 0)
            body.velocity = new Vector2(
                Mathf.Clamp(body.velocity.x, -11.11f, 11.11f),
                Mathf.Clamp(body.velocity.y, -33.33f, 11.33f)
            );
        else
            body.velocity = new Vector2(
                Mathf.Clamp(body.velocity.x, -11.11f, 11.11f),
                Mathf.Clamp(body.velocity.y, -11.11f, 11.11f)
            );
    }

    private void FixedUpdate()
    {
        desiredVelocity =
            new Vector2(rabbitMove.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        velocity = body.velocity;

        maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        if (body.velocity.x != 0 || body.velocity.y != 0) 
            line.setRabbitMoving(true);
        else
            line.setRabbitMoving(false);

        if(rabbitMove.x != 0)
            line.setRabbitInput(true);
        else
            line.setRabbitInput(false);

        if (body.velocity.x != 0)
        {
            animatorController.SetBool("isWalking", true);
        }
        else
        {
            animatorController.SetBool("isWalking", false);
        }

        if (rabbitMove.x < 0)
        {
            sprite.flipX = true;
        }
        else if (rabbitMove.x > 0)
        {
            sprite.flipX = false;
        }

        if (isGrounded && !isJumping)
        {
            hasJumps = 2;
            coyoteTimer = -1;
        }

        if (
            (rabbitMove.y > 0 && hasJumps > 0 && jumpTimer <= 0)
            || (coyoteTimer > 0 && rabbitMove.y > 0 && hasJumps > 0 && jumpTimer <= 0)
        )
        {
            jump();
            isJumping = true;
            jumpTimer = jumpTimerReset;
        }

        if (jumpTimer > 0)
            jumpTimer--;

        if (isGrounded && isJumping && jumpTimer <= 0)
        {
            isJumping = false;

            animatorController.SetBool("isJumping", false);
            animatorController.SetBool("isDoubleJumping", false);
        }

        if (!isGrounded && !isJumping && coyoteTimer == -1)
            coyoteTimer = coyoteTimerReset;

        if (coyoteTimer > 0)
            coyoteTimer--;

        if (coyoteTimer == 0 && !isJumping)
        {
            if (hasJumps == 2)
                hasJumps = 1;
        }

        if (fallingJumpTimer >= 0)
        {
            fallingJumpTimer--;
        }

        if (fallingJumpTimer > 0 && !isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y - 0.2f);
        }
    }

    private void jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        hasJumps--;
        fallingJumpTimer = fallingJumpTimerReset;

        if (hasJumps == 1)
        {
            animatorController.SetBool("isJumping", true);
        }
        else
        {
            animatorController.SetBool("isDoubleJumping", true);
        }

        Instantiate(jumpEffect, effectContainer);
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public bool getIsJumping()
    {
        return isJumping;
    }
}
