using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 실제로 어떤 행동을 수행하는 노드, 무조건 true 반환 (leaf Node)
public class ActionNode : INode
{
    // 반환형이 INode.STATE인 대리자
    public Func<INode.STATE> action = null;

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
