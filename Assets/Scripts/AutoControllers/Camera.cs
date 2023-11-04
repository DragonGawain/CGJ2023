using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    GameObject Cat,
        Rabbit;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            Vector3.Lerp(Cat.transform.position, Rabbit.transform.position, 0.5f),
            Time.fixedDeltaTime
        );
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
