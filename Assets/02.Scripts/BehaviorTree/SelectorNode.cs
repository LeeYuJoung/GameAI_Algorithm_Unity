using System.Collections.Generic;

// 여러 행동 중 하나만 실행해야 할 때 사용 (or 연산자)
// 자식 노드들을 왼쪽 부터 오른쪽으로 차례로 결과들을 평가했을 때 하나라도 성공 상태가 있다면 그 Selector Node는 성공으로 간주하고 정지
public class SelectorNode : INode
{

    // 여러 노드를 가질 수 있도록 리스트 생서
    List<INode> childrens;

    public SelectorNode()
    {
        childrens = new List<INode>();
    }

    // Selector에 자식 노드를 추가하는 함수
    public void Add(INode node)
    {
        childrens.Add(node);
    }

    // 자식 노드 중에서 처음으로 Success나 Run 상태를 가진 노드가 발생하면 노드 진행 멈춤
    public INode.STATE Evaluate()
    {
        if(childrens == null)
        {
            return INode.STATE.FAILED;
        }

        // 리스트 내의 노드들을 왼쪽부터(넣은 순으로) 검사
        foreach(INode child in childrens)
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
