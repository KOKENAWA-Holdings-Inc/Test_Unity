using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // �^�[�Q�b�g��Transform�B�X�N���v�g�������ŒT��
    private Transform target;

    // �J�����ƃ^�[�Q�b�g�̊Ԃ̋�����ێ�����ϐ�
    private Vector3 offset;

    // �S�Ă�Update�������I�������ɌĂ΂��
    void LateUpdate()
    {
        // �܂��^�[�Q�b�g�iPlayer�j�������Ă��Ȃ��ꍇ
        if (target == null)
        {
            // "Player" �^�O���t�����I�u�W�F�N�g��T��
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            // ��������������
            if (playerObj != null)
            {
                // �^�[�Q�b�g�Ƃ��Đݒ�
                target = playerObj.transform;

                // �������u�ԂɁA�J�����ƃ^�[�Q�b�g�̍����i�I�t�Z�b�g�j���v�Z���ĕۑ�
                offset = transform.position - target.position;
            }
        }

        // �^�[�Q�b�g���������Ă���Ȃ�i���������ꍇ���܂ށj
        if (target != null)
        {
            // �J�����̈ʒu���u�^�[�Q�b�g�̈ʒu + �ۑ������I�t�Z�b�g�v�ɍX�V����
            transform.position = target.position + offset;
        }
    }
}