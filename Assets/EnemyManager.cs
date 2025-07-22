using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Transform playerTransform; // �v���C���[�Ǐ]�p��Transform
    public int EnemyHP = 10;
    public int Attack = 5;
    public int EnemyExperience = 1;
    [SerializeField] private float moveSpeed = 1f;

    public static event Action OnEnemyDied;

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
        if (EnemyHP <= 0)
        {
            Die(); // �C�x���g�𔭍s

            // �V�[������ "Player" �^�O�̃I�u�W�F�N�g��T��
            GameObject playerToReward = GameObject.FindGameObjectWithTag("Player");

            // �v���C���[�����������ꍇ�̂݌o���l��^����
            if (playerToReward != null)
            {
                Player playerComponent = playerToReward.GetComponent<Player>();
                if (playerComponent != null)
                {
                    playerComponent.ExperiencePoint += EnemyExperience;
                    playerComponent.ExperienceTotal += EnemyExperience;
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

    // OnCollisionEnter2D��TakeDamage���\�b�h�͕s�v
}