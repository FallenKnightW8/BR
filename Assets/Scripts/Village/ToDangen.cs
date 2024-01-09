using UnityEngine;
using UnityEngine.SceneManagement;
public class ToDangen : MonoBehaviour
{
    [SerializeField] private string Dangeon;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(Dangeon);
    }
}
