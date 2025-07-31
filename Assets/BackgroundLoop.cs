using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    // �J������Transform
    private Transform cameraTransform;

    // �X�v���C�g�̕��ƍ���
    private float spriteWidth;
    private float spriteHeight;

    // �O���b�h�̑傫���i�����3x3�j
    private const int GridSize = 3;

    void Start()
    {
        // ���C���J������Transform���擾
        cameraTransform = Camera.main.transform;

        // ���̃I�u�W�F�N�g�̃X�v���C�g�̕��ƍ������擾
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
        spriteHeight = spriteRenderer.sprite.bounds.size.y;
    }

    void LateUpdate()
    {
        // === ���������̃��[�v�`�F�b�N ===
        // �J���������̔w�i�����E�ɑ傫���ړ�������
        if (transform.position.x + spriteWidth < cameraTransform.position.x)
        {
            // �w�i���E�[�Ɉړ�������
            transform.position += new Vector3(spriteWidth * GridSize, 0, 0);
        }
        // �J���������̔w�i�������ɑ傫���ړ�������
        else if (transform.position.x - spriteWidth > cameraTransform.position.x)
        {
            // �w�i�����[�Ɉړ�������
            transform.position -= new Vector3(spriteWidth * GridSize, 0, 0);
        }

        // === ���������̃��[�v�`�F�b�N (�ǉ�) ===
        // �J���������̔w�i������ɑ傫���ړ�������
        if (transform.position.y + spriteHeight < cameraTransform.position.y)
        {
            // �w�i����[�Ɉړ�������
            transform.position += new Vector3(0, spriteHeight * GridSize, 0);
        }
        // �J���������̔w�i�������ɑ傫���ړ�������
        else if (transform.position.y - spriteHeight > cameraTransform.position.y)
        {
            // �w�i�����[�Ɉړ�������
            transform.position -= new Vector3(0, spriteHeight * GridSize, 0);
        }
    }
}