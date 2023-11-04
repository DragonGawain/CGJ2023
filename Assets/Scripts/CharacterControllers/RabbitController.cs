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
        rabbitOnCat;
    Ground ground;
    Rigidbody2D body;
    BoxCollider2D collisionBox;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 100f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 12f;
    float maxSpeedChange;

    [SerializeField]
    GameObject lineObject;
    Line line;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        collisionBox = GetComponent<BoxCollider2D>();

        line = lineObject.GetComponent<Line>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetIsRabbitGrounded();
        rabbitOnCat = ground.GetIsRabbitOnCat();
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
    }
}
