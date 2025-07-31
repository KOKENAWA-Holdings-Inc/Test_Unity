using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RingController : MonoBehaviour
{
    // (SerializeFieldなどの変数は変更なし)
    [SerializeField] private float speed = 15f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float damageInterval = 0.2f;

    private Transform target;
    private Transform player; // この変数に、タグで探したプレイヤーを格納します
    private Vector3 targetPosition;
    private Collider2D ringCollider;
    private List<GameObject> hitEnemies;

    // ★変更点: Initializeメソッドからプレイヤーの引数を削除
    public void Initialize(Transform targetEnemy)
    {
        this.target = targetEnemy;
        this.ringCollider = GetComponent<Collider2D>();
        this.hitEnemies = new List<GameObject>();

        // ★追加: タグを使ってプレイヤーオブジェクトを検索し、そのTransformを格納
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            this.player = playerObject.transform;
        }
        else
        {
            // プレイヤーが見つからなかった場合のエラー処理
            Debug.LogError("Tag 'Player' not found in scene! Destroying ring.");
            Destroy(gameObject);
            return;
        }

        StartCoroutine(RingLifecycleCoroutine());
    }

    // (RingLifecycleCoroutine以下のメソッドは変更ありません)
    private IEnumerator RingLifecycleCoroutine()
    {
        // --- 1. 敵に向かうフェーズ ---
        if (target != null)
        {
            targetPosition = target.position;
        }
        else
        {
            Destroy(gameObject);
            yield break;
        }
        StartAttackPhase();
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        EndAttackPhase();

        // --- 2. 敵の位置で停止し、定期的に攻撃するフェーズ ---
        yield return StartCoroutine(WaitingAttackPhase());

        // --- 3. プレイヤーに戻るフェーズ ---
        StartAttackPhase();
        if (player != null)
        {
            while (Vector2.Distance(transform.position, player.position) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        EndAttackPhase();

        // --- 4. 消滅 ---
        Destroy(gameObject);
    }

    private IEnumerator WaitingAttackPhase()
    {
        float timer = 0f;
        while (timer < waitTime)
        {
            StartAttackPhase();
            yield return new WaitForSeconds(damageInterval);
            EndAttackPhase();
            timer += damageInterval;
        }
    }

    private void StartAttackPhase()
    {
        hitEnemies.Clear();
        ringCollider.enabled = true;
    }

    private void EndAttackPhase()
    {
        ringCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;
        if ((tag == "Enemy" || tag == "Boss") && !hitEnemies.Contains(other.gameObject))
        {
            Debug.Log(other.name + "に攻撃がヒット！");
            hitEnemies.Add(other.gameObject);
        }
    }
}