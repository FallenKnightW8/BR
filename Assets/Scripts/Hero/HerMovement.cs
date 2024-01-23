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

    [SerializeField] private PlayerCombat playerCombat;
    [Header("Player combat")]
    [SerializeField] private float attackPointChangePosition;

    void Start()
    {
        ActivityMoveSpeed = MoveSpeed;

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveAndDash();
        ChangeAttackPosition();
        //Debug.Log("x and y" + "" + Direction.x + "" + Direction.y);
    }

    public Vector2 getDirection()
    {
        return Direction;
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

    private void ChangeAttackPosition()
    {
        // Check player direction of X
        if (Direction.x > 0)
        {
            playerCombat.SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x + attackPointChangePosition, Rigidbody.transform.position.y, 0));
        }
        else if(Direction.x < 0)
        {
            playerCombat.SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x + -attackPointChangePosition, Rigidbody.transform.position.y, 0));
        }

        // Check player direction of Y
        if (Direction.y > 0)
        {
            playerCombat.SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x, Rigidbody.transform.position.y + attackPointChangePosition, 0));
        }
        else if (Direction.y < 0)
        {
            playerCombat.SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x, Rigidbody.transform.position.y + -attackPointChangePosition, 0));
        }
    }
}
