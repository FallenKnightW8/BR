using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform Player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10); // Добавляем смещение

    private void LateUpdate() // Используем LateUpdate вместо FixedUpdate
    {
        if (Player == null)
        {
            Debug.LogWarning("Player не назначен!");
            return;
        }

        // Плавное движение с использованием Time.deltaTime
        Vector3 targetPosition = new Vector3(
            Player.position.x + offset.x,
            Player.position.y + offset.y,
            transform.position.z // Сохраняем Z камеры
        );

        // Интерполяция с учетом времени
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime * 10f // Множитель для регулировки скорости
        );
    }
}
