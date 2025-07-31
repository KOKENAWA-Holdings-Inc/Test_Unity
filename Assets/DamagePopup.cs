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
        damageText = GetComponent<TextMeshProUGUI>(); // TextMeshPro を使う場合
        // damageText = GetComponent<Text>(); // Text (UI) を使う場合
    }

    public void Setup(float damageAmount)
    {
        damageText.text = damageAmount.ToString();
        timer = displayDuration;
        // 初期位置を調整 (例: 敵の頭上など)
        // transform.position = transform.parent.position + offset; // 親要素がある場合
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
