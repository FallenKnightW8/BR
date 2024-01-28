using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallenKnight : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private GameObject Sword;
    [SerializeField] private Transform ZoneAttack;
    private bool PlayerInAttackR = false;
    private bool Attacking = false;
    private bool Thinking = false;
    private bool StopRun = false;
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
        if (Attacking == false && Thinking == false)
        {
            StartCoroutine(ChangeAttack());
        }
    }

    private IEnumerator ChangeAttack()
    {
        Thinking = true;
        yield return new WaitForSeconds(2);
        Attack();
        StopCoroutine(ChangeAttack());
    }
    private void GetPlayer()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        Move(movement);
        ChangeDirection(movement);
    }

    private void Move(Vector2 direction)
    {
        Rigidbody.MovePosition((Vector2)transform.position + (direction * speed / 4 * Time.deltaTime));
    }

    private void ChangeDirection(Vector2 direction)
    {
        ZoneAttack.transform.position =  direction * 2;
    }
    private void Attack()
    {
        Attacking = true;
        Thinking = false;
        int WhatTheAttack;
        int Stadia;
        Stadia = Convert.ToInt32(Mathf.Round(Health / 10));
        WhatTheAttack = Random.Range(0, Stadia);
        switch (WhatTheAttack) 
        {
            case 0:
                ShotAttack();
                break;
            case 1:
                MeleeAttack();
                break;
            case 2:
                AimSwods();
                break;

        }
    }

    private void ShotAttack()//Zero Attak
    {
        transform.position = new Vector2(10,10); //change the position!!!
        for (int i = 0;i < 18; i++)
        {
            GameObject Spawned;
            Spawned = Instantiate(Sword);
            Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * 20));
        }
        Attacking = false;
    }

    private void MeleeAttack() //first Attack
    {
        GetPlayer();
        if (PlayerInAttackR)
        {
            StartCoroutine(CuldawnAttack());
        }
        else
        {
            StopRun = false;
            StartCoroutine(CuldawnAttack());
            while (!PlayerInAttackR || !StopRun) 
            {
                GetPlayer();
            }
            StartCoroutine(CuldawnAttack());
        }
        Attacking = false;
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(2);
        StopRun = true;
        StopCoroutine(Run());

    }
    private IEnumerator CuldawnAttack() 
    {
        yield return new WaitForSeconds(1);
        if (PlayerInAttackR) Player.SendMessage("GetDamage", 1);
        StopCoroutine(CuldawnAttack());
    }

    private void AimSwods() //second attack
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 RandomP;
            RandomP.x = Random.Range(-10, 10);
            RandomP.y = Random.Range(-10, 10);
            GameObject Spawned = Instantiate(Sword, RandomP, Quaternion.identity);
            Vector3 targ = Spawned.transform.position;
            Vector3 objectPos = Player.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
        Attacking = false;
    }
}
