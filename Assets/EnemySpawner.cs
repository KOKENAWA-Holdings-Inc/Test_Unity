using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform player;

    public GameObject enemyPrefab;
    public TimeManager timeManager;

    // ▼▼▼ スポーン間隔の設定をこちらに変更 ▼▼▼
    [Header("Spawn Interval Settings")]
    [Tooltip("ゲーム開始時のスポーン間隔")]
    [SerializeField] private float initialSpawnInterval = 2.0f;
    [Tooltip("最短のスポーン間隔")]
    [SerializeField] private float minSpawnInterval = 0.1f;
<<<<<<< HEAD
    [Tooltip("�ŒZ�Ԋu�ɓ��B����܂ł̎��ԁi�b�j")]
    [SerializeField] private float timeToMinInterval = 180f; // 3��
=======
    [Tooltip("最短間隔に到達するまでの時間（秒）")]
    [SerializeField] private float timeToMinInterval = 300f; // 5分
>>>>>>> 927486844afcf2844f2a35d193214de238cece5c

    // ★追加: 敵の数の上限設定
    [Header("Spawn Limit Settings")]
    [Tooltip("シーン上の敵の最大数")]
    [SerializeField] private int maxEnemies = 500;

    private float spawnTimer = 0f;
    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);
    private bool isSpawningActive = true;

    [Header("HP Scaling Settings")]
    [Tooltip("敵の基本的な最大HP")]
    [SerializeField] private float baseEnemyMaxHp = 10f;
    [Tooltip("1秒あたりに増加する最大HPの量")]
    [SerializeField] private float hpGrowthPerSecond = 0.2f;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            //Debug.LogError("Playerオブジェクトが見つかりません！ 'Player'タグが設定されているか確認してください。");
            isSpawningActive = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (isSpawningActive)
        {
            // ★追加: シーン上の敵の数が上限に達していたら、スポーン処理を中断
            if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies)
            {
                //Debug.Log("Upper Limited");
                return; // 上限に達しているので何もしない
            }

            // ★変更: 経過時間に基づいて現在のスポーン間隔を計算
            float progress = Mathf.Clamp01(timeManager.elapsedTime / timeToMinInterval);
            float currentSpawnInterval = Mathf.Lerp(initialSpawnInterval, minSpawnInterval, progress);

            spawnTimer += Time.deltaTime;

            // ★変更: 計算した現在のスポーン間隔で判定
            if (spawnTimer >= currentSpawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f; // タイマーをリセット
            }

            if (timeManager.elapsedTime >= 240)
            {
                //Debug.Log("指定時間を超えたため、エネミーのスポーンを停止し、既存のエネミーを全て破壊します。");
                isSpawningActive = false;
                DestroyAllEnemies();
            }
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            //Debug.LogError("Enemy Prefabが設定されていません。");
            return;
        }

        Vector2 offset = Vector2.zero;
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y); break;
            case 1: offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -spawnArea.y); break;
            case 2: offset = new Vector2(spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y)); break;
            case 3: offset = new Vector2(-spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y)); break;
        }
        Vector2 spawnPosition = (Vector2)player.position + offset;

        float scaledMaxHp = baseEnemyMaxHp + (hpGrowthPerSecond * timeManager.elapsedTime);
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyManager enemyManager = enemyInstance.GetComponent<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.InitializeStats(scaledMaxHp);
        }

        // ▼▼▼ この行は重複して敵を生成してしまうため削除しました ▼▼▼
        // Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}