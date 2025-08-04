using UnityEngine;
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

    private float timer;

    void Awake()
    {
        // GetComponentInChildren �̕����A�e�L�X�g���q�I�u�W�F�N�g�ł����S�Ɏ擾�ł��܂�
        damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(float damageAmount)
    {
        // ���ύX: �����_�ȉ���؂�̂ĂāA���ꂢ�Ȑ����ŕ\������
        damageText.text = Mathf.FloorToInt(damageAmount).ToString();
        timer = displayDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // �㏸
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // �t�F�[�h�A�E�g
        damageText.color = Color.Lerp(endColor, startColor, timer / displayDuration);

        if (timer <= 0)
        {
            Destroy(gameObject); // �\�����Ԍo�߂Ŕj��
        }
    }
}
