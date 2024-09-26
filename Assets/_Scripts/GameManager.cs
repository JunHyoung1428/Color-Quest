using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] GraphicRaycaster panelsRaycaster;
    [SerializeField] GridLayoutGroup grid; // girdLayout 
    [SerializeField] Panel panel; // Panel Prefab;
    [SerializeField] List<Panel> panels = new List<Panel>();

    [SerializeField] GameTimer timer;
    [SerializeField] GameObject gameOverText; //임시

    [SerializeField] GameOverSequences gameOverSequences;

    [Space(5),Header("Effects")]
    [SerializeField] Volume postProcessing;
    Vignette vignette;

    [SerializeField] CinemachineImpulseSource impulseSource;

    float increaseTime = 1f;
    float decreaseTime = 1f;
    int answerIndex = 0;

    int gameStage=1;
    public int gameLevel = 0;

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
        foreach(var panel in panels)
        {
            panel.OnPanelClicked += OnPanelClicked;
        }
    }

    private void Start()
    {
        postProcessing.profile.TryGet(out vignette);
        InitPanels();
    }

    /**********************************************
    *                   Methods
    ***********************************************/


    void InitPanels()
    {
        if (gameLevel != 0)
            return;
        StartCoroutine(ActivePanelSequence());
    }


    [ContextMenu("NextLevel")]
    void NextLevel()
    {
        ++gameLevel;

        //if(gameLevel==2)
        if(gameLevel %10 == 0 && gameStage <3)
            GrowGrid();
        

        answerIndex = Random.Range(0, panels.Count);
        Color newColor = Random.ColorHSV(0, 1, 0.5f, 1f, 0.8f, 1f);
        Color diffColor = GenerateDifferentColor(newColor, 0.1f);
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == answerIndex)
            {
                panels[i].SetPanel(diffColor,true);
            }
            else
            {
                panels[i].SetPanel(newColor);
            }
        }
    }

    /// <summary>
    /// Adding more panel to Gird
    /// </summary>
    [ContextMenu("NextStage")]
    void GrowGrid()
    {
        ++gameStage;
        grid.constraintCount++;
        int iterate = (grid.constraintCount * grid.constraintCount) - panels.Count; 

        for(int i=0; i < iterate; i++)
        {
            Panel newPanel = Instantiate(panel, grid.transform);
            newPanel.OnPanelClicked += OnPanelClicked;
            panels.Add(newPanel);
        }

        grid.cellSize = grid.cellSize * 0.8f;
    }

    Color GenerateDifferentColor(Color color, float hueDiff)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);

        // Hue 값을 약간 변경 (범위를 벗어나지 않도록 1.0 이상인 경우에는 다시 0으로 돌아가게 함)
        float newHue = (h + hueDiff) % 1f;

        return Color.HSVToRGB(newHue, s, v);
    }

    /**********************************************
    *               Event Process
    ***********************************************/

    private void OnPanelClicked(bool isAnswer)
    {
        if (isAnswer)
        {
            Debug.Log("Correct!");
            timer.Increase(increaseTime);
            NextLevel();
        }
        else
        {
            StartCoroutine(WrongPanelClickRoutine());
            timer.Decrease(decreaseTime);
            Debug.Log("Wrong Panel Clicked");
        }
    }

    // 타이머 끝났을때, 게임오버 처리
    void OnTimerFinished()
    {
        panelsRaycaster.enabled = false;  
        gameOverText.SetActive(true);
        StartCoroutine(gameOverSequences.WithScreenNoise(panels));
    }


    /**********************************************
    *                Routines
    ***********************************************/

    IEnumerator ActivePanelSequence()
    {
        float terms = 0f;
        foreach (int i in panels.RandomIndex())
        {
            panels[i].SetPanel(Color.white);
            float term = Random.Range(0.02f, 0.1f);
            terms += term;
            yield return new WaitForSeconds(term);
        }

        yield return new WaitForSeconds(terms);
        timer.ResetTimer();
        timer.TimerFinish += OnTimerFinished;
        NextLevel();
    }

    //todo : 특수효과같은거 따로 처리하는 스크립트 만들어서 옮기자
    IEnumerator WrongPanelClickRoutine()
    {
        float durationUp = 0.50f;
        float durationDown = 0.25f;
        float maxIntensity = 0.3f;

        vignette.active = true;
        float elapsedTime = 0f;
        impulseSource.GenerateImpulseWithForce(5f);


        while (elapsedTime < durationUp)
        {
            elapsedTime += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(0f, maxIntensity, elapsedTime / durationUp);
            yield return null;
        }

        vignette.intensity.value = maxIntensity;

        elapsedTime = 0f;
        while (elapsedTime < durationDown)
        {
            elapsedTime += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(maxIntensity, 0f, elapsedTime / durationDown);
            yield return null;
        }

        vignette.intensity.value = 0f;
        vignette.active = false;
    }

}
