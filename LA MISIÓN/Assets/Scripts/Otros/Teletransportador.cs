using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    public string sceneName; // Nombre de la escena de destino

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador sea el que toque el objeto
        {
            SceneManager.LoadScene(sceneName); // Carga la escena de destino
        }
    }
}
