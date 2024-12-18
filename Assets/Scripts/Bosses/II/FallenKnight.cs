using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FallenKnight : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject AimSword;
    [SerializeField] private Slider HealthBar;
    [SerializeField]private bool PlayerInAttackR = false;
    [SerializeField] private int Health = 30;
    [SerializeField] private int Damage = 2;
    
    private AudioSource audioSource;
    private bool FStarted = false;
    private bool Attacking = false;
    private bool Thinking = false;

    private void Awake()
    {
        audioSource = GameObject.FindWithTag("Mysic").GetComponent<AudioSource>();
        audioSource.SendMessage("ChangerMys", 5);
        HealthBar.value = Health;
    }
    private void FixedUpdate()
    {
        if (Health <= 0)
        {
            Die();
        }
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (Attacking == false && Thinking == false)
        {
            Debug.Log("Thinking");
            StartCoroutine(ChangeAttack());
        }
        if (transform.position.x <=-9 || transform.position.x >= 0.5) transform.position = new Vector2(0, transform.position.y);
        if (transform.position.y >= 19 || transform.position.y <= 3) transform.position = new Vector2(transform.position.x, 18);
    }
    private void GetDamage(int Damage)
    {
        Health -= Damage;
        HealthBar.value = Health; 
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
        if (Stadia == 1 && !FStarted) 
        {
            FStarted = true;
            audioSource.SendMessage("ChangerMys", 6);
        }
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
        Vector2 RandomPosition = new Vector2(0,23);
        RandomPosition.x = Random.Range(-4,4);
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
        int DirectionX = Random.Range(0, 1);
        for (int i = 0; i < (13 - 4*(Convert.ToInt32(Mathf.Round(Health / 10)))) ; i++)
        {
            Vector2 RandomP;
            if (DirectionX == 1)
            {
                //were atack
            }
            GameObject Spawned = Instantiate(AimSword,transform.position, Quaternion.identity);
            Vector3 targ = Spawned.transform.position;
            Vector3 objectPos = Player.transform.position;
            targ.x -= objectPos.x;
            targ.y -= objectPos.y;
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            Spawned.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
        transform.position = new Vector2(0,23);
        Attacking = false;
    }

    private void Die()
    {
        audioSource.SendMessage("ChangerMys", 7);
        Destroy(this.gameObject);

    }
}
