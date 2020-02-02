using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager managerInstance;
    static int curScene = 0;

    private void Awake()
    {
        if (managerInstance != null)
            GameSceneManager.Destroy(managerInstance);
        else
            managerInstance = this;

        DontDestroyOnLoad(this);
        
    }


    public void LoadNextScene()
    {
        curScene += 1;
        SceneManager.LoadScene(curScene);
    }
}
