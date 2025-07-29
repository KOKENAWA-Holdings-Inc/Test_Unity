using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EliteAttackBalletShooter : MonoBehaviour
{// �C���X�y�N�^�[����e�̃v���n�u��ݒ�
    public GameObject bulletPrefab;
    // �e�̔��ˑ��x
    public float bulletSpeed = 15f;

    // ���ǉ�: �ˌ��̃N�[���_�E���^�C���i3�b�j
    private float shootCooldown = 3f;
    // ���ǉ�: �Ō�Ɍ��������Ԃ��L�^����ϐ�
    private float lastShotTime = -3f; // �ŏ��ɂ������Ă�悤�Ƀ}�C�i�X�l�ŏ�����

    void Update()
    {
        // ���ύX:�u�O��̎ˌ�����3�b��v�̏����ɕύX
        if (Time.time >= lastShotTime + shootCooldown)
        {
            // ���ǉ�: �ŏI�ˌ����������݂̎����ɍX�V
            lastShotTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        // ���ύX: "Player"�^�O�����u�ł��߂��v�I�u�W�F�N�g��T���悤�ɏC��
        GameObject nearestPlayer = FindNearestPlayer();

        // �G��������Ȃ������ꍇ�͉������Ȃ�
        if (nearestPlayer == null)
        {
            Debug.LogWarning("�V�[����Player�����܂���B");
            return;
        }

        // �G�̕������Ɍv�Z (�Ώۂ�nearestPlayer�ɕύX)
        Vector2 direction = (nearestPlayer.transform.position - transform.position).normalized;

        // ��������p�x���v�Z���AQuaternion�i��]���j�𐶐�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        // �v�Z������]���(rotation)�Œe�𐶐�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // �e�ɗ͂������Ĕ���
        rb.velocity = direction * bulletSpeed;
    }

    // ���ǉ�: �ł��߂��G��T���ĕԂ����\�b�h
    GameObject FindNearestPlayer()
    {
        // "Enemy"�^�O�����S�ẴI�u�W�F�N�g��z��Ƃ��Ď擾
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Player");

        // �G����l�����Ȃ����null��Ԃ�
        if (enemys.Length == 0)
        {
            return null;
        }

        // LINQ���g���A�v���C���[����̋����ŏ����ɕ��בւ��A�ŏ��̗v�f�i���ł��߂��G�j��Ԃ�
        return enemys.OrderBy(player =>
            Vector2.Distance(transform.position, player.transform.position)
        ).FirstOrDefault();
    }
}
