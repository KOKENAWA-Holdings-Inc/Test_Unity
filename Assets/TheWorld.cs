using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
    public GameManager GameManager;
    // public Player player; // ★ 不要なので削除

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Theworld()
    {
        // ★ このメソッドが呼ばれた瞬間に "Player" タグでプレイヤーを探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーが見つからなかった場合は、エラーログを出して処理を中断する
        if (playerObj == null)
        {
            Debug.LogError("Playerオブジェクトが見つかりませんでした。");
            return;
        }

        // 見つかったオブジェクトからPlayerスクリプトを取得する
        Player playerComponent = playerObj.GetComponent<Player>();
        if (playerComponent == null)
        {
            Debug.LogError("PlayerオブジェクトにPlayerスクリプトがアタッチされていません。");
            return;
        }


        // ★★★ これ以降、'player' の代わりに 'playerComponent' を使う ★★★
        switch (GameManager.selectedItem1)
        {
            case "attack":
                playerComponent.Attack++;
                break;
            case "defence":
                playerComponent.Defence++;
                break;
            case "HP":
                // ロジックを改善: 最大HPを増やし、現在HPを最大値にする
                playerComponent.PlayerMAXHP += 5;
                playerComponent.PlayerHP = playerComponent.PlayerMAXHP;
                break;
            case "luck":
                playerComponent.Luck++;
                break;
        }

        GameManager.ResumeGame();
        GameManager.PassiveUI.SetActive(false);
        GameManager.PassiveUI1.SetActive(false);
    }
}