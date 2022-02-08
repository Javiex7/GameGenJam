using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Lose : MonoBehaviour
{
    public static Win_Lose instance;
    public GameObject winUI;
    public GameObject loseUI;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    public void setWin()
    {
        winUI.SetActive(true);
    }

    public void setLose()
    {
        loseUI.SetActive(true);
    }
}