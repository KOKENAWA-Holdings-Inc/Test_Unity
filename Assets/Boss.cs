using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public float BossHP = 10f;
    public float BossMAXHP = 20f;
    public float Attack = 10f;
    public float Defence = 0f;
    public int BossExperience = 10;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float previousHP;


    public static event Action OnBossDied;
    private void Awake()
    {
        BossHP = BossMAXHP;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHP != previousHP)
        {
            if (damagePopupPrefab != null)
            {
                float damage = previousHP - BossHP;

                // ★変更: 敵の頭上に直接生成する
                Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // 敵の1ユニット上に表示
                GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);

                // ▼▼▼ 問題の原因なので、この行を完全に削除 ▼▼▼
                // popup.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

                // 生成したポップアップにダメージ量を設定
                popup.GetComponent<DamagePopup>().Setup(damage);
            }
        }
        previousHP = BossHP;
        // 自身のHPが0以下になったかを毎フレーム監視する
        if (BossHP <= 0)
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
                    playerComponent.ExperiencePoint += BossExperience;
                    playerComponent.ExperienceTotal += BossExperience;
                }
            }

            // 自身を破壊する
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        OnBossDied?.Invoke();

    }
}
