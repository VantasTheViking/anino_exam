using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ReelBehaviour : MonoBehaviour
{
    [SerializeField] List<Sprite> rowSprites;
    [SerializeField] List<Image> rowImages;
    [SerializeField] List<int> rowValues;
    
    int spinSpeed;
    float spinTime;

    public enum State { spinning, stopping, stopped };
    State currentState;

    int currentMidIndex = 0;


    float targetY = 0;
    float startY;
    float interpolationVal;
    [SerializeField] int stopSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
        UpdateRows();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.spinning)
        {
            SpinReel();
        } if (currentState == State.stopping)
        {
            StoppingReel();
        }
        
    }

    public void StartSpin()
    {
        currentState = State.spinning;
        spinSpeed = Random.Range(450, 810);
        spinTime = Random.Range(3.0f, 6.0f);
        StartCoroutine(SpinTimer(spinTime));

    }

    IEnumerator SpinTimer(float time)
    {
        yield return new WaitForSeconds(time);
        currentState = State.stopping;
        startY = gameObject.transform.localPosition.y;
    }

    public void SpinReel()
    {
        //Spin Downwards
        gameObject.transform.localPosition -= new Vector3(0, spinSpeed * Time.deltaTime, 0);

        //Reset position when off row is at original top row position then update
        if (gameObject.transform.localPosition.y <= -90)
        {
            gameObject.transform.localPosition += new Vector3(0, 90, 0);

            currentMidIndex++;

            if (currentMidIndex == rowValues.Count)
            {
                currentMidIndex = 0;
            }

            UpdateRows();
        }
        
    }

    public void StoppingReel()
    {
        transform.localPosition = new Vector3(0, Mathf.Lerp(startY, targetY, interpolationVal), 0);

        interpolationVal += stopSpeed * Time.deltaTime;

        if (interpolationVal >= 1.0f)
        {

            interpolationVal = 0.0f;

            currentState = State.stopped;

        }


    }

    public int GetBotRowVal()
    {
        if (currentMidIndex != 0)
        {
            return rowValues[currentMidIndex - 1];

        }
        else return rowValues.Last();
    }

    public int GetMidRowVal()
    {
        return rowValues[currentMidIndex];
    }

    public int GetTopRowVal()
    {
        if (currentMidIndex != rowValues.Count - 1)
        {
            return rowValues[currentMidIndex + 1];

        }
        else return rowValues.First();
    }

    public int GetOffRowVal()
    {
        if (currentMidIndex == rowValues.Count - 2)
        {

            return rowValues.First(); 

        }
        else if (currentMidIndex == rowValues.Count - 1)
        {

            return rowValues[1];

        }
        else return rowValues[currentMidIndex + 2];
    }

    //Change row sprites according to current mid row
    public void UpdateRows()
    {

        //Bottom Row

        rowImages[0].sprite = rowSprites[GetBotRowVal() - 1];

        //Middle Row

        rowImages[1].sprite = rowSprites[GetMidRowVal() - 1];

        //Top Row

        rowImages[2].sprite = rowSprites[GetTopRowVal() - 1];

        //Off Screen Row

        rowImages[3].sprite = rowSprites[GetOffRowVal() - 1];

        
    }

    public State GetState()
    {
        return currentState;
    }


}
