using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnim;

    public void PlayGame()
    {
        StartCoroutine(LoadScene());
    }

    public void Restart()
    {
        StartCoroutine(LoadRestart());
    }

    public void Menu()
    {
        StartCoroutine(LoadMenu());
    }

    public void Tutorial()
    {
        StartCoroutine(LoadTutorial());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadRestart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadTutorial()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Tutorial");
    }
}
