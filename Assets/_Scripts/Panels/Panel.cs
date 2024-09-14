using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Panel : MonoBehaviour, IPointerClickHandler
{
    public bool isAnswer;

    [SerializeField] PanelTransition transition;

    public Action<bool> OnPanelCliked;

    /**********************************************
    *                 Unity Events
    ***********************************************/


    /**********************************************
    *                Methods
    ***********************************************/

    public void NextStage(Color color, bool isAnswer=false)
    {
        this.isAnswer = isAnswer;

        transition.StartTransition(color);
    }


    /**********************************************
    *                 IPointer Events
    ***********************************************/

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnPanelCliked != null)
        {
            OnPanelCliked(isAnswer);
        }
    }





}
