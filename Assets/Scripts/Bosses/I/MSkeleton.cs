using System.Collections;
using UnityEngine;

public class MSkeleton : MonoBehaviour
{
    [SerializeField] private GameObject Player;//hero
    [SerializeField] private GameObject Father;//necromant
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int Health = 2;
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private int Damage = 1;
    [SerializeField] private SpriteRenderer SpriteColor;
    private bool playerInRange = false;

    private Vector2 movement;
    private void GetDamage(int Damage)
    {
        Health -= Damage;
        UpdateSpriteColor();
    }
    void FixedUpdate()
    {
        if (transform.position.x >= 5 && transform.position.x <= -5) transform.position = new Vector2(0, transform.position.y);
        if (transform.position.y >= 5 && transform.position.y <= -5) transform.position = new Vector2(transform.position.x, 10);

        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Father == null)
            Father = GameObject.FindWithTag("Boss");
        if (Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody2D>();
        if (Health <= 0) Died();
        GetPlayer();
    }

    private void GetPlayer()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        Move(movement);
    }

    private void Move(Vector2 direction)
    {
        Rigidbody.MovePosition((Vector2)transform.position + (direction * speed /4 * Time.deltaTime));
    }
    public void ChangePlayerR(bool Check)
    {
        playerInRange = Check;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
         if(playerInRange) Player.SendMessage("GetDamage",Damage);
        StopCoroutine(Attack());
    }    
    private void Died()
    {
        if (Father != null)
        {
            Father.SendMessageUpwards("MSkeletDied", 1);
        }
        Destroy(gameObject);
    }

    private void UpdateSpriteColor()
    {
        SpriteColor.color = new Color(1, 0, 0, 1);
        StartCoroutine(DisableGhostSpriteAfterDelay(0.1f));
    }

    private IEnumerator DisableGhostSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ожидаем указанное время
        SpriteColor.color = new Color(1, 1, 1, 1);     // Отключаем компонент
    }
}
