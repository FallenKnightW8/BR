using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecTile : MonoBehaviour
{
    [SerializeField] private Vector2 Direction;
    [SerializeField] private float speed = 10000;
    [SerializeField] private int Damage = 1;

    void FixedUpdate()
    {
        transform.Translate(50 * speed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.SendMessage("GetDamage", 1);
        }
    }
}
