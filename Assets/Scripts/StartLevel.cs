using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public static string selectedPuzzle;
    public Animator transition;

    private GameObject condition1;
    private GameObject condition2;
    private GameObject condition3;
    private GameObject result1;
    private GameObject result2;
    private GameObject result3;

    private GameObject[] rulesArray;
    private CardDragger[] sendRules;

    private bool c;
    private DropCard dc;

    void Awake()
    {
        rulesArray = new GameObject[6];
        sendRules = new CardDragger[6];
        rulesArray[0] = condition1 = GameObject.Find("c1");
        rulesArray[1] = condition2 = GameObject.Find("c2");
        rulesArray[2] = condition3 = GameObject.Find("c3");
        rulesArray[3] = result1 = GameObject.Find("r1");
        rulesArray[4] = result2 = GameObject.Find("r2");
        rulesArray[5] = result3 = GameObject.Find("r3");
    }

    public void checkRules()
    {
        c = true;

        for (int i = 0; i < 6; i++)
        {
            if (rulesArray[i] != null)
            {
                dc = rulesArray[i].GetComponent<DropCard>();
                if (dc.transform.childCount != 1)
                {
                    c = false;
                }
                else
                {
                    sendRules[i] = dc.actualCard;
                }
            }
        }

        if (c == true)
        {
            LoadRules.c1 = sendRules[0].GetComponentInChildren<Text>().text;
            LoadRules.c2 = sendRules[1].GetComponentInChildren<Text>().text;
            LoadRules.c3 = sendRules[2].GetComponentInChildren<Text>().text;
            LoadRules.r1 = sendRules[3].GetComponentInChildren<Text>().text;
            LoadRules.r2 = sendRules[4].GetComponentInChildren<Text>().text;
            LoadRules.r3 = sendRules[5].GetComponentInChildren<Text>().text;

            // Select level/puzzle (1, 2 or 3)
            selectedPuzzle = "MainScene";

            StartCoroutine(loadLevel(selectedPuzzle));
        } 
    }

    IEnumerator loadLevel(string newScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(newScene);
    }

}