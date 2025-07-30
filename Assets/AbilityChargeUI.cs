using UnityEngine;
using UnityEngine.UI; // UIを扱うために必要

public class AbilityChargeUI : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("ブーストのチャージ量を表示するスライダー")]
    [SerializeField] private Slider chargeSlider;

    // 監視対象のスクリプトを格納する変数
    private PlayerMoveSpeedAbility speedAbility;

    void Start()
    {
        // シーン内に存在するPlayerMoveSpeedAbilityコンポーネントを探す
        speedAbility = FindObjectOfType<PlayerMoveSpeedAbility>();

        // スクリプトまたはスライダーが見つからない場合はエラーを出す
        if (speedAbility == null)
        {
            Debug.LogError("シーンにPlayerMoveSpeedAbilityコンポーネントが見つかりません！");
            this.enabled = false; // スクリプトを無効化
            return;
        }
        if (chargeSlider == null)
        {
            Debug.LogError("Charge SliderがInspectorから設定されていません！");
            this.enabled = false;
            return;
        }

        // スライダーの最大値を初期設定
        chargeSlider.maxValue = speedAbility.MaxCharge;
    }

    void Update()
    {
        // スクリプトとスライダーが正常に存在する場合のみ、毎フレーム値を更新
        if (speedAbility != null && chargeSlider != null)
        {
            // スライダーの現在の値を、ブーストの現在のチャージ量に合わせる
            chargeSlider.value = speedAbility.CurrentCharge;
        }
    }
}