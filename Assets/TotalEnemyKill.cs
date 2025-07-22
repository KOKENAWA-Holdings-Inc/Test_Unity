using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalEnemyKill : MonoBehaviour
{
    //public EnemyManager EnemyManager;
    private int KilledEnemy = 0;
    public TextMeshProUGUI ResultKilledEnemyText;
    // Start is called before the first frame update
    void Start()
    {
        ResultKilledEnemyText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        // Bossの静的イベントに、自分のUI表示メソッドを登録（購読）する
        EnemyManager.OnEnemyDied += TotalKilledEnemy;
    }

    // このオブジェクトが無効になった時に呼ばれる
    void OnDisable()
    {
        // 登録を解除する（メモリリーク防止のため重要）
        EnemyManager.OnEnemyDied -= TotalKilledEnemy;
    }

    public void TotalKilledEnemy() 
    {
        KilledEnemy++;
        ResultKilledEnemyText.text = ("Kill Enemy:"+KilledEnemy);
        //Debug.Log("now killed enemy is "+KilledEnemy);
    }
}
