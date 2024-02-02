using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private FallenKnight playerCheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerCheck.ChangePlayerR(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerCheck.ChangePlayerR(false);
    }

}
