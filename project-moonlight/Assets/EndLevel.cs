using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    bool animPlayed = false;
    private Animator animator;

    private void Update()
    {
        if(gameObject.GetComponentInParent<SpawnEnemy>().isCompleted && !animPlayed)
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger("OpenTrapdoor");
            animPlayed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && animPlayed)
        {
            SceneManager.LoadScene(0);
        }
    }
}
