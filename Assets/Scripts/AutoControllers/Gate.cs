using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    bool rabbitWin = false,
        catWin = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
            catWin = true;
        if (other.gameObject.layer == 14)
            rabbitWin = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
            catWin = false;
        if (other.gameObject.layer == 14)
            rabbitWin = false;
    }

    private void Update()
    {
        if (rabbitWin && catWin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
