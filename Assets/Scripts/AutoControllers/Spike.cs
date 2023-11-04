using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    bool done;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 13 || collision.gameObject.layer == 14) && !done)
        {
            done = true;
            StartCoroutine(kill());
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            //collision.gameObject.GetComponent<Animator>().Play("DeathAnim");
            //SoundManager.Play("TacoBellReverb.wav");
        }
    }
    IEnumerator kill()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
