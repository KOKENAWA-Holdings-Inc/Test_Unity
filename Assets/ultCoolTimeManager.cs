using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ultCoolTimeManager : MonoBehaviour
{
    [SerializeField] private Slider ultChargeSlider;
    private PlayerUltShooter PlayerUltShooter; // �ϐ��錾�͂��̂܂�

    void Start()
    {
        // ���ǉ�: �V�[��������PlayerUltShooter�R���|�[�l���g��T���ĕϐ��ɑ������
        PlayerUltShooter = FindObjectOfType<PlayerUltShooter>();

        // ���ǉ�: ����������Ȃ������ꍇ�̃G���[����
        if (PlayerUltShooter == null)
        {
            Debug.LogError("�V�[����PlayerUltShooter�R���|�[�l���g��������܂���I");
            return; // �����𒆒f
        }

        // PlayerUltShooter�𐳂����擾������ɁA�X���C�_�[�̏����ݒ���s��
        if (ultChargeSlider != null)
        {
            ultChargeSlider.maxValue = PlayerUltShooter.maxUltCharge;
            ultChargeSlider.value = PlayerUltShooter.currentUltCharge;
        }
    }

    void Update()
    {
        // PlayerUltShooter��null�łȂ���Βl���X�V����
        if (ultChargeSlider != null && PlayerUltShooter != null)
        {
            ultChargeSlider.value = PlayerUltShooter.currentUltCharge;
        }
    }
}