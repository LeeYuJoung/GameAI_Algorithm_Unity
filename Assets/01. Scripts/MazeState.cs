using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chaper03.01 숫자 모으기 미로 AI
// 1인 게임 규칙
// 1턴에 상하좌우 네 방향 중 하나로 1칸 이동
// 바닥에 있는 점수를 차지하면 자신의 점수가 되고, 바닥의 점수는 사라짐
// END_TURN 시점에 높은 점수를 얻는 것이 목적
public class MazeState : MonoBehaviour
{
    // 좌표 저장 구조체
    public struct Coord
    {
        public int x;
        public int y;
    }
    public Coord character = new Coord();

    // 오른쪽, 왼쪽, 아래쪽, 위쪽으로 이동하는 이동방향 x와 y축 값
    public int[] dx = new int[4] {1, -1, 0, 0};    
    public int[] dy = new int[4] {0, 0, 1, -1};

    public const int H = 3;        // 미로의 높이 (y축)
    public const int W = 4;        // 미로의 너비 (x축)
    public const int END_TURN = 4; // 게임 종료의 턴

    private int[,] points = new[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };  // 바닥의 점수는 1~9 중 하나
    private int turn = 0;          // 현재 턴
    public int gameScore = 0;      // 게임에서 획득한 점수

    void Start()
    {
        CreateMaze(10);
    }

    void Update()
    {
        Play();
    }

    // h * w 크기의 미로를 생성
    public void CreateMaze(int seed)
    {
        // 플레이어 랜덤 난수 생성 (3*4 게임판 위 랜덤한 위치에 플레이어 초기 설정)
        character.x = Random.Range(0, 4);
        character.y = Random.Range(0, 3);

        // 게임판 구성용 랜덤 난수 생성
        for (int y = 0; y < H; y++)
        {
            for(int x = 0; x < W; x++)
            {
                if(y == character.y && x == character.x)
                {
                    continue;
                }

                points[y, x] = Random.Range(0, seed) % 10;
            }
        }
    }

    // 지정한 action으로 게임을 1턴 진행
    public void Advance(int action)
    {
        character.x += dx[action];
        character.y += dy[action];
        int point = points[character.y, character.x];

        if(point > 0)
        {
            gameScore += point;
            point = 0;
        }

        turn++;
    }

    // 현재 상황에서 플레이어가 가능한 행동을 모두 획득
    public int LegalActions()
    {
        int actions = 0;

        for(int action = 0; action < 4; action ++)
        {
            int ty = character.y + dy[action];
            int tx = character.x + dx[action];

            if(ty >= 0 && ty< H && tx >= 0 && tx < W)
            {
                actions = action;
            }
        }

        return actions;
    }

    // 무작위로 행동을 결정하는 AI
    public int RandomActionAI()
    {
        // 행동 선택용 난수 생성기 초기화

        return LegalActions();
    }

    // 시드를 지정해서 게임 상황을 표시하면서 AI에 플레이
    public void Play()
    {
        while (!IsDone())
        {
            Advance(RandomActionAI());
        }
    }

    // 게임 종료
    public bool IsDone()
    {
        return turn == END_TURN;
    }
}