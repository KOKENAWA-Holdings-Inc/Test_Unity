using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BossIndicator : MonoBehaviour
{
    private Transform player;
    private Transform boss;
    private TextMeshProUGUI uiText;
    private RectTransform uiRectTransform;
    private Camera mainCamera;

    public Vector2 hideArea = new Vector2(9f, 5f);
    public float screenPadding = 100f;

    // ★★★ UIを画面端から内側へ移動させる距離を追加 ★★★
    public float inwardOffset = 30f;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        uiRectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        uiText.enabled = false;

        if (boss == null)
        {
            Debug.Log("Start時点では、bossはnullです。これは正常な状態です。");
        }
        else
        {
            Debug.Log("警告: Start時点で、bossに何らかの値が入っています！これが原因です。");
        }
    }

    void Update()
    {
        // プレイヤーとボスを探す処理 (変更なし)
        if (player == null)
        {
            // ★★★ このログが表示されるか確認してください ★★★
            //Debug.LogError("Playerが見つからないため、インジケーターの処理を中断します。Playerのタグを確認してください。");
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // 毎フレーム探しに行くように修正
            return;
        }
        if (boss == null)
        {
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null) boss = bossObj.transform;
            else { uiText.enabled = false; return; }
            Debug.Log("Boss Spawned");
        }

        // 表示/非表示の判定 (変更なし)
        Vector2 relativePosition = boss.position - player.position;
        bool isBossInsideArea = Mathf.Abs(relativePosition.x) <= hideArea.x &&
                                Mathf.Abs(relativePosition.y) <= hideArea.y;
        uiText.enabled = !isBossInsideArea;

        if (uiText.enabled)
        {
            // --- UIの向きを計算 ---
            transform.up = relativePosition.normalized;

            // --- UIの位置を画面端に計算 ---
            // (このブロックは前回の修正と同じです)
            Vector3 bossScreenPos = mainCamera.WorldToScreenPoint(boss.position);
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            if (bossScreenPos.z < 0)
            {
                bossScreenPos = center + (center - bossScreenPos);
            }
            Vector3 dir = bossScreenPos - center;
            float divX = ((Screen.width / 2) - screenPadding) / Mathf.Abs(dir.x);
            float divY = ((Screen.height / 2) - screenPadding) / Mathf.Abs(dir.y);
            float scale = Mathf.Min(divX, divY);
            Vector3 edgePosition = center + dir * scale;

            // ★★★ ここからが追加した微調整の処理 ★★★
            // 画面端の位置(edgePosition)からプレイヤーの画面上の位置へ向かうベクトルを計算
            Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
            Vector3 directionToPlayer = (playerScreenPos - edgePosition).normalized;

            // 画面端の位置から、プレイヤー方向へ inwardOffset 分だけ移動させる
            Vector3 finalPosition = edgePosition + directionToPlayer * inwardOffset;

            // 最終的な位置を適用
            uiRectTransform.position = finalPosition;
        }
    }
}
