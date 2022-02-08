using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Lose : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    void Start()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    void setWin()
    {
        winUI.SetActive(true);
    }

    void setLose()
    {
        loseUI.SetActive(true);
    }
}