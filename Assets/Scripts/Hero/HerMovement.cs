using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 2;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private Rigidbody2D Rigidbody;

    [SerializeField] private float ActivityMoveSpeed;
    [SerializeField] private float DashSpeed = 7;

    [SerializeField] private float DashLength = .15f;
    [SerializeField] private float DashCooldown = 1f;

    [SerializeField] private float DashCounter;
    [SerializeField] private float DashCoolCounter;

    void Start()
    {
        ActivityMoveSpeed = MoveSpeed;

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveAndDash();
    }
    private void MoveAndDash()
    {
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");

        Direction.Normalize();

        Rigidbody.velocity = Direction * ActivityMoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashCoolCounter <= 0 && DashCounter <= 0)
            {
                ActivityMoveSpeed = DashSpeed;
                DashCounter = DashLength;
            }
        }

        if (DashCounter > 0)
        {
            DashCounter -= Time.deltaTime;

            if (DashCounter <= 0)
            {
                ActivityMoveSpeed = MoveSpeed;
                DashCoolCounter = DashCooldown;
            }
        }

        if (DashCoolCounter > 0)
        {
            DashCoolCounter -= Time.deltaTime;
        }
    }
}
