using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを扱うために必要

public class StatusUIManager : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("ステータス全体を囲む親パネル")]
    [SerializeField] private GameObject statusPanel;
    [Tooltip("HPを表示するテキスト")]
    [SerializeField] private TextMeshProUGUI hpText;
    [Tooltip("攻撃力を表示するテキスト")]
    [SerializeField] private TextMeshProUGUI attackText;
    [Tooltip("防御力を表示するテキスト")]
    [SerializeField] private TextMeshProUGUI defenceText;
    [Tooltip("移動速度を表示するテキスト")]
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    private Player targetPlayer; // 参照するプレイヤー
    private bool isPanelActive = false; // パネルが表示されているかどうかの状態

    void Start()
    {
        // 念のため、開始時は必ず非表示にしておく
        if (statusPanel != null)
        {
            statusPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Escapeキーが押された瞬間を検知
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 現在の状態を反転させる (表示 -> 非表示, 非表示 -> 表示)
            isPanelActive = !isPanelActive;

            if (isPanelActive)
            {
                ShowStatus();
            }
            else
            {
                HideStatus();
            }
        }
    }

    /// <summary>
    /// ステータスを表示し、ゲームを一時停止する
    /// </summary>
    private void ShowStatus()
    {
        // 常に最新のプレイヤー情報を探す
        targetPlayer = FindObjectOfType<Player>();
        if (targetPlayer == null)
        {
            Debug.LogError("Playerオブジェクトが見つかりません！");
            isPanelActive = false; // 表示できないので状態を戻す
            return;
        }

        // ゲームの時間を止める
        Time.timeScale = 0f;
        // パネルを表示する
        statusPanel.SetActive(true);

        // 各テキストに現在のステータスを反映させる
        // ToString("F1") などは、小数点以下の表示桁数を指定する書式設定です
        hpText.text = $"HP: {targetPlayer.PlayerHP.ToString("F0")} / {targetPlayer.PlayerMAXHP.ToString("F0")}";
        attackText.text = $"Attack: {targetPlayer.Attack.ToString("F2")}";
        defenceText.text = $"Defence: {targetPlayer.Defence.ToString("F2")}";
        moveSpeedText.text = $"Move Speed: {targetPlayer.moveSpeed.ToString("F1")}";
    }

    /// <summary>
    /// ステータスを非表示にし、ゲームを再開する
    /// </summary>
    private void HideStatus()
    {
        // ゲームの時間を元に戻す
        Time.timeScale = 1f;
        // パネルを非表示にする
        statusPanel.SetActive(false);
    }
}