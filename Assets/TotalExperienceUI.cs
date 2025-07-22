using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalExperienceUI : MonoBehaviour
{
    public TextMeshProUGUI TotalExperienceText;
    public EnemyManager EnemyManager;
    public Boss Boss;
    private int TotalExperienceCount;
    // Start is called before the first frame update
    void Start()
    {
        TotalExperienceText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // Boss�̐ÓI�C�x���g�ɁA������UI�\�����\�b�h��o�^�i�w�ǁj����
        EnemyManager.OnEnemyDied += TotalExperience;
        Boss.OnBossDied += BossTotalExperience;
    }

    // ���̃I�u�W�F�N�g�������ɂȂ������ɌĂ΂��
    void OnDisable()
    {
        // �o�^����������i���������[�N�h�~�̂��ߏd�v�j
        EnemyManager.OnEnemyDied -= TotalExperience;
        Boss.OnBossDied -= BossTotalExperience;
    }

    public void TotalExperience()
    {
        TotalExperienceCount = TotalExperienceCount + EnemyManager.EnemyExperience;
        TotalExperienceText.text = ("Total Experience:" + TotalExperienceCount);
        //Debug.Log("now killed enemy is " + KilledEnemy);
    }

    public void BossTotalExperience() 
    {
        TotalExperienceCount = TotalExperienceCount + Boss.BossExperience;
        TotalExperienceText.text = ("Total Experience:" + TotalExperienceCount);
    }
}
