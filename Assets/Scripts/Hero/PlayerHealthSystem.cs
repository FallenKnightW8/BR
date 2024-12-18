using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealt;
    [SerializeField] private int Health;
    [SerializeField] private Image ImageLive;
    [SerializeField] private HerMovement ScriptHeroMovment;

    public void GetDamage(int damage)
    {
        Health -= damage;
        UpdateHealthUi();
    }

    public void SetHealth(int heal)
    {
        Health += heal;
    }

    public int GetHealth()
    {
        return Health;
    }

    private void Start()
    {
        Health = MaxHealt;
        ImageLive.fillAmount = 1f; // Шкала заполнена в начале
    }

    private void UpdateHealthUi()
    {
        float HealthF = MaxHealt;
        ImageLive.fillAmount = Health / HealthF;
    }
}
