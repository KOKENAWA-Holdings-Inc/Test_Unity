using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを扱うために必要

public class ExperienceUIManager : MonoBehaviour
{
    [Header("UI参照")]
    [SerializeField] private Slider experienceSlider; // Inspectorから設定する経験値スライダー

    [Header("監視対象")]
    private Player targetPlayer; // シーン内のプレイヤーを格納する変数

    void Start()
    {
        // シーン内に存在するPlayerコンポーネントを探して、targetPlayer変数に格納する
        targetPlayer = FindObjectOfType<Player>();

        // PlayerまたはSliderが見つからない場合は、エラーログを出して処理を止める
        if (targetPlayer == null)
        {
            Debug.LogError("シーンにPlayerコンポーネントを持つオブジェクトが見つかりません！");
            this.enabled = false; // このスクリプトを無効にする
            return;
        }

        if (experienceSlider == null)
        {
            Debug.LogError("Experience SliderがInspectorから設定されていません！");
            this.enabled = false; // このスクリプトを無効にする
            return;
        }
    }

    void Update()
    {
        // PlayerとSliderが正常に設定されている場合のみ、毎フレーム値を更新する
        if (targetPlayer != null && experienceSlider != null)
        {
            // スライダーの最大値を、レベルアップに必要な経験値量(ExperiencePool)に設定
            experienceSlider.maxValue = targetPlayer.ExperiencePool;

            // スライダーの現在の値を、現在溜まっている経験値量(ExperiencePoint)に設定
            experienceSlider.value = targetPlayer.ExperiencePoint;
        }
    }
}