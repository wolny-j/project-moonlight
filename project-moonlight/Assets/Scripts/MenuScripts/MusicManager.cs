using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource volumeSource;

    private void Update()
    {
        if (volumeSlider != null)
        {
            volumeSource.volume = volumeSlider.value;

        }
    }
    private void Awake()
    {
        // SprawdŸ, czy istnieje ju¿ inny obiekt z AudioSource
        AudioSource existingAudio = FindObjectOfType<AudioSource>();

        // Jeœli istnieje, usuñ nowo utworzony obiekt
        if (existingAudio != null && existingAudio != GetComponent<AudioSource>())
        {
            Destroy(gameObject);
            return;
        }

        // W przeciwnym razie zachowaj obiekt przy prze³¹czaniu scen
        DontDestroyOnLoad(gameObject);
    }

}
