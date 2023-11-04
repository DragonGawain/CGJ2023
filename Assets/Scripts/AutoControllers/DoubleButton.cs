using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
    [SerializeField]
    GameObject presserSprite;
    [SerializeField]
    DoubleButtonManager Manager;
    Vector3 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        targetDirection = transform.position - presserSprite.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 13 || collision.gameObject.layer == 14) && !Manager.done)
        {
            StartCoroutine(buttonDown());
            if (gameObject.name.Contains("Left"))
            {
                Manager.left = true;
            }
            else
            {
                Manager.right = true;
            }
            Manager.checkState();
            if (Manager.left && Manager.right)
                Manager.done = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 13 || collision.gameObject.layer == 14) && !Manager.done)
        {
            StartCoroutine(buttonUp());
            if (gameObject.name.Contains("Left"))
            {
                Manager.left = false;
            }
            else
            {
                Manager.right = false;
            }
        }
    }
    IEnumerator buttonDown()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(.05f);
            Vector3 newPosition = presserSprite.transform.position + targetDirection * .1f;
            presserSprite.transform.position = newPosition;
        }
    }
    IEnumerator buttonUp()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(.05f);
            Vector3 newPosition = presserSprite.transform.position + targetDirection * -.1f;
            presserSprite.transform.position = newPosition;
        }
    }
}
