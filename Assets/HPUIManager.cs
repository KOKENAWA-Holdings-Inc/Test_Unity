using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    [SerializeField] private Slider HPslider;
    private Player targetPlayer; // 参照するPlayerスクリプト

    void Update()
    {
        // ターゲットのプレイヤーがいない場合のみ、シーンから探す
        if (targetPlayer == null)
        {
            // "Player"タグの付いたオブジェクトを探し、そのオブジェクトからPlayerスクリプトを取得
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<Player>();
            }
        }

        // ターゲットのプレイヤーが見つからない場合はスライダーを非表示
        if (targetPlayer == null)
        {
            HPslider.gameObject.SetActive(false);
            return;
        }

        // プレイヤーが見つかっていたら、スライダーをアクティブにしてUIを更新
        HPslider.gameObject.SetActive(true);
        UpdateHPUI();
    }

    // UIを更新するメソッド
    public void UpdateHPUI()
    {
        // targetPlayerの現在値を使ってUIを更新
        HPslider.maxValue = targetPlayer.PlayerMAXHP;
        HPslider.value = targetPlayer.PlayerHP;
    }
}