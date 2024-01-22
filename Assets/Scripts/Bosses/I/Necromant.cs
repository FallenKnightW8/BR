using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Necromant : MonoBehaviour
{
    [SerializeField] private GameObject MSkelet;
    [SerializeField] private GameObject BSkelet;
    [SerializeField] private GameObject Hands;
    [SerializeField]private int MskeletCount = 0;
    private GameObject Spawned;
    [SerializeField]private int BskeletCount = 0;
    private bool CanSpawn = true;
    private bool CRIsWork = true;
    public bool FStadia = false;
    private bool CountFStadia = false;
    [SerializeField]private int Health = 20;
    private int GetDamageC = 0;


    private void FixedUpdate()
    {
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
        yield return new WaitForSeconds(10);
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
        if (CRIsWork)
        StartCoroutine(StartingM());
    }

    private void Teleport()
    {
        float PositionX = Random.Range(-10,10);
        float PositionY = Random.Range(0, 10);
        transform.position = new Vector2 (PositionX,PositionY);
        if (transform.position.x > 10 && transform.position.x < -10) transform.position = new Vector2(0, transform.position.y);
        if (transform.position.y > 10 && transform.position.y < -10) transform.position = new Vector2(transform.position.x, 10);
    }

    private void FBatle()
    {
        for (int i = 0; i < 2; i++)
        {
            Spawned = Instantiate(Hands);
            Spawned.transform.position = new Vector2(0, 10);
        }
        transform.position = new Vector2(100, 100);
    }

    private void Died()
    {
        Destroy(gameObject);
    }
}
