using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject spellPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
       Instantiate(spellPrefab, transform);
    }
}
