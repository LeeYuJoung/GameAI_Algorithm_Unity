using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum STATE
    {
        FAILED,   // 실패
        RUN,      // 동작 중 
        SUCCESS   // 성공
    }

    // 성공인지 실패인지 판단하는 함수
    public STATE Evaluate();
}
