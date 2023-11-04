using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    Door[] doors;
    [SerializeField]
    GameObject presserSprite;
    Vector3 targetDirection;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        targetDirection = transform.position - presserSprite.transform.position;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 13 || collision.gameObject.layer == 14) && !done) {
            StartCoroutine(buttonDown());
        }
    }
    IEnumerator buttonDown()
    {
        done = true;
        foreach (Door d in doors)
        {
            d.StartCoroutine(d.pressed());
        }
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(.05f);
            Vector3 newPosition = presserSprite.transform.position + targetDirection * .1f;
            presserSprite.transform.position = newPosition;
        }
    }
}
