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

    [SerializeField, Range(0f, 50)]
    float jumpForce = 15;
    float maxSpeedChange;

    [SerializeField]
    GameObject lineObject;
    Line line;
    int hasJumps = 2;
    const int jumpTimerReset = 25;
    int jumpTimer = 0;

    const int coyoteTimerReset = 5;
    int coyoteTimer = 0;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        line = lineObject.GetComponent<Line>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetIsRabbitGrounded();
        rabbitMove = inputs.Player.MoveRabbit.ReadValue<Vector2>();
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

        if (body.velocity.x != 0 || body.velocity.y != 0)
            line.setCatMoving(true);
        else
            line.setCatMoving(false);

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
            isJumping = false;

        if (!isGrounded && !isJumping && coyoteTimer == -1)
            coyoteTimer = coyoteTimerReset;

        if (coyoteTimer > 0)
            coyoteTimer--;

        if (coyoteTimer == 0 && !isJumping)
        {
            if (hasJumps == 2)
                hasJumps = 1;
        }
    }

    private void jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        hasJumps--;
    }
}
