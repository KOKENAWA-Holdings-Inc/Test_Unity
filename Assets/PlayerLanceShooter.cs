using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerLanceShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public float shootCooldown = 3f;
    //public float shootCooldownMax = 3f;
    public float lastShotTime { get; private set; } = -3f;

    private Camera mainCamera;
    private GameManager gameManager;

    void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.IsPaused && Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + shootCooldown)
        {
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            lastShotTime = Time.time;
            Shoot(targetPosition);
        }
        //Debug.Log("Current Bullet Speed: " + this.bulletSpeed);
    }

    void Shoot(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 direction = (target - transform.position).normalized;

        // その方向を向くための角度を計算（Atan2を使用）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 画像のアセットが上向き（↑）を正面としている場合、90度オフセットを加える
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        // --- 2. 弾の生成と発射 ---
        // 計算した角度で弾を生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);

        // 生成した弾のRigidbody2Dを取得
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }

    GameObject FindNearestEnemy()
    {
        // "Enemy", "Boss", "Elite"のタグを持つオブジェクトをそれぞれ取得
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        GameObject[] elites = GameObject.FindGameObjectsWithTag("Elite"); // ★追加

        // 3つの配列を1つのリストに結合
        var allTargets = enemies.Concat(bosses).Concat(elites); // ★変更

        // 結合したリストが空かどうかをチェック
        if (!allTargets.Any())
        {
            return null;
        }

        // 結合したリストから最も近いターゲットを探す
        return allTargets.OrderBy(target =>
            Vector2.Distance(transform.position, target.transform.position)
        ).FirstOrDefault();
    }
}