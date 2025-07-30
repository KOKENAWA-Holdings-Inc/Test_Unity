using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    [SerializeField] private Slider CoolTimeslider;
    private PlayerLanceShooter targetPlayer;

    void Update()
    {
        // �Q�Ƃ��؂�Ă�����A�T���ɍs��
        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                // ������GetComponent�����݂�
                targetPlayer = playerObj.GetComponent<PlayerLanceShooter>();
            }
        }

        // ���ύX: �ēx�`�F�b�N��ǉ�
        // �ŏI�I��targetPlayer���L���ȎQ�Ƃ������Ă���ꍇ�̂݁AUI���X�V����
        if (targetPlayer != null)
        {
            UpdateCoolTimeUI();
        }
        // else
        // {
        //     // �v���C���[�����Ȃ��ꍇ�̓X���C�_�[���\���ɂ���Ȃ�
        //     CoolTimeslider.gameObject.SetActive(false);
        // }
    }

    public void UpdateCoolTimeUI()
    {
        CoolTimeslider.maxValue = targetPlayer.shootCooldown;
        float elapsedTime = Time.time - targetPlayer.lastShotTime;
        CoolTimeslider.value = Mathf.Min(elapsedTime, targetPlayer.shootCooldown);
    }
}