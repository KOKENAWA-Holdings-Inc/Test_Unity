using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerHP = 10;
    public int PlayerMAXHP = 10; 
    public int ExperiencePool = 10;
    public int ExperiencePoint = 0;
    public int ExperienceTotal = 0;
    public int PlayerLv = 1;
    public int Attack = 5;
    public int Defence = 5;
    public int Luck = 1;
    public float moveSpeed = 5f;
    private GameObject Enemy;

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
                PlayerHP = PlayerHP - EnemyComponent.Attack; // �_���[�W���󂯂�
                Debug.Log("Take "+ EnemyComponent.Attack);
                //EnemyHP = EnemyHP - playerComponent.Attack;
                //Destroy(this.gameObject);
            }

            // �����Ƀ_���[�W������m�b�N�o�b�N�����Ȃǂ�ǉ��ł��܂�
            //PlayerHP = PlayerHP - EnemyComponent.Attack;
            //ExperiencePoint++;
        }

        /*if (collision.gameObject.CompareTag("Boss")) 
        {
            //PlayerHP = PlayerHP - Boss.Attack;
        }*/
    }

    public void PlayerLvManager()
    {
        
    }

    void Die()
    {
        OnPlayerDied?.Invoke();

    }
}
