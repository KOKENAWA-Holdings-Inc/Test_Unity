using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    

    // プレイヤーオブジェクト
    public Transform player;

    // スポーンさせる敵のプレハブ
    public GameObject BossPrefab;

    // スポーンさせる矩形領域のサイズ（中心からの距離）
    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);

    public TimeManager timeManager;

    

    //private bool hasBossSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnBoss", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (((int)timeManager.elapsedTime) == 420 && !hasBossSpawned) 
        {
            SpawnBoss();
            hasBossSpawned = true;
        }*/

        
        
    }

    void SpawnBoss() 
    {
        Vector2 offset = Vector2.zero; // スポーン位置のオフセットを初期化

        // 0:上, 1:下, 2:右, 3:左 の4つの辺から1つをランダムに選ぶ
        int side = Random.Range(0, 4);

        switch (side)
        {
            // 上辺にスポーン
            case 0:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y);
                break;
            // 下辺にスポーン
            case 1:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -spawnArea.y);
                break;
            // 右辺にスポーン
            case 2:
                offset = new Vector2(spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
            // 左辺にスポーン
            case 3:
                offset = new Vector2(-spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
        }

        // プレイヤーの位置に計算したオフセットを加算
        Vector2 spawnPosition = (Vector2)player.position + offset;

        // 敵を生成
        Instantiate(BossPrefab, spawnPosition, Quaternion.identity);
    }

    
}
