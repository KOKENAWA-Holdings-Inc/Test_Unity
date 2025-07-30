using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ultCoolTimeManager : MonoBehaviour
{
    [SerializeField] private Slider ultChargeSlider;
    private PlayerUltShooter PlayerUltShooter; // 変数宣言はそのまま

    void Start()
    {
        // ★追加: シーン内からPlayerUltShooterコンポーネントを探して変数に代入する
        PlayerUltShooter = FindObjectOfType<PlayerUltShooter>();

        // ★追加: もし見つからなかった場合のエラー処理
        if (PlayerUltShooter == null)
        {
            Debug.LogError("シーンにPlayerUltShooterコンポーネントが見つかりません！");
            return; // 処理を中断
        }

        // PlayerUltShooterを正しく取得した後に、スライダーの初期設定を行う
        if (ultChargeSlider != null)
        {
            ultChargeSlider.maxValue = PlayerUltShooter.maxUltCharge;
            ultChargeSlider.value = PlayerUltShooter.currentUltCharge;
        }
    }

    void Update()
    {
        // PlayerUltShooterがnullでなければ値を更新する
        if (ultChargeSlider != null && PlayerUltShooter != null)
        {
            ultChargeSlider.value = PlayerUltShooter.currentUltCharge;
        }
    }
}