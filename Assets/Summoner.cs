using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを扱うために必要

public class Summoner : MonoBehaviour
{
    [Header("召喚するオブジェクト")]
    [Tooltip("召喚する球のプレハブ")]
    public GameObject ballPrefab;

    [Header("UI設定")]
    [Tooltip("マーカーとして表示するCanvas")]
    [SerializeField] private Canvas markerCanvas;

    [Header("召喚タイミング")]
    [Tooltip("召喚を実行する最短間隔（秒）")]
    public float minSummonInterval = 2f;
    [Tooltip("召喚を実行する最長間隔（秒）")]
    public float maxSummonInterval = 5f;

    void Start()
    {
        StartCoroutine(FindBossAndStartSummoning());
    }

    void Update()
    {
        // このスクリプトはコルーチンで動作するためUpdateは不要
    }

    private IEnumerator FindBossAndStartSummoning()
    {
        while (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            yield return new WaitForSeconds(1f);
        }

        if (markerCanvas != null)
        {
            markerCanvas.gameObject.SetActive(false);
        }
        else
        {
            //Debug.LogError("Marker CanvasがInspectorから設定されていません！");
        }

        StartCoroutine(SummoningLoopCoroutine());
    }

    private IEnumerator SummoningLoopCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(minSummonInterval, maxSummonInterval);
            yield return new WaitForSeconds(randomDelay);
            StartCoroutine(SummonSequenceCoroutine());
        }
    }

    private IEnumerator SummonSequenceCoroutine()
    {
        if (markerCanvas == null) yield break;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            //Debug.LogError("Playerが見つからなかったため、召喚シーケンスを中断します。");
            yield break;
        }

        // --- UIマーカーの表示と追従処理 ---
        markerCanvas.gameObject.SetActive(true);
        //Debug.Log("プレイヤーをターゲットに設定しました。");

        float markerVisibleTime = 0.5f;
        float timer = 0f;
        Vector3 lastKnownPosition = playerObj.transform.position; // 最後の座標を保存する変数

        // 0.5秒間、マーカーをプレイヤーの位置に追従させるループ
        while (timer < markerVisibleTime)
        {
            // ループ中にプレイヤーが破壊された場合に対応
            if (playerObj == null)
            {
                markerCanvas.gameObject.SetActive(false);
                yield break; // プレイヤーがいなくなったらコルーチンを中断
            }

            // マーカーの位置をプレイヤーの現在位置に更新し、その座標を保存
            lastKnownPosition = playerObj.transform.position;
            markerCanvas.transform.position = lastKnownPosition;

            timer += Time.deltaTime;
            yield return null; // 1フレーム待機
        }

        // 0.5秒経過後、マーカーを非表示にする
        markerCanvas.gameObject.SetActive(false);

        // --- 球の召喚処理 ---
        // さらに0.2秒待機 (合計0.7秒)
        yield return new WaitForSeconds(0.2f);

        // ★変更: マーカーが最後にあった座標に球を召喚する
        Instantiate(ballPrefab, lastKnownPosition, Quaternion.identity);
        //Debug.Log("プレイヤーの位置に球を召喚しました。");
    }
}