using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance()
    {
        return instance;
    }

    public Text turnText;
    public Text pointText;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    void Start()
    {
        
    }

    public void TextUpdate(int _turn, int _point)
    {
        turnText.text = string.Format("현재턴 : {0:00}", _turn);
        pointText.text = string.Format("현재점수 : {0:00}", _point);
    }
}