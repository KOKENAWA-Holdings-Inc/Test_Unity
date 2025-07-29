using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitStatus : MonoBehaviour
{
    private GameObject Boss;
    private GameObject Enemy;
    public float OrbitAttack = 2f;
    public bool attacked = false;

    [SerializeField] private float knockbackForce = 10f;

    void Start()
    {
        // ...
    }

    void Update()
    {
        // ...
    }

    // ★変更: メソッド名と引数の型をトリガー用に変更
    private void OnTriggerEnter2D(Collider2D other)
    {
        attacked = true;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player playerComponent = playerObj.GetComponent<Player>();

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyManager EnemyComponent = other.gameObject.GetComponent<EnemyManager>();
            if (EnemyComponent != null)
            {
                EnemyComponent.EnemyHP = EnemyComponent.EnemyHP - OrbitAttack * (playerComponent.Attack * 0.2f);
            }

            Rigidbody2D enemyRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        if (other.gameObject.CompareTag("Elite"))
        {
            EliteManager EliteComponent = other.gameObject.GetComponent<EliteManager>();
            if (EliteComponent != null)
            {
                EliteComponent.EliteHP = EliteComponent.EliteHP - OrbitAttack * (playerComponent.Attack * 0.2f);
            }

            Rigidbody2D eliteRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (eliteRb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                eliteRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            // 注意: BossコンポーネントがBossManagerを指しているか確認してください
            BossManager BossComponent = other.gameObject.GetComponent<BossManager>();
            if (BossComponent != null)
            {
                // 注意: BossManagerにBossHPという変数があるか確認してください
                // BossComponent.BossHP = BossComponent.BossHP - OrbitAttack; 
            }
        }
        if (attacked == true)
        {
            attacked = false;
        }
    }
}