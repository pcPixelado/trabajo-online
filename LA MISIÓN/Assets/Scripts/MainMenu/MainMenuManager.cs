using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneManager.LoadScene("prueba");
    }

    public void OptionsBtn()
    {
        SceneManager.LoadScene("Nivel 2");
    }
    public void level3Btn()
    {
        SceneManager.LoadScene("Nivel 3");
    }

    public void level4Btn()
    {
        SceneManager.LoadScene("Nivel 4");
    }

}
