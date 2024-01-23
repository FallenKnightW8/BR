using System.Collections;
using System.Drawing;
using UnityEngine;

public class BSkeleton : MonoBehaviour
{
    [SerializeField] private GameObject Player;//hero
    [SerializeField] private GameObject Father;//necromant
    [SerializeField] private Rigidbody2D Rigidbody; //hem self
    [SerializeField] private GameObject Arrow; //shot him
    private GameObject Spawned;
    [SerializeField] private int Health = 1;
    [SerializeField] private float speed = 0.05f;
    private bool Shoted = false;
    private Vector2 movement;

    private void GetDamage(int Damage)
    {
        Health -= Damage;
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
        if(!Shoted)
        StartCoroutine(AttakCuldown());
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
        Rigidbody.MovePosition((Vector2)transform.position + ((direction*-1) * speed/10 * Time.deltaTime));
    }

    private IEnumerator AttakCuldown() 
    {
        Shoted = true;
        yield return new WaitForSeconds(8);
        Attack();
        Shoted = false;
        StopAllCoroutines();
    }
    private void Attack()
    {
        Spawned = Instantiate(Arrow,transform.position, transform.rotation);
        Vector3 targ = Spawned.transform.position;
        Vector3 objectPos = Player.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    private void Died()
    {
        if (Father != null)
        {
            Father.SendMessageUpwards("BSkeletDied", 1);
        }
        Destroy(gameObject);
    }
}
