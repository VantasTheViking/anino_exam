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
        money -= bet.GetBet();
        moneyText.text = $"Coins: {money}";
    }

    public void GetWinnings(int changeVal)
    {
        money += changeVal;
        moneyText.text = $"Coins: {money}";
    }
    public int GetMoney()
    {
        return money;
    }


}
