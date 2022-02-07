using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public static bool paused;

    public Animator transition;

    void Start()
    {
        pauseUI.SetActive(false);
        paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        paused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        paused = true;
    }

    IEnumerator loadLevel(string newScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(newScene);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(loadLevel("MainMenu"));
    }
}