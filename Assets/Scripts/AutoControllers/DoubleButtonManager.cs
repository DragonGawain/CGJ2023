using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButtonManager : MonoBehaviour
{
    [SerializeField]
    Door[] doors;
    public bool left;
    public bool right;
    public bool done;
    public void checkState()
    {
        if (left && right)
        {
            foreach (Door d in doors)
            {
                d.StartCoroutine(d.pressed());
            }
        }
    }
}
