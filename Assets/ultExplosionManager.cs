using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultExplosionManager : MonoBehaviour
{
    [SerializeField] private float ultAttack = 100.0f;
    public bool attacked = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        attacked = true;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Player playerComponent = playerObj.GetComponent<Player>();
        // ★変更箇所: 衝突した相手が "Enemy" または "Boss" タグを持っていたら
        if (other.CompareTag("Enemy") || other.CompareTag("Elite") || other.CompareTag("Boss"))
        {
            EnemyManager EnemyComponent = other.GetComponent<EnemyManager>();
            EliteManager EliteComponent = other.GetComponent<EliteManager>();
            Boss BossComponent = other.GetComponent<Boss>();
            PlayerUltShooter.RaiseOnEnemyHit();

            if (EnemyComponent != null)
            {
                EnemyComponent.EnemyHP = EnemyComponent.EnemyHP - ultAttack * (playerComponent.Attack * 1.5f);
            }
            if (EliteComponent != null)
            {
                EliteComponent.EliteHP = EliteComponent.EliteHP - ultAttack * (playerComponent.Attack * 0.5f);
            }
            if (BossComponent != null)
            {
                BossComponent.BossHP = BossComponent.BossHP - ultAttack * (playerComponent.Attack * 0.5f);
            }

            // Destroy(gameObject);
        }
        if (attacked == true)
        {
            attacked = false;
        }
    }
}
