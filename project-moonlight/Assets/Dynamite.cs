using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject dynamite;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] AudioSource explosionSound;


    private float timer;
    private const float explosionTime = 2f;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > explosionTime)
        {
            timer = -2f;
            StartCoroutine(Detonate());
        }
    }

    IEnumerator Detonate()
    {
        explosionSound.Play();
        explosion.SetActive(true);
        dynamite.SetActive(false);
        yield return new WaitForSeconds(.5f);
        explosion.SetActive(false);
        Destroy(gameObject);

    }
}
