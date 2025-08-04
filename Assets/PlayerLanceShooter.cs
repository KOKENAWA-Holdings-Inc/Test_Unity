using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerLanceShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public float shootCooldown = 3f;
    //public float shootCooldownMax = 3f;
    public float lastShotTime { get; private set; } = -3f;

    private Camera mainCamera;
    private GameManager gameManager;

    void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.IsPaused && Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + shootCooldown)
        {
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            lastShotTime = Time.time;
            Shoot(targetPosition);
        }
        //Debug.Log("Current Bullet Speed: " + this.bulletSpeed);
    }

    void Shoot(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 direction = (target - transform.position).normalized;

        // ���̕������������߂̊p�x���v�Z�iAtan2���g�p�j
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // �摜�̃A�Z�b�g��������i���j�𐳖ʂƂ��Ă���ꍇ�A90�x�I�t�Z�b�g��������
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        // --- 2. �e�̐����Ɣ��� ---
        // �v�Z�����p�x�Œe�𐶐�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);

        // ���������e��Rigidbody2D���擾
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }

    GameObject FindNearestEnemy()
    {
        // "Enemy", "Boss", "Elite"�̃^�O�����I�u�W�F�N�g�����ꂼ��擾
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        GameObject[] elites = GameObject.FindGameObjectsWithTag("Elite"); // ���ǉ�

        // 3�̔z���1�̃��X�g�Ɍ���
        var allTargets = enemies.Concat(bosses).Concat(elites); // ���ύX

        // �����������X�g���󂩂ǂ������`�F�b�N
        if (!allTargets.Any())
        {
            return null;
        }

        // �����������X�g����ł��߂��^�[�Q�b�g��T��
        return allTargets.OrderBy(target =>
            Vector2.Distance(transform.position, target.transform.position)
        ).FirstOrDefault();
    }
}