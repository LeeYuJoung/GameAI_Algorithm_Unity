using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �ڽ� ������ �������� �ʾƾ� �������� ����
public class SequenceNode : MonoBehaviour
{
    List<INode> children; // �ڽ� ������ ���� �� �ִ� ����Ʈ

    public SequenceNode() { children = new List<INode>(); }

    public void Add(INode node) { children.Add(node); }

    public INode.STATE Evaluate()
    {
        // �ڽ� ����� ���� 0 ���϶�� ����
        if (children.Count <= 0)
            return INode.STATE.FAILED;

        foreach (INode child in children)
        {
            // �ڽ� ������ �ϳ��� FAILED ��� �������� FAILED
            switch (child.Evaluate())
            {
                case INode.STATE.RUN:
                    return INode.STATE.RUN;
                // SUCCESS �̸� �Ʒ��� �˻����� �ʰ� continue Ű����� �ٽ� �ݺ��� ȣ��
                case INode.STATE.SUCCESS:
                    continue;
                case INode.STATE.FAILED:
                    return INode.STATE.FAILED;
            }
        }
        // FAILED �� �ɸ��� �ʰ� �ݺ����� �����������Ƿ� �������� SUCCESS
        return INode.STATE.SUCCESS;
    }
}
