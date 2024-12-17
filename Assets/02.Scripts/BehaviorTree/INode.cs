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

    // Node ���� ��ȯ
    public STATE Evaluate();
}
