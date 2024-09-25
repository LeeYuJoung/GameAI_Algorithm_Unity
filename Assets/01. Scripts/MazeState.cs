using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chaper03. 숫자 모으기 미로 구현
public class MazeState : MonoBehaviour
{
    public struct Coord
    {
        public int x;
        public int y;
    }
    public Coord character = new Coord();

    // 오른쪽, 왼쪽, 아래쪽, 위쪽으로 이동하는 이동방향 x와 y축 값
    public int[] dx = new int[4] {1, -1, 0, 0};    
    public int[] dy = new int[4] {0, 0, 1, -1};

    public const int H = 3;        // 미로의 높이
    public const int W = 4;        // 미로의 너비
    public const int END_TURN = 4; // 게임 종료의 턴

    private int[,] points = new int[H, W];   // 바닥의 점수는 1~9 중 하나
    private int turn = 0;          // 현재 턴
    public int gameScore = 0;      // 게임에서 획득한 점수

    void Start()
    {
        Play();
    }

    void Update()
    {
        
    }

    // h * w 크기의 미로를 생성
    public void CreateMaze(int seed)
    {
        // 게임판 구성용 난수 생성
        character.x = Random.Range(0, seed) % W;
        character.y = Random.Range(0, seed) % H;

        for(int y = 0; y < H; y++)
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

    // 현재 게임 상황을 문자열로 표현
    public string MazeToString()
    {
        string s = string.Empty;

        return s;
    }

    // 무작위로 행동을 결정하는 AI 구현
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