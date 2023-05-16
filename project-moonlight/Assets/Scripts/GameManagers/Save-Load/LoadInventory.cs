using UnityEngine;
using static Inventory;

public class LoadInventory : MonoBehaviour
{
    public static LoadInventory Instance;

    [SerializeField] private Item zombieBrain;
    [SerializeField] private Item eye;
    [SerializeField] private Item shell;
    [SerializeField] private Item seed;
    [SerializeField] private Item poppy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Load(PlayerSaveData data)
    {
        foreach (string item in data.items)
        {
            switch (item)
            {
                case "Brain":
                    Inventory.Instance.AddItem(zombieBrain);
                    break;
                case "Eye":
                    Inventory.Instance.AddItem(eye);
                    break;
                case "Shell":
                    Inventory.Instance.AddItem(shell);
                    break;
                case "Poppy Seed":
                    Inventory.Instance.AddItem(seed);
                    break;
                case "Poppy":
                    Inventory.Instance.AddItem(poppy);
                    break;
            }
        }
    }
}
