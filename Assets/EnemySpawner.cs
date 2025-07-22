using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemyPrefab;
    public TimeManager timeManager;

    // �G�l�~�[���X�|�[��������Ԋu�i�b�j
    public float spawnInterval = 2f;

    // ������ �X�|�[���p�̃^�C�}�[�ϐ���ǉ� ������
    private float spawnTimer = 0f;

    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);
    private bool isSpawningActive = true;

    // Start���\�b�h�͕s�v�ɂȂ�̂ō폜���܂�
    // void Start() { ... }

    void Update()
    {
        // �X�|�i�[���A�N�e�B�u�ȏꍇ�̂ݏ��������s
        if (isSpawningActive)
        {
            // ������ �������炪�^�C�}�[���� ������
            // �^�C�}�[�ɖ��t���[���̌o�ߎ��Ԃ����Z
            spawnTimer += Time.deltaTime;

            // �^�C�}�[���w�肵���X�|�[���Ԋu�𒴂�����
            if (spawnTimer >= spawnInterval)
            {
                // �G�l�~�[���X�|�[��
                SpawnEnemy();

                // �^�C�}�[�����Z�b�g
                // spawnTimer = 0f; �ł��ǂ����A������̕�����萳�m
                spawnTimer -= spawnInterval;
            }
            // ������ �^�C�}�[���������܂� ������


            // ���Ԃ�420�b�ȏ�ɂȂ�����A�X�|�[�����~����
            if (timeManager.elapsedTime >= 420)
            {
                Debug.Log("�w�莞�Ԃ𒴂������߁A�G�l�~�[�̃X�|�[�����~���A�����̃G�l�~�[��S�Ĕj�󂵂܂��B");
                isSpawningActive = false;

                // InvokeRepeating���g���Ă��Ȃ��̂ŁACancelInvoke�͕s�v

                DestroyAllEnemies();
            }
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void SpawnEnemy()
    {
        // isSpawningActive�̃`�F�b�N�͕s�v�ɂȂ�܂�
        // if (!isSpawningActive) return;

        if (player == null || enemyPrefab == null)
        {
            Debug.LogError("Player�܂���Enemy Prefab���ݒ肳��Ă��܂���B");
            return;
        }

        Vector2 offset = Vector2.zero;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y);
                break;
            case 1:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -spawnArea.y);
                break;
            case 2:
                offset = new Vector2(spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
            case 3:
                offset = new Vector2(-spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
        }

        Vector2 spawnPosition = (Vector2)player.position + offset;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}