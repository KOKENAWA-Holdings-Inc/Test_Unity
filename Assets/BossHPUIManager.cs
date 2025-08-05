using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUIManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    private Boss targetBoss; // 参照するBossスクリプト

    void Update()
    {
        // ターゲットのボスがいない場合のみ、シーンから探す
        if (targetBoss == null)
        {
            // "Boss"タグの付いたオブジェクトを探し、そのオブジェクトからBossスクリプトを取得
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null)
            {
                targetBoss = bossObj.GetComponent<Boss>();
            }
        }

        // ターゲットのボスが見つからない、またはHPが0で破壊された場合はスライダーを非表示
        if (targetBoss == null)
        {
            hpSlider.gameObject.SetActive(false);
            return;
        }

        // ボスが見つかっていたら、スライダーをアクティブにしてUIを更新
        hpSlider.gameObject.SetActive(true);
        UpdateBossHPUI();
    }

    // UIを更新するメソッド
    public void UpdateBossHPUI()
    {
        // targetBossの現在値を使ってUIを更新
        hpSlider.maxValue = targetBoss.BossMAXHP;
        hpSlider.value = targetBoss.BossHP;
    }
}