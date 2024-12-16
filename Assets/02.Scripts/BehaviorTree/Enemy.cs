using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int detectiveRange;
    public int attackRange;

    SelectorNode rootNode;
    SequenceNode attackSequence;
    SequenceNode detectiveSequence;
    ActionNode idleAction;
    ActionNode returnAction;
    Transform target;

    Vector3 originPos;

    void Start()
    {
        originPos = transform.position;

        attackSequence = new SequenceNode();
        attackSequence.Add(new ActionNode(CheckInAttackRange));
        attackSequence.Add(new ActionNode(Attack));

        detectiveSequence = new SequenceNode(); // 탐지 시퀀스
        detectiveSequence.Add(new ActionNode(CheckInDetectiveRange)); // 탐지 체크 액션
        detectiveSequence.Add(new ActionNode(TraceTarget)); // 쫓기 액션

        returnAction = new ActionNode(ReturnAction); // 귀환 액션 노드
        idleAction = new ActionNode(IdleAction); // 대기 액션노드

        rootNode = new SelectorNode();
        //rootNode.Add(attackSequence);
        //rootNode.Add(detectiveSequence);
        rootNode.Add(returnAction);
        rootNode.Add(idleAction);
    }

    INode.STATE Attack()
    {
        Debug.Log("공격중");
        return INode.STATE.RUN;
    }

    INode.STATE CheckInAttackRange()
    {
        if (target == null)
        {
            return INode.STATE.FAILED;
        }
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            Debug.Log("공격 범위 감지 됨");
            return INode.STATE.SUCCESS;
        }

        return INode.STATE.FAILED;
    }

    INode.STATE TraceTarget()
    {
        if (Vector3.Distance(transform.position, target.position) >= 0.1f)
        {
            Debug.Log("Trace!!");
            transform.forward = (target.position - transform.position).normalized;
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
            return INode.STATE.RUN;
        }
        else
        {
            return INode.STATE.FAILED;
        }
    }

    INode.STATE IdleAction()
    {
        Debug.Log("Idle..");
        return INode.STATE.RUN;
    }

    INode.STATE ReturnAction()
    {
        if (Vector3.Distance(transform.position, originPos) >= 0.1f)
        {
            Debug.Log("Return..");
            transform.forward = (originPos - transform.position).normalized;
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
            return INode.STATE.RUN;
        }
        else
        {
            return INode.STATE.FAILED;
        }
    }

    INode.STATE CheckInDetectiveRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectiveRange, 1 << 8);
        if (cols.Length > 0)
        {
            Debug.Log("Detective..");
            target = cols[0].transform;
            return INode.STATE.SUCCESS;
        }

        return INode.STATE.FAILED;
    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Evaluate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectiveRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
