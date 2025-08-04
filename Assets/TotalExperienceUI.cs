using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalExperienceUI : MonoBehaviour
{
    public TextMeshProUGUI TotalExperienceText;

    void Start()
    {
        TotalExperienceText.enabled = false;
    }
    // このオブジェクトが有効になった時に呼ばれる
    void OnEnable()
    {
        // Playerの静的イベントに、自分のUI表示メソッドを登録（購読）する
        Player.OnPlayerDied += ShowFinalExperience;
    }

    // このオブジェクトが無効になった時に呼ばれる
    void OnDisable()
    {
        // 登録を解除する（メモリリーク防止のため重要）
        Player.OnPlayerDied -= ShowFinalExperience;
    }

    /// <summary>
    /// Player.OnPlayerDiedイベントが発生した時に呼び出されるメソッド
    /// </summary>
    /// <param name="finalExperience">Playerから渡された総獲得経験値</param>
    public void ShowFinalExperience(float finalExperience)
    {
        // テキストUIを有効にする
        if (TotalExperienceText != null)
        {
            //TotalExperienceText.enabled = true;
            // 渡された経験値を整数にしてテキストに設定
            TotalExperienceText.text = "Total Experience: " + Mathf.FloorToInt(finalExperience);
        }
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Player playerComponent = player.GetComponent<Player>();
            TotalExperienceText.text = "Total Experience: " + playerComponent.ExperienceTotal;
        }
        else 
        {
            return;
        }
        
    }
}
