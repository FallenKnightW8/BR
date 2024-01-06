using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 2;
    [SerializeField] private Vector2 Direction;
    [SerializeField] private Rigidbody2D Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetDirection();
    }
    private void GetDirection()
    {
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position +  Direction * Speed * Time.deltaTime);
    }
}
