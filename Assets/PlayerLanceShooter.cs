using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLanceShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public float shootCooldown = 3f;
    //public float shootCooldownMax = 3f;
    public float lastShotTime { get; private set; } = -3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastShotTime + shootCooldown)
        {
            lastShotTime = Time.time;
            Shoot();
        }
        Debug.Log("Current Bullet Speed: " + this.bulletSpeed);
    }

    void Shoot()
    {
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy == null)
        {
            Debug.LogWarning("�V�[����Enemy�܂���Boss�����܂���B");
            return;
        }

        Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
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