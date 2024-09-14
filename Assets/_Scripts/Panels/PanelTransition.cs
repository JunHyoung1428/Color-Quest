using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransition : MonoBehaviour
{
    [SerializeField] Image img;
    Material mat; 

    [Space(10), SerializeField] Color spreadColor;
    [SerializeField] Color basicColor;

    private float transitionProgress = 0f;
    public float transitionSpeed = 1f;
    
   
    bool isTransitioning = false;

    readonly string _SpreadColor = "_SpreadColor";
    readonly string _BasicColor = "_BasicColor";
    readonly string _TransitionProgress = "_TransitionProgress";

    /**********************************************
     *                 Unity Events
     ***********************************************/

    private void Start()
    {
        mat = new Material(img.material); // 인스턴싱 안하면 게임뷰에서는 안보임...
        img.material = mat;
    }


    /**********************************************
    *                 Methods
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
            StartTransition();
            yield return new WaitForSeconds(1f);
        }
    }

    [ContextMenu("Transition")]
    public void StartTransition()
    {
        SetRandomColor();
        if (!isTransitioning)
        {
            StartCoroutine(TransitionCoroutine());
        }
    }

    void SetRandomColor()
    {
        spreadColor = basicColor;
        basicColor = Random.ColorHSV();
    }

    IEnumerator TransitionCoroutine()
    {
        mat.SetColor(_SpreadColor, spreadColor);
        mat.SetColor(_BasicColor, basicColor);

        isTransitioning = true;
        transitionProgress = 0f;

        while (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;
            mat.SetFloat(_TransitionProgress, transitionProgress);
            yield return null;
        }

        mat.SetFloat(_TransitionProgress, 1f);
        isTransitioning = false;
    }

}
