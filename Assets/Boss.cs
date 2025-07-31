using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public float BossHP = 10f;
    public float BossMAXHP = 20f;
    public float Attack = 10f;
    public float Defence = 0f;
    public int BossExperience = 10;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float previousHP;


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
        if (BossHP != previousHP)
        {
            if (damagePopupPrefab != null)
            {
                float damage = previousHP - BossHP;

                // ���ύX: �G�̓���ɒ��ڐ�������
                Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // �G��1���j�b�g��ɕ\��
                GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);

                // ������ ���̌����Ȃ̂ŁA���̍s�����S�ɍ폜 ������
                // popup.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

                // ���������|�b�v�A�b�v�Ƀ_���[�W�ʂ�ݒ�
                popup.GetComponent<DamagePopup>().Setup(damage);
            }
        }
        previousHP = BossHP;
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
