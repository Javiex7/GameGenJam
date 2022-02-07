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

    public Text textC1;
    public Text textC2;
    public Text textC3;
    public Text textR1;
    public Text textR2;
    public Text textR3;

    void Start()
    {
        textC1.text = c1;
        textC2.text = c2;
        textC3.text = c3;
        textR1.text = r1;
        textR2.text = r2;
        textR3.text = r3;
    }
}