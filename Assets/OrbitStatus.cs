using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitStatus : MonoBehaviour
{
    private GameObject Boss;
    private GameObject Enemy;
    public float OrbitAttack = 2f;
    public bool attacked = false;

    [SerializeField] private float knockbackForce = 10f;

    void Start()
    {
        // ...
    }

    void Update()
    {
        // ...
    }

    // ���ύX: ���\�b�h���ƈ����̌^���g���K�[�p�ɕύX
    private void OnTriggerEnter2D(Collider2D other)
    {
        attacked = true;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player playerComponent = playerObj.GetComponent<Player>();

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyManager EnemyComponent = other.gameObject.GetComponent<EnemyManager>();
            if (EnemyComponent != null)
            {
                EnemyComponent.EnemyHP = EnemyComponent.EnemyHP - OrbitAttack * (playerComponent.Attack * 0.2f);
            }

            Rigidbody2D enemyRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        if (other.gameObject.CompareTag("Elite"))
        {
            EliteManager EliteComponent = other.gameObject.GetComponent<EliteManager>();
            if (EliteComponent != null)
            {
                EliteComponent.EliteHP = EliteComponent.EliteHP - OrbitAttack * (playerComponent.Attack * 0.2f);
            }

            Rigidbody2D eliteRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (eliteRb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                eliteRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            // ����: Boss�R���|�[�l���g��BossManager���w���Ă��邩�m�F���Ă�������
            BossManager BossComponent = other.gameObject.GetComponent<BossManager>();
            if (BossComponent != null)
            {
                // ����: BossManager��BossHP�Ƃ����ϐ������邩�m�F���Ă�������
                // BossComponent.BossHP = BossComponent.BossHP - OrbitAttack; 
            }
        }
        if (attacked == true)
        {
            attacked = false;
        }
    }
}