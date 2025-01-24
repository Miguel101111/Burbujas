using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Mov Player;
    public GameObject menuCanvas; // El Canvas del menú
    /*private void Start()
    {
        // Activa el menú principal solo si estás en la escena inicial
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            menuCanvas.SetActive(true);

        }
        else
        {
            menuCanvas.SetActive(false);
        }
    }
    */


    // Función que se ejecuta al hacer clic en "Start"
    public void StartGame()
    {
        // Desactiva el Canvas del menú para mostrar el juego
        menuCanvas.SetActive(false);
        Player.bStart = true;
    }
    public void ShowRetryMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void close()
    {
        Application.Quit();
    }
}

