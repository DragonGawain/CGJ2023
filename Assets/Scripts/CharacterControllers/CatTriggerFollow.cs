using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTriggerFollow : MonoBehaviour
{
    [SerializeField] GameObject cat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cat.transform.position;
    }
}
