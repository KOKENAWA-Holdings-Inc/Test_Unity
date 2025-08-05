using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform player;

    public GameObject enemyPrefab;
    public TimeManager timeManager;

    // ������ �X�|�[���Ԋu�̐ݒ��������ɕύX ������
    [Header("Spawn Interval Settings")]
    [Tooltip("�Q�[���J�n���̃X�|�[���Ԋu")]
    [SerializeField] private float initialSpawnInterval = 2.0f;
    [Tooltip("�ŒZ�̃X�|�[���Ԋu")]
    [SerializeField] private float minSpawnInterval = 0.1f;
    [Tooltip("�ŒZ�Ԋu�ɓ��B����܂ł̎��ԁi�b�j")]
    [SerializeField] private float timeToMinInterval = 180f; // 3��

    // ���ǉ�: �G�̐��̏���ݒ�
    [Header("Spawn Limit Settings")]
    [Tooltip("�V�[����̓G�̍ő吔")]
    [SerializeField] private int maxEnemies = 500;

    private float spawnTimer = 0f;
    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);
    private bool isSpawningActive = true;

    [Header("HP Scaling Settings")]
    [Tooltip("�G�̊�{�I�ȍő�HP")]
    [SerializeField] private float baseEnemyMaxHp = 10f;
    [Tooltip("1�b������ɑ�������ő�HP�̗�")]
    [SerializeField] private float hpGrowthPerSecond = 0.2f;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            //Debug.LogError("Player�I�u�W�F�N�g��������܂���I 'Player'�^�O���ݒ肳��Ă��邩�m�F���Ă��������B");
            isSpawningActive = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (isSpawningActive)
        {
            // ���ǉ�: �V�[����̓G�̐�������ɒB���Ă�����A�X�|�[�������𒆒f
            if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies)
            {
                //Debug.Log("Upper Limited");
                return; // ����ɒB���Ă���̂ŉ������Ȃ�
            }

            // ���ύX: �o�ߎ��ԂɊ�Â��Č��݂̃X�|�[���Ԋu���v�Z
            float progress = Mathf.Clamp01(timeManager.elapsedTime / timeToMinInterval);
            float currentSpawnInterval = Mathf.Lerp(initialSpawnInterval, minSpawnInterval, progress);

            spawnTimer += Time.deltaTime;

            // ���ύX: �v�Z�������݂̃X�|�[���Ԋu�Ŕ���
            if (spawnTimer >= currentSpawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f; // �^�C�}�[�����Z�b�g
            }

            if (timeManager.elapsedTime >= 240)
            {
                //Debug.Log("�w�莞�Ԃ𒴂������߁A�G�l�~�[�̃X�|�[�����~���A�����̃G�l�~�[��S�Ĕj�󂵂܂��B");
                isSpawningActive = false;
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
        if (enemyPrefab == null)
        {
            //Debug.LogError("Enemy Prefab���ݒ肳��Ă��܂���B");
            return;
        }

        Vector2 offset = Vector2.zero;
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y); break;
            case 1: offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -spawnArea.y); break;
            case 2: offset = new Vector2(spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y)); break;
            case 3: offset = new Vector2(-spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y)); break;
        }
        Vector2 spawnPosition = (Vector2)player.position + offset;

        float scaledMaxHp = baseEnemyMaxHp + (hpGrowthPerSecond * timeManager.elapsedTime);
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyManager enemyManager = enemyInstance.GetComponent<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.InitializeStats(scaledMaxHp);
        }

        // ������ ���̍s�͏d�����ēG�𐶐����Ă��܂����ߍ폜���܂��� ������
        // Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}