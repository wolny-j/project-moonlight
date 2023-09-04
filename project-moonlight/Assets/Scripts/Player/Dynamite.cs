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

        if (timer > explosionTime)
        {
            Detonate();
            timer = -2;
        }
    }

    private void Detonate()
    {
        StartCoroutine(DetonationSequence());
    }

    IEnumerator DetonationSequence()
    {
        explosionSound.Play();

        explosion.SetActive(true);
        dynamite.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        explosion.SetActive(false);

        Destroy(gameObject);
    }
}
