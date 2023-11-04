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
        catOnRabbit;
    Ground ground;
    Rigidbody2D body;
    BoxCollider2D collisionBox;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 100f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 12f;
    float maxSpeedChange;
    double dashDirection;

    const int timerReset = 20;
    int timer = 0;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        collisionBox = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetIsCatGrounded();
        catOnRabbit = ground.GetIsCatOnRabbit();
        catMove = inputs.Player.MoveCat.ReadValue<Vector2>();
        dashDirection = inputs.Player.CatDash.ReadValue<double>();
    }

    private void FixedUpdate()
    {
        desiredVelocity =
            new Vector2(catMove.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        velocity = body.velocity;

        maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        // Make cat fall through rabbit land

        RaycastHit2D ray1 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0f, 0.5f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)))
                * 1f,
            1f,
            LayerMask.GetMask("RabbitGround")
        );

        RaycastHit2D ray2 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0f, 0.5f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)))
                * -1f,
            1f,
            LayerMask.GetMask("RabbitGround")
        );

        RaycastHit2D ray3 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0.5f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
                * 1f,
            1f,
            LayerMask.GetMask("RabbitGround")
        );

        RaycastHit2D ray4 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0.5f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
                * -1f,
            1f,
            LayerMask.GetMask("RabbitGround")
        );

        if (
            ray1.collider != null
            || ray2.collider != null
            || ray3.collider != null
            || ray4.collider != null
        )
            collisionBox.enabled = false;
        else
            collisionBox.enabled = true;
    }
}
