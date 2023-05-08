using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingItems : MonoBehaviour
{
    public GameObject map { get; set; }

    private const string HearthTag = "Hearth";
    private const string ItemTag = "Item";
    private const string MapTag = "Map";


    //OnTriggerEnter with object check each function to pick up item.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectHeart(collision, PlayerStats.Instance.health);
        CollectMap(collision);
        CollectItem(collision, ItemTag);
        CollectGem(collision);
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
            PlayerStats.Instance.UnlockMap();
            Destroy(collision.gameObject);
        }
    }

    public void CollectGem(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "SpeedGem":
                CollectPowerup("SpeedGem", collision);
                break;
            case "PowerGem":
                CollectPowerup("PowerGem", collision);
                break;
            case "ShootGem":
                CollectPowerup("ShootGem", collision);
                break;
            default:
                break;
        }
    }

    private void CollectPowerup(string key, Collider2D collision)
    {
        PlayerStats.Instance.AddPowerup(key);
        Destroy(collision.gameObject);
    }
}
