using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSequences : MonoBehaviour
{
   
    /// <summary>
    /// 화면이 깨지는 연출
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithBreakScreen()
    {
        yield return null;
    }


    /// <summary>
    /// 화면이 흐려지는(뿌옇게) 연출
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithSightBlur()
    {
        yield return null;
    }

    [SerializeField] Material noiseMat;

    /// <summary>
    /// 화면에  지지직 노이즈 끼는 연출
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithScreenNoise(List<Panel> panels)
    {

        foreach (var panel in panels)
        {
            panel.SetPanel(noiseMat);
        }

        yield return null;
    }
}
