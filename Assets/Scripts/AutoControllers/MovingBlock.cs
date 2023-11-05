using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingBlock : MonoBehaviour
{
    [SerializeField, Range(-50f, 50f)]
    float distanceX;
    [SerializeField, Range(-50f, 50f)]
    float distanceY;
    [SerializeField, Range(0, 5f)]
    float speed;
    Vector3 initialPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        // Move the object
        //transform.Translate(new Vector3(speed * Time.deltaTime * x, speed * Time.deltaTime * y, 0));
        float moveX = distanceX * Time.deltaTime * speed;
        float moveY = distanceY * Time.deltaTime * speed;

        // Update the object's position
        transform.position = new Vector3(transform.position.x + moveX, transform.position.y + moveY, 0);
        float fuckThisX = initialPosition.x - transform.position.x;
        float fuckThisY = initialPosition.y - transform.position.y;
        
        // Check if reset condition is met
        if (Mathf.Abs(fuckThisX) > Mathf.Abs(distanceX) || Mathf.Abs(fuckThisY) > Mathf.Abs(distanceY))
        {
            // Reset the position to the initial position
            transform.position = initialPosition;
        }
        
    }
}
