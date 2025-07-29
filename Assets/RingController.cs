using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RingController : MonoBehaviour
{
    // (SerializeField�Ȃǂ̕ϐ��͕ύX�Ȃ�)
    [SerializeField] private float speed = 15f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float damageInterval = 0.2f;

    private Transform target;
    private Transform player; // ���̕ϐ��ɁA�^�O�ŒT�����v���C���[���i�[���܂�
    private Vector3 targetPosition;
    private Collider2D ringCollider;
    private List<GameObject> hitEnemies;

    // ���ύX�_: Initialize���\�b�h����v���C���[�̈������폜
    public void Initialize(Transform targetEnemy)
    {
        this.target = targetEnemy;
        this.ringCollider = GetComponent<Collider2D>();
        this.hitEnemies = new List<GameObject>();

        // ���ǉ�: �^�O���g���ăv���C���[�I�u�W�F�N�g���������A����Transform���i�[
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            this.player = playerObject.transform;
        }
        else
        {
            // �v���C���[��������Ȃ������ꍇ�̃G���[����
            Debug.LogError("Tag 'Player' not found in scene! Destroying ring.");
            Destroy(gameObject);
            return;
        }

        StartCoroutine(RingLifecycleCoroutine());
    }

    // (RingLifecycleCoroutine�ȉ��̃��\�b�h�͕ύX����܂���)
    private IEnumerator RingLifecycleCoroutine()
    {
        // --- 1. �G�Ɍ������t�F�[�Y ---
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

        // --- 2. �G�̈ʒu�Œ�~���A����I�ɍU������t�F�[�Y ---
        yield return StartCoroutine(WaitingAttackPhase());

        // --- 3. �v���C���[�ɖ߂�t�F�[�Y ---
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

        // --- 4. ���� ---
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
            Debug.Log(other.name + "�ɍU�����q�b�g�I");
            hitEnemies.Add(other.gameObject);
        }
    }
}