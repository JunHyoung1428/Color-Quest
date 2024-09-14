using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] List<Panel> panels = new List<Panel>();
    int panelCount = 0;
    int answerIndex = 0;

    [SerializeField] int gameLevel = 0;

    /**********************************************
    *                 Unity Events
    ***********************************************/

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void OnEnable()
    {
        panelCount = panels.Count;

        foreach(var panel in panels)
        {
            panel.OnPanelClicked = OnPanelClicked;
        }
    }

    /**********************************************
    *                   Methods
    ***********************************************/

    [ContextMenu("NextLevel")]
    void NextLevel()
    {
        ++gameLevel;
        answerIndex = Random.Range(0, panelCount);
        Color newColor = Random.ColorHSV();
        Color wrongColor = GenerateDifferentColor(newColor, 0.1f);
        for (int i = 0; i < panelCount; i++)
        {
            if (i != answerIndex)
            {
                panels[i].NextStage(newColor);
            }
            else
            {
                panels[i].NextStage(wrongColor,true);
            }

        }
    }

    Color GenerateDifferentColor(Color color, float hueDiff)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);

        // Hue 값을 약간 변경 (범위를 벗어나지 않도록 1.0 이상인 경우에는 다시 0으로 돌아가게 함)
        float newHue = (h + hueDiff) % 1f;

        return Color.HSVToRGB(newHue, s, v);
    }

    private void OnPanelClicked(bool isAnswer)
    {
        if (isAnswer)
        {
            Debug.Log("Correct!");
            NextLevel();
        }
        else
        {
            Debug.Log("Wrong Panel Clicked");
        }
    }


    [ContextMenu("Transition10")]
    void TransitionTest()
    {
        StartCoroutine(TestStartTransition());
    }


    IEnumerator TestStartTransition()
    {
        for (int i = 0; i < 10; i++)
        {
            Color newColor = Random.ColorHSV();
            foreach (Panel panel in panels){

                panel.NextStage(newColor);
            }
            yield return new WaitForSeconds(1f);
        }
    }


}
