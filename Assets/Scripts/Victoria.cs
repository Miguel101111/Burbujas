using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : MonoBehaviour
{
    public GameObject CanavasVictoria;
    public MainMenu MainMenu;
    public void ShowRetryMenu()
    {
        CanavasVictoria.SetActive(true); // Activa el menú de Retry
    }
    public void EmpezarDeNuevo()
    {
        CanavasVictoria.SetActive(false);
        MainMenu.ShowRetryMenu();
    }
}
