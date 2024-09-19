using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] Slider[] sliders;

    public event Action TimerFinish;

    public void StartTimer()
    {

    }

    IEnumerator TimerRoutine()
    {

        yield return null;
    }


    public void Damaged()
    {
        
    }

    public void Heal()
    {

    }

}
