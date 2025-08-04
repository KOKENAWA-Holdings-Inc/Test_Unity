using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBallet : MonoBehaviour
{
    [SerializeField] private float Attack = 5f;

    private float ownerAttack = 0f;

    
    public void Initialize(float bossAttack)
    {
        this.ownerAttack = bossAttack;
    }
    void Start()
    {
        // 2秒後に弾が自動的に消えるようにする
        Destroy(gameObject, 2f);
    }

    // 何かに衝突した時に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        Boss bossComponent = bossObj.GetComponent<Boss>();
        // 衝突した相手が"Enemy"タグを持っていたら
        if (other.CompareTag("Player")) // other.tag == "Enemy" でも可
        {
            // ★修正点: 衝突した相手(other)からEnemyManagerコンポーネントを取得
            Player PlayerComponent = other.GetComponent<Player>();

            // ★追加: EnemyManagerが取得できた場合のみダメージを与える
            if (PlayerComponent != null)
            {
                float totalDamage = Attack * (this.ownerAttack * 0.5f) - PlayerComponent.Defence;
                if (totalDamage >= 1)
                {
                    PlayerComponent.PlayerHP -= totalDamage;
                }
                else 
                {
                    PlayerComponent.PlayerHP -= 1f;
                }
                
                //Debug.Log(other.name + "にヒット！残りHP: " + EnemyComponent.EnemyHP);
            }


            // 敵に当たったら弾を消す場合（任意）
            // Destroy(gameObject);
        }
    }
}