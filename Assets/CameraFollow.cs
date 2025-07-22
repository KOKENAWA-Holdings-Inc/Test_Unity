using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ターゲットのTransform。スクリプトが自動で探す
    private Transform target;

    // カメラとターゲットの間の距離を保持する変数
    private Vector3 offset;

    // 全てのUpdate処理が終わった後に呼ばれる
    void LateUpdate()
    {
        // まだターゲット（Player）を見つけていない場合
        if (target == null)
        {
            // "Player" タグが付いたオブジェクトを探す
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            // もし見つかったら
            if (playerObj != null)
            {
                // ターゲットとして設定
                target = playerObj.transform;

                // 見つけた瞬間に、カメラとターゲットの差分（オフセット）を計算して保存
                offset = transform.position - target.position;
            }
        }

        // ターゲットが見つかっているなら（今見つけた場合も含む）
        if (target != null)
        {
            // カメラの位置を「ターゲットの位置 + 保存したオフセット」に更新する
            transform.position = target.position + offset;
        }
    }
}