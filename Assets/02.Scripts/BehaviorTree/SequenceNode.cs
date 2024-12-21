using System.Collections.Generic;
using UnityEngine;

// ���� �ൿ�� ������� �����ؾ� �� �� ��� (and ������)
// ��� �ڽ� ������ �������� �ʾƾ� �������� ���� (������ ��尡 ���� �� ���� ����)
public class SequenceNode : INode
{
    List<INode> childrens; // �ڽ� ������ ���� �� �ִ� ����Ʈ

    public SequenceNode()
    { 
        childrens = new List<INode>(); 
    }

    public void Add(INode node) 
    { 
        childrens.Add(node); 
    }

    // �ڽ� ��带 ���ʿ��� ���������� �����ϸ鼭 Failed ���°� ���� ������ ����
    public INode.STATE Evaluate()
    {
        // �ڽ� ����� ���� 0 ���϶�� ����
        if (childrens == null || childrens.Count <= 0)
            return INode.STATE.FAILED;

        foreach (INode child in childrens)
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