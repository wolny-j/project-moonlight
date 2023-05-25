using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ChestDTO
{
    public List<string> items= new List<string>();
    public int space;

    public ChestDTO(ChestInventory chestInventory)
    {
        foreach (Item item in chestInventory.items)
        {
            items.Add(item.name);
        }
        space = chestInventory.space;
    }

    public ChestDTO()
    {
        space = 3;
    }
}

