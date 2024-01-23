using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallenKnight : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int Health = 30;
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private int Damage = 1;

    private Vector2 movement;
    private void FixedUpdate()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody2D>();
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
        Rigidbody.MovePosition((Vector2)transform.position + (direction * speed / 4 * Time.deltaTime));
    }

    private void Attack()
    {
        int WhatTheAttack;
        int Stadia;
        Stadia = Convert.ToInt32(Mathf.Round(Health / 10));
        WhatTheAttack = Random.Range(0, Stadia);
        switch (WhatTheAttack) 
        {
            case 0:
                //shot
                break;
            case 1:
                //melee
                break;
            case 2:
                //melle jump
                break;

        }
    }
}
