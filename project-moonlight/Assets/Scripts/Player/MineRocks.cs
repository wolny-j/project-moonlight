using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRocks : MonoBehaviour
{
    private bool rockTouched = false;

    private GameObject rock = null;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && rockTouched && PlayerStats.Instance.pickaxe1.hasPickaxe)
        {
            PlayerStats.Instance.UpdatePickaxe();
            rockTouched = false; 
            rock.GetComponent<RockDropItem>().DestroyRock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            rockTouched = true;
            rock = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            rockTouched = false;
        }
    }
}
