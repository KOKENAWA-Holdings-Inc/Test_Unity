using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour
{
    [Header("出現させるオブジェクト")]
    [SerializeField]
    private GameObject objectPrefab;

    [Header("UI設定")]
    [SerializeField]
    private GameObject markerUiPrefab;

    [Header("設定")]
    [SerializeField]
    private int numberOfObjects = 12;
    [SerializeField]
    private float displayDuration = 0.1f;
    [SerializeField]
    private float markerDuration = 0.5f;
    [SerializeField]
    private float spawnDelayAfterMarker = 0.5f;

    [Header("実行タイミング")]
    [SerializeField]
    private float minWaitTime = 15f;
    [SerializeField]
    private float randomWaitTime = 20f;

    private Camera mainCamera;

    // ★追加: マーカーUIのインスタンスを保管しておくためのリスト（オブジェクトプール）
    private List<GameObject> markerPool = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;

        // ★変更: 最初に必要な数のマーカーを生成し、非表示にしてプールしておく
        if (markerUiPrefab != null)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                GameObject marker = Instantiate(markerUiPrefab);
                marker.SetActive(false); // 生成したインスタンスを非表示に
                markerPool.Add(marker);
            }
        }

        StartCoroutine(FindBossAndStartSpawning());
    }

    private IEnumerator FindBossAndStartSpawning()
    {
        while (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("ボスを発見！TimedSpawnerの処理を開始します。");
        StartCoroutine(SpawningLoopCoroutine());
    }

    private IEnumerator SpawningLoopCoroutine()
    {
        while (true)
        {
            float delay = minWaitTime + Random.Range(0f, randomWaitTime);
            yield return new WaitForSeconds(delay);
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        if (objectPrefab == null || markerUiPrefab == null)
        {
            Debug.LogError("Object Prefab または Marker Ui Prefab が設定されていません！");
            return;
        }

        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(0, Screen.width);
            float randomY = Random.Range(0, Screen.height);

            Vector3 screenPosition = new Vector3(randomX, randomY, 10f);
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            // ★変更: プールのインデックスと座標を渡す
            StartCoroutine(SpawnWithMarkerCoroutine(i, worldPosition));
        }
    }

    // ★変更: プールのインデックスを受け取るように引数を変更
    private IEnumerator SpawnWithMarkerCoroutine(int poolIndex, Vector3 spawnPosition)
    {
        // --- UIマーカーの表示処理 ---
        // ★変更: プールからマーカーを取得
        GameObject markerInstance = markerPool[poolIndex];

        // ★変更: 生成(Instantiate)ではなく、位置をセットして表示(SetActive)
        markerInstance.transform.position = spawnPosition;
        markerInstance.SetActive(true);

        yield return new WaitForSeconds(markerDuration);

        // ★変更: 破棄(Destroy)ではなく、非表示(SetActive)
        markerInstance.SetActive(false);

        // --- オブジェクトの召喚処理 ---
        yield return new WaitForSeconds(spawnDelayAfterMarker);

        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        Destroy(spawnedObject, displayDuration);
    }
}