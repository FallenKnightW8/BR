using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Necromant : MonoBehaviour
{
    private GameObject Player;

    [SerializeField] private GameObject MSkelet;
    [SerializeField] private GameObject BSkelet;
    [SerializeField] private GameObject Hands;
    [SerializeField] private int MskeletCount = 0;
    [SerializeField] private int Health = 20;
    [SerializeField] private Animator Animator;

    private GameObject Spawned;
    private int BskeletCount = 0;
    private bool CanSpawn = true;
    private bool CRIsWork = true;
    public bool FStadia = false;
    private bool CountFStadia = true;
    private int GetDamageC = 0;


    private void FixedUpdate()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        if (FStadia == false)
            BatelMind();
        else if (FStadia == true)
        {
            FStadia = false;
            FBatle();
        }
    }
    private IEnumerator StartingM()
    {
        CRIsWork = true;
        yield return new WaitForSeconds(3);
        CanSpawn = true;
        CRIsWork = false;
        StopAllCoroutines();
    }

    private void GetDamage(int Damage)
    {
        Health -= Damage;
        GetDamageC++;
        if (GetDamageC % 2 == 0) { Teleport(); GetDamageC = 0; }

        if (Health <= 5 && CountFStadia == true)
        {
            FStadia = true;
            CountFStadia = false;
        }
    }

    private void BatelMind()
    {
        SpawnSK();

        if(Health <=0) 
        {
            Died();
        }
    }
    private void MSkeletDied (int value) 
    {
        MskeletCount -= value;
    }
    private void BSkeletDied(int value)
    {
        BskeletCount -= value;
    }
    private void SpawnSK()
    {
        Animator.SetBool("Idle?", false);
        Animator.SetFloat("StateNecromant", 0);
        if (MskeletCount == 0 && CanSpawn == true) 
        {
            for (int i = 0;i < 4;i++)      
            {
                float PositionX = Random.Range(-2,2);
                float PositionY = Random.Range(-2,2);
                Spawned = Instantiate(MSkelet);
                Spawned.transform.position = new Vector2(transform.position.x + PositionX, transform.position.y + PositionY);
                MskeletCount++;
                CanSpawn = false;
            }


        }
        if (BskeletCount == 0 && CanSpawn == true)
        {
            for (int i = 0; i < 4; i++)
            {
                float PositionX = Random.Range(-2, 2);
                float PositionY = Random.Range(-2, 2);
                Spawned = Instantiate(BSkelet);
                Spawned.transform.position = new Vector2(transform.position.x + PositionX, transform.position.y + PositionY);
                BskeletCount++;
                CanSpawn = false;   
            }
        }
        Animator.SetBool("Idle?", true);
        if (CRIsWork)
        StartCoroutine(StartingM());
    }

    private void Teleport()
    {
        Animator.SetBool("Idle?", false);
        Animator.SetFloat("StateNecromant", 1);
        float PositionX = Random.Range(-5, 5);
        float PositionY = Random.Range(0, 10);
        transform.position = new Vector2 (PositionX,PositionY);
        if (transform.position.x >= 4 && transform.position.x <= -4) transform.position = new Vector2(0, transform.position.y);
        if (transform.position.y >= 4 && transform.position.y <= -4) transform.position = new Vector2(transform.position.x, 10);
        Animator.SetBool("Idle?", true);
    }

    private void FBatle()
    {
        for (int i = 0; i < 2; i++)
        {
            Spawned = Instantiate(Hands);
            Spawned.transform.position = new Vector2(0, 10);
        }
        Animator.SetBool("Idle?", false);
        Animator.SetFloat("StateNecromant", 1);
        Animator.SetBool("Idle?", true);
        transform.position = new Vector2(100, 100);
    }

    private void Died()
    {
        Player.SendMessage("SetHeart", 2);
        Player.SendMessage("SetHealth",10);
        Destroy(gameObject);
    }
}
