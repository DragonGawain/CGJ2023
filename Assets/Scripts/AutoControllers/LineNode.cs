using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineNode : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 12)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 0.25f,
                transform.position.z
            );
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 12)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 0.25f,
                transform.position.z
            );
        }
    }
}
