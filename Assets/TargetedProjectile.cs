using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedProjectile : MonoBehaviour
{
    // Inspector���珢���������I�u�W�F�N�g�̃v���n�u��ݒ�
    public GameObject objectToSummon;

    private Vector3 targetPosition;
    private float speed;
    private bool isInitialized = false;

    /// <summary>
    /// �v���C���[����ڕW�n�_�Ƒ��x���󂯎�邽�߂̏��������\�b�h
    /// </summary>
    public void Initialize(Vector3 target, float projectileSpeed)
    {
        this.targetPosition = target;
        this.speed = projectileSpeed;
        this.isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            OnArrive();
        }
    }

    /// <summary>
    /// �����������̏���
    /// </summary>
    private void OnArrive()
    {
        if (objectToSummon != null)
        {
            // ���ύX�_1�F���������I�u�W�F�N�g���ꎞ�I�ȕϐ��Ɋi�[����
            GameObject summonedObject = Instantiate(objectToSummon, transform.position, Quaternion.identity);

            // ���ύX�_2�F�i�[�����I�u�W�F�N�g��0.2�b��ɔj�󂷂�
            Destroy(summonedObject, 0.2f);
        }

        // ���g�̃Q�[���I�u�W�F�N�g�i�e�j�͑����ɔj�󂷂�
        Destroy(gameObject);
    }
}
