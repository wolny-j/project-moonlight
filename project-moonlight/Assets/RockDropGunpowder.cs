using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropGunpowder : MonoBehaviour
{
    [SerializeField] GameObject gunpowder;
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        
        int rand = Random.Range(1, 10);
        if (rand >= 8)
        {
            var gunpowderGO = Instantiate(gunpowder, transform);
            gunpowderGO.transform.parent = null;
        }
    }
}
