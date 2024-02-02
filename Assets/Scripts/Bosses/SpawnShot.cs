using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShot : MonoBehaviour
{
    [SerializeField] private GameObject ProjectTile;

    private void SpawnShotTile()
    {
        Instantiate(ProjectTile);
    }
}
