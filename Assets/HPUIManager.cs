using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    [SerializeField] private Slider HPslider;
    private Player targetPlayer; // �Q�Ƃ���Player�X�N���v�g

    void Update()
    {
        // �^�[�Q�b�g�̃v���C���[�����Ȃ��ꍇ�̂݁A�V�[������T��
        if (targetPlayer == null)
        {
            // "Player"�^�O�̕t�����I�u�W�F�N�g��T���A���̃I�u�W�F�N�g����Player�X�N���v�g���擾
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<Player>();
            }
        }

        // �^�[�Q�b�g�̃v���C���[��������Ȃ��ꍇ�̓X���C�_�[���\��
        if (targetPlayer == null)
        {
            HPslider.gameObject.SetActive(false);
            return;
        }

        // �v���C���[���������Ă�����A�X���C�_�[���A�N�e�B�u�ɂ���UI���X�V
        HPslider.gameObject.SetActive(true);
        UpdateHPUI();
    }

    // UI���X�V���郁�\�b�h
    public void UpdateHPUI()
    {
        // targetPlayer�̌��ݒl���g����UI���X�V
        HPslider.maxValue = targetPlayer.PlayerMAXHP;
        HPslider.value = targetPlayer.PlayerHP;
    }
}