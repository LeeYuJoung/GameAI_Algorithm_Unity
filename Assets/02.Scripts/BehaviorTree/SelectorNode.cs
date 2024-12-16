using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자식 노드들의 성공 여부를 검사하여 하나라도 성공하면 성공 반환
public class SelectorNode : INode
{
    // 여러 노드를 가질 수 있도록 리스트 생서
    List<INode> children;

    public SelectorNode()
    {
        children = new List<INode>();
    }

    // Selector에 자식 노드를 추가하는 함수
    public void Add(INode node)
    {
        children.Add(node);
    }

    public INode.STATE Evaluate()
    {
        // 리스트 내의 노드들을 왼쪽부터(넣은 순으로) 검사
        foreach(INode child in children)
        {
            INode.STATE state = child.Evaluate();
            // child 노드의 State가 하나라도 SUCCESS이면 성공 반환
            // 실행 중인 경우 RUN 반환
            switch (state)
            {
                case INode.STATE.SUCCESS:
                    return INode.STATE.SUCCESS;
                case INode.STATE.RUN:
                    return INode.STATE.RUN;
            }
        }

        // 반복문이 끝났다면 해당 Selector의 자식 노드들은 전부 FAILED 상태이므로 Selector는 FAILED 반롼
        return INode.STATE.FAILED;
    }
}
