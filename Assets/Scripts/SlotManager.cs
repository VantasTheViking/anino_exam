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
        public List<ReelPosition> reelPatern;
    }
    [SerializeField] List<ReelPositionSerializable> reelPatterns;

    [System.Serializable]
    public class ComboSerializable
    {
        public List<int> comboPayouts;
    }
    [SerializeField] List<int> symbols;
    [SerializeField] List<ComboSerializable> payouts;

    int totalWinnings;
    int comboCount;


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
        if (canSpin == false)
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
                //Add Win Money

                canSpin = true;
                spinButton.interactable = true;

            }
        }
    }
    
}
