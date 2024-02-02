using UnityEngine;

public class CheckAttackHands : MonoBehaviour
{
    [SerializeField]private Hands playerCheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerCheck.ChangePlayerR(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCheck.ChangePlayerR(false);
    }

}
