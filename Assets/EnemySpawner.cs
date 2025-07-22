using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemyPrefab;
    public TimeManager timeManager;

    // エネミーをスポーンさせる間隔（秒）
    public float spawnInterval = 2f;

    // ★★★ スポーン用のタイマー変数を追加 ★★★
    private float spawnTimer = 0f;

    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);
    private bool isSpawningActive = true;

    // Startメソッドは不要になるので削除します
    // void Start() { ... }

    void Update()
    {
        // スポナーがアクティブな場合のみ処理を実行
        if (isSpawningActive)
        {
            // ★★★ ここからがタイマー処理 ★★★
            // タイマーに毎フレームの経過時間を加算
            spawnTimer += Time.deltaTime;

            // タイマーが指定したスポーン間隔を超えたら
            if (spawnTimer >= spawnInterval)
            {
                // エネミーをスポーン
                SpawnEnemy();

                // タイマーをリセット
                // spawnTimer = 0f; でも良いが、こちらの方がより正確
                spawnTimer -= spawnInterval;
            }
            // ★★★ タイマー処理ここまで ★★★


            // 時間が420秒以上になったら、スポーンを停止する
            if (timeManager.elapsedTime >= 420)
            {
                Debug.Log("指定時間を超えたため、エネミーのスポーンを停止し、既存のエネミーを全て破壊します。");
                isSpawningActive = false;

                // InvokeRepeatingを使っていないので、CancelInvokeは不要

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
        // isSpawningActiveのチェックは不要になります
        // if (!isSpawningActive) return;

        if (player == null || enemyPrefab == null)
        {
            Debug.LogError("PlayerまたはEnemy Prefabが設定されていません。");
            return;
        }

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