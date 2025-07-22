using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI titletext;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        titletext.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // Bossの静的イベントに、自分のUI表示メソッドを登録（購読）する
        Boss.OnBossDied += TitleUI;
    }

    // このオブジェクトが無効になった時に呼ばれる
    void OnDisable()
    {
        // 登録を解除する（メモリリーク防止のため重要）
        Boss.OnBossDied -= TitleUI;
    }

    public void TitleUI() 
    {
        titletext.enabled = true;
    }
    public void TitleButto()
    {
        GameManager.ResumeGame();
        //Debug.Log("ok");
        SceneManager.LoadScene("Start Scene");

    }
}
