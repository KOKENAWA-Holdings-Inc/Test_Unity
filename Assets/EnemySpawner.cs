using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // public Transform player; // ← インスペクターでの設定は不要になるので削除またはコメントアウト
    private Transform player; // 内部でプレイヤーの情報を保持するための変数

    public GameObject enemyPrefab;
    public TimeManager timeManager;

    public float spawnInterval = 0.5f;
    private float spawnTimer = 0f;
    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);
    private bool isSpawningActive = true;

    // ★★★ ゲーム開始時に一度だけ実行されるAwakeメソッドを追加 ★★★
    void Start()
    {
        // "Player"というタグが付いているゲームオブジェクトを探し、そのTransformコンポーネントを取得する
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            // もし見つからなかった場合、エラーログを出してスポーンを停止する
            Debug.LogError("Playerオブジェクトが見つかりません！ 'Player'タグが設定されているか確認してください。");
            isSpawningActive = false;
        }
    }

    void Update()
    {
        // playerがいない場合は何もしない
        if (player == null) return;

        // スポナーがアクティブな場合のみ処理を実行
        if (isSpawningActive)
        {
            // タイマー処理
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                Debug.Log("Spawned");
                spawnTimer -= spawnInterval;
            }

            // 時間が420秒以上になったら、スポーンを停止する
            if (timeManager.elapsedTime >= 420)
            {
                Debug.Log("指定時間を超えたため、エネミーのスポーンを停止し、既存のエネミーを全て破壊します。");
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
            Debug.LogError("Enemy Prefabが設定されていません。");
            return;
        }

        //Debug.Log("Spawning enemy near player at position: " + player.position);

        Vector2 offset = Vector2.zero;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y);
                break;
            case 1:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -spawnArea.y);
                break;
            case 2:
                offset = new Vector2(spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
            case 3:
                offset = new Vector2(-spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
        }

        Vector2 spawnPosition = (Vector2)player.position + offset;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}