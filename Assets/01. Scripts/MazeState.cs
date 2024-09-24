using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chaper03. 숫자 모으기 미로 구현
public class MazeState : MonoBehaviour
{
    public struct Coord
    {
        int _x;
        int _y;
    }
    public Coord character = new Coord();

    public const int H = 3;        // 미로의 높이
    public const int W = 4;        // 미로의 너비
    public const int END_TURN = 4; // 게임 종료의 턴

    private int[,] points = new int[H, W];   // 바닥의 점수는 1~9 중 하나
    private int turn = 0;          // 현재 턴
    public int gameScore = 0;      // 게임에서 획득한 점수

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // h*w 크기의 미로를 생성
    public void CreateMaze(int seed)
    {
        // 게임판 구성용 난수 생성

    }
}
