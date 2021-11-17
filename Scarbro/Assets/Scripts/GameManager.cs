using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //References
    public Dialogues dialogues;

    private void Awake()
    {
        dialogues = GameObject.FindObjectOfType<Dialogues>();
    }

    private void OnLevelWasLoaded(int level)
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
    }
}
