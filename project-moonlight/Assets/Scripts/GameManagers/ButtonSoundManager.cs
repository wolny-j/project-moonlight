using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource click;

    public void PlaySound()
    {
        click.Play();
    }
}
