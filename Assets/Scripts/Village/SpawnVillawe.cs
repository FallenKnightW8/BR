using UnityEngine;

public class SpawnVillawe : MonoBehaviour
{
    [SerializeField] private GameObject Prefab; // сюда вставить нпс для спавна или игрока
    void Start()
    {
        Instantiate(Prefab, transform);
    }

}
