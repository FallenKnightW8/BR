using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecTile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private Collider2D Collider;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private float speed = 10000;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(Vector3.up * 50 * speed * Time.deltaTime);
    }
}
