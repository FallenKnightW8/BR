using UnityEngine;

public class Necromant : MonoBehaviour
{
    [SerializeField] private GameObject MSkelet;
    [SerializeField] private GameObject BSkelet;
    private int MskeletCount = 0;
    private GameObject Spawned;
    private int BskeletCount = 0;
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        SpawnSK();
    }
    private void SpawnSK()
    {
        if (MskeletCount == 0) 
        {
            for (int i = 0;i <= 2;i++)      
            {
                float PositionX = Random.Range(-2,-1);
                float PositionY = Random.Range(-2,-1);
                Spawned = Instantiate(MSkelet);
                Spawned.transform.position = new Vector2(transform.position.x + PositionX, transform.position.y + PositionY);
            }


        }
        if (BskeletCount == 0)
        {

        }
    }

    private void Teleport()
    {
        float PositionX = Random.Range(-10,10);
        float PositionY = Random.Range(0, 10);
        transform.position = new Vector2 (PositionX,PositionY);
        if (transform.position.x > 10) transform.position = new Vector2(10, transform.position.y);
        if (transform.position.y > 10) transform.position = new Vector2(transform.position.x, 10);
    }
}
