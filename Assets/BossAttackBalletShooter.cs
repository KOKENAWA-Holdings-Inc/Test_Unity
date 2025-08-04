using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttackBalletShooter : MonoBehaviour
{
    // インスペクターから弾のプレハブを設定
    public GameObject bulletPrefab;
    // 弾の発射速度
    public float bulletSpeed = 15f;
    private Boss bossComponent;

    // ★追加: 射撃のクールダウンタイム（3秒）
    private float shootCooldown = 1f;
    // ★追加: 最後に撃った時間を記録する変数
    private float lastShotTime = -1f; // 最初にすぐ撃てるようにマイナス値で初期化

    void Update()
    {
        if (bossComponent == null)
        {
            // "Boss"タグを持つオブジェクトを探し、そこからBossコンポーネントを取得
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null)
            {
                bossComponent = bossObj.GetComponent<Boss>();
                Debug.Log("ボスを発見しました。攻撃を開始します。");
            }

            // まだボスが見つからない場合は、射撃処理を行わずに中断
            if (bossComponent == null)
            {
                return;
            }
        }
        // ★変更:「前回の射撃から3秒後」の条件に変更
        if (Time.time >= lastShotTime + shootCooldown)
        {
            // ★追加: 最終射撃時刻を現在の時刻に更新
            lastShotTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        // ★変更: "Player"タグを持つ「最も近い」オブジェクトを探すように修正
        GameObject nearestPlayer = FindNearestPlayer();

        // 敵が見つからなかった場合は何もしない
        if (nearestPlayer == null)
        {
            //Debug.LogWarning("シーンにPlayerがいません。");
            return;
        }

        // 敵の方向を先に計算 (対象をnearestPlayerに変更)
        Vector2 direction = (nearestPlayer.transform.position - transform.position).normalized;

        // 向きから角度を計算し、Quaternion（回転情報）を生成
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        // 計算した回転情報(rotation)で弾を生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // 弾に力を加えて発射
        rb.velocity = direction * bulletSpeed;
    }

    // ★追加: 最も近い敵を探して返すメソッド
    GameObject FindNearestPlayer()
    {
        // "Enemy"タグを持つ全てのオブジェクトを配列として取得
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Player");

        // 敵が一人もいなければnullを返す
        if (enemys.Length == 0)
        {
            return null;
        }

        // LINQを使い、プレイヤーからの距離で昇順に並べ替え、最初の要素（＝最も近い敵）を返す
        return enemys.OrderBy(player =>
            Vector2.Distance(transform.position, player.transform.position)
        ).FirstOrDefault();
    }
}