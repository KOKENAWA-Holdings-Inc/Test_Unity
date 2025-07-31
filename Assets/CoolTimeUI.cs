using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    [SerializeField] private Slider CoolTimeslider;
    private PlayerLanceShooter targetPlayer;

    void Update()
    {
        // 参照が切れていたら、探しに行く
        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                // ここでGetComponentを試みる
                targetPlayer = playerObj.GetComponent<PlayerLanceShooter>();
            }
        }

        // ★変更: 再度チェックを追加
        // 最終的にtargetPlayerが有効な参照を持っている場合のみ、UIを更新する
        if (targetPlayer != null)
        {
            UpdateCoolTimeUI();
        }
        // else
        // {
        //     // プレイヤーがいない場合はスライダーを非表示にするなど
        //     CoolTimeslider.gameObject.SetActive(false);
        // }
    }

    public void UpdateCoolTimeUI()
    {
        CoolTimeslider.maxValue = targetPlayer.shootCooldown;
        float elapsedTime = Time.time - targetPlayer.lastShotTime;
        CoolTimeslider.value = Mathf.Min(elapsedTime, targetPlayer.shootCooldown);
    }
}