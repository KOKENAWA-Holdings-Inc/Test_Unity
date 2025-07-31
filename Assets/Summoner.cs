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
    public float minSummonInterval = 5f;
    [Tooltip("召喚を実行する最長間隔（秒）")]
    public float maxSummonInterval = 10f;

    void Start()
    {
        // ★変更: ボスを探し、見つかったらメイン処理を開始するコルーチンを起動
        StartCoroutine(FindBossAndStartSummoning());
    }

    void Update()
    {
        // このスクリプトはコルーチンで動作するためUpdateは不要
    }

    /// <summary>
    /// ★追加: ボスが出現するまで待機し、出現したらメインループを開始するコルーチン
    /// </summary>
    private IEnumerator FindBossAndStartSummoning()
    {
<<<<<<< HEAD
        //Debug.Log("�{�X��T���Ă��܂�...");
=======
        Debug.Log("ボスを探しています...");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b

        // ボスが見つかるまで1秒ごとに探し続ける
        while (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            yield return new WaitForSeconds(1f);
        }

<<<<<<< HEAD
        // --- �{�X������������A�������牺�̏����ݒ肪���s����� ---
        //Debug.Log("�{�X�𔭌��I�����������J�n���܂��B");
=======
        // --- ボスが見つかったら、ここから下の初期設定が実行される ---
        Debug.Log("ボスを発見！召喚処理を開始します。");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b

        if (markerCanvas != null)
        {
            markerCanvas.gameObject.SetActive(false);
        }
        else
        {
<<<<<<< HEAD
            //Debug.LogError("Marker Canvas��Inspector����ݒ肳��Ă��܂���I");
=======
            Debug.LogError("Marker CanvasがInspectorから設定されていません！");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
        }

        // メインの召喚ループを開始する
        StartCoroutine(SummoningLoopCoroutine());
    }

    /// <summary>
    /// 召喚処理をランダムな間隔で無限に繰り返すためのループ
    /// </summary>
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
<<<<<<< HEAD
            //Debug.LogError("Player��������Ȃ��������߁A�����V�[�P���X�𒆒f���܂��B");
=======
            Debug.LogError("Playerが見つからなかったため、召喚シーケンスを中断します。");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
            yield break;
        }

        Vector3 summonPosition = playerObj.transform.position;

        // --- UIマーカーの表示処理 ---
        markerCanvas.transform.position = summonPosition;
        markerCanvas.gameObject.SetActive(true);
<<<<<<< HEAD
        //Debug.Log("�v���C���[���^�[�Q�b�g�ɐݒ肵�܂����B");
=======
        Debug.Log("プレイヤーをターゲットに設定しました。");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b

        yield return new WaitForSeconds(0.5f);

        markerCanvas.gameObject.SetActive(false);

        // --- 球の召喚処理 ---
        yield return new WaitForSeconds(0.5f);

        Instantiate(ballPrefab, summonPosition, Quaternion.identity);
<<<<<<< HEAD
        //Debug.Log("�v���C���[�̈ʒu�ɋ����������܂����B");
=======
        Debug.Log("プレイヤーの位置に球を召喚しました。");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
    }
}
