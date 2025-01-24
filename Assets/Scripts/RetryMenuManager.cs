using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenuManager : MonoBehaviour
{
    public GameObject retryMenuCanvas; // Canvas del men� de Retry
 

    // Llamar a este m�todo cuando el personaje se destruya
    public void ShowRetryMenu()
    {
        retryMenuCanvas.SetActive(true); // Activa el men� de Retry
        Time.timeScale = 0f; // Pausa el juego
    }

    // M�todo para reiniciar la escena
    public void RetryGame()
    {
        Time.timeScale = 1f; // Reactiva el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}
