using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SelectorNode rootNode;           // ��Ʈ ���� ������ ��� (���� �ൿ ���)
    SelectorNode attackNode;         // ���� ��� ������ ���
    SelectorNode targetSettingNode;  // Ÿ�� ���� ������ ���
    SequenceNode attackSequence;     // ���� ������ ���
    SequenceNode detectiveSequence;  // Ž�� ������ ���
    ActionNode idleAction;           // ��� �׼� ���
    ActionNode returnAction;         // ��ȯ ���

    Transform target;          // Ÿ�� ����
    public int detectiveRange; // Ž�� ���� ����
    public int attackRange;    // ���� ���� ����
    Vector3 originPos;         // �ʱ� ��ġ�� �����ϴ� ����

    void Start()
    {
        originPos = transform.position;

        attackSequence = new SequenceNode();                      // ���� ������ ��� ����
        attackSequence.Add(new ActionNode(CheckInAttackRange));   // ���� ���� üũ �׼� ��忡 �Լ� �Ҵ�
        attackSequence.Add(new ActionNode(Attack));               // ���� �׼� ��忡 �Լ� �Ҵ�

        detectiveSequence = new SequenceNode();                       // Ž�� ������ ��� ����   
        detectiveSequence.Add(new ActionNode(CheckInDetectiveRange)); // Ž�� ���� üũ �׼� �Լ� �Ҵ�
        detectiveSequence.Add(new ActionNode(TraceTarget));           // �Ѵ� �׼� �Ҵ�

        returnAction = new ActionNode(ReturnAction); // ��ȯ �׼� ���
        idleAction = new ActionNode(IdleAction);     // ��� �׼� ��� 

        // ��Ʈ ��� ����
        rootNode = new SelectorNode();
        rootNode.Add(attackSequence);     // ��Ʈ ����� �ڽ����� ���� ������
        rootNode.Add(detectiveSequence);  // Ž�� ������
        rootNode.Add(returnAction);       // ��ȯ ������
        rootNode.Add(idleAction);         // ��� �������� ����

        // ���� ��� ���
        attackNode = new SelectorNode();
        attackNode.Add(new ActionNode(SkillAttack));  // ��ų ���� �׼� �Լ� �Ҵ�
        attackNode.Add(new ActionNode(Attack));       // �Ϲ� ���� �׼� �Լ� �Ҵ�

        // Ÿ�� ���� ������ ���
        targetSettingNode = new SelectorNode();
        //targetSettingNode.Add(new ActionNode());

    }

    // ���� �׼ǿ� �Ҵ�� �Լ�
    INode.STATE Attack()
    {
        Debug.Log("������");
        return INode.STATE.RUN;
    }

    INode.STATE SkillAttack()
    {
        Debug.Log("��ų ����");
        return INode.STATE.RUN;
    }

    // ���� ���� üũ �׼ǿ� �Ҵ�� �Լ�
    INode.STATE CheckInAttackRange()
    {
        if (target == null)
            return INode.STATE.FAILED;

        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            Debug.Log("���� ���� ���� ��");
            return INode.STATE.SUCCESS;
        }

        return INode.STATE.FAILED;
    }

    // �߰� ��ǥ�� üũ �Լ�
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
    
    // ��� �׼� ���
    INode.STATE IdleAction()
    {
        Debug.Log("Idle...");
        return INode.STATE.RUN;
    }

    // ��ȯ �Լ�
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

    // Ž�� ���� üũ �׼� �Լ�
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

    // ������Ʈ �������� ��Ʈ ���(�ൿ Ʈ�� ��ü)�� ���¸� ���Ѵ�.
    void Update()
    {
        rootNode.Evaluate();
    }
}