using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUIManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    private Boss targetBoss; // �Q�Ƃ���Boss�X�N���v�g

    void Update()
    {
        // �^�[�Q�b�g�̃{�X�����Ȃ��ꍇ�̂݁A�V�[������T��
        if (targetBoss == null)
        {
            // "Boss"�^�O�̕t�����I�u�W�F�N�g��T���A���̃I�u�W�F�N�g����Boss�X�N���v�g���擾
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null)
            {
                targetBoss = bossObj.GetComponent<Boss>();
            }
        }

        // �^�[�Q�b�g�̃{�X��������Ȃ��A�܂���HP��0�Ŕj�󂳂ꂽ�ꍇ�̓X���C�_�[���\��
        if (targetBoss == null)
        {
            hpSlider.gameObject.SetActive(false);
            return;
        }

        // �{�X���������Ă�����A�X���C�_�[���A�N�e�B�u�ɂ���UI���X�V
        hpSlider.gameObject.SetActive(true);
        UpdateBossHPUI();
    }

    // UI���X�V���郁�\�b�h
    public void UpdateBossHPUI()
    {
        // targetBoss�̌��ݒl���g����UI���X�V
        hpSlider.maxValue = targetBoss.BossMAXHP;
        hpSlider.value = targetBoss.BossHP;
    }
}