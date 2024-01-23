using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] private GameObject Player;//hero
    [SerializeField] private GameObject Father;//necromant
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int Health = 4;
    [SerializeField] private float speed = 3;
    [SerializeField] private int Damage = 1;
    private Vector2 movement;

    // Update is called once per frame
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
        Rigidbody.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
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
