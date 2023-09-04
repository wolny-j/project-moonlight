using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentChange : MonoBehaviour
{
    private PlayerStats playerStats;

    private bool changed = false;
    private float timer = 0;

    private void Start()
    {
        playerStats = GameObject.Find("PlayerStatsManager").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if(changed)
        {
            timer += Time.deltaTime;
        }
        if(timer > 0.5f)
        {
            changed = false;
            timer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerStats == null)
        {
            return;
        }    
        if(!playerStats.IsCompleted)
        {
            return;
        }
        if(changed)
        {
            return;
        }

        if(collision.CompareTag("Left"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x + 0.4f, transform.localPosition.y, transform.localPosition.z);
            changed = true;
        }
        else if (collision.CompareTag("Right"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 0.4f, transform.localPosition.y, transform.localPosition.z);
            changed = true;
        }
        else if (collision.CompareTag("Top"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
            changed = true;
        }
        else if (collision.CompareTag("Bottom"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, transform.localPosition.z);
            changed = true;
        }
    }
}
