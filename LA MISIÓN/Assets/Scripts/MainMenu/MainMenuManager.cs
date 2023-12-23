using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void OptionsBtn()
    {
        SceneManager.LoadScene("Nivel 2");
    }
    public void Level3Btn()
    {
        SceneManager.LoadScene("Nivel 3");
    }

    public void Level4Btn()
    {
        SceneManager.LoadScene("Nivel 4");
    }

}
