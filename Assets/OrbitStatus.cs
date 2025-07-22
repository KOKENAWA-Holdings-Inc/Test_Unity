using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitStatus : MonoBehaviour
{
    private GameObject Boss;
    private GameObject Enemy;
    public int OrbitAttack = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂�������̃I�u�W�F�N�g�̃^�O���r
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager EnemyComponent = collision.gameObject.GetComponent<EnemyManager>();
            // ����̃^�O�� "Enemy" �������ꍇ�A�R���\�[���Ƀ��b�Z�[�W���o��
            //Debug.Log("�G�l�~�[�ɐڐG���܂����I");
            if (EnemyComponent != null)
            {
                EnemyComponent.EnemyHP = EnemyComponent.EnemyHP - OrbitAttack; // �_���[�W���󂯂�
                Debug.Log("Take " + OrbitAttack);

            }

            // �����Ƀ_���[�W������m�b�N�o�b�N�����Ȃǂ�ǉ��ł��܂�

        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss BossComponent = collision.gameObject.GetComponent<Boss>();
            // ����̃^�O�� "Enemy" �������ꍇ�A�R���\�[���Ƀ��b�Z�[�W���o��
            //Debug.Log("�G�l�~�[�ɐڐG���܂����I");
            if (BossComponent != null)
            {
                BossComponent.BossHP = BossComponent.BossHP - OrbitAttack; // �_���[�W���󂯂�
                Debug.Log("Take " + OrbitAttack);

            }

            // �����Ƀ_���[�W������m�b�N�o�b�N�����Ȃǂ�ǉ��ł��܂�

        }


    }
}