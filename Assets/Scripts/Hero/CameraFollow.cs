using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform Player; // Объект, за которым следит камера
    public float smoothSpeed = 0.125f; // Скорость сглаживания

    private void FixedUpdate()
    {
        if (Player)
        {
            // Плавное движение камеры с использованием интерполяции и времени
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, Player.position, smoothSpeed);

            // Устанавливаем позицию камеры, игнорируя ось Z для 2D
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
