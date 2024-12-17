using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum STATE
    {
        RUN,       // 동작 중 
        SUCCESS,   // 성공
        FAILED     // 실패
    }

    // Node 상태 반환
    public STATE Evaluate();
}
