using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    PlayerInputs inputs;
    Vector2 catMove,
        desiredVelocity,
        velocity;
    bool isGrounded,
        isJumping = false,
        isDashing = false;
    Ground ground;
    Rigidbody2D body;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 100f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 12f;

    [SerializeField, Range(0f, 50)]
    float jumpForce = 15;

    [SerializeField, Range(0f, 100)]
    float dashForce = 50;
    float maxSpeedChange;
    float dashDirection;

    [SerializeField]
    GameObject lineObject;
    Line line;
    int hasJumps = 1;
    const int jumpTimerReset = 25;
    int jumpTimer = 0;

    private Animator animatorController;
    private SpriteRenderer sprite;

    const int coyoteTimerReset = 5;
    int coyoteTimer = 0;

    int hasDash = 1;
    const int dashTimerReset = 20;
    int dashTimer = 0;

    const int fallingJumpTimerReset = 500;
    int fallingJumpTimer = 0;

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
        isGrounded = ground.GetIsCatGrounded();
        catMove = inputs.Player.MoveCat.ReadValue<Vector2>();
        dashDirection = inputs.Player.CatDash.ReadValue<float>();

        if (isJumping && !isGrounded && fallingJumpTimer > 0 && !isDashing)
            body.velocity = new Vector2(
                Mathf.Clamp(body.velocity.x, -11.11f, 11.11f),
                Mathf.Clamp(body.velocity.y, -33.33f, 33.33f)
            );
        else if (isJumping && !isGrounded && fallingJumpTimer > 0 && isDashing)
            body.velocity = new Vector2(
                body.velocity.x,
                Mathf.Clamp(body.velocity.y, -33.33f, 33.33f)
            );
        else if (!isDashing)
            body.velocity = new Vector2(
                Mathf.Clamp(body.velocity.x, -11.11f, 11.11f),
                Mathf.Clamp(body.velocity.y, -11.11f, 11.11f)
            );
        else
            body.velocity = new Vector2(
                body.velocity.x,
                Mathf.Clamp(body.velocity.y, -11.11f, 11.11f)
            );
    }

    private void FixedUpdate()
    {
        desiredVelocity =
            new Vector2(catMove.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        velocity = body.velocity;

        maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        if (body.velocity.x != 0 || body.velocity.y != 0)
        {
            line.setCatMoving(true);
        }
        else
        {
            line.setCatMoving(false);
        }

        if (body.velocity.x != 0)
        {
            animatorController.SetBool("isWalking", true);
        }
        else
        {
            animatorController.SetBool("isWalking", false);
        }

        if (catMove.x < 0)
        {
            sprite.flipX = true;
        }
        else if (catMove.x > 0)
        {
            sprite.flipX = false;
        }
        // jump
        if (isGrounded && !isJumping)
        {
            hasJumps = 1;
            coyoteTimer = -1;
        }

        if (
            (catMove.y > 0 && hasJumps > 0 && jumpTimer <= 0)
            || (coyoteTimer > 0 && catMove.y > 0 && hasJumps > 0 && jumpTimer <= 0)
        )
        {
            jump();
            isJumping = true;
            jumpTimer = jumpTimerReset;

            animatorController.SetBool("isJumping", true);
        }

        if (jumpTimer > 0)
            jumpTimer--;

        if (isGrounded && isJumping && jumpTimer <= 0)
        {
            isJumping = false;

            animatorController.SetBool("isJumping", false);
        }

        if (!isGrounded && !isJumping && coyoteTimer == -1)
            coyoteTimer = coyoteTimerReset;

        if (coyoteTimer > 0)
            coyoteTimer--;

        if (coyoteTimer == 0 && !isJumping)
            hasJumps--;

        // Dash
        // We have a *very* dashing cat (blush)
        if (dashDirection != 0 && hasDash > 0 && dashTimer <= 0 && !isDashing)
        {
            dash();
            isDashing = true;
            dashTimer = dashTimerReset;

            animatorController.SetBool("isDashing", true);
        }

        if (dashTimer > 0)
            dashTimer--;

        if (isGrounded && isDashing && dashTimer <= 0)
        {
            isDashing = false;
            hasDash = 1;

            animatorController.SetBool("isDashing", false);
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
    }

    private void dash()
    {
        if (dashDirection > 0)
        {
            body.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
        }
        else if (dashDirection < 0)
        {
            body.AddForce(-transform.right * dashForce, ForceMode2D.Impulse);
        }
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
