using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum STATE
    {
        RUN,       // ���� �� 
        SUCCESS,   // ����
        FAILED     // ����
    }

    // �Ǵ��Ͽ� ���� ����
    public INode.STATE Evaluate();
}
