using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteManager : MonoBehaviour
{
    private Transform playerTransform; // プレイヤー追従用のTransform
    public float EliteHP = 10f;
    public float EliteMaxHP = 10f;
    public float Attack = 1f;
    public int EliteExperience = 10;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    private float previousHP;

    public static event Action OnEnemyDied;


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
                    playerComponent.ExperiencePoint += EliteExperience;
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