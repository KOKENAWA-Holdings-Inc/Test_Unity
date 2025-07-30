using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI���������߂ɕK�v

public class ExperienceUIManager : MonoBehaviour
{
    [Header("UI�Q��")]
    [SerializeField] private Slider experienceSlider; // Inspector����ݒ肷��o���l�X���C�_�[

    [Header("�Ď��Ώ�")]
    private Player targetPlayer; // �V�[�����̃v���C���[���i�[����ϐ�

    void Start()
    {
        // �V�[�����ɑ��݂���Player�R���|�[�l���g��T���āAtargetPlayer�ϐ��Ɋi�[����
        targetPlayer = FindObjectOfType<Player>();

        // Player�܂���Slider��������Ȃ��ꍇ�́A�G���[���O���o���ď������~�߂�
        if (targetPlayer == null)
        {
            Debug.LogError("�V�[����Player�R���|�[�l���g�����I�u�W�F�N�g��������܂���I");
            this.enabled = false; // ���̃X�N���v�g�𖳌��ɂ���
            return;
        }

        if (experienceSlider == null)
        {
            Debug.LogError("Experience Slider��Inspector����ݒ肳��Ă��܂���I");
            this.enabled = false; // ���̃X�N���v�g�𖳌��ɂ���
            return;
        }
    }

    void Update()
    {
        // Player��Slider������ɐݒ肳��Ă���ꍇ�̂݁A���t���[���l���X�V����
        if (targetPlayer != null && experienceSlider != null)
        {
            // �X���C�_�[�̍ő�l���A���x���A�b�v�ɕK�v�Ȍo���l��(ExperiencePool)�ɐݒ�
            experienceSlider.maxValue = targetPlayer.ExperiencePool;

            // �X���C�_�[�̌��݂̒l���A���ݗ��܂��Ă���o���l��(ExperiencePoint)�ɐݒ�
            experienceSlider.value = targetPlayer.ExperiencePoint;
        }
    }
}