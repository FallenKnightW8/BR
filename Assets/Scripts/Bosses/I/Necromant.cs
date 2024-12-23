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
    [SerializeField] private Win StartWin;
    [SerializeField] private float LengOfBatle;
    [Header("Attacks")]
    [SerializeField] private GameObject AimFire;
    [SerializeField] private GameObject CircleFire;
    [SerializeField] private GameObject Fire;

    [SerializeField] private int CountOfFireAi = 360;
    [SerializeField] private int CountOfFire = 360;

    private AudioSource audioSource;

    private GameObject Spawned;
    public bool FStadia = false;
    private bool CountFStadia = true;
    private int GetDamageC = 0;
    private bool CanAttack = true;


    private void Start()
    {
        HealthBar.fillAmount = 1f;
        MaxHealth = Health;
    }

    private void Awake()
    {
        audioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        audioSource.SendMessage("ChangerMys", 1);
    }

    private void FixedUpdate()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        Animator.SetFloat("Horizontal", GetPlayerHorizontalPos());
        if (FStadia == false && Health >0)
            BatelMind();
        /*else if (FStadia == true)
        {
            FStadia = false;
            FBatle();
        }*/
    }

    public float GetPlayerHorizontalPos()
    {

        if(Player.transform.position.y - transform.position.y <= 0)
            return 0f;
        return Player.transform.position.x - transform.position.x;
    }
    private void GetDamage(int Damage)
    {
        Health -= Damage;
        float HealthF = MaxHealth;
        HealthBar.fillAmount = Health / HealthF;
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        Animator.SetBool("Idle?", false);
        Animator.SetFloat("StateNecromant", 1);
        
        GetDamageC++;
        if (Health <= 0)
        {
            Died();
        }
        if (GetDamageC % 2 == 0 && Health!=0) { Teleport(); GetDamageC = 0; CountOfFireAi+=2; CountOfFire = CountOfFire + 2; }

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
            Animator.SetTrigger("IsAttack?");
            int RandomAttack = Random.Range(0, 4);
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
            

    }

    public void MskeleMSkeletDied(int Value)
    {
        MskeletCount -= Value;
    }

    private void SpawnSK()
    {
        Animator.SetBool("Idle?", false);
        Animator.SetFloat("StateNecromant", 0);
        if (MskeletCount == 0) 
        {
            for (int i = 0;i < 2;i++)      
            {
                float PositionX = Random.Range(-2,2);
                float PositionY = Random.Range(-2,2);
                Spawned = Instantiate(MSkelet);
                Spawned.transform.position = new Vector2(transform.position.x + PositionX, transform.position.y + PositionY);
                MskeletCount++;
            }


        }
        /*if (BskeletCount == 0 && CanSpawn == true)
        {
            for (int i = 0; i < 2; i++)
            {
                float PositionX = Random.Range(-2, 2);
                float PositionY = Random.Range(-2, 2);
                Spawned = Instantiate(BSkelet);
                Spawned.transform.position = new Vector2(transform.position.x + PositionX, transform.position.y + PositionY);
                BskeletCount++;
                CanSpawn = false;   
            }
        }*/ 
    }

    private void SpawnAimFire()
    {
        for (int i = 0; i < CountOfFireAi; i++)
        {
            int j;
            j = Random.Range(-1, 2);
            Vector3 AroundP = new Vector3(j * 10f, 10f, transform.position.z);
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
        float PositionX = Random.Range(-10, 10);
        float PositionY = Random.Range(-8, 8);
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
        Animator.SetTrigger("IsDie?");
        audioSource.SendMessage("ChangerMys", 3);
        audioSource.loop = false;
        //Player.SendMessage("SetHeart", 2);
        //Player.SendMessage("SetHealth",10);
        StartCoroutine(WaitYouPie());

    }

    private IEnumerator CouldownFAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(1.5f);
        CanAttack = true;
        StopCoroutine(CouldownFAttack());
    }

    private IEnumerator WaitYouPie()
    {

        yield return new WaitForSeconds(2);
        StartWin.WinGame();
        Destroy(gameObject);
        StopCoroutine(WaitYouPie());
    }
}
