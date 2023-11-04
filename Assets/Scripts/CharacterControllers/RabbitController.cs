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

        // Make cat fall through rabbit land
        // Ground

        RaycastHit2D ray1 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0f, 1.25f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)))
                * 1f,
            1f,
            LayerMask.GetMask("CatGround")
        );

        RaycastHit2D ray2 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0f, 1.25f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)))
                * -1f,
            1f,
            LayerMask.GetMask("CatGround")
        );

        RaycastHit2D ray3 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0.6f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
                * 1f,
            1f,
            LayerMask.GetMask("CatGround")
        );

        RaycastHit2D ray4 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0.6f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
                * -1f,
            1f,
            LayerMask.GetMask("CatGround")
        );

        // Walls
        RaycastHit2D ray5 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0f, 1.25f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)))
                * 1f,
            1f,
            LayerMask.GetMask("CatWall")
        );

        RaycastHit2D ray6 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0f, 1.25f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)))
                * -1f,
            1f,
            LayerMask.GetMask("CatWall")
        );

        RaycastHit2D ray7 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0.6f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
                * 1f,
            1f,
            LayerMask.GetMask("CatWall")
        );

        RaycastHit2D ray8 = Physics2D.Raycast(
            new Vector2(body.position.x, body.position.y),
            new Vector2(0.6f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
                * -1f,
            1f,
            LayerMask.GetMask("CatWall")
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

        if (
            ray5.collider != null
            || ray6.collider != null
            || ray7.collider != null
            || ray8.collider != null
        )
        {
            collisionBox.enabled = false;
            body.constraints =
                RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            if (
                ray1.collider == null
                && ray2.collider == null
                && ray3.collider == null
                && ray4.collider == null
            )
            {
                collisionBox.enabled = true;
            }
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

// RAY VISUALIZER => play scene, then switch to scene view

// Debug.DrawRay(
//     new Vector2(body.position.x, body.position.y),
//     new Vector2(-1f * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), 0f)
//         * 2.5f,
//     Color.green,
//     0.01f
// );
