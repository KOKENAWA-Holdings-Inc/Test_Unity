using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    // ... （変数の大部分はそのまま） ...
    private Transform playerTransform;
    [SerializeField] private GameObject orbitingObjectPrefab;
    [SerializeField] private int numberOfObjects = 8;
    [SerializeField] private float radius = 2.0f;
    public float orbitSpeed = 70.0f;
    [SerializeField] private float initialSpawnDelay = 2.0f;
    public float lifeTime = 10.0f;
    public float respawnDelay = 5.0f;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    public List<GameObject> SpawnedObjects => spawnedObjects;
    private Coroutine lifecycleCoroutine;

    void Start()
    {
        // ★変更: ライフサイクルを開始するだけにする
        lifecycleCoroutine = StartCoroutine(LifecycleCoroutine());
    }

    // ▼▼▼ InitializeCoroutineは不要になったため削除 ▼▼▼
    /*
    private IEnumerator InitializeCoroutine() { ... }
    */

    void LateUpdate()
    {
        // ★変更: プレイヤーへの参照が無効になったら、毎フレーム探し直す
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                // プレイヤーがいない場合は何もしない
                return;
            }
        }

        // プレイヤーが見つかっていれば、周回処理を実行
        if (spawnedObjects.Count > 0)
        {
            Orbit();
        }
    }

    // ... （他のメソッドはそのまま） ...
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
    public void StopLifecycle()
    {
        if (lifecycleCoroutine != null)
        {
            StopCoroutine(lifecycleCoroutine);
        }
    }
    public void RestartLifecycle()
    {
        StopLifecycle();
        DespawnObjects();
        lifecycleCoroutine = StartCoroutine(LifecycleCoroutine());
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
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}