using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    [SerializeField]private FallenKnight playerCheck;
    [SerializeField]private int whoIam;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerCheck.ChangePlayerR(whoIam);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCheck.ChangePlayerR(0);
    }

}
