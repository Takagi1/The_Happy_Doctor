using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager managerInstance;

    void Awake()
    {
        if (managerInstance != null)
            GameObject.Destroy(managerInstance);
        else
            managerInstance = this;

        DontDestroyOnLoad(this);
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
