using UnityEngine;

public class MSkeleton : MonoBehaviour
{
    [SerializeField] private GameObject Player;//hero
    [SerializeField] private GameObject Father;//necromant
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int Health = 2;
    [SerializeField] private float speed = 3;
    private Vector2 movement;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if(Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Father == null)
            Father = GameObject.FindWithTag("Boss");
        if (Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody2D>();
        if (Health > 0) Died();
        GetPlayer();
    }

    private void GetPlayer()
    {
        Vector3 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
        Move(movement);
    }

    private void Move(Vector2 direction)
    {
        Rigidbody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void Attack()
    {

    }

    private void Died()
    {
        if (Father != null)
        {
            Father.SendMessageUpwards("MSkeletDied", 1);
        }
        Destroy(gameObject);
    }
}
