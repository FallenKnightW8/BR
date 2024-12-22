using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int Health;
    [SerializeField] private Image ImageLive;
    [SerializeField] private HerMovement ScriptHeroMovment;
    [SerializeField] private SpriteRenderer SpriteRendererColor;
    [SerializeField] private HealthRegen ScriptHealthRegen;

    public void GetDamage(int damage)
    {
        Health -= damage;
        ScriptHealthRegen.StartRegeneration();
        SpriteUpdate();
        UpdateHealthUi();
    }

    public void AddHealth(int heal)
    {
        Health += heal;
        UpdateHealthUi();
    }

    public int GetHealth()
    {
        return Health;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public void UpdateHealthUi()
    {
        float HealthF = MaxHealth;
        ImageLive.fillAmount = Health / HealthF;
    }

    private void Start()
    {
        Health = MaxHealth;
        ImageLive.fillAmount = 1f; // Шкала заполнена в начале
    }

    private void SpriteUpdate()
    {
        SpriteRendererColor.color = new Color(1, 0, 0, 1);
        StartCoroutine(DisableGhostSpriteAfterDelay(0.1f));
    }

    private IEnumerator DisableGhostSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ожидаем указанное время
        SpriteRendererColor.color = new Color(1, 1, 1, 1);     // Отключаем компонент
    }
}
