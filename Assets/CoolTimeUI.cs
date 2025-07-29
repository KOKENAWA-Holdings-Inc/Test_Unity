using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    [SerializeField] private Slider CoolTimeslider;
    private PlayerLanceShooter targetPlayer; // �Q�Ƃ���Player�X�N���v�g

    void Update()
    {
        // �^�[�Q�b�g�̃v���C���[�����Ȃ��ꍇ�̂݁A�V�[������T��
        if (targetPlayer == null)
        {
            // "Player"�^�O�̕t�����I�u�W�F�N�g��T���A���̃I�u�W�F�N�g����Player�X�N���v�g���擾
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<PlayerLanceShooter>();
            }
        }

        // �^�[�Q�b�g�̃v���C���[��������Ȃ��ꍇ�̓X���C�_�[���\��
        if (targetPlayer == null)
        {
            CoolTimeslider.gameObject.SetActive(false);
            return;
        }

        // �v���C���[���������Ă�����A�X���C�_�[���A�N�e�B�u�ɂ���UI���X�V
        CoolTimeslider.gameObject.SetActive(true);
        UpdateCoolTimeUI();
    }

    // UI���X�V���郁�\�b�h
    // CoolTimeUI.cs
    public void UpdateCoolTimeUI()
    {
        // �X���C�_�[�̍ő�l���A�v���C���[�̃N�[���_�E�����Ԃɐݒ�
        CoolTimeslider.maxValue = targetPlayer.shootCooldown;

        // �X���C�_�[�̌��݂̒l���A�u���ݎ��� - �Ō�Ɍ��������ԁv�ɂ���
        // �������A�l���ő�l�𒴂��Ȃ��悤��Mathf.Min���g���Ƃ����S
        float elapsedTime = Time.time - targetPlayer.lastShotTime;
        CoolTimeslider.value = Mathf.Min(elapsedTime, targetPlayer.shootCooldown);
    }
}
