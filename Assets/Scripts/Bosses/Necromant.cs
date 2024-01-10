using UnityEngine;

public class Necromant : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        Teleport();
    }
    private void SpawnSK()
    {
        
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
