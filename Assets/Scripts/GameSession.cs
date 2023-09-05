using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int playersLive = 3;
    public int score = 0;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playersLive.ToString();
        scoreText.text = score.ToString();

        
    }

    public void ProcessPlayerDeath()
    {
        if (playersLive > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void TakeLife()
    {
        playersLive -= 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playersLive.ToString();

    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
    }

    public void Score()
    {
        score += 100;
        scoreText.text = score.ToString();

    }
}
