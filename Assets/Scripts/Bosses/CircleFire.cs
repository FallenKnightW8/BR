using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private int Damage = 1;
    [SerializeField] private float Timing;
    private bool IsDestroying = false;
    private float speed = 0.1f;

    private bool Aiming = true;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        StartCoroutine(TimeToAim());
        if (Aiming)
            RotateToPlayer();
        if(!Aiming)
            FlyToPlayer();
        else
        {
            //Vector3 Position =new Vector3(Player.transform.position.x + 2, Player.transform.position.y + 2, Player.transform.position.z);
            //transform.position = Position;
        }
        if (!IsDestroying) StartCoroutine(TimeToDie());
    }
    private void RotateToPlayer()
    {
        Vector3 targ = transform.position;
        Vector3 objectPos = Player.transform.position;
        targ.x -= objectPos.x;
        targ.y -= objectPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("GetDamage", Damage);
            DestroyBySword();
        }
    }
    private void DestroyBySword()
    {
        Destroy(this.gameObject);
    }
    public void GetHit()
    {
        DestroyBySword();
    }
    private IEnumerator TimeToAim()
    {
        yield return new WaitForSeconds(Timing);
        Aiming = false;
    }
    private IEnumerator TimeToDie()
    {
        IsDestroying = true;
        yield return new WaitForSeconds(5);
        DestroyBySword();
    }
    private void FlyToPlayer()
    {
        transform.Translate(50 * speed * Time.deltaTime * Vector3.up);
    }
}
