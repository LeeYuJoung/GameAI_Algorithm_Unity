using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지능형 움직임을 생성할 뿐만 아니라 지능형 동작을 변경하고 추가하는 모듈식 시스템
// 에이전트의 이동 및 회전을 저장하기 위한 사용자 정의 데이터 (조종)
public class Steering
{
    public float angular;
    public Vector3 linear;

    public Steering()
    {
        angular = 0.0f;
        linear = new Vector3();
    }
}
