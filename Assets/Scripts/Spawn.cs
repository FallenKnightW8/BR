using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    void Start()
    {
        Instantiate(Player);
    }
}
