using UnityEngine;
using UnityEngine.UI; // Text (UI) を使う場合
using TMPro; // TextMeshPro を使う場合

public class DamagePopup : MonoBehaviour
{
    public float displayDuration = 1f; // ダメージ表示時間
    public float moveSpeed = 1f; // 上昇速度
    public Color startColor = Color.white; // 開始時の色
    public Color endColor = new Color(1, 1, 1, 0); // 終了時の色 (透明)
    public Vector3 offset = new Vector3(0, 0.5f, 0); // 表示位置のオフセット

    private TextMeshProUGUI damageText; // TextMeshPro を使う場合
    // private Text damageText; // Text (UI) を使う場合

    private float timer;

    void Awake()
    {
        // GetComponentInChildren の方が、テキストが子オブジェクトでも安全に取得できます
        damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(float damageAmount)
    {
        // ★変更: 小数点以下を切り捨てて、きれいな整数で表示する
        damageText.text = Mathf.FloorToInt(damageAmount).ToString();
        timer = displayDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // 上昇
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // フェードアウト
        damageText.color = Color.Lerp(endColor, startColor, timer / displayDuration);

        if (timer <= 0)
        {
            Destroy(gameObject); // 表示時間経過で破棄
        }
    }
}
