using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public int BossHP = 10;
    public int BossMAXHP = 20;
    public int Attack = 5;
    public int Defence = 5;
    public int BossExperience = 10;
    private Rigidbody2D rb;
    private Vector2 movement;

    
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
