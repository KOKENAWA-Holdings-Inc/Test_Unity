using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public Player Player;
    public GameManager GameManager;
    public TextMeshProUGUI ResultText;
    public Image BackGround;
    public TotalEnemyKill TotalEnemyKill;
    public TotalExperienceUI TotalExperienceUI;
    public ResultPlayerLvUI ResultPlayerLvUI;
    public TitleManager Title;
    // Start is called before the first frame update
    void Start()
    {
        ResultText.enabled = false;
        BackGround.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resultUIonlyPlayer() 
    {
        ResultText.enabled = true;
        BackGround.enabled = true;
        TotalEnemyKill.ResultKilledEnemyText.enabled = true;
        TotalExperienceUI.TotalExperienceText.enabled = true;
        ResultPlayerLvUI.ResultLvText.enabled = true;
        Title.titletext.enabled = true;
        ResultText.text = ("Stage Fail");
            GameManager.PauseGame();
        
            
        
    }

    void OnEnable()
    {
        // Boss�̐ÓI�C�x���g�ɁA������UI�\�����\�b�h��o�^�i�w�ǁj����
        Boss.OnBossDied += ShowBossDefeatedUI;
        Player.OnPlayerDied += resultUIonlyPlayer;
    }

    // ���̃I�u�W�F�N�g�������ɂȂ������ɌĂ΂��
    void OnDisable()
    {
        // �o�^����������i���������[�N�h�~�̂��ߏd�v�j
        Boss.OnBossDied -= ShowBossDefeatedUI;
        Player.OnPlayerDied -= resultUIonlyPlayer;
    }

    public void ShowBossDefeatedUI()
    {
        //Debug.Log("�{�X���j�̃C�x���g�����m�IUI��\�����܂��B");
        // UI�\������
        ResultText.enabled = true;
        BackGround.enabled = true;
        TotalEnemyKill.ResultKilledEnemyText.enabled = true;
        TotalExperienceUI.TotalExperienceText.enabled = true;
        ResultText.text = ("Stage Clear");
        GameManager.PauseGame();
    }
}
