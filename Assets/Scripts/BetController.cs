using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BetController : MonoBehaviour
{
    [SerializeField] TMP_Text betText;
    [SerializeField] MoneyManager money;
    public int betValue;


    private void Start()
    {
        betValue = 100;
    }
    
    public void IncreaseBet()
    {
        if(betValue + 100 <= money.GetMoney())
        {
            betValue += 100;
        }
        SetText();
    }
    public void DecreaseBet()
    {
        if(betValue - 100 > 0)
        {
            betValue -= 100;
        }
        SetText();
    }

    public void SetText()
    {
        betText.text = betValue.ToString();
    }
    public int GetBet()
    {
        return betValue;
    }
}
