using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecTile : MonoBehaviour
{
    [SerializeField] private Vector2 Direction;
    [SerializeField] private float speed = 10000;
    [SerializeField] private int Damage = 1;
    private bool IsDestroying = false;

    void FixedUpdate()
    {
        transform.Translate(2 * speed * Time.deltaTime * Vector3.up);
        if (!IsDestroying )
        StartCoroutine(TimeToDie());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("GetDamage", Damage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Props"))
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator TimeToDie()
    {
        IsDestroying = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
