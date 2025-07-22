using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BossIndicator : MonoBehaviour
{
    private Transform player;
    private Transform boss;
    private TextMeshProUGUI uiText;
    private RectTransform uiRectTransform;
    private Camera mainCamera;

    public Vector2 hideArea = new Vector2(9f, 5f);
    public float screenPadding = 100f;

    // ������ UI����ʒ[��������ֈړ������鋗����ǉ� ������
    public float inwardOffset = 30f;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        uiRectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        uiText.enabled = false;

        if (boss == null)
        {
            Debug.Log("Start���_�ł́Aboss��null�ł��B����͐���ȏ�Ԃł��B");
        }
        else
        {
            Debug.Log("�x��: Start���_�ŁAboss�ɉ��炩�̒l�������Ă��܂��I���ꂪ�����ł��B");
        }
    }

    void Update()
    {
        // �v���C���[�ƃ{�X��T������ (�ύX�Ȃ�)
        if (player == null)
        {
            // ������ ���̃��O���\������邩�m�F���Ă������� ������
            //Debug.LogError("Player��������Ȃ����߁A�C���W�P�[�^�[�̏����𒆒f���܂��BPlayer�̃^�O���m�F���Ă��������B");
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // ���t���[���T���ɍs���悤�ɏC��
            return;
        }
        if (boss == null)
        {
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null) boss = bossObj.transform;
            else { uiText.enabled = false; return; }
            Debug.Log("Boss Spawned");
        }

        // �\��/��\���̔��� (�ύX�Ȃ�)
        Vector2 relativePosition = boss.position - player.position;
        bool isBossInsideArea = Mathf.Abs(relativePosition.x) <= hideArea.x &&
                                Mathf.Abs(relativePosition.y) <= hideArea.y;
        uiText.enabled = !isBossInsideArea;

        if (uiText.enabled)
        {
            // --- UI�̌������v�Z ---
            transform.up = relativePosition.normalized;

            // --- UI�̈ʒu����ʒ[�Ɍv�Z ---
            // (���̃u���b�N�͑O��̏C���Ɠ����ł�)
            Vector3 bossScreenPos = mainCamera.WorldToScreenPoint(boss.position);
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            if (bossScreenPos.z < 0)
            {
                bossScreenPos = center + (center - bossScreenPos);
            }
            Vector3 dir = bossScreenPos - center;
            float divX = ((Screen.width / 2) - screenPadding) / Mathf.Abs(dir.x);
            float divY = ((Screen.height / 2) - screenPadding) / Mathf.Abs(dir.y);
            float scale = Mathf.Min(divX, divY);
            Vector3 edgePosition = center + dir * scale;

            // ������ �������炪�ǉ������������̏��� ������
            // ��ʒ[�̈ʒu(edgePosition)����v���C���[�̉�ʏ�̈ʒu�֌������x�N�g�����v�Z
            Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
            Vector3 directionToPlayer = (playerScreenPos - edgePosition).normalized;

            // ��ʒ[�̈ʒu����A�v���C���[������ inwardOffset �������ړ�������
            Vector3 finalPosition = edgePosition + directionToPlayer * inwardOffset;

            // �ŏI�I�Ȉʒu��K�p
            uiRectTransform.position = finalPosition;
        }
    }
}
