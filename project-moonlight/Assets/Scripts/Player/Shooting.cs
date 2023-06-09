using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject spellPrefab;
    private float timer = 0;
    // Update is called once per frame
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > playerStats.shootFrequency)
        {
            if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "MainScene")
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
