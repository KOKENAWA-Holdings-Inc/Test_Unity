using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    [Header("ターゲット設定")]
    private Transform playerTransform; // 追従するプレイヤー

    [Header("周回オブジェクト設定")]
    [SerializeField] private GameObject orbitingObjectPrefab;
    [SerializeField] private int numberOfObjects = 8;
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private float orbitSpeed = 50.0f;

    [Header("時間設定")]
    [SerializeField] private float initialSpawnDelay = 2.0f;
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] private float respawnDelay = 5.0f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    // ★ 変更点: Startでは初期化コルーチンを開始するだけ
    void Start()
    {
        // プレイヤーを探して、見つかったらメイン処理を始めるコルーチンを開始
        StartCoroutine(InitializeCoroutine());
    }

    // ★ 追加: プレイヤーを探して初期化を行うコルーチン
    private IEnumerator InitializeCoroutine()
    {
        // playerTransformが見つかるまでループし続ける
        while (playerTransform == null)
        {
            // "Player"タグを頼りにプレイヤーオブジェクトを探す
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                // 見つかったら参照を保存
                playerTransform = playerObj.transform;
            }

            // プレイヤーが見つからなければ、次のフレームまで待機
            yield return null;
        }

        // --- プレイヤーが見つかったら、ここから下の処理が実行される ---

        // 召喚と消滅を繰り返すメインのサイクルを開始
        StartCoroutine(LifecycleCoroutine());
    }

    void LateUpdate()
    {
        if (playerTransform != null && spawnedObjects.Count > 0)
        {
            Orbit();
        }
    }

    private void Orbit()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            float angle = i * (360f / numberOfObjects);
            float currentAngle = angle + Time.time * orbitSpeed;
            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians) * radius;
            float y = Mathf.Sin(angleInRadians) * radius;
            Vector3 offset = new Vector3(x, y, 0);
            spawnedObjects[i].transform.position = playerTransform.position + offset;
        }
    }

    private IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        while (true)
        {
            SpawnObjects();
            yield return new WaitForSeconds(lifeTime);
            DespawnObjects();
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void SpawnObjects()
    {
        if (spawnedObjects.Count > 0) DespawnObjects();
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject obj = Instantiate(orbitingObjectPrefab, transform.position, Quaternion.identity);
            spawnedObjects.Add(obj);
        }
    }

    private void DespawnObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}