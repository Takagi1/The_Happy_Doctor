using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGame : MonoBehaviour
{

    public void StartGame()
    {
        GameSceneManager.managerInstance.LoadNextScene();
    }
}

