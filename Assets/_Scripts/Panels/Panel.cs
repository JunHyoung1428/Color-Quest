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

    public Action<bool> OnPanelClicked;

    /**********************************************
    *                 Unity Events
    ***********************************************/


    /**********************************************
    *                Methods
    ***********************************************/

    public void SetPanel(Color color, bool isAnswer=false)
    {
        this.isAnswer = isAnswer;

#if UNITY_EDITOR
        if (isAnswer)
            gameObject.name = "Answer";
        else
            gameObject.name = "Panel";
#endif
        transition.StartTransition(color);
    }


    /**********************************************
    *                 IPointer Events
    ***********************************************/

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnPanelClicked != null)
        {
            OnPanelClicked(isAnswer);
        }
    }





}
