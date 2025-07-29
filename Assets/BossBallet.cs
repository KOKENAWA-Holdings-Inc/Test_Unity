using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBallet : MonoBehaviour
{
    [SerializeField] private float Attack = 50f;

    void Start()
    {
        // 2�b��ɒe�������I�ɏ�����悤�ɂ���
        Destroy(gameObject, 2f);
    }

    // �����ɏՓ˂������ɌĂ΂��
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        Boss bossComponent = bossObj.GetComponent<Boss>();
        // �Փ˂������肪"Enemy"�^�O�������Ă�����
        if (other.CompareTag("Player")) // other.tag == "Enemy" �ł���
        {
            // ���C���_: �Փ˂�������(other)����EnemyManager�R���|�[�l���g���擾
            Player PlayerComponent = other.GetComponent<Player>();

            // ���ǉ�: EnemyManager���擾�ł����ꍇ�̂݃_���[�W��^����
            if (PlayerComponent != null)
            {
                if (Attack * (bossComponent.Attack * 0.5f) - PlayerComponent.Defence >= 1)
                {
                    PlayerComponent.PlayerHP = PlayerComponent.PlayerHP - (Attack * (bossComponent.Attack * 0.5f) - PlayerComponent.Defence);
                }
                else 
                {
                    PlayerComponent.PlayerHP -= 1f;
                }
                
                //Debug.Log(other.name + "�Ƀq�b�g�I�c��HP: " + EnemyComponent.EnemyHP);
            }


            // �G�ɓ���������e�������ꍇ�i�C�Ӂj
            // Destroy(gameObject);
        }
    }
}