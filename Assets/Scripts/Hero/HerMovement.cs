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

    private void Move()
    {
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");

        Direction.Normalize();

        Rigidbody.velocity = Direction * ActivityMoveSpeed;

        // Animations
        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Rigidbody.velocity.sqrMagnitude);
    }
}
