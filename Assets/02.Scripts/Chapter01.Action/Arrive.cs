using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// �������� �����ϰų� ���� �������κ��� ����� �־����ٴ� ������ �����Ǹ� Agent �ڵ����� ����
public class Arrive : AgentBehaviour
{
    public float targetRadius;
    public float slowRadius;
    public float timeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        return base.GetSteering();
    }
}
