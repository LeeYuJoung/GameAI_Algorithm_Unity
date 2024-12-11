using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 목적지에 도착하거나 위험 지점으로부터 충분히 멀어졌다는 조건이 충족되면 Agent 자동으로 중지
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
