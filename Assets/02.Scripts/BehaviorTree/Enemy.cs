using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SelectorNode rootNode;           // 루트 노드는 셀렉터 노드 (적의 행동 양식)
    SelectorNode attackNode;         // 공격 방식 셀렉터 노드
    SelectorNode targetSettingNode;  // 타겟 설정 셀렉터 노드
    SequenceNode attackSequence;     // 공격 시퀀스 노드
    SequenceNode detectiveSequence;  // 탐지 시퀀스 노드
    ActionNode idleAction;           // 대기 액션 노드
    ActionNode returnAction;         // 귀환 노드

    Transform target;          // 타겟 변수
    public int detectiveRange; // 탐지 범위 변수
    public int attackRange;    // 공격 범위 변수
    Vector3 originPos;         // 초기 위치를 저장하는 변수

    void Start()
    {
        originPos = transform.position;

        attackSequence = new SequenceNode();                      // 공격 시퀸스 노드 생선
        attackSequence.Add(new ActionNode(CheckInAttackRange));   // 공격 범위 체크 액션 노드에 함수 할당
        attackSequence.Add(new ActionNode(Attack));               // 공격 액션 노드에 함수 할당

        detectiveSequence = new SequenceNode();                       // 탐지 시퀸스 노드 생성   
        detectiveSequence.Add(new ActionNode(CheckInDetectiveRange)); // 탐지 범위 체크 액션 함수 할당
        detectiveSequence.Add(new ActionNode(TraceTarget));           // 쫓는 액션 할당

        returnAction = new ActionNode(ReturnAction); // 귀환 액션 노드
        idleAction = new ActionNode(IdleAction);     // 대기 액션 노드 

        // 루트 노드 생성
        rootNode = new SelectorNode();
        rootNode.Add(attackSequence);     // 루트 노드의 자식으로 공격 시퀀스
        rootNode.Add(detectiveSequence);  // 탐지 시퀀스
        rootNode.Add(returnAction);       // 귀환 시퀀스
        rootNode.Add(idleAction);         // 대기 시퀀스를 가짐

        // 공격 방식 노드
        attackNode = new SelectorNode();
        attackNode.Add(new ActionNode(SkillAttack));  // 스킬 공격 액션 함수 할당
        attackNode.Add(new ActionNode(Attack));       // 일반 공격 액션 함수 할당

        // 타겟 설정 셀렉터 노드
        targetSettingNode = new SelectorNode();
        //targetSettingNode.Add(new ActionNode());

    }

    // 공격 액션에 할당될 함수
    INode.STATE Attack()
    {
        Debug.Log("공격중");
        return INode.STATE.RUN;
    }

    INode.STATE SkillAttack()
    {
        Debug.Log("스킬 공격");
        return INode.STATE.RUN;
    }

    // 공격 범위 체크 액션에 할당될 함수
    INode.STATE CheckInAttackRange()
    {
        if (target == null)
            return INode.STATE.FAILED;

        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            Debug.Log("공격 범위 감지 됨");
            return INode.STATE.SUCCESS;
        }

        return INode.STATE.FAILED;
    }

    // 추격 목표물 체크 함수
    INode.STATE TraceTarget()
    {
        if(Vector3.Distance(transform.position, target.position) >= 0.1f)
        {
            Debug.Log("Trace");
            transform.forward = (target.position - transform.position).normalized;
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
            return INode.STATE.RUN;
        }
        else
        {
            return INode.STATE.FAILED;
        }
    }
    
    // 대기 액션 노드
    INode.STATE IdleAction()
    {
        Debug.Log("Idle...");
        return INode.STATE.RUN;
    }

    // 귀환 함수
    INode.STATE ReturnAction()
    {
         if(Vector3.Distance(transform.position, originPos) >= 0.1f)
        {
            transform.forward = (originPos - transform.position).normalized;
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
            return INode.STATE.RUN;
        }
        else
        {
            return INode.STATE.FAILED;
        }
    }

    // 탐지 범위 체크 액션 함수
    INode.STATE CheckInDetectiveRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectiveRange, 1 << 8);

        if(cols.Length > 0)
        {
            Debug.Log("Detective...");
            target = cols[0].transform;
            return INode.STATE.SUCCESS;
        }

        return INode.STATE.FAILED;
    }

    // 업데이트 내에서는 루트 노드(행동 트리 전체)의 상태를 평가한다.
    void Update()
    {
        rootNode.Evaluate();
    }
}