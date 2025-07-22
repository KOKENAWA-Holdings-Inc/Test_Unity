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

        // Rigidbody2Dの速度を更新してオブジェクトを移動させる
        // movement.normalizedで斜め移動が速くならないように正規化する
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手のオブジェクトのタグを比較
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager EnemyComponent = collision.gameObject.GetComponent<EnemyManager>();
            // 相手のタグが "Enemy" だった場合、コンソールにメッセージを出力
            //Debug.Log("エネミーに接触しました！");
            if (EnemyComponent != null)
            {
                PlayerHP = PlayerHP - EnemyComponent.Attack; // ダメージを受ける
                Debug.Log("Take "+ EnemyComponent.Attack);
                //EnemyHP = EnemyHP - playerComponent.Attack;
                //Destroy(this.gameObject);
            }

            // ここにダメージ処理やノックバック処理などを追加できます
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
