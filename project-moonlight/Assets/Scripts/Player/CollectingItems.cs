using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingItems : MonoBehaviour
{
    private GameObject map;

    private const string HearthTag = "Hearth";
    private const string BrainTag = "Brain";
    private const string MapTag = "Map";

    // Start is called before the first frame update
    void Awake()
    {
        map = GameObject.Find("Map");
        map.SetActive(false);
    }

    //OnTriggerEnter with object check each function to pick up item.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectHeart(collision, PlayerStats.Instance.health);
        CollectMap(collision);
        CollectItem(collision, BrainTag);
    }

    //Pick up hearth and add 1 HP to the Singleton Class HealthUIManager attached to game manager
    public void CollectHeart(Collider2D collision, int health)
    {
        if (collision.gameObject.CompareTag(HearthTag) && health < 8)
        {
            health++;
            PlayerStats.Instance.health = health;
            HealthUIManager.Instance.AddHealth(health);
            Destroy(collision.gameObject);
        }
    }

    //Check item tag and add item to Inventory. Inventory is a Singleton class attached to game manager.
    public void CollectItem(Collider2D collision, string tag)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            PickingUpItem item = collision.gameObject.GetComponent<PickingUpItem>();
            bool result = Inventory.Instance.AddItem(item.GetItem());
            if (result)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    //Check item tag, display map in the UI and add changes bool in a Singleton PlayerStats class attached to the player.
    public void CollectMap(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(MapTag))
        {
            map.SetActive(true);
            PlayerStats.Instance.isMapObtained = true;
            Destroy(collision.gameObject);
        }
    }
}
