using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    int sceneIndex = 0;

    bool rabbitWin = false,
        catWin = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 13)
            catWin = true;
        if (other.gameObject.layer == 14)
            rabbitWin = true;
    }

    private void OnCollisionExit(Collision other)
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
            sceneIndex++;
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
