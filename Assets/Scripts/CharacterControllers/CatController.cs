using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    PlayerInputs inputs;
    Vector2 catMove, desiredVelocity, velocity;
    bool isGrounded;
    Ground ground;
    Rigidbody2D body;
    
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 100f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 12f;
    float maxSpeedChange;
    double dashDirection;
    
    
    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetIsGrounded();
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
    }
}
