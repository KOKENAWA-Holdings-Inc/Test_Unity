using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    // 指定されたゲームオブジェクト（UIパネル）をアクティブにするメソッド
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    // 指定されたゲームオブジェクト（UIパネル）を非アクティブにするメソッド
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    // 指定されたゲームオブジェクト（UIパネル）の表示・非表示を切り替えるメソッド
    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}