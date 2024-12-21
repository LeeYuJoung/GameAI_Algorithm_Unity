using System.Collections.Generic;
using UnityEngine;

// 여러 행동을 순서대로 진행해야 할 때 사용 (and 연산자)
// 모든 자식 노드들이 실패하지 않아야 성공으로 간주 (실패한 노드가 있을 때 까지 진행)
public class SequenceNode : INode
{
    List<INode> childrens; // 자식 노드들을 담을 수 있는 리스트

    public SequenceNode()
    { 
        childrens = new List<INode>(); 
    }

    public void Add(INode node) 
    { 
        childrens.Add(node); 
    }

    // 자식 노드를 왼쪽에서 오른쪽으로 진행하면서 Failed 상태가 나올 때까지 진행
    public INode.STATE Evaluate()
    {
        // 자식 노드의 수가 0 이하라면 실패
        if (childrens == null || childrens.Count <= 0)
            return INode.STATE.FAILED;

        foreach (INode child in childrens)
        {
            // 자식 노드들중 하나라도 FAILED 라면 시퀀스는 FAILED
            switch (child.Evaluate())
            {
                case INode.STATE.RUN:
                    return INode.STATE.RUN;
                // SUCCESS 이면 아래는 검사하지 않고 continue 키워드로 다시 반복문 호출
                case INode.STATE.SUCCESS:
                    continue;
                case INode.STATE.FAILED:
                    return INode.STATE.FAILED;
            }
        }
        // FAILED 에 걸리지 않고 반복문을 빠져나왔으므로 시퀀스는 SUCCESS
        return INode.STATE.SUCCESS;
    }
}