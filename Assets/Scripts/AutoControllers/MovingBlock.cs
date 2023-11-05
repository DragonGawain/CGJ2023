using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField, Range(-50f, 50f)]
    float distanceX;
    [SerializeField, Range(-50f, 50f)]
    float distanceY;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(move());
    }
    IEnumerator move()
    {
        for (int i = 0; i < (int)(Mathf.Max(Mathf.Abs(distanceY), Mathf.Abs(distanceX)) * 10); i++)
        {
            transform.position = new Vector3(transform.position.x + distanceX / 200f, transform.position.y + distanceY / 200f, transform.position.z);
            yield return new WaitForSecondsRealtime(.01f);
        }
        transform.position = initialPosition;
        StartCoroutine(move());
    }
}
