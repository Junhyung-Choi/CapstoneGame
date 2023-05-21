using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAction : Action
{
    /// <summary>
    /// 1회의 동작을 검사하는 함수.
    /// base.CheckRep() 에선 동작을 본인의 Set에 보낸다.
    /// </summary>
    public override void CheckRep()
    {
        if(isStarted)
        {
            // Write Something
            DoRep();
        }
    }

    /// <summary>
    /// 동작이 끝난 후 변수들을 초기화 하는 함수.
    /// base.InitRep() 에선 isStarted = false로 바뀌어 추가 세트가 바로 시작되지 않게 한다.
    /// </summary>
    public override void InitRep()
    {
        base.InitRep();
    }

    /// <summary>
    /// 동작이 시작될때 isStarted를 true로 바꾸는 함수.
    /// base.StartRep() 에선 isStarted = true로 바꾸어 세트를 시작할 수 있도록 만든다.
    /// </summary>
    public override void StartRep()
    {
        base.StartRep();
    }
}
