using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld1 : MonoBehaviour
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
        PlayerLanceShooter lanceShooterComponent = playerObj.GetComponent<PlayerLanceShooter>();
        OrbitManager OrbitComponent = playerObj.GetComponent<OrbitManager>();
        if (playerComponent == null)
        {
            Debug.LogError("PlayerオブジェクトにPlayerスクリプトがアタッチされていません。");
            return;
        }


        // ★★★ これ以降、'player' の代わりに 'playerComponent' を使う ★★★
        switch (GameManager.selectedItem2)
        {
            case "Attack+10%":
                playerComponent.Attack = playerComponent.Attack * 1.1f;
                break;
            case "Defence+5%":
                playerComponent.Defence = playerComponent.Defence * 1.05f;
                break;
            case "MaxHP+5%":
                // ロジックを改善: 最大HPを増やし、現在HPを最大値にする
                playerComponent.PlayerMAXHP = playerComponent.PlayerMAXHP * 1.05f;
                playerComponent.PlayerHP = playerComponent.PlayerMAXHP;
                break;
            case "MoveSpeed+0.1":
                playerComponent.moveSpeed += 0.1f;
                break;
            case "Cool Time-10%":
                OrbitComponent.respawnDelay = OrbitComponent.respawnDelay * 0.9f;
                lanceShooterComponent.shootCooldown = lanceShooterComponent.shootCooldown * 0.9f;
                break;
            case "Weapon Speed+30%":
                OrbitComponent.orbitSpeed = OrbitComponent.orbitSpeed * 1.3f;
                lanceShooterComponent.bulletSpeed = lanceShooterComponent.bulletSpeed * 1.3f;
                break;
        }

        GameManager.ResumeGame();
        GameManager.PassiveUI.SetActive(false);
        GameManager.PassiveUI1.SetActive(false);
    }
}