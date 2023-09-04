using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShrine : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GameObject.Find("ShrinePanel").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            animator.Play("ShrinePanelEnter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            animator.Play("ShrinePanelClose2");
        }
    }

}
