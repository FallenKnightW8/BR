using UnityEngine;
using UnityEngine.UI;

public class HerMovement : MonoBehaviour
{
    [SerializeField] private GameObject SpriteObject;

    [SerializeField] private float MoveSpeed = 2;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private float RigidbodyVelocity;

    [SerializeField] private float ActivityMoveSpeed;
    [SerializeField] private float DashSpeed = 7;

    [SerializeField] private float DashLength = .15f;
    [SerializeField] private float DashCooldown = 1f;

    [SerializeField] private float DashCounter;
    [SerializeField] private float DashCoolCounter;

    [Header("Image cooldown")]
    [SerializeField] private Image ColldownDash;

    [Header("Player combat")]
    [SerializeField] private float attackPointChangePosition = 0.2f;
    [SerializeField] private PlayerCombat playerCombat;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool FacingLeft = false;

    void Start()
    {
        ActivityMoveSpeed = MoveSpeed;

        ColldownDash.fillAmount = 1;

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveAndDash();
        CheckFlip();
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

        RigidbodyVelocity = Direction.x * ActivityMoveSpeed;

        Rigidbody.velocity = Direction * ActivityMoveSpeed;

        // Animations
        //animator.SetFloat("RigidbodyVelocity", Mathf.Abs(RigidbodyVelocity));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashCoolCounter <= 0 && DashCounter <= 0)
            {
                ActivityMoveSpeed = DashSpeed;
                DashCounter = DashLength;

                ColldownDash.fillAmount = 0;
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

            ColldownDash.fillAmount += 1 / (DashCoolCounter*2f) * Time.deltaTime;

            if (DashCoolCounter <= 0)
            {
                ColldownDash.fillAmount = 1;
            }
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

    private void CheckFlip()
    {
        if (Direction.x < 0 && FacingLeft)
        {
            Flip();
        }
        else if (Direction.x > 0 && !FacingLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingLeft = !FacingLeft;

        Vector3 theScale = SpriteObject.transform.localScale;
        theScale.x *= -1;
        SpriteObject.transform.localScale = theScale;
    }
}
