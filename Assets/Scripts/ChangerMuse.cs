using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangerMuse : MonoBehaviour
{
    [SerializeField] private AudioClip[] Mysics;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = Mysics[1];
        audioSource.Play();
    }
    private void ChangerMys(int Clip)
    {
        audioSource.clip = Mysics[Clip];
        audioSource.Play();
    }
}
