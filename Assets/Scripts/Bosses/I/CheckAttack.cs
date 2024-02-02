using UnityEngine;

public class CheckAttack : MonoBehaviour
{
    [SerializeField]private MSkeleton playerCheck;

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
