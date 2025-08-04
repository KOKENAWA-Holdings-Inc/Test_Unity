using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro���������߂ɕK�v

public class StatusUIManager : MonoBehaviour
{
    [Header("UI�Q��")]
    [Tooltip("�X�e�[�^�X�S�̂��͂ސe�p�l��")]
    [SerializeField] private GameObject statusPanel;
    [Tooltip("HP��\������e�L�X�g")]
    [SerializeField] private TextMeshProUGUI hpText;
    [Tooltip("�U���͂�\������e�L�X�g")]
    [SerializeField] private TextMeshProUGUI attackText;
    [Tooltip("�h��͂�\������e�L�X�g")]
    [SerializeField] private TextMeshProUGUI defenceText;
    [Tooltip("�ړ����x��\������e�L�X�g")]
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    private Player targetPlayer; // �Q�Ƃ���v���C���[
    private bool isPanelActive = false; // �p�l�����\������Ă��邩�ǂ����̏��

    void Start()
    {
        // �O�̂��߁A�J�n���͕K����\���ɂ��Ă���
        if (statusPanel != null)
        {
            statusPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Escape�L�[�������ꂽ�u�Ԃ����m
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���݂̏�Ԃ𔽓]������ (�\�� -> ��\��, ��\�� -> �\��)
            isPanelActive = !isPanelActive;

            if (isPanelActive)
            {
                ShowStatus();
            }
            else
            {
                HideStatus();
            }
        }
    }

    /// <summary>
    /// �X�e�[�^�X��\�����A�Q�[�����ꎞ��~����
    /// </summary>
    private void ShowStatus()
    {
        // ��ɍŐV�̃v���C���[����T��
        targetPlayer = FindObjectOfType<Player>();
        if (targetPlayer == null)
        {
            Debug.LogError("Player�I�u�W�F�N�g��������܂���I");
            isPanelActive = false; // �\���ł��Ȃ��̂ŏ�Ԃ�߂�
            return;
        }

        // �Q�[���̎��Ԃ��~�߂�
        Time.timeScale = 0f;
        // �p�l����\������
        statusPanel.SetActive(true);

        // �e�e�L�X�g�Ɍ��݂̃X�e�[�^�X�𔽉f������
        // ToString("F1") �Ȃǂ́A�����_�ȉ��̕\���������w�肷�鏑���ݒ�ł�
        hpText.text = $"HP: {targetPlayer.PlayerHP.ToString("F0")} / {targetPlayer.PlayerMAXHP.ToString("F0")}";
        attackText.text = $"Attack: {targetPlayer.Attack.ToString("F2")}";
        defenceText.text = $"Defence: {targetPlayer.Defence.ToString("F2")}";
        moveSpeedText.text = $"Move Speed: {targetPlayer.moveSpeed.ToString("F1")}";
    }

    /// <summary>
    /// �X�e�[�^�X���\���ɂ��A�Q�[�����ĊJ����
    /// </summary>
    private void HideStatus()
    {
        // �Q�[���̎��Ԃ����ɖ߂�
        Time.timeScale = 1f;
        // �p�l�����\���ɂ���
        statusPanel.SetActive(false);
    }
}