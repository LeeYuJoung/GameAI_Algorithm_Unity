using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum STATE
    {
        FAILED,   // ����
        RUN,      // ���� �� 
        SUCCESS   // ����
    }

    // �������� �������� �Ǵ��ϴ� �Լ�
    public STATE Evaluate();
}
