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
        isJumping;
    Ground ground;
    Rigidbody2D body;
    BoxCollider2D collisionBox;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 100f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 12f;

    [SerializeField, Range(0f, 200f)]
    float jumpForce = 100f;
    float maxSpeedChange;
    float dashDirection;
    int hasJumps = 1;

    [SerializeField]
    GameObject lineObject;
    Line line;
    const int jumpTimerReset = 25;
    int jumpTimer = 0;

    const int coyoteTimerReset = 10;
    int coyoteTimer = 0;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        collisionBox = GetComponent<BoxCollider2D>();

        line = lineObject.GetComponent<Line>();

        Time.timeScale = 1.5f;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetIsCatGrounded();
        catMove = inputs.Player.MoveCat.ReadValue<Vector2>();
        dashDirection = inputs.Player.CatDash.ReadValue<float>();
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
            line.setCatMoving(true);
        else
            line.setCatMoving(false);

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
        }

        if (jumpTimer > 0)
            jumpTimer--;

        if (isGrounded && isJumping && jumpTimer <= 0)
            isJumping = false;

        if (!isGrounded && !isJumping && coyoteTimer == -1)
            coyoteTimer = coyoteTimerReset;

        if (coyoteTimer > 0)
            coyoteTimer--;

        if (coyoteTimer == 0 && !isJumping)
            hasJumps--;
    }

    private void jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        hasJumps--;
    }
}
