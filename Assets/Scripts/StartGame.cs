using UnityEngine;

public class StartGame : MonoBehaviour
{
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

        }
    }
}