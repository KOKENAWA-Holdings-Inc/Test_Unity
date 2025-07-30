using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EliteBallet : MonoBehaviour
{
    // ★追加: 自分を発射したEliteManagerを格納するための変数
    public EliteManager shooter;

    [SerializeField] private float Attack = 5f;

    void Start()
    {
        // 2秒後に弾が自動的に消えるようにする
        Destroy(gameObject, 2f);
    }

    // 何かに衝突した時に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ▼▼▼▼▼ 削除 ▼▼▼▼▼
        // GameObject eliteObj = GameObject.FindGameObjectWithTag("Elite");
        // EliteManager EliteComponent = eliteObj.GetComponent<EliteManager>();
        // ▲▲▲▲▲ 削除 ▲▲▲▲▲

        // 衝突した相手が"Player"タグを持っていたら
        if (other.CompareTag("Player"))
        {
            Player PlayerComponent = other.GetComponent<Player>();

            // shooterがセットされており、Playerが見つかった場合
            if (PlayerComponent != null && shooter != null)
            {
                // ★変更: EliteComponentの代わりに、保持しておいたshooterを使う
                if (Attack * (shooter.Attack * 0.5f) - PlayerComponent.Defence >= 1)
                {
                    PlayerComponent.PlayerHP = PlayerComponent.PlayerHP - (Attack * (shooter.Attack * 0.5f) - PlayerComponent.Defence);
                }
                else
                {
                    PlayerComponent.PlayerHP -= 1f;
                }
                Debug.Log("hit");
            }
        }
    }
}