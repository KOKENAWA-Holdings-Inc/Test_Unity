using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public float BossHP = 10f;
    public float BossMAXHP = 20f;
    public float Attack = 10f;
    public float Defence = 0f;
    public int BossExperience = 10;
    private Rigidbody2D rb;
    private Vector2 movement;

    
    public static event Action OnBossDied;
    private void Awake()
    {
        BossHP = BossMAXHP;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���g��HP��0�ȉ��ɂȂ������𖈃t���[���Ď�����
        if (BossHP <= 0)
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
                    playerComponent.ExperiencePoint += BossExperience;
                    playerComponent.ExperienceTotal += BossExperience;
                }
            }

            // ���g��j�󂷂�
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        OnBossDied?.Invoke();

    }
}
