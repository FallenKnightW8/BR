using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] private GameObject Player;//hero
    [SerializeField] private GameObject Father;//necromant
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int Health = 4;
    [SerializeField] private float speed = 0.15f;
    [SerializeField] private int Damage = 2;
    private bool PlayerInAttackR = false;
    private Vector2 movement;

    // Update is called once per frame
    private void GetDamage(int Damage)
    {
        Health -= Damage;
    }
    void FixedUpdate()
    {
        if (Health <= 0) Died();
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Father == null)
            Father = GameObject.FindWithTag("Boss");
        if (Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody2D>();
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
        Rigidbody.MovePosition((Vector2)transform.position + (speed/5 * Time.deltaTime * direction));
    }
    
    public void ChangePlayerR(bool Check)
    {
        PlayerInAttackR = Check;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        if (PlayerInAttackR) Player.SendMessage("GetDamage",Damage);
    }
    private void Died()
    {
        if (Father != null)
        {
            Father.SendMessageUpwards("GetDamage", 1);
            Father.GetComponent<Necromant>().FStadia = false;
        }
        Destroy(gameObject);
    }
}
