using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceBullet : MonoBehaviour
{
    [SerializeField] private float LanceAttack = 10.0f;
    public bool attacked = false;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        attacked = true;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player playerComponent = playerObj.GetComponent<Player>();
        // š•ÏX‰ÓŠ: Õ“Ë‚µ‚½‘Šè‚ª "Enemy" ‚Ü‚½‚Í "Boss" ƒ^ƒO‚ğ‚Á‚Ä‚¢‚½‚ç
        if (other.CompareTag("Enemy") || other.CompareTag("Elite") || other.CompareTag("Boss"))
        {
            EnemyManager EnemyComponent = other.GetComponent<EnemyManager>();
            EliteManager EliteComponent = other.GetComponent<EliteManager>();
            Boss BossComponent = other.GetComponent<Boss>();
            PlayerUltShooter.RaiseOnEnemyHit();

            if (EnemyComponent != null)
            {
                EnemyComponent.EnemyHP = EnemyComponent.EnemyHP - LanceAttack * (playerComponent.Attack * 0.5f);
            }
            if (EliteComponent != null)
            {
                EliteComponent.EliteHP = EliteComponent.EliteHP - LanceAttack * (playerComponent.Attack * 0.5f);
            }
            if (BossComponent != null)
            {
                BossComponent.BossHP = BossComponent.BossHP - LanceAttack * (playerComponent.Attack * 0.5f);
            }
            
            // Destroy(gameObject);
        }
        if (attacked == true) 
        {
            attacked = false;
        }
    }
}