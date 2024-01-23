using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Random = UnityEngine.Random;

public class FallenKnight : MonoBehaviour
{
    [SerializeField] private GameObject Player; //player
    [SerializeField] private Rigidbody2D Rigidbody; //Himself
    [SerializeField] private GameObject Sword; //What spawn
    [SerializeField] private Collider2D CyrcleRange; //check player for attack
    [SerializeField] private Transform AttackPlace; //were is attack
    [SerializeField] private int Health = 30;
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private int Damage = 1;
    private bool StopBenihils = false;
    private bool PlayerInrange = false;
    private bool Thinking = false;
    private bool Attakin = false;
    private Vector2 movement;
    private void FixedUpdate()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody2D>();
        if (Thinking == false && Attakin == false)
            StartCoroutine(BattleMind());
    }
    private IEnumerator BattleMind()
    {
        Thinking = true;
        yield return new WaitForSeconds(2);
        Attack();
    }
    private void GetPlayer()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        Move(movement);
        ChangePositionAttack(movement);
    }

    private void ChangePositionAttack(Vector2 direction)
    {
        AttackPlace.transform.position = direction * 2;
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
                Attakin = true;
                SwordShotAttack();
                break;
            case 1:
                Attakin = true;
                MeleeAttack();
                //melee
                break;
            case 2:
                //melle jump
                break;

        }
    }
    private void SwordShotAttack()
    {
        transform.position = new Vector3(0,0,0);//change coordinates!!!
        for (int i = 0; i < 18; i++)
        {
            GameObject Spawned = Instantiate(Sword);
            float Radius = i * 20;
            Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Radius));
        }
        Attakin = false;
    }

    private void MeleeAttack()
    {
        if (PlayerInrange)
        {
            //attack
        }
        else
        {
            StartCoroutine(Run());
            while (!PlayerInrange && !StopBenihils)
            {
                GetPlayer();
            }
            //attack
        }
        Attakin = false;
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(2);
        StopBenihils = true;
        StopAllCoroutines();
    }

}
