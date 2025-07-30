using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EliteBallet : MonoBehaviour
{
    // ���ǉ�: �����𔭎˂���EliteManager���i�[���邽�߂̕ϐ�
    public EliteManager shooter;

    [SerializeField] private float Attack = 5f;

    void Start()
    {
        // 2�b��ɒe�������I�ɏ�����悤�ɂ���
        Destroy(gameObject, 2f);
    }

    // �����ɏՓ˂������ɌĂ΂��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������� �폜 ����������
        // GameObject eliteObj = GameObject.FindGameObjectWithTag("Elite");
        // EliteManager EliteComponent = eliteObj.GetComponent<EliteManager>();
        // ���������� �폜 ����������

        // �Փ˂������肪"Player"�^�O�������Ă�����
        if (other.CompareTag("Player"))
        {
            Player PlayerComponent = other.GetComponent<Player>();

            // shooter���Z�b�g����Ă���APlayer�����������ꍇ
            if (PlayerComponent != null && shooter != null)
            {
                // ���ύX: EliteComponent�̑���ɁA�ێ����Ă�����shooter���g��
                if (Attack * (shooter.Attack * 0.5f) - PlayerComponent.Defence >= 1)
                {
                    PlayerComponent.PlayerHP = PlayerComponent.PlayerHP - (Attack * (shooter.Attack * 0.5f) - PlayerComponent.Defence);
                }
                else
                {
                    PlayerComponent.PlayerHP -= 1f;
                }
                Debug.Log("hit");
            }
        }
    }
}