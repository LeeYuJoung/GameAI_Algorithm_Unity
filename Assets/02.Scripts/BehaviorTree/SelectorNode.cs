using System.Collections.Generic;

// ���� �ൿ �� �ϳ��� �����ؾ� �� �� ��� (or ������)
// �ڽ� ������ ���� ���� ���������� ���ʷ� ������� ������ �� �ϳ��� ���� ���°� �ִٸ� �� Selector Node�� �������� �����ϰ� ����
public class SelectorNode : INode
{

    // ���� ��带 ���� �� �ֵ��� ����Ʈ ����
    List<INode> childrens;

    public SelectorNode()
    {
        childrens = new List<INode>();
    }

    // Selector�� �ڽ� ��带 �߰��ϴ� �Լ�
    public void Add(INode node)
    {
        childrens.Add(node);
    }

    // �ڽ� ��� �߿��� ó������ Success�� Run ���¸� ���� ��尡 �߻��ϸ� ��� ���� ����
    public INode.STATE Evaluate()
    {
        if(childrens == null)
        {
            return INode.STATE.FAILED;
        }

        // ����Ʈ ���� ������ ���ʺ���(���� ������) �˻�
        foreach(INode child in childrens)
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
