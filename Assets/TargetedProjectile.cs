using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedProjectile : MonoBehaviour
{
    // Inspectorから召喚したいオブジェクトのプレハブを設定
    public GameObject objectToSummon;

    private Vector3 targetPosition;
    private float speed;
    private bool isInitialized = false;

    /// <summary>
    /// プレイヤーから目標地点と速度を受け取るための初期化メソッド
    /// </summary>
    public void Initialize(Vector3 target, float projectileSpeed)
    {
        this.targetPosition = target;
        this.speed = projectileSpeed;
        this.isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            OnArrive();
        }
    }

    /// <summary>
    /// 到着した時の処理
    /// </summary>
    private void OnArrive()
    {
        if (objectToSummon != null)
        {
            // ★変更点1：生成したオブジェクトを一時的な変数に格納する
            GameObject summonedObject = Instantiate(objectToSummon, transform.position, Quaternion.identity);

            // ★変更点2：格納したオブジェクトを0.2秒後に破壊する
            Destroy(summonedObject, 0.2f);
        }

        // 自身のゲームオブジェクト（弾）は即座に破壊する
        Destroy(gameObject);
    }
}
