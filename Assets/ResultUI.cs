using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public Player Player;
    public GameManager GameManager;
    public TextMeshProUGUI ResultText;
    public Image BackGround;
    public TotalEnemyKill TotalEnemyKill;
    public TotalExperienceUI TotalExperienceUI;
    public ResultPlayerLvUI ResultPlayerLvUI;
    public TitleManager Title;
    // Start is called before the first frame update
    void Start()
    {
        ResultText.enabled = false;
        BackGround.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resultUIonlyPlayer() 
    {
        ResultText.enabled = true;
        BackGround.enabled = true;
        TotalEnemyKill.ResultKilledEnemyText.enabled = true;
        TotalExperienceUI.TotalExperienceText.enabled = true;
        ResultPlayerLvUI.ResultLvText.enabled = true;
        Title.titletext.enabled = true;
        ResultText.text = ("Stage Fail");
            GameManager.PauseGame();
        
            
        
    }

    void OnEnable()
    {
        // Bossの静的イベントに、自分のUI表示メソッドを登録（購読）する
        Boss.OnBossDied += ShowBossDefeatedUI;
        Player.OnPlayerDied += resultUIonlyPlayer;
    }

    // このオブジェクトが無効になった時に呼ばれる
    void OnDisable()
    {
        // 登録を解除する（メモリリーク防止のため重要）
        Boss.OnBossDied -= ShowBossDefeatedUI;
        Player.OnPlayerDied -= resultUIonlyPlayer;
    }

    public void ShowBossDefeatedUI()
    {
        //Debug.Log("ボス撃破のイベントを検知！UIを表示します。");
        // UI表示処理
        ResultText.enabled = true;
        BackGround.enabled = true;
        TotalEnemyKill.ResultKilledEnemyText.enabled = true;
        TotalExperienceUI.TotalExperienceText.enabled = true;
        ResultText.text = ("Stage Clear");
        GameManager.PauseGame();
    }
}
