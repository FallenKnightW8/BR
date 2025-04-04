using System.Globalization;
using UnityEngine;
using Unity.Netcode;


public class CameraFollower : NetworkBehaviour
{
    public Camera playerCamera; // Ссылка на камеру игрока

    private void Start()
    {
        // Активируем камеру только для своего игрока
        if (IsOwner)
        {
            playerCamera.enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true; // Включаем аудиолистенер
        }
        else
        {
            playerCamera.enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false; // Отключаем аудиолистенер
        }
    }
}
