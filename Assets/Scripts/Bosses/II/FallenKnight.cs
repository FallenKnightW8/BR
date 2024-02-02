using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallenKnight : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private GameObject Sword;
    [SerializeField]private bool PlayerInAttackR = false;
    private bool Attacking = false;
    private bool Thinking = false;
    [SerializeField] private int Health = 30;
    [SerializeField] private int Damage = 2;
    private void FixedUpdate()
    {
        if (Health <=0)
        {
            Die();
        }
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody2D>();
        if (Attacking == false && Thinking == false)
        {
            Debug.Log("Thinking");
            StartCoroutine(ChangeAttack());
        }
        if (transform.position.x <-9 || transform.position.x > 0.5) transform.position = new Vector2(0, transform.position.y);
        if (transform.position.y > 19 || transform.position.y < 3) transform.position = new Vector2(transform.position.x, 18);
    }
    private void GetDamage(int Damage)
    {
        Health -= Damage;
    }
        
    public void ChangePlayerR(bool Check)
    {
        PlayerInAttackR = Check;
    }
    private IEnumerator ChangeAttack()
    {
        Thinking = true;
        yield return new WaitForSeconds(2);
        Attack();
        StopCoroutine(ChangeAttack());
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
                AimSwods();
                break;
            case 1:
                ShotAttack();
                break;
            case 2:
                MeleeAttack();
                break;

        }
    }

    private void ShotAttack()//Zero Attak
    {
        Vector2 RandomPosition = new Vector2(0,18);
        RandomPosition.x = Random.Range(-3,0);
        transform.position = RandomPosition; 
        for (int i = 0;i < 18; i++)
        {
            GameObject Spawned;
            Spawned = Instantiate(Sword);
            Spawned.transform.position = transform.position;
            Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * 20));
        }
        Attacking = false;
    }

    private void MeleeAttack() //first Attack
    {
        Vector3 AroundP = new Vector3(0,0,0);
        AroundP.x = Random.Range(-1,1);
        AroundP.y = Random.Range(-1,1);
        transform.position = Player.transform.position + AroundP;
        StartCoroutine(CuldawnAttack());
    }
    private IEnumerator CuldawnAttack() 
    {
        yield return new WaitForSeconds(0.5f);
        if (PlayerInAttackR) Player.SendMessage("GetDamage", Damage);
        Attacking = false;
        StopCoroutine(CuldawnAttack());
    }

    private void AimSwods() //second attack
    {
        Vector2 Changeposition;
        for (int i = 0; i < 3; i++)
        {
            Vector2 RandomP;
            RandomP.x = Random.Range(-9, 9);
            RandomP.y = Random.Range(20, 22);
            GameObject Spawned = Instantiate(Sword, RandomP, Quaternion.identity);
            Vector3 targ = Spawned.transform.position;
            Vector3 objectPos = Player.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
        Changeposition.x = Random.Range(-1, 1);
        Changeposition.y = Random.Range(-1, 1);
        transform.position = new Vector2(transform.position.x + Changeposition.x, transform.position.y + Changeposition.y);
        Attacking = false;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
