using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 자식 노드들이 실패하지 않아야 성공으로 간주
public class SequenceNode : MonoBehaviour
{
    List<INode> children; // 자식 노드들을 담을 수 있는 리스트

    public SequenceNode() { children = new List<INode>(); }

    public void Add(INode node) { children.Add(node); }

    public INode.STATE Evaluate()
    {
        // 자식 노드의 수가 0 이하라면 실패
        if (children.Count <= 0)
            return INode.STATE.FAILED;

        foreach (INode child in children)
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
