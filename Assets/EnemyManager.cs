using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Transform playerTransform; // プレイヤー追従用のTransform
    public int EnemyHP = 10;
    public int Attack = 5;
    public int EnemyExperience = 1;
    [SerializeField] private float moveSpeed = 1f;

    public static event Action OnEnemyDied;

    void Start()
    {
        // 追従するためにプレイヤーのTransformを保持
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
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
                    playerComponent.ExperiencePoint += EnemyExperience;
                    playerComponent.ExperienceTotal += EnemyExperience;
                }
            }

            // 自身を破壊する
            Destroy(this.gameObject);
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

    // OnCollisionEnter2DとTakeDamageメソッドは不要
}