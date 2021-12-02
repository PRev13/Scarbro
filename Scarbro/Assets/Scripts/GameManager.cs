using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //References
    public Dialogues dialogues;
    public Game_ui ui;

    private void Awake()
    {
        dialogues = GameObject.FindObjectOfType<Dialogues>();
        ui = GameObject.FindObjectOfType<Game_ui>();
    }

    void OnLevelChange(Scene _newScene ,LoadSceneMode _mode)
    {
        Awake();
    }


    //Singleton
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    //This will autogenerate the GameObject
    [RuntimeInitializeOnLoadMethod]
    public static void OnLoad()
    {
        GameObject go = new GameObject();
        instance = go.AddComponent<GameManager>();
        DontDestroyOnLoad(go);
        SceneManager.sceneLoaded += instance.OnLevelChange;
    }
}
