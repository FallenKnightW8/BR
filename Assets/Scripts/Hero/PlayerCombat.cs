using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // ----> Uncomment
    // private Animator animator;
    [SerializeField] private HerMovement scriptMovment;
    [SerializeField] private Rigidbody2D Rigidbody;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackPointChangePosition = 0.2f;

    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private int damage = 1;

    public void setDamage(int _damage)
    {
        damage = _damage;
    }

    public int getDamage()
    {
        return damage;
    }

    public void SetAttackPointPosition(Vector3 _position)
    {
        attackPoint.position = _position;
    }

    private void Awake()
    {
        scriptMovment = GetComponent<HerMovement>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChangeAttackPosition();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    // Enemy attack and debug hit
    private void Attack()
    {
        // ----> Uncomment
        // Play an attack
        // animator.SetTrigger("Attack");

        // Detect enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.SendMessage("GetDamage", damage);
            Debug.Log("We hit " + enemy.name);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void ChangeAttackPosition()
    {
        // Check player direction of X
        if (scriptMovment.GetDirection().x > 0)
        {
            SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x + attackPointChangePosition, Rigidbody.transform.position.y, 0));
        }
        else if (scriptMovment.GetDirection().x < 0)
        {
            SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x + -attackPointChangePosition, Rigidbody.transform.position.y, 0));
        }

        // Check player direction of Y
        if (scriptMovment.GetDirection().y > 0)
        {
            SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x, Rigidbody.transform.position.y + attackPointChangePosition, 0));
        }
        else if (scriptMovment.GetDirection().y < 0)
        {
            SetAttackPointPosition(new Vector3(Rigidbody.transform.position.x, Rigidbody.transform.position.y + -attackPointChangePosition, 0));
        }
    }
}
