using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Necromant : MonoBehaviour
{
    private GameObject Player;

    [SerializeField] private GameObject MSkelet;
    [SerializeField] private GameObject BSkelet;
    [SerializeField] private GameObject Hands;
    [SerializeField] private Image HealthBar;
    [SerializeField] private int MskeletCount = 0;
    [SerializeField] private int Health = 20;
    [SerializeField] private int MaxHealth;
    [SerializeField] private Animator Animator;
    [Header("Attacks")]
    [SerializeField] private GameObject AimFire;
    [SerializeField] private GameObject CircleFire;
    [SerializeField] private GameObject Fire;

    [SerializeField] private int CountOfFireAi = 360;
    [SerializeField] private int CountOfFire = 360;

    private AudioSource audioSource;

    private GameObject Spawned;
    private int BskeletCount = 0;
    private bool CanSpawn = true;
    private bool CRIsWork = true;
    public bool FStadia = false;
    private bool CountFStadia = true;
    private int GetDamageC = 0;
    private bool CanAttack = true;


    private void Start()
    {
        HealthBar.fillAmount = 1f;
        MaxHealth = Health;
    }

    /*private void Awake()
    {
        audioSource = GameObject.FindWithTag("Mysic").GetComponent<AudioSource>();
        audioSource.SendMessage("ChangerMys", 1);
    }*/

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
        float HealthF = MaxHealth;
        HealthBar.fillAmount = Health / HealthF;

        Animator.SetBool("Idle?", false);
        Animator.SetFloat("StateNecromant", 1);
        Health -= Damage;
        GetDamageC++;

        if (GetDamageC % 2 == 0) { Teleport(); GetDamageC = 0; CountOfFireAi+=2; CountOfFire = CountOfFire + 2; }

        if (Health <= 5 && CountFStadia == true)
        {
            FStadia = true;
            CountFStadia = false;
        }
    }

    private void BatelMind()
    {
        if (CanAttack)
        {
            int RandomAttack = Random.Range(1, 4);
            switch (RandomAttack)
            {
                case 0:
                    SpawnSK();
                    break;
                case 1:
                    SpawnAimFire();
                    break;
                case 2:
                    SpawnFireCircle();
                    break;
                case 3:
                    RoundShoot();
                    break;
            }
        }

        if(Health <=0) 
        {
            Died();
        }
    }


    private void MSkeletDied (int value) 
    {
        MskeletCount -= value;
        if (MskeletCount == 0) CRIsWork = true;
    }
    private void BSkeletDied(int value)
    {
        BskeletCount -= value;
        if (BskeletCount == 0) CRIsWork = true;
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

    private void SpawnAimFire()
    {
        for (int i = 0; i < CountOfFireAi; i++)
        {
            Vector3 AroundP = new Vector3(transform.position.x + (i * 1.5f), transform.position.y +(i * 1.5f), transform.position.z);
            GameObject Fire = Instantiate(AimFire, AroundP, Quaternion.identity);
        }
        StartCoroutine(CouldownFAttack());
    }

    private void RoundShoot()
    {

        for (int i = 0; i < CountOfFire; i++)
        {
            float P = 360 / CountOfFire;
            P = P * i * Mathf.PI / 180;
            Vector3 AroundP = new Vector3(transform.position.x + (Mathf.Cos(P) * 2), transform.position.y + (2 * Mathf.Sin(P)), transform.position.z);
            GameObject Spawned = Instantiate(Fire, AroundP, Quaternion.identity);
            Spawned.transform.rotation = Quaternion.Euler(0, 0, 360 / (CountOfFire) * i);
        }

        StartCoroutine(CouldownFAttack());
    }
    private void SpawnFireCircle()
    {
        for (int i = 1; i < CountOfFireAi + 1; i++)
        {
            float P = 360 / CountOfFireAi;
            P = P * i * Mathf.PI / 180;
            Vector3 AroundP = new Vector3(Player.transform.position.x + ( Mathf.Cos(P) *3), Player.transform.position.y + (3 *Mathf.Sin(P)), Player.transform.position.z);
            Instantiate(CircleFire, AroundP, Quaternion.identity);
            StartCoroutine(CouldownFAttack());
        }
    }

    private void Teleport()
    {
        float PositionX = Random.Range(-5, 5);
        float PositionY = Random.Range(0, 10);
        transform.position = new Vector2 (PositionX,PositionY);
        if (transform.position.x >= 4 && transform.position.x <= -4) transform.position = new Vector2(0, transform.position.y);
        if (transform.position.y >= 4 && transform.position.y <= -4) transform.position = new Vector2(transform.position.x, 10);
        Animator.SetBool("Idle?", true);
    }

    private void FBatle()
    {
        audioSource.SendMessage("ChangerMys", 2);
        Animator.SetFloat("StateNecromant", 0);
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
        audioSource.SendMessage("ChangerMys", 3);
        Player.SendMessage("SetHeart", 2);
        Player.SendMessage("SetHealth",10);
        Destroy(gameObject);
    }

    private IEnumerator CouldownFAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(1.5f);
        CanAttack = true;
        StopCoroutine(CouldownFAttack());
    }
}
