using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFigth : MonoBehaviour
{
    [SerializeField] private GameObject BossSpawn;
     private ChangerMuse Muse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            BossSpawn.SendMessage("StartFigth",1);
            Muse = GameObject.FindWithTag("Mysic").GetComponent<ChangerMuse>();
            Muse.SendMessage("ChangerMys", 5);
        }
    }

}
