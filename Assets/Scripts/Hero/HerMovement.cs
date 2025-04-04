using UnityEngine;
using UnityEngine.UI;

public class HerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 2;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private Rigidbody2D Rigidbody;

    [SerializeField] private float ActivityMoveSpeed;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    public Vector2 GetDirection()
    {
        return Direction;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public float GetActivityMoveSpeed()
    {
        return ActivityMoveSpeed;
    }

    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }

    public void SetActivityMoveSpeed(float activityMoveSpeed)
    {
        ActivityMoveSpeed = activityMoveSpeed;
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ActivityMoveSpeed = MoveSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Direction * ActivityMoveSpeed;
    }

    private void Move()
    {
        // Получаем ввод по горизонтали и вертикали
        Direction.x = Input.GetAxisRaw("Horizontal"); // -1 для влево, 1 для вправо
        Direction.y = Input.GetAxisRaw("Vertical");   // -1 для вниз, 1 для вверх

        // Нормализуем вектор для равномерного движения по диагонали
        Direction.Normalize();

        // Анимации
        if (Direction.x != 0 || Direction.y != 0)
        {
            animator.SetFloat("Horizontal", Direction.x);
            animator.SetFloat("Vertical", Direction.y);
        }
        animator.SetFloat("Speed", Rigidbody.velocity.sqrMagnitude);

        if (Direction.x > 0) { animator.SetFloat("IdleLeftRight", 1); }
        else if (Direction.x < 0) { animator.SetFloat("IdleLeftRight", -1); }
             
    }
}
