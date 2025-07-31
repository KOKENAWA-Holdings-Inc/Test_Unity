using System.Collections;
using System.Collections.Generic;
using System; // event Action を使うために必要
using UnityEngine;
using UnityEngine.UI; // UIのスライダーを使う場合は必要

public class PlayerUltShooter : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 15f;

    [Header("必殺技チャージ設定")]
    public float maxUltCharge = 500f; // チャージの最大値
    public float passiveChargeRate = 2f;  // 1秒あたりの自然増加量
    public float hitChargeAmount = 3f;    // 1ヒットあたりの増加量
    public float currentUltCharge = 0f;  // 現在のチャージ量

    private GameManager gameManager;


    // ★追加：敵に攻撃がヒットしたことを通知するためのイベント
    public static event Action OnEnemyHit;

    // ★追加：外部からイベントを発生させるための公開メソッド
    public static void RaiseOnEnemyHit()
    {
        // メソッドの内部からならイベントを呼び出せる
        OnEnemyHit?.Invoke();
    }


    private Camera mainCamera;

    // ★追加：イベントの購読を開始
    private void OnEnable()
    {
        OnEnemyHit += HandleEnemyHit;
    }

    // ★追加：イベントの購読を解除（オブジェクト破棄時に必ず行う）
    private void OnDisable()
    {
        OnEnemyHit -= HandleEnemyHit;
    }

    void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // --- 1. チャージを増やす ---
        // 時間経過でチャージ
        currentUltCharge += passiveChargeRate * Time.deltaTime;
        // 最大値を超えないように制御
        currentUltCharge = Mathf.Min(currentUltCharge, maxUltCharge);

        // --- 2. 発射条件をチェック ---
        // ★変更：クリックに加えて、チャージが満タンであるかを確認
        if (!gameManager.IsPaused && Input.GetMouseButtonDown(0) && currentUltCharge >= maxUltCharge)
        {
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            Shoot(targetPosition);

            // ★追加：発射したらチャージをリセット
            currentUltCharge = 0f;
        }

        // --- 3. UIを更新（任意） ---
        /*if (ultChargeSlider != null)
        {
            ultChargeSlider.value = currentUltCharge;
        }*/
    }

    void Shoot(Vector3 target)
    {
        if (projectilePrefab == null) return;
        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        TargetedProjectile projectile = projectileObj.GetComponent<TargetedProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(target, projectileSpeed);
        }
    }

    // ★追加：イベントが発生した時に呼び出されるメソッド
    private void HandleEnemyHit()
    {
        // ヒットしたのでチャージを追加
        currentUltCharge += hitChargeAmount;
    }
}