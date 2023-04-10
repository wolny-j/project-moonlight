using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingItems : MonoBehaviour
{
    private GameObject map;
    // Start is called before the first frame update
    void Awake()
    {
        map = GameObject.Find("Map");
        map.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectHeart(collision, PlayerStats.Instance.health);
        CollectMap(collision);
        CollectItem(collision, "Brain");
    }

    public void CollectHeart(Collider2D collision, int health)
    {
        if (collision.gameObject.CompareTag("Hearth") && health < 8)
        {

            health++;
            PlayerStats.Instance.health = health;
            HealthUIManager.Instance.AddHealth(health);
            Destroy(collision.gameObject);
        }
    }

    public void CollectItem(Collider2D collision, string tag)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            bool result = Inventory.Instance.AddItem(collision.gameObject.GetComponent<PickingUpItem>().GetItem());
            if (result)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void CollectMap(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            map.SetActive(true);
            PlayerStats.Instance.isMapObtained = true;
            Destroy(collision.gameObject);
        }
    }
}
