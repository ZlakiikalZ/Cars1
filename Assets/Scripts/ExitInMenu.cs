using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
