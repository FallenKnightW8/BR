using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private HerMovement scriptMovment;
    [SerializeField] private Rigidbody2D Rigidbody;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackPointChangePosition = 0.2f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 2f;

    [SerializeField] private Vector3 directionToMouse;

    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private int damage = 1;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        scriptMovment = GetComponent<HerMovement>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Меняем позицию точки атаки в зависимости от положения мыши
        ChangeAttackPosition();

        // Проверяем возможность атаки
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // Изменение позиции точки атаки
    private void ChangeAttackPosition()
    {
        // Получаем текущую позицию мыши в мировых координатах и ограничеваем их по ширине и высоте экрана
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
        mousePosition.z = 0; // Устанавливаем Z в 0

        // Вычисляем направление от игрока к мыши
        Vector3 directionToMouse = (mousePosition - Rigidbody.transform.position).normalized;
        animator.SetFloat("MouseDetectX", directionToMouse.x);

        // Устанавливаем новую позицию точки атаки
        SetAttackPointPosition(new Vector3(
            Rigidbody.transform.position.x + directionToMouse.x * attackPointChangePosition,
            Rigidbody.transform.position.y + directionToMouse.y * attackPointChangePosition,
            0
        ));

    }

    // Метод атаки
    private void Attack()
    {
        // Запускаем анимацию атаки
        animator.SetTrigger("Attack");

        // Определяем врагов в радиусе атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Наносим урон врагу
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

    // Устанавливаем позицию точки атаки
    public void SetAttackPointPosition(Vector3 _position)
    {
        attackPoint.position = _position;
    }

    // Методы для работы с уроном
    public void setDamage(int _damage)
    {
        damage = _damage;
    }

    public int getDamage()
    {
        return damage;
    }
}
