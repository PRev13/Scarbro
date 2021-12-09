using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menubar : MonoBehaviour
{
    public Transform menuPanel;

    public void OpenPopup ()
    {
        menuPanel.localScale = new Vector2 (1, 1);
        Time.timeScale = 0f;
    }

    public void ClosePopup ()
    {
        menuPanel.localScale = Vector2.zero;
        Time.timeScale = 1f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LoadGame()
    {
        menuPanel.localScale = Vector2.zero;
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
