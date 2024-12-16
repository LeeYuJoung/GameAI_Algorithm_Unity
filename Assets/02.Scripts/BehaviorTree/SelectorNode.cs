using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڽ� ������ ���� ���θ� �˻��Ͽ� �ϳ��� �����ϸ� ���� ��ȯ
public class SelectorNode : INode
{
    // ���� ��带 ���� �� �ֵ��� ����Ʈ ����
    List<INode> children;

    public SelectorNode()
    {
        children = new List<INode>();
    }

    // Selector�� �ڽ� ��带 �߰��ϴ� �Լ�
    public void Add(INode node)
    {
        children.Add(node);
    }

    public INode.STATE Evaluate()
    {
        // ����Ʈ ���� ������ ���ʺ���(���� ������) �˻�
        foreach(INode child in children)
        {
            INode.STATE state = child.Evaluate();
            // child ����� State�� �ϳ��� SUCCESS�̸� ���� ��ȯ
            // ���� ���� ��� RUN ��ȯ
            switch (state)
            {
                case INode.STATE.SUCCESS:
                    return INode.STATE.SUCCESS;
                case INode.STATE.RUN:
                    return INode.STATE.RUN;
            }
        }

        // �ݺ����� �����ٸ� �ش� Selector�� �ڽ� ������ ���� FAILED �����̹Ƿ� Selector�� FAILED �ݷ�
        return INode.STATE.FAILED;
    }
}
