using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private HerMovement scriptMovment;
    [SerializeField] private GhostSprites scriptGhostSprite;
    [SerializeField] private float DashSpeed = 7f;
    [SerializeField] private float DashLength = .15f;
    [SerializeField] private float DashCooldown = 1f;
    [SerializeField] private float delayDisableGhostSpriteEffect = .5f;

    [Header("Cooldown UI")]
    [SerializeField] private Image ColldownDash;

    private float DashCounter;       // Время текущего рывка
    private float DashCoolCounter;   // Таймер перезарядки рывка
    private float ActivityMoveSpeed; // Текущая скорость передвижения

    private Animator animator;

    private void Awake()
    {
        scriptMovment = GetComponent<HerMovement>();
        animator = GetComponent<Animator>();
        scriptGhostSprite = GetComponent<GhostSprites>();
    }

    private void Start()
    {
        ActivityMoveSpeed = scriptMovment.GetActivityMoveSpeed();
        ColldownDash.fillAmount = 1; // Шкала заполнена в начале
    }

    private void Update()
    {
        HandleDashInput();
        UpdateDashTimers();
        BoostSpeed();
    }

    /// <summary>
    /// Обработка нажатия на пробел для рывка.
    /// </summary>
    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanDash())
        {
            StartDash();
        }
    }

    /// <summary>
    /// Обновление таймеров рывка и шкалы перезарядки.
    /// </summary>
    private void UpdateDashTimers()
    {
        if (DashCounter > 0)
        {
            DashCounter -= Time.deltaTime;
            if (DashCounter <= 0)
            {
                EndDash();
            }
        }

        if (DashCoolCounter > 0)
        {
            DashCoolCounter -= Time.deltaTime;
            UpdateCooldownUI();
        }
    }

    /// <summary>
    /// Проверка возможности выполнения рывка.
    /// </summary>
    private bool CanDash()
    {
        return DashCoolCounter <= 0 && DashCounter <= 0 && scriptMovment.GetDirection() != Vector2.zero;
    }

    private void BoostSpeed()
    {
        if (DashCounter > 0 && scriptMovment.GetActivityMoveSpeed() <= DashSpeed) { 
            scriptMovment.SetActivityMoveSpeed(scriptMovment.GetActivityMoveSpeed() + DashCounter / 1.2f);
        }
    }

    /// <summary>
    /// Запуск рывка.
    /// </summary>
    private void StartDash()
    {
        DashCounter = DashLength;

        ColldownDash.fillAmount = 0;
        animator.SetBool("IsDash?", true);
        scriptGhostSprite.enabled = true;

    }

    /// <summary>
    /// Окончание рывка.
    /// </summary>
    private void EndDash()
    {
        scriptMovment.SetActivityMoveSpeed(scriptMovment.GetMoveSpeed());
        DashCoolCounter = DashCooldown;
        animator.SetBool("IsDash?", false);
        StartCoroutine(DisableGhostSpriteAfterDelay(delayDisableGhostSpriteEffect));
    }

    /// <summary>
    /// Обновление UI шкалы перезарядки.
    /// </summary>
    private void UpdateCooldownUI()
    {
        ColldownDash.fillAmount = Mathf.Clamp01(1 - (DashCoolCounter / DashCooldown));
    }

    private IEnumerator DisableGhostSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ожидаем указанное время
        scriptGhostSprite.enabled = false;     // Отключаем компонент
    }
}