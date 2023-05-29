using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseDynamite : MonoBehaviour
{
    public static UseDynamite Instance;
    [SerializeField] Text dynamiteCounterText;
    [SerializeField] GameObject dynamite;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && PlayerStats.Instance.dynamiteCounter > 0)
        {
            PlayerStats.Instance.dynamiteCounter--;
            var position = GameObject.Find("Player(Clone)").transform.position;
            var dynamiteObj = Instantiate(dynamite, position, Quaternion.identity);
            UpdateCounterUI();
        }
    }

    public void UpdateCounterUI()
    {
        dynamiteCounterText.text = PlayerStats.Instance.dynamiteCounter.ToString();
    }
}
