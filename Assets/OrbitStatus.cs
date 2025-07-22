using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitStatus : MonoBehaviour
{
    private GameObject Boss;
    private GameObject Enemy;
    public int OrbitAttack = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手のオブジェクトのタグを比較
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager EnemyComponent = collision.gameObject.GetComponent<EnemyManager>();
            // 相手のタグが "Enemy" だった場合、コンソールにメッセージを出力
            //Debug.Log("エネミーに接触しました！");
            if (EnemyComponent != null)
            {
                EnemyComponent.EnemyHP = EnemyComponent.EnemyHP - OrbitAttack; // ダメージを受ける
                Debug.Log("Take " + OrbitAttack);

            }

            // ここにダメージ処理やノックバック処理などを追加できます

        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss BossComponent = collision.gameObject.GetComponent<Boss>();
            // 相手のタグが "Enemy" だった場合、コンソールにメッセージを出力
            //Debug.Log("エネミーに接触しました！");
            if (BossComponent != null)
            {
                BossComponent.BossHP = BossComponent.BossHP - OrbitAttack; // ダメージを受ける
                Debug.Log("Take " + OrbitAttack);

            }

            // ここにダメージ処理やノックバック処理などを追加できます

        }


    }
}