using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public void PlayAgain ()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame ()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
