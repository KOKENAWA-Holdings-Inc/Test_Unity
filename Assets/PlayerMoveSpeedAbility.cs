using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSpeedAbility : MonoBehaviour
{
    [Header("ブースト設定")]
    [SerializeField] private float moveSpeedBoost = 1.5f; // 速度の増加倍率

    [Header("チャージ設定")]
    [SerializeField] private float chargeMax = 30.0f;     // チャージの最大値
    [SerializeField] private float regenerationDelay = 3.0f;  // 回復が始まるまでの待機時間
    [SerializeField] private float regenerationRate = 1.0f;   // 1秒あたりの回復量
    [SerializeField] private float drainRate = 0.2f;      // 1秒あたりの最大チャージに対する減少率 (20%)
    // ★追加: 外部から現在のチャージ量を読み取るためのプロパティ
    public float CurrentCharge => charge;
    // ★追加: 外部から最大チャージ量を読み取るためのプロパティ
    public float MaxCharge => chargeMax;

    private float charge;              // 現在のチャージ量
    private bool isBoosting = false;   // 現在ブースト中かどうかのフラグ
    private float timeOfLastChargeChange; // 最後にチャージが変化した時刻

    private Player playerComponent; // プレイヤーコンポーネントへの参照

    private GameManager gameManager;

    void Start()
    {
        // 最初にプレイヤーを探してコンポーネントを保持しておく（効率化）
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerComponent = playerObj.GetComponent<Player>();
        }
        else
        {
            //Debug.LogError("Playerオブジェクトが見つかりませんでした。");
            this.enabled = false; // プレイヤーがいないならスクリプトを無効化
            return;
        }

        charge = chargeMax; // 最初はチャージ満タン
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // --- 1. Eキー入力の処理 ---
        if (!gameManager.IsPaused && Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 現在ブースト中なら停止、そうでなければ開始を試みる
            if (isBoosting)
            {
                StopBoost();
            }
            else
            {
                StartBoost();
            }
        }

        // --- 2. ブースト中の処理 ---
        if (isBoosting)
        {
            // チャージを減少させる
            charge -= (chargeMax * drainRate) * Time.deltaTime;
            timeOfLastChargeChange = Time.time; // チャージが変化した時刻を更新

            // チャージが0になったら強制的にブーストを終了
            if (charge <= 0)
            {
                //Debug.Log("チャージ切れ！ブーストを終了します。");
                StopBoost();
            }
        }
        // --- 3. 非ブースト中の回復処理 ---
        else if (charge < chargeMax)
        {
            // 最後にチャージが変化してから指定秒数経過したら、回復を開始
            if (Time.time >= timeOfLastChargeChange + regenerationDelay)
            {
                charge += regenerationRate * Time.deltaTime;
            }
        }

        // チャージが0未満や最大値を超えないように値を丸める
        charge = Mathf.Clamp(charge, 0f, chargeMax);
    }

    /// <summary>
    /// ブーストを開始する処理
    /// </summary>
    private void StartBoost()
    {
        // プレイヤーが見つかっていて、チャージが少しでも残っていたら開始
        if (playerComponent != null && charge > 0)
        {
            isBoosting = true;
            playerComponent.moveSpeed *= moveSpeedBoost;
            timeOfLastChargeChange = Time.time;
            //Debug.Log("ブースト開始！ 現在の速度: " + playerComponent.moveSpeed);
        }
    }

    /// <summary>
    /// ブーストを停止する処理
    /// </summary>
    private void StopBoost()
    {
        if (playerComponent != null && isBoosting) // isBoostingチェックで二重停止を防止
        {
            isBoosting = false;
            playerComponent.moveSpeed /= moveSpeedBoost;
            timeOfLastChargeChange = Time.time;
            //Debug.Log("ブースト停止。 現在の速度: " + playerComponent.moveSpeed);
        }
    }
}