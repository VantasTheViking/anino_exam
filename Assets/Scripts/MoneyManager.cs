using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] BetController bet;
    int money;

    private void Start()
    {
        money = 1000;
    }

    public void BetMoney()
    {
        if(money >= bet.GetBet())
        {
            money -= bet.GetBet();
            bet.betValue = 100;
            bet.SetText();
            moneyText.text = $"Coins: {money}";
        }
        
    }

    public void AddWinnings(int changeVal)
    {
        money += changeVal;
        moneyText.text = $"Coins: {money}";
    }
    public int GetMoney()
    {
        return money;
    }


}
