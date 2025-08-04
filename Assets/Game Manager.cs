using System.Collections.Generic;
using System.Linq; // 念のため追加
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    public bool IsPaused => isPaused;
    private int playerLvre;
    // public Player player; // ★ 不要なので削除
    private Player targetPlayer; // ★ 自動で検出するための変数

    public GameObject PassiveUI; // これらもpassiveChoiceUIParentで管理推奨
    public GameObject PassiveUI1; //
    public string selectedItem1;
    public string selectedItem2;

    [Header("UI参照")]
    public GameObject passiveChoiceUIParent;
    public TextMeshProUGUI RandomPassive1;
    public TextMeshProUGUI RandomPassive2;
    public TextMeshProUGUI Choice;

    [Header("選択肢の管理")]
    public string[] masterItemChoices = { "Attack+10%", "Defence+5%", "MaxHP+5%", "MoveSpeed+0.1", "Cool Time-2%", "Weapon Speed+3%" };
    private List<string> remainingChoices;

    void Start()
    {
        // 最初はUIを非表示にしておく
        if (passiveChoiceUIParent != null)
        {
            passiveChoiceUIParent.SetActive(false);
        }  

        // ★ プレイヤーを探し、見つけたらレベル監視を始めるコルーチンを開始
        StartCoroutine(InitializeAndMonitorPlayer());
    }

    // ★ プレイヤーの初期化とレベル監視を全て行うコルーチン
    private IEnumerator InitializeAndMonitorPlayer()
    {
        // --- 1. プレイヤーが見つかるまで待機 ---
        while (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<Player>();
            }
            yield return null; // 1フレーム待ってから再検索
        }

        // --- 2. プレイヤーが見つかったら初期設定を行う ---
        playerLvre = (int)targetPlayer.PlayerLv;
        ResetRemainingChoices();
        //Debug.Log("プレイヤーを検出しました。レベル監視を開始します。");

        // --- 3. レベルアップの監視ループ（Updateの代わり） ---
        while (true)
        {
            // isPausedがfalseの時だけレベルアップを検知する
            if (!isPaused)
            {
                if (targetPlayer.PlayerLv != playerLvre)
                {
                    DisplayUniqueChoices();
                    PauseGame();
                    playerLvre = (int)targetPlayer.PlayerLv;

                    if (passiveChoiceUIParent != null)
                    {
                        passiveChoiceUIParent.SetActive(true);
                        PassiveUI.SetActive(true);
                        PassiveUI1.SetActive(true);
                    }
                        
                }
            }
            yield return null; // 1フレーム待つ
        }
    }

    // void Update() はコルーチンに統合されたため不要

    public void ResetRemainingChoices()
    {
        remainingChoices = new List<string>(masterItemChoices);
    }

    public void DisplayUniqueChoices()
    {
        ResetRemainingChoices();

        int randomIndex1 = Random.Range(0, remainingChoices.Count);
        selectedItem1 = remainingChoices[randomIndex1];
        remainingChoices.RemoveAt(randomIndex1);

        int randomIndex2 = Random.Range(0, remainingChoices.Count);
        selectedItem2 = remainingChoices[randomIndex2];

        RandomPassive1.text = selectedItem1;
        RandomPassive2.text = selectedItem2;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (passiveChoiceUIParent != null)
        {
            passiveChoiceUIParent.SetActive(false);
        }
            
    }
}