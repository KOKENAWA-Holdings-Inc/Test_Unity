using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPlayerLvUI : MonoBehaviour
{
    public TextMeshProUGUI ResultLvText; // ★SerializeFieldにしてインスペクターから設定
    private Player targetPlayer; // 参照するPlayerスクリプト

    // 現在表示しているレベルを記録しておく変数
    private int currentDisplayedLv = -1;


    void Start()
    {
        ResultLvText.enabled = false;
    }
    void Update()
    {
        // ターゲットのプレイヤーをまだ見つけていない場合、探す
        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<Player>();
            }
        }

        // ターゲットのプレイヤーが見つからない場合はUIを非表示にして処理を終える
        if (targetPlayer == null)
        {
            ResultLvText.gameObject.SetActive(false);
            return;
        }

        // --- プレイヤーが見つかった場合の処理 ---

        // UIを確実に表示状態にする
        ResultLvText.gameObject.SetActive(true);

        // ★プレイヤーのレベルが、現在表示しているレベルと異なる場合のみテキストを更新
        if (targetPlayer.PlayerLv != currentDisplayedLv)
        {
            UpdateLvText();
        }
    }

    // テキストを更新する処理
    private void UpdateLvText()
    {
        // テキストをプレイヤーの現在のレベルで更新
        ResultLvText.text = "Final Lv:" + targetPlayer.PlayerLv;

        // 現在表示しているレベルの値を更新
        currentDisplayedLv = (int)targetPlayer.PlayerLv;
    }
}
