using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    
    public GameObject damagePopupPrefab;
    private Transform playerTransform; // プレイヤー追従用のTransform
    public float EnemyHP = 10f;
    public float EnemyMaxHP = 10f;
    public float Attack = 1f;
    public int EnemyExperience = 5;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private UnityEngine.UI.Slider healthSlider;

    // ★追加: HPの変化を検知するために、直前のHPを保存する変数
    private float previousHP;

    public static event Action OnEnemyDied;

    public void InitializeStats(float newMaxHp)
    {
        EnemyMaxHP = newMaxHp;
        EnemyHP = EnemyMaxHP; // HPも最大値に設定
    }
    private void Awake()
    {
        EnemyHP = EnemyMaxHP;
    }

    void Start()
    {
        // 追従するためにプレイヤーのTransformを保持
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        // ★追加: 直前のHPを初期化
        previousHP = EnemyHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = EnemyMaxHP;
            healthSlider.value = EnemyHP;
            // 初期状態では非表示にする
            healthSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ★変更点: HPが変化したかどうかをチェック
        if (EnemyHP != previousHP)
        {
            // HPバーが非表示なら、最初に表示する
            if (healthSlider != null && !healthSlider.gameObject.activeSelf)
            {
                healthSlider.gameObject.SetActive(true);
            }

            // Sliderの値を更新
            if (healthSlider != null)
            {
                healthSlider.value = EnemyHP;
            }
            if (damagePopupPrefab != null)
            {
                float damage = previousHP - EnemyHP;

                // ★変更: 敵の頭上に直接生成する
                Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // 敵の1ユニット上に表示
                GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);

                // ▼▼▼ 問題の原因なので、この行を完全に削除 ▼▼▼
                // popup.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

                // 生成したポップアップにダメージ量を設定
                popup.GetComponent<DamagePopup>().Setup(damage);
            }


            // 現在のHPを「直前のHP」として保存し、次回のフレームで比較できるようにする
            previousHP = EnemyHP;
        }

        // 自身のHPが0以下になったかを毎フレーム監視する
        if (EnemyHP <= 0)
        {
            Die(); // イベントを発行

            // シーンから "Player" タグのオブジェクトを探す
            GameObject playerToReward = GameObject.FindGameObjectWithTag("Player");

            // プレイヤーが見つかった場合のみ経験値を与える
            if (playerToReward != null)
            {
                Player playerComponent = playerToReward.GetComponent<Player>();
                if (playerComponent != null)
                {
                    // ★変更: PlayerのAddExperienceメソッドを呼び出して経験値を渡す
                    playerComponent.AddExperience(EnemyExperience);
                    playerComponent.ExperienceTotal += EnemyExperience;
                }
            }

            // 自身を破壊する
            Destroy(this.gameObject);
        }
    }

    // ★追加: HPバーの位置を敵に追従させるための処理
    private void LateUpdate()
    {
        // HPバーが表示されている場合のみ、位置を更新する
        if (healthSlider != null && healthSlider.gameObject.activeSelf)
        {
            // 敵本体の位置から真上に0.5ずらした位置にHPバーを配置する
            healthSlider.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    private void FixedUpdate()
    {
        // プレイヤーが見つかっている場合のみ追従
        if (playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    void Die()
    {
        OnEnemyDied?.Invoke();
    }
}