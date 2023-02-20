using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject HighscoreTextMenu;

    public void Start()
    {
        SaveManager.Instance.HighdcoreText1.SetActive(false);
        SaveManager.Instance.HighdcoreText2.SetActive(true);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
