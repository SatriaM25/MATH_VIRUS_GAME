using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader1 : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject MainMenu;
    public GameObject EndScreen;
    public GameObject FinishScreen;
    public GameObject terrain;
    
    public void LoadNextScene()
    {
        GameScreen.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void Retry()
    {
        EndScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void LoadEndScreen()
    {
        terrain.SetActive(false);
        GameScreen.SetActive(false);
        EndScreen.SetActive(true);
    }
    public void Finish()
    {
        GameScreen.SetActive(false);
        FinishScreen.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}
