using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerHP = 100f;
    public float PlayerMAXHP = 100f;
    public float ExperiencePool = 20f;
    public float ExperiencePoint = 0f;
    public float ExperienceTotal = 0f;
    public float ExperienceBuff = 1f;
    public float PlayerLv = 1f;
    public float Attack = 10f;
    public float Defence = 1f;
    public float moveSpeed = 5f;
    private GameObject Enemy;
    private float damageCooldown = 0.1f;
    private float nextDamageTime = 0f;

    // ★追加: 緊急無敵が一度発動したかを記録するためのフラグ
    private bool emergencyInvincibilityTriggered = false;

    public static event Action<float> OnPlayerDied;
    //public static event Action OnPlayerDied;
    private Rigidbody2D rb;
    private Vector2 movement;
    private BoxCollider2D boxCollider; // ★追加: BoxCollider2Dへの参照

    private void Awake()
    {
        PlayerHP = PlayerMAXHP;
        ExperiencePoint = 0;
        ExperienceTotal = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ★追加: 開始時に自身のBoxCollider2Dを取得しておく
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (ExperiencePoint / ExperiencePool >= 1)
        {
            PlayerLv++;
            Attack = Attack * 1.05f;
            Defence = Defence * 1.01f;
            ExperiencePoint = ExperiencePoint - ExperiencePool;
            ExperiencePool = (int)(Mathf.Pow(5, PlayerLv) / Mathf.Pow(4, PlayerLv) + 10 * PlayerLv);
        }
    }
    public void AddExperience(float amount)
    {
       ExperiencePoint += amount * ExperienceBuff;
       ExperienceTotal += amount * ExperienceBuff; 
    }
    void FixedUpdate()
    {
        if (PlayerHP <= 0)
        {
            Die();
        }
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 衝突した相手のオブジェクトのタグを比較
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Elite"))
        {
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + damageCooldown;
                EnemyManager EnemyComponent = collision.gameObject.GetComponent<EnemyManager>();
                EliteManager EliteComponent = collision.gameObject.GetComponent<EliteManager>();

                if (EnemyComponent != null)
                {
                    if (EnemyComponent.Attack - (Defence - 1) >= 1)
                    {
                        PlayerHP = PlayerHP - (EnemyComponent.Attack - (Defence - 1));
                    }
                    else
                    {
                        PlayerHP -= 1f;
                    }
                }
                if (EliteComponent != null)
                {
                    if (EliteComponent.Attack - (Defence - 1) >= 1)
                    {
                        PlayerHP = PlayerHP - (EliteComponent.Attack - (Defence - 1));
                    }
                    else
                    {
                        PlayerHP -= 1f;
                    }
                }

                // ★追加: ダメージを受けた直後にHPをチェック
                CheckForEmergencyInvincibility();
            }
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss BossComponent = collision.gameObject.GetComponent<Boss>();
            if (BossComponent != null)
            {
                if (BossComponent.Attack - Defence >= 1)
                {
                    PlayerHP = PlayerHP - (BossComponent.Attack - Defence);
                }
                else
                {
                    PlayerHP -= 1f;
                }

                // ★追加: ダメージを受けた直後にHPをチェック
                CheckForEmergencyInvincibility();
            }
        }
    }

    /// <summary>
    /// ★追加: HPをチェックして、条件を満たしていれば緊急無敵を発動するメソッド
    /// </summary>
    private void CheckForEmergencyInvincibility()
    {
        // HPが10以下、かつ、まだこの機能が発動していない場合
        if (PlayerHP <= 10f && !emergencyInvincibilityTriggered)
        {
            // 発動したことを記録
            emergencyInvincibilityTriggered = true;
            // コルーチンを開始
            StartCoroutine(TriggerInvincibilityCoroutine());
        }
    }

    /// <summary>
    /// ★追加: 5秒間BoxCollider2Dを無効化するコルーチン
    /// </summary>
    private IEnumerator TriggerInvincibilityCoroutine()
    {
        if (boxCollider != null)
        {
            Debug.Log("緊急無敵発動！ 5秒間当たり判定を無効化します。");
            // コライダーを無効にする
            boxCollider.enabled = false;

            // 5秒間待機する
            yield return new WaitForSeconds(5f);

            // 5秒後にコライダーを再度有効にする
            boxCollider.enabled = true;
            Debug.Log("無敵時間が終了しました。");
        }
    }

    public void PlayerLvManager()
    {
    }

    void Die()
    {
        OnPlayerDied?.Invoke(ExperienceTotal);
    }
}
