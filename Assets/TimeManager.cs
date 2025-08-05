using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{// Inspectorから設定するテキストUI
    public TextMeshProUGUI timeText;

    public float elapsedTime; // 経過時間を記録する変数

    void Update()
    {
        // 毎フレームの時間を加算していく
        elapsedTime += Time.deltaTime;

        // 経過時間を分と秒に変換
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // テキストUIに表示する文字列を整形 (例: "02:05")
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
