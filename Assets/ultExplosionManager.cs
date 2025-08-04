using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        // ÅöïœçXâ”èä: è’ìÀÇµÇΩëäéËÇ™ "Enemy" Ç‹ÇΩÇÕ "Boss" É^ÉOÇéùÇ¡ÇƒÇ¢ÇΩÇÁ
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
