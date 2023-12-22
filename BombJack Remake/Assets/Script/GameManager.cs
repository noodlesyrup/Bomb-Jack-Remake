    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;
    public GameObject SoundManager;

    bool gameHasEnded = false;
    bool gameHasFinish = false;
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            gameOverUI.SetActive(true);
            SoundManager.SetActive(false);
        }
    }

    public void FinishGame()
    {
        if (gameHasFinish == false)
        {
            gameHasFinish = true;
            levelCompleteUI.SetActive(true);
            SoundManager.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }



}
