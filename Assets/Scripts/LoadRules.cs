using UnityEngine;
using UnityEngine.UI;

public class LoadRules : MonoBehaviour
{
    public static string c1;
    public static string c2;
    public static string c3;
    public static string r1;
    public static string r2;
    public static string r3;

    public static int c1ID, c2ID, c3ID, r1ID, r2ID, r3ID;    

    public Text textC1;
    public Text textC2;
    public Text textC3;
    public Text textR1;
    public Text textR2;
    public Text textR3;

    void Start()
    {
        textC1.text = c1;
        textC1.GetComponentInParent<CardDragger>().RuleSetted = true;
        textC2.text = c2;
        textC2.GetComponentInParent<CardDragger>().RuleSetted = true;
        textC3.text = c3;
        textC3.GetComponentInParent<CardDragger>().RuleSetted = true;
        textR1.text = r1;
        textR1.GetComponentInParent<CardDragger>().RuleSetted = true;
        textR2.text = r2;
        textR2.GetComponentInParent<CardDragger>().RuleSetted = true;
        textR3.text = r3;
        textR3.GetComponentInParent<CardDragger>().RuleSetted = true;

        GameController.Instance.rule1 = int.Parse(c1ID.ToString() + r1ID.ToString());
        GameController.Instance.rule2 = int.Parse(c2ID.ToString() + r2ID.ToString());
        GameController.Instance.rule3 = int.Parse(c3ID.ToString() + r3ID.ToString());
    }
}