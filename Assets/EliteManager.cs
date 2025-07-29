using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteManager : MonoBehaviour
{
    private Transform playerTransform; // �v���C���[�Ǐ]�p��Transform
    public float EliteHP = 10f;
    public float EliteMaxHP = 10f;
    public float Attack = 1f;
    public int EliteExperience = 10;
    [SerializeField] private float moveSpeed = 1f;

    public static event Action OnEnemyDied;


    private void Awake()
    {
        EliteHP = EliteMaxHP;
    }
    void Start()
    {
        // �Ǐ]���邽�߂Ƀv���C���[��Transform��ێ�
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        // ���g��HP��0�ȉ��ɂȂ������𖈃t���[���Ď�����
        if (EliteHP <= 0)
        {
            Die(); // �C�x���g�𔭍s

            // ���������� �폜 ����������
            // // �V�[������ "Player" �^�O�̃I�u�W�F�N�g��T��
            // GameObject playerToReward = GameObject.FindGameObjectWithTag("Player");
            // ���������� �폜 ����������


            // ���ύX: �ێ����Ă���playerTransform���g���A�v���C���[�����������ꍇ�̂݌o���l��^����
            if (playerTransform != null)
            {
                // ���ύX: playerTransform����R���|�[�l���g���擾
                Player playerComponent = playerTransform.GetComponent<Player>();
                if (playerComponent != null)
                {
                    playerComponent.ExperiencePoint += EliteExperience;
                    playerComponent.ExperienceTotal += EliteExperience;
                }
            }

            // ���g��j�󂷂�
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        // �v���C���[���������Ă���ꍇ�̂ݒǏ]
        if (playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    void Die()
    {
        OnEnemyDied?.Invoke();
    }
}