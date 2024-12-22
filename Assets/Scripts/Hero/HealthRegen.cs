using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [Header("Parameters")]
    [Space(10)]
    [SerializeField] private float DelayRegeneration = 4f;
    [SerializeField] private float DelayBetweenRegeneration = 2f;
    [SerializeField] private int RegenerationHealth = 1;

    private PlayerHealthSystem ScriptPlayerHealthSystem;

    public void StartRegeneration()
    {
        CancelInvoke("IncrementHealth");
        InvokeRepeating("IncrementHealth", DelayRegeneration, DelayBetweenRegeneration);
    }

    private void Awake()
    {
        ScriptPlayerHealthSystem = GetComponent<PlayerHealthSystem>();
    }

    private void IncrementHealth()
    {
        if (ScriptPlayerHealthSystem.GetHealth() < ScriptPlayerHealthSystem.GetMaxHealth())
        {
            ScriptPlayerHealthSystem.AddHealth(RegenerationHealth);
        }
    }
}
