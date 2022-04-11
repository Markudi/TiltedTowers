using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public TextMeshProUGUI wonGameText;
    public TextMeshProUGUI lostGameText;

    public GameObject myEnemySpawner;
    
    private bool gameEnded = false;


    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        //If player loses all lives
        if (Economy.playerLives <= 0)
        {
            EndGameLost();
            myEnemySpawner.SetActive(false);
        }
    }

    //If player loses. Display Text for losing.
    public void EndGameLost()
    {
        gameEnded = true;

        //Display Losing Text
        lostGameText.gameObject.SetActive(true);
        StartCoroutine(GoToNextScene());
    }

    //If player wins (kills all enemy waves). Display winning text.
    public void EndGameWon()
    {
        gameEnded = true;
        wonGameText.gameObject.SetActive(true);
        StartCoroutine(GoToNextScene());
    }
    
    //Load up restart scene
    IEnumerator GoToNextScene()
    {
        
        yield return new WaitForSeconds(5);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
