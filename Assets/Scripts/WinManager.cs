using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinManager : MonoBehaviour
{
    int winCount;
    [SerializeField] TMP_Text winCounter;

    private void Start()
    {
        UpdateCounterText();
    }

    public void SetWins(int wins)
    {
        winCount = wins;
        UpdateCounterText();
    }

    public void UpdateCounterText()
    {
        winCounter.text = winCount.ToString();
    }
    
}
