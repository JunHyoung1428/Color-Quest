using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransition : MonoBehaviour
{
    [SerializeField] Image img;
    Material mat; 

    [Space(10), SerializeField] Color basicColor;
    [SerializeField] Color spreadColor;

    private float transitionProgress = 0f;
    public float transitionSpeed = 1f;
    
   
    public bool isTransitioning = false;

    readonly string _SpreadColor = "_SpreadColor";
    readonly string _BasicColor = "_BasicColor";
    readonly string _TransitionProgress = "_TransitionProgress";

    /**********************************************
     *                 Unity Events
     ***********************************************/

    private void Awake()
    {
        mat = new Material(img.material); // 인스턴싱 안하면 게임뷰에서는 안보임...
        img.material = mat;
    }


    /**********************************************
    *                Test Methods
    ***********************************************/

    [ContextMenu("Transition10")]
    void TransitionTest()
    {
        StartCoroutine(TestStartTransition());
    }


    IEnumerator TestStartTransition()
    {
        for (int i = 0; i < 10; i++)
        {
            StartTransition(Random.ColorHSV());
            yield return new WaitForSeconds(1f);
        }
    }


    /**********************************************
    *                Methods
    ***********************************************/

    [ContextMenu("Transition")]
    public void StartTransition(Color color)
    {
        basicColor = spreadColor;
        spreadColor = color;

        if (!isTransitioning)
        {
            StartCoroutine(TransitionCoroutine());
        }
    }

    public void ReverseTransition(Color color)
    {
        basicColor = spreadColor;
        spreadColor = color;

        if (!isTransitioning)
        {
            StartCoroutine(ReverseTransition());
        }
    }

    IEnumerator TransitionCoroutine()
    {
        mat.SetColor(_SpreadColor, spreadColor);
        mat.SetColor(_BasicColor, basicColor);

        isTransitioning = true;
        transitionProgress = 0f;

        while (transitionProgress < 0.5f)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;
            mat.SetFloat(_TransitionProgress, transitionProgress);
            yield return null;
        }

        mat.SetFloat(_TransitionProgress, 1f);
        isTransitioning = false;
    }


    IEnumerator ReverseTransition()
    {
        mat.SetColor(_SpreadColor, basicColor);
        mat.SetColor(_BasicColor, spreadColor);

        isTransitioning = true;
        transitionProgress = 0.5f;

        while (transitionProgress > 0f)
        {
            transitionProgress -= Time.deltaTime * transitionSpeed;
            mat.SetFloat(_TransitionProgress, transitionProgress);
            yield return null;
        }

        mat.SetFloat(_TransitionProgress, 0f);
        isTransitioning = false;
    }
}
