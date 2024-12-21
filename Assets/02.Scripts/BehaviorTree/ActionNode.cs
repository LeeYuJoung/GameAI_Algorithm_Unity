using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ � �ൿ�� �����ϴ� ���, ������ true ��ȯ (leaf Node)
public class ActionNode : INode
{
    // ��ȯ���� INode.STATE�� �븮��
    public Func<INode.STATE> action = null;

    // ��带 ������ �� �Ű������� �븮�ڸ� ����
    public ActionNode(Func<INode.STATE> action)
    {
        this.action = action;
    }

    public INode.STATE Evaluate()
    {
        // �븮�ڰ� null�� �ƴ� �� ȣ��, null�� ��� FAILED ��ȯ
        return action?.Invoke() ?? INode.STATE.FAILED;
    }
}
