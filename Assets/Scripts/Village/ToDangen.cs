using UnityEngine;
using UnityEngine.SceneManagement;
public class ToDangen : MonoBehaviour
{
    [SerializeField] private string Dangeon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(Dangeon);
    }
}
