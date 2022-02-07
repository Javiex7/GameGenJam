using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Animator transition;

    public void load1()
    {
        StartLevel.selectedPuzzle = "Level1";
        StartCoroutine(loadLevel("Rules"));
    }

    public void load2()
    {
        StartLevel.selectedPuzzle = "Level2";
        StartCoroutine(loadLevel("Rules"));
    }

    public void load3()
    {
        StartLevel.selectedPuzzle = "Level3";
        StartCoroutine(loadLevel("Rules"));
    }

    IEnumerator loadLevel(string newScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(newScene);
    }
}