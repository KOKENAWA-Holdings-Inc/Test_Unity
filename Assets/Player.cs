using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerHP = 100f;
    public float PlayerMAXHP = 100f;
    public float ExperiencePool = 20f;
    public float ExperiencePoint = 0f;
    public float ExperienceTotal = 0f;
    public float ExperienceBuff = 1f;
    public float PlayerLv = 1f;
    public float Attack = 10f;
    public float Defence = 1f;
    public float moveSpeed = 5f;
    private GameObject Enemy;
    private float damageCooldown = 0.1f;
    private float nextDamageTime = 0f;

    // ���ǉ�: �ً}���G����x�������������L�^���邽�߂̃t���O
    private bool emergencyInvincibilityTriggered = false;

    public static event Action<float> OnPlayerDied;
    //public static event Action OnPlayerDied;
    private Rigidbody2D rb;
    private Vector2 movement;
    private BoxCollider2D boxCollider; // ���ǉ�: BoxCollider2D�ւ̎Q��

    private void Awake()
    {
        PlayerHP = PlayerMAXHP;
        ExperiencePoint = 0;
        ExperienceTotal = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���ǉ�: �J�n���Ɏ��g��BoxCollider2D���擾���Ă���
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (ExperiencePoint / ExperiencePool >= 1)
        {
            PlayerLv++;
            Attack = Attack * 1.05f;
            Defence = Defence * 1.01f;
            ExperiencePoint = ExperiencePoint - ExperiencePool;
            ExperiencePool = (int)(Mathf.Pow(5, PlayerLv) / Mathf.Pow(4, PlayerLv) + 10 * PlayerLv);
        }
    }
    public void AddExperience(float amount)
    {
       ExperiencePoint += amount * ExperienceBuff;
       ExperienceTotal += amount * ExperienceBuff; 
    }
    void FixedUpdate()
    {
        if (PlayerHP <= 0)
        {
            Die();
        }
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �Փ˂�������̃I�u�W�F�N�g�̃^�O���r
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Elite"))
        {
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + damageCooldown;
                EnemyManager EnemyComponent = collision.gameObject.GetComponent<EnemyManager>();
                EliteManager EliteComponent = collision.gameObject.GetComponent<EliteManager>();

                if (EnemyComponent != null)
                {
                    if (EnemyComponent.Attack - (Defence - 1) >= 1)
                    {
                        PlayerHP = PlayerHP - (EnemyComponent.Attack - (Defence - 1));
                    }
                    else
                    {
                        PlayerHP -= 1f;
                    }
                }
                if (EliteComponent != null)
                {
                    if (EliteComponent.Attack - (Defence - 1) >= 1)
                    {
                        PlayerHP = PlayerHP - (EliteComponent.Attack - (Defence - 1));
                    }
                    else
                    {
                        PlayerHP -= 1f;
                    }
                }

                // ���ǉ�: �_���[�W���󂯂������HP���`�F�b�N
                CheckForEmergencyInvincibility();
            }
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss BossComponent = collision.gameObject.GetComponent<Boss>();
            if (BossComponent != null)
            {
                if (BossComponent.Attack - Defence >= 1)
                {
                    PlayerHP = PlayerHP - (BossComponent.Attack - Defence);
                }
                else
                {
                    PlayerHP -= 1f;
                }

                // ���ǉ�: �_���[�W���󂯂������HP���`�F�b�N
                CheckForEmergencyInvincibility();
            }
        }
    }

    /// <summary>
    /// ���ǉ�: HP���`�F�b�N���āA�����𖞂����Ă���΋ً}���G�𔭓����郁�\�b�h
    /// </summary>
    private void CheckForEmergencyInvincibility()
    {
        // HP��10�ȉ��A���A�܂����̋@�\���������Ă��Ȃ��ꍇ
        if (PlayerHP <= 10f && !emergencyInvincibilityTriggered)
        {
            // �����������Ƃ��L�^
            emergencyInvincibilityTriggered = true;
            // �R���[�`�����J�n
            StartCoroutine(TriggerInvincibilityCoroutine());
        }
    }

    /// <summary>
    /// ���ǉ�: 5�b��BoxCollider2D�𖳌�������R���[�`��
    /// </summary>
    private IEnumerator TriggerInvincibilityCoroutine()
    {
        if (boxCollider != null)
        {
            Debug.Log("�ً}���G�����I 5�b�ԓ����蔻��𖳌������܂��B");
            // �R���C�_�[�𖳌��ɂ���
            boxCollider.enabled = false;

            // 5�b�ԑҋ@����
            yield return new WaitForSeconds(5f);

            // 5�b��ɃR���C�_�[���ēx�L���ɂ���
            boxCollider.enabled = true;
            Debug.Log("���G���Ԃ��I�����܂����B");
        }
    }

    public void PlayerLvManager()
    {
    }

    void Die()
    {
        OnPlayerDied?.Invoke(ExperienceTotal);
    }
}
