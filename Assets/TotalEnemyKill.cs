using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalEnemyKill : MonoBehaviour
{
    //public EnemyManager EnemyManager;
    private int KilledEnemy = 0;
    public TextMeshProUGUI ResultKilledEnemyText;
    // Start is called before the first frame update
    void Start()
    {
        ResultKilledEnemyText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        // Boss�̐ÓI�C�x���g�ɁA������UI�\�����\�b�h��o�^�i�w�ǁj����
        EnemyManager.OnEnemyDied += TotalKilledEnemy;
    }

    // ���̃I�u�W�F�N�g�������ɂȂ������ɌĂ΂��
    void OnDisable()
    {
        // �o�^����������i���������[�N�h�~�̂��ߏd�v�j
        EnemyManager.OnEnemyDied -= TotalKilledEnemy;
    }

    public void TotalKilledEnemy() 
    {
        KilledEnemy++;
        ResultKilledEnemyText.text = ("Kill Enemy:"+KilledEnemy);
        //Debug.Log("now killed enemy is "+KilledEnemy);
    }
}
