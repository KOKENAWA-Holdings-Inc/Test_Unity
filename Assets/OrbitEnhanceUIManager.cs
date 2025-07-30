using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitEnhanceUIManager : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("クールダウンを表示するスライダー")]
    [SerializeField] private Slider cooldownSlider;

    // 監視対象のスクリプトを格納する変数
    private OrbitEnhancer orbitEnhancer;

    void Start()
    {
        // シーン内に存在するOrbitEnhancerコンポーネントを探す
        orbitEnhancer = FindObjectOfType<OrbitEnhancer>();

        if (orbitEnhancer == null)
        {
            Debug.LogError("シーンにOrbitEnhancerコンポーネントが見つかりません！");
            this.enabled = false;
            return;
        }
        if (cooldownSlider == null)
        {
            Debug.LogError("Cooldown SliderがInspectorから設定されていません！");
            this.enabled = false;
            return;
        }

        
    }

    void Update()
    {
        if (orbitEnhancer == null || cooldownSlider == null) return;

        // 現在時刻が、アビリティが使用可能になる時刻より前か？（＝クールダウン中か？）
        if (Time.time < orbitEnhancer.AbilityReadyTime)
        {
            

            // クールダウンの残り時間を計算
            float timeRemaining = orbitEnhancer.AbilityReadyTime - Time.time;
            // 全体のクールダウン時間に対する進捗率を計算（0.0～1.0）
            float progress = (orbitEnhancer.CooldownDuration - timeRemaining) / orbitEnhancer.CooldownDuration;

            // スライダーの値を更新
            cooldownSlider.value = progress;
        }
        
    }
}
