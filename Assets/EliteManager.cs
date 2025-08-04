using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteManager : MonoBehaviour
{
    private Transform playerTransform; // プレイヤー追従用のTransform
    public GameObject damagePopupPrefab;
    public float EliteHP = 100f;
    public float EliteMaxHP = 100f;
    public float Attack = 5f;
    public int EliteExperience = 50;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    private float previousHP;

    public static event Action OnEnemyDied;


    public void InitializeStats(float newMaxHp)
    {
        EliteMaxHP = newMaxHp;
        EliteHP = EliteMaxHP; // HPも最大値に設定
    }
    private void Awake()
    {
        EliteHP = EliteMaxHP;
    }
    void Start()
    {
        // 追従するためにプレイヤーのTransformを保持
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        previousHP = EliteHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = EliteMaxHP;
            healthSlider.value = EliteHP;
            // 初期状態では非表示にする
            healthSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (EliteHP != previousHP)
        {
            // HPバーが非表示なら、最初に表示する
            if (healthSlider != null && !healthSlider.gameObject.activeSelf)
            {
                healthSlider.gameObject.SetActive(true);
            }

            // Sliderの値を更新
            if (healthSlider != null)
            {
                healthSlider.value = EliteHP;
            }
            if (damagePopupPrefab != null)
            {
                float damage = previousHP - EliteHP;

                // ★変更: 敵の頭上に直接生成する
                Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // 敵の1ユニット上に表示
                GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);

                // ▼▼▼ 問題の原因なので、この行を完全に削除 ▼▼▼
                // popup.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

                // 生成したポップアップにダメージ量を設定
                popup.GetComponent<DamagePopup>().Setup(damage);
            }

            // 現在のHPを「直前のHP」として保存し、次回のフレームで比較できるようにする
            previousHP = EliteHP;
        }
        // 自身のHPが0以下になったかを毎フレーム監視する
        if (EliteHP <= 0)
        {
            Die(); // イベントを発行

            // ▼▼▼▼▼ 削除 ▼▼▼▼▼
            // // シーンから "Player" タグのオブジェクトを探す
            // GameObject playerToReward = GameObject.FindGameObjectWithTag("Player");
            // ▲▲▲▲▲ 削除 ▲▲▲▲▲


            // ★変更: 保持しているplayerTransformを使い、プレイヤーが見つかった場合のみ経験値を与える
            if (playerTransform != null)
            {
                // ★変更: playerTransformからコンポーネントを取得
                Player playerComponent = playerTransform.GetComponent<Player>();
                if (playerComponent != null)
                {
                    playerComponent.AddExperience(EliteExperience);
                    playerComponent.ExperienceTotal += EliteExperience;
                }
            }

            // 自身を破壊する
            Destroy(this.gameObject);
        }
    }
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