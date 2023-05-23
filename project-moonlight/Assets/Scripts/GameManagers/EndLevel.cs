using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField]bool animPlayed = false;
    private Animator animator;

    private void Update()
    {
        if (!animPlayed && gameObject.GetComponentInParent<SpawnEnemy>().isCompleted)
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
            PlayerStats.Instance.level++;
            SaveSystem.BuildSaveObject(PlayerStats.Instance, Inventory.Instance);
            SceneManager.LoadScene(1);
        }
    }
}
