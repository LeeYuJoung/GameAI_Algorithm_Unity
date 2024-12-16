using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum STATE
    {
        RUN,       // 실행 중 
        SUCCESS,   // 성공
        FAILED     // 실패
    }

    // 판단하여 상태 전달
    public INode.STATE Evaluate();
}
