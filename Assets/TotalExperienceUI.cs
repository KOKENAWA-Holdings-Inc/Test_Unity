using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalExperienceUI : MonoBehaviour
{
    public TextMeshProUGUI TotalExperienceText;
    public EnemyManager EnemyManager;
    public Boss Boss;
    private int TotalExperienceCount;
    // Start is called before the first frame update
    void Start()
    {
        TotalExperienceText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // Bossの静的イベントに、自分のUI表示メソッドを登録（購読）する
        EnemyManager.OnEnemyDied += TotalExperience;
        Boss.OnBossDied += BossTotalExperience;
    }

    // このオブジェクトが無効になった時に呼ばれる
    void OnDisable()
    {
        // 登録を解除する（メモリリーク防止のため重要）
        EnemyManager.OnEnemyDied -= TotalExperience;
        Boss.OnBossDied -= BossTotalExperience;
    }

    public void TotalExperience()
    {
        TotalExperienceCount = TotalExperienceCount + EnemyManager.EnemyExperience;
        TotalExperienceText.text = ("Total Experience:" + TotalExperienceCount);
        //Debug.Log("now killed enemy is " + KilledEnemy);
    }

    public void BossTotalExperience() 
    {
        TotalExperienceCount = TotalExperienceCount + Boss.BossExperience;
        TotalExperienceText.text = ("Total Experience:" + TotalExperienceCount);
    }
}
