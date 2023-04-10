using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject spellPrefab;
    private float cooldown = 0.4f;
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                timer = 0;
            }
        }
    }

    private void Shoot()
    {
       Instantiate(spellPrefab, transform);
    }
}
