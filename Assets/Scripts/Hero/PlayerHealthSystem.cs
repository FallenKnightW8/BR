using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealt;
    [SerializeField] private int Health;
    [SerializeField] private Image ImageLive;

    public void GetDamage(int damage)
    {
        Health -= damage;
    }

    public void SetHealth(int heal)
    {
        Health += heal;
    }

    private void Start()
    {
        Health = MaxHealt;
        ImageLive.fillAmount = 1f; // Шкала заполнена в начале
    }

    private void Update()
    {
        ChangeHealth();
    }

    private void UpdateHealthUi()
    {
        float HealthF = MaxHealt;
        ImageLive.fillAmount = Health / HealthF;
    }

    private void ChangeHealth()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetDamage(1);
            UpdateHealthUi();
        }
    }
}
