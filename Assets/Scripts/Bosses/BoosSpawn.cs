using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Boss;
    private int Read = 0;
    private void StartFigth(int Ready)
    {
        Read += Ready;
        if (Read == 1)
        { 
             if (Boss != null)
             {
                Instantiate(Boss);
             }
        }
    }
}
