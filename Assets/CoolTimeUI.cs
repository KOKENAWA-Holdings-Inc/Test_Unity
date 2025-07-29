using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    [SerializeField] private Slider CoolTimeslider;
    private PlayerLanceShooter targetPlayer; // 参照するPlayerスクリプト

    void Update()
    {
        // ターゲットのプレイヤーがいない場合のみ、シーンから探す
        if (targetPlayer == null)
        {
            // "Player"タグの付いたオブジェクトを探し、そのオブジェクトからPlayerスクリプトを取得
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<PlayerLanceShooter>();
            }
        }

        // ターゲットのプレイヤーが見つからない場合はスライダーを非表示
        if (targetPlayer == null)
        {
            CoolTimeslider.gameObject.SetActive(false);
            return;
        }

        // プレイヤーが見つかっていたら、スライダーをアクティブにしてUIを更新
        CoolTimeslider.gameObject.SetActive(true);
        UpdateCoolTimeUI();
    }

    // UIを更新するメソッド
    // CoolTimeUI.cs
    public void UpdateCoolTimeUI()
    {
        // スライダーの最大値を、プレイヤーのクールダウン時間に設定
        CoolTimeslider.maxValue = targetPlayer.shootCooldown;

        // スライダーの現在の値を、「現在時間 - 最後に撃った時間」にする
        // ただし、値が最大値を超えないようにMathf.Minを使うとより安全
        float elapsedTime = Time.time - targetPlayer.lastShotTime;
        CoolTimeslider.value = Mathf.Min(elapsedTime, targetPlayer.shootCooldown);
    }
}
