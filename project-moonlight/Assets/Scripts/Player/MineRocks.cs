using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRocks : MonoBehaviour
{
    public static MineRocks Instance;

    public bool rockTouched = false;

    public GameObject rock = null;

    void Awake()
    {
        if (Instance == null)
        {
            MineRocks.Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

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
