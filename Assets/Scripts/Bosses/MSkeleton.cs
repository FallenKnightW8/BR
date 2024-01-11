using UnityEngine;

public class MSkeleton : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
    }

    private void Move()
    {

    }

    private void Attack()
    {

    }
}
