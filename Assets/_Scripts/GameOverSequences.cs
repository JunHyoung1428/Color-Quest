using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSequences : MonoBehaviour
{
   
    /// <summary>
    /// ȭ���� ������ ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithBreakScreen()
    {
        yield return null;
    }


    /// <summary>
    /// ȭ���� �������(�ѿ���) ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithSightBlur()
    {
        yield return null;
    }

    /// <summary>
    /// ȭ�鿡  ������ ������ ���� ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithScreenNoise()
    {
        yield return null;
    }
}
