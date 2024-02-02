using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimProjectail : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float Timing;
    private float speed = 0.1f;
    private bool IsDestroying = false;
    private bool Aiming = true;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeToAim());
        FlyToPlayer();
        if (Aiming)
        RotateToPlayer();
        if (!IsDestroying) StartCoroutine(TimeToDie());
    }

    private void FlyToPlayer()
    {
        transform.Translate(50 * speed * Time.deltaTime * Vector3.up);
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

    private IEnumerator TimeToAim()
    {
        yield return new WaitForSeconds(Timing);
        Aiming = false;
    }
    private IEnumerator TimeToDie()
    {
        IsDestroying = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
