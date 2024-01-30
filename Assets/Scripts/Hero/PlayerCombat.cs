using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // ----> Uncomment
    // private Animator animator;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private HerMovement herMovement;

    [SerializeField] private int damage = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

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
        this.attackPoint.position = _position;
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
}
