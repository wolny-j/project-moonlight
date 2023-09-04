using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public static BossHealthBar Instance;

    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject healthSliderObject;
    [SerializeField] Animator animator;
    [SerializeField] Image healthBar;

    public static int SPIDER_HEALTH = 68;
    public static int SHOOTER_HEALTH = 80;
    public static int WIZZARD_HEALTH = 90;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadSpiderBar()
    {
        animator.Play("BossBarEnter");
        healthSlider.maxValue = SPIDER_HEALTH;
        healthSlider.value = healthSlider.maxValue;
    }

    public void LoadShooterBar()
    {
        animator.Play("BossBarEnter");
        healthSlider.maxValue = SHOOTER_HEALTH;
        healthSlider.value = healthSlider.maxValue;
    }

    public void LoadWizzardBar()
    {
        animator.Play("BossBarEnter");
        healthSlider.maxValue = WIZZARD_HEALTH;
        healthSlider.value = healthSlider.maxValue;
    }

    public void UpdateHealthBar(float damage)
    {
        healthSlider.value -= damage;
        
        if(healthSlider.value < healthSlider.maxValue/2 && healthSlider.value > healthSlider.maxValue / 4)
            healthBar.color = new Color32(183, 121, 0, 160 );
        else if(healthSlider.value < healthSlider.maxValue / 4)
            healthBar.color = new Color32(233, 18, 0, 160);

        if (healthSlider.value <= 0 )
        {
            animator.Play("BossBarClose");
        }
    }
}
