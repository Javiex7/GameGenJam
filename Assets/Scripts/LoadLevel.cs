using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void load1()
    {
        StartLevel.selectedPuzzle = "Level1";
        SceneManager.LoadScene("Rules");
    }

    public void load2()
    {
        StartLevel.selectedPuzzle = "Level2";
        SceneManager.LoadScene("Rules");
    }

    public void load3()
    {
        StartLevel.selectedPuzzle = "Level3";
        SceneManager.LoadScene("Rules");
    }
}