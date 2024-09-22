using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float totalTime = 10f; //Total Game Play Time
    float remainTime;
    float inverseTotalTime; // (1/totalTime)

    [SerializeField] Slider[] sliders;


    public event Action TimerFinish;

    /**********************************************
    *                 Unity Events
    ***********************************************/

    private void Update()
    {
        if(remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            TimerFinish?.Invoke();
            foreach(var timer in sliders)
            {
                timer.gameObject.SetActive(false);
            }
            TimerFinish = null;
        }
    }

    /**********************************************
    *                   Methods
    ***********************************************/

    // Called From GameManager's ActivePanelSequence
    public void ResetTimer()
    {
        remainTime = totalTime;
        inverseTotalTime = 1 / totalTime;

        foreach(var timer in sliders)
        {
            timer.gameObject.SetActive(true);
            timer.value = 1.0f;
        }
    }

    void UpdateTimer()
    {
        float sliderValue = remainTime * inverseTotalTime;

        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = sliderValue;
        }
    }

   
    public void Decrease(float val)
    {
        remainTime -= val;
        UpdateTimer();
    }

    public void Increase(float val)
    {
        remainTime += val;
        UpdateTimer();
    }

}
