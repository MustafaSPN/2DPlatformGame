using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nexScene = currentSceneIndex + 1;
        
        if (nexScene == SceneManager.sceneCountInBuildSettings)
        {
            nexScene = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nexScene);
    }
}
