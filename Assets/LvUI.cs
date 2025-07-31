using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LvUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LvText; // ��SerializeField�ɂ��ăC���X�y�N�^�[����ݒ�
    private Player targetPlayer; // �Q�Ƃ���Player�X�N���v�g

    // ���ݕ\�����Ă��郌�x�����L�^���Ă����ϐ�
    private int currentDisplayedLv = -1;

    void Update()
    {
        // �^�[�Q�b�g�̃v���C���[���܂������Ă��Ȃ��ꍇ�A�T��
        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<Player>();
            }
        }

        // �^�[�Q�b�g�̃v���C���[��������Ȃ��ꍇ��UI���\���ɂ��ď������I����
        if (targetPlayer == null)
        {
            LvText.gameObject.SetActive(false);
            return;
        }

        // --- �v���C���[�����������ꍇ�̏��� ---

        // UI���m���ɕ\����Ԃɂ���
        LvText.gameObject.SetActive(true);

        // ���v���C���[�̃��x�����A���ݕ\�����Ă��郌�x���ƈقȂ�ꍇ�̂݃e�L�X�g���X�V
        if (targetPlayer.PlayerLv != currentDisplayedLv)
        {
            UpdateLvText();
        }
    }

    // �e�L�X�g���X�V���鏈��
    private void UpdateLvText()
    {
        // �e�L�X�g���v���C���[�̌��݂̃��x���ōX�V
        LvText.text = "Lv" + targetPlayer.PlayerLv;

        // ���ݕ\�����Ă��郌�x���̒l���X�V
        currentDisplayedLv = (int)targetPlayer.PlayerLv;
    }
}
