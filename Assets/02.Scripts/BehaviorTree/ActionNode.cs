using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리 외부의 함수를 매개변수로 받아 대신 호출
public class ActionNode : INode
{
    // 반환옇이 INode.STATE인 대리자
    public Func<INode.STATE> action;

    // 노드를 생성할 때 매개변수로 대리자를 받음
    public ActionNode(Func<INode.STATE> action)
    {
        this.action = action;
    }

    public INode.STATE Evaluate()
    {
        // 대리자가 null이 아닐 때 호출, null인 경우 FAILED 반환
        return action?.Invoke() ?? INode.STATE.FAILED;
    }
}
