using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropItem : MonoBehaviour
{
    [SerializeField] GameObject gunpowder;
    [SerializeField] GameObject speedGem;
    [SerializeField] GameObject powerGem;
    [SerializeField] GameObject shootFrequencyGem;
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        
        int rand = Random.Range(1, 100);
        if (rand >= 78 && rand < 95)
        {
            var gunpowderGO = Instantiate(gunpowder, transform);
            gunpowderGO.transform.parent = null;
        }
        else if(rand > 95)
        {
            rand = Random.Range(1, 4);
            GameObject item;
            switch (rand)
            {
                case 1:
                    item = Instantiate(speedGem, transform);
                    item.GetComponent<Collider2D>().isTrigger= true;
                    item.transform.parent = null;
                    break;
                case 2:
                    item = Instantiate(powerGem, transform);
                    item.GetComponent<Collider2D>().isTrigger = true;
                    item.transform.parent = null;
                    break;
                case 3:
                    item = Instantiate(shootFrequencyGem, transform);
                    item.GetComponent<Collider2D>().isTrigger = true;
                    item.transform.parent = null;
                    break;
                default:
                    item = Instantiate(powerGem, transform);
                    item.GetComponent<Collider2D>().isTrigger = true;
                    item.transform.parent = null;
                    break;
            }
        }
    }
}
