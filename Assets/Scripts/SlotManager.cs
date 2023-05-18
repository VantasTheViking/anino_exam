using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotManager : MonoBehaviour
{
    [SerializeField] List<ReelBehaviour> reels;

    [SerializeField] Button spinButton;
    bool canSpin = true;
    bool isSpinning;

    [SerializeField] BetController bet;
    int betVal;

    public enum ReelPosition { Top, Mid, Bot};
    [System.Serializable]
    public class ReelPositionSerializable
    {
        public List<ReelPosition> reelPattern;
    }
    [SerializeField] List<ReelPositionSerializable> reelPatterns;

    [System.Serializable]
    public class ComboSerializable
    {
        public List<int> comboPayouts;
    }
    [SerializeField] List<int> symbols;
    [SerializeField] List<ComboSerializable> payouts;

    [SerializeField] MoneyManager moneyManager;
    [SerializeField] WinManager winManager;
    int startingSymbol;
    public int totalWinnings;
    int winCount;
    bool isCombo;
    int comboCount;

    //Sets the reels to spin
    public void Spin()
    {
        betVal = bet.GetBet();
        if (canSpin)
        {
            foreach (ReelBehaviour s in reels)
            {
                s.StartSpin();
            }
        }
        spinButton.interactable = false;
        canSpin = false;

    }
    
    private void Update()
    {
        if (canSpin == false && moneyManager.GetMoney() > 0)
        {
            isSpinning = false;

            foreach (ReelBehaviour s in reels)
            {
                if(s.GetState() != ReelBehaviour.State.stopped)
                {
                    isSpinning = true;
                }
            }

            if (isSpinning == false)
            {
                //Check Win Patterns
                CheckPatterns();
                //Add Win Money
                UpdateWin();

                canSpin = true;
                spinButton.interactable = true;

            }
        }
    }

    //Updates stats with the spin's results
    public void UpdateWin()
    {
        moneyManager.AddWinnings(totalWinnings);
        winManager.SetWins(winCount);
        totalWinnings = 0;
        winCount = 0;
    }
    public void CheckPatterns()
    {
        foreach(ReelPositionSerializable r in reelPatterns)
        {
            SetStartingSymbol(r.reelPattern[0]);

            comboCount = 0;
            isCombo = true;
            for(int i = 0; i <= 4; i++)
            {
                if (isCombo)
                {
                    if (CheckPosToReel(r.reelPattern[i], reels[i]))
                    {
                        comboCount++;
                    }
                    else isCombo = false;
                }
            }

            totalWinnings += AddPatternPrize() * betVal;
            
            
        }
    }

    public int AddPatternPrize()
    {
        if (payouts[symbols.IndexOf(startingSymbol)].comboPayouts[comboCount - 1] > 0)
        {
            winCount++;
        }
        return payouts[symbols.IndexOf(startingSymbol)].comboPayouts[comboCount - 1];

    }

    //Find the starting symbol for the line pattern
    public void SetStartingSymbol(ReelPosition rp)
    {
        if (rp == ReelPosition.Top)
        {
            startingSymbol = reels[0].GetTopRowVal();
        }
        else if (rp == ReelPosition.Bot)
        {
            startingSymbol = reels[0].GetBotRowVal();
        }
        else if (rp == ReelPosition.Mid)
        {
            startingSymbol = reels[0].GetMidRowVal();
        }
    }

    //checks the current reel's symbol if it matches the starting symbol of the pattern.
    public bool CheckPosToReel(ReelPosition rp, ReelBehaviour reel)
    {
        if (rp == ReelPosition.Top)
        {
            return reel.GetTopRowVal() == startingSymbol;
        }
        else if (rp == ReelPosition.Bot)
        {
            return reel.GetBotRowVal() == startingSymbol;
        }
        else if (rp == ReelPosition.Mid)
        {
            return reel.GetMidRowVal() == startingSymbol;
        }
        else return false;

    }
}
