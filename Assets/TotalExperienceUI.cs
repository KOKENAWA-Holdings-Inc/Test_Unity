using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalExperienceUI : MonoBehaviour
{
    public TextMeshProUGUI TotalExperienceText;

    void Start()
    {
        TotalExperienceText.enabled = false;
    }
    // ���̃I�u�W�F�N�g���L���ɂȂ������ɌĂ΂��
    void OnEnable()
    {
        // Player�̐ÓI�C�x���g�ɁA������UI�\�����\�b�h��o�^�i�w�ǁj����
        Player.OnPlayerDied += ShowFinalExperience;
    }

    // ���̃I�u�W�F�N�g�������ɂȂ������ɌĂ΂��
    void OnDisable()
    {
        // �o�^����������i���������[�N�h�~�̂��ߏd�v�j
        Player.OnPlayerDied -= ShowFinalExperience;
    }

    /// <summary>
    /// Player.OnPlayerDied�C�x���g�������������ɌĂяo����郁�\�b�h
    /// </summary>
    /// <param name="finalExperience">Player����n���ꂽ���l���o���l</param>
    public void ShowFinalExperience(float finalExperience)
    {
        // �e�L�X�gUI��L���ɂ���
        if (TotalExperienceText != null)
        {
            //TotalExperienceText.enabled = true;
            // �n���ꂽ�o���l�𐮐��ɂ��ăe�L�X�g�ɐݒ�
            TotalExperienceText.text = "Total Experience: " + Mathf.FloorToInt(finalExperience);
        }
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Player playerComponent = player.GetComponent<Player>();
            TotalExperienceText.text = "Total Experience: " + playerComponent.ExperienceTotal;
        }
        else 
        {
            return;
        }
        
    }
}
