using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [SerializeField] private HerMovement scriptMovment;

    [SerializeField] private float DashSpeed = 7;
    [SerializeField] private float DashLength = .15f;
    [SerializeField] private float DashCooldown = 1f;

    private float DashCounter;
    private float DashCoolCounter;
    private float ActivityMoveSpeed;
    private float MoveSpeed;

    [Header("Image cooldown")]
    [SerializeField] private Image ColldownDash;

    private void Awake()
    {
        scriptMovment = GetComponent<HerMovement>();
    }

    private void Start()
    {
        ActivityMoveSpeed = scriptMovment.GetActivityMoveSpeed();
        MoveSpeed = scriptMovment.GetMoveSpeed();
        ColldownDash.fillAmount = 1;
    }

    private void Update()
    {
        Dashed();
    }

    private void Dashed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashCoolCounter <= 0 && DashCounter <= 0 && scriptMovment.GetDirection() != new Vector2(0, 0))
            {
                scriptMovment.SetActivityMoveSpeed(DashSpeed);
                DashCounter = DashLength;

                ColldownDash.fillAmount = 0;
            }
        }

        if (DashCounter > 0)
        {
            DashCounter -= Time.deltaTime;

            if (DashCounter <= 0)
            {
                scriptMovment.SetActivityMoveSpeed(MoveSpeed);
                DashCoolCounter = DashCooldown;
            }
        }

        if (DashCoolCounter > 0)
        {
            DashCoolCounter -= Time.deltaTime;

            ColldownDash.fillAmount += 1 / (DashCoolCounter * 2f) * Time.deltaTime;

            if (DashCoolCounter <= 0)
            {
                ColldownDash.fillAmount = 1;
            }
        }
    }
}
