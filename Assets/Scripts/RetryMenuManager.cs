using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenuManager : MonoBehaviour
{
    public GameObject retryMenuCanvas; // Canvas del menú de Retry
 

    // Llamar a este método cuando el personaje se destruya
    public void ShowRetryMenu()
    {
        retryMenuCanvas.SetActive(true); // Activa el menú de Retry
        Time.timeScale = 0f; // Pausa el juego
    }

    // Método para reiniciar la escena
    public void RetryGame()
    {
        Time.timeScale = 1f; // Reactiva el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}
