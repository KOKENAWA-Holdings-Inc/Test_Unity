using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerHP = 100f;
    public float PlayerMAXHP = 100f; 
    public float ExperiencePool = 10f;
    public float ExperiencePoint = 0f;
    public float ExperienceTotal = 0f;
    public float PlayerLv = 1f;
    public float Attack = 10f;
    public float Defence = 1f;
    //public int Luck = 1;
    public float moveSpeed = 5f;
    private GameObject Enemy;
    // �_���[�W���󂯂�Ԋu�i�b�j
    private float damageCooldown = 0.1f;
    // ���Ƀ_���[�W���󂯂邱�Ƃ��ł��鎞�����L�^����ϐ�
    private float nextDamageTime = 0f;


    public static event Action OnPlayerDied;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        PlayerHP = PlayerMAXHP;
        ExperiencePoint = 0;
        ExperienceTotal = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        /*if (PlayerHP == 0) 
        {
            //Destroy(this.gameObject);
           
        }*/
        if (ExperiencePoint/ExperiencePool >= 1)
        {
            PlayerLv++;
            Attack = Attack * 1.05f;
            Defence = Defence * 1.01f;
            ExperiencePoint = ExperiencePoint - ExperiencePool;
            ExperiencePool = (int)(Mathf.Pow(5,PlayerLv)/Mathf.Pow(4, PlayerLv) + 10 * PlayerLv);
            //Debug.Log("Now ExperiencePool Is"+ExperiencePool+".");
        }
    }
    void FixedUpdate()
    {
        if (PlayerHP <= 0) 
        {
            Die();
        }

        // Rigidbody2D�̑��x���X�V���ăI�u�W�F�N�g���ړ�������
        // movement.normalized�Ŏ΂߈ړ��������Ȃ�Ȃ��悤�ɐ��K������
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �Փ˂�������̃I�u�W�F�N�g�̃^�O���r
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Elite"))
        {
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + damageCooldown;
                EnemyManager EnemyComponent = collision.gameObject.GetComponent<EnemyManager>();
                EliteManager EliteComponent = collision.gameObject.GetComponent<EliteManager>();
                // ����̃^�O�� "Enemy" �������ꍇ�A�R���\�[���Ƀ��b�Z�[�W���o��
                //Debug.Log("�G�l�~�[�ɐڐG���܂����I");
                if (EnemyComponent != null)
                {
                    if (EnemyComponent.Attack - (Defence - 1) >= 1)
                    {
                        PlayerHP = PlayerHP - (EnemyComponent.Attack - (Defence - 1)); // �_���[�W���󂯂�
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
                        PlayerHP = PlayerHP - (EliteComponent.Attack - (Defence - 1)); // �_���[�W���󂯂�
                    }
                    else
                    {
                        PlayerHP -= 1f;
                    }


                }
            }

            
        }

        if (collision.gameObject.CompareTag("Boss")) 
        {
            Boss BossComponent = collision.gameObject.GetComponent<Boss>();
            if (BossComponent != null)
            {
                if (BossComponent.Attack - Defence >= 1)
                {
                    PlayerHP = PlayerHP - (BossComponent.Attack - Defence); // �_���[�W���󂯂�
                }
                else 
                {
                    PlayerHP -= 1f;
                }
                
                
            }
            
        }
    }

    public void PlayerLvManager()
    {
        
    }

    void Die()
    {
        OnPlayerDied?.Invoke();

    }
}
