using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField, Range(-50f, 50f)]
    float targetX;
    [SerializeField, Range(-50f, 50f)]
    float targetY;
    public IEnumerator pressed()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSecondsRealtime(.01f);
            Vector3 targetPos = new Vector3(transform.position.x + targetX / 15f, transform.position.y + targetY / 15f, transform.position.z);
            transform.position = targetPos;
        }
    }
}
