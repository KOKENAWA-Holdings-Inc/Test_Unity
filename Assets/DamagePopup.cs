using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI; // Text (UI) ���g���ꍇ
using TMPro; // TextMeshPro ���g���ꍇ

public class DamagePopup : MonoBehaviour
{
    public float displayDuration = 1f; // �_���[�W�\������
    public float moveSpeed = 1f; // �㏸���x
    public Color startColor = Color.white; // �J�n���̐F
    public Color endColor = new Color(1, 1, 1, 0); // �I�����̐F (����)
    public Vector3 offset = new Vector3(0, 0.5f, 0); // �\���ʒu�̃I�t�Z�b�g

    private TextMeshProUGUI damageText; // TextMeshPro ���g���ꍇ
    // private Text damageText; // Text (UI) ���g���ꍇ
=======
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
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b

    private float timer;

    void Awake()
    {
<<<<<<< HEAD
        // GetComponentInChildren �̕����A�e�L�X�g���q�I�u�W�F�N�g�ł����S�Ɏ擾�ł��܂�
        damageText = GetComponentInChildren<TextMeshProUGUI>();
=======
        damageText = GetComponent<TextMeshProUGUI>(); // TextMeshPro を使う場合
        // damageText = GetComponent<Text>(); // Text (UI) を使う場合
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
    }

    public void Setup(float damageAmount)
    {
<<<<<<< HEAD
        // ���ύX: �����_�ȉ���؂�̂ĂāA���ꂢ�Ȑ����ŕ\������
        damageText.text = Mathf.FloorToInt(damageAmount).ToString();
        timer = displayDuration;
=======
        damageText.text = damageAmount.ToString();
        timer = displayDuration;
        // 初期位置を調整 (例: 敵の頭上など)
        // transform.position = transform.parent.position + offset; // 親要素がある場合
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
    }

    void Update()
    {
        timer -= Time.deltaTime;

<<<<<<< HEAD
        // �㏸
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // �t�F�[�h�A�E�g
=======
        // 上昇
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // フェードアウト
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
        damageText.color = Color.Lerp(endColor, startColor, timer / displayDuration);

        if (timer <= 0)
        {
<<<<<<< HEAD
            Destroy(gameObject); // �\�����Ԍo�߂Ŕj��
=======
            Destroy(gameObject); // 表示時間経過で破棄
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
        }
    }
}
