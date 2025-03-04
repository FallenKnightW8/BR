using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBoost : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 10f; // Время перезарядки
    [SerializeField] private float boostTime = 5f; // Длительность использования способности
    private float nextAllowedTime = 0f; // Время, когда можно снова нажать Q
    private bool canDestroyObjects = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= nextAllowedTime)
        {
            EnableDestroyForSeconds(boostTime);
            nextAllowedTime = Time.time + cooldownTime;
            Debug.Log("Pressed Q");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Q доступна через {Mathf.Ceil(nextAllowedTime - Time.time)} секунд.");
        }
    }

    private void EnableDestroyForSeconds(float seconds)
    {
        StartCoroutine(TemporaryDestroyAccess(seconds));
    }

    private IEnumerator TemporaryDestroyAccess(float seconds)
    {
        // Включаем доступ к уничтожению
        canDestroyObjects = true;
        yield return new WaitForSeconds(seconds);
        // Отключаем доступ к уничтожению
        canDestroyObjects = false;
    }

    public bool GetCanDestroyObjects()
    {
        return canDestroyObjects;
    }
}
