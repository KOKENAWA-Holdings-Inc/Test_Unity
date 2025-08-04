using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteSpawner : MonoBehaviour
{
    private Transform player; // �����Ńv���C���[�̏���ێ����邽�߂̕ϐ�

    public GameObject ElitePrefab;
    public TimeManager timeManager;

    public float spawnInterval = 60f;
    private float spawnTimer = 0f;
    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);
    private bool isSpawningActive = true;
    [Tooltip("�G�̊�{�I�ȍő�HP")]
    [SerializeField] private float baseEliteMaxHp = 100f;
    [Tooltip("1�b������ɑ�������ő�HP�̗�")]
    [SerializeField] private float hpGrowthPerSecond = 0.5f;

    // ������ �Q�[���J�n���Ɉ�x�������s�����Awake���\�b�h��ǉ� ������
    void Start()
    {
        // "Player"�Ƃ����^�O���t���Ă���Q�[���I�u�W�F�N�g��T���A����Transform�R���|�[�l���g���擾����
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            // ����������Ȃ������ꍇ�A�G���[���O���o���ăX�|�[�����~����
            //Debug.LogError("Player�I�u�W�F�N�g��������܂���I 'Player'�^�O���ݒ肳��Ă��邩�m�F���Ă��������B");
            isSpawningActive = false;
        }
    }

    void Update()
    {
        // player�����Ȃ��ꍇ�͉������Ȃ�
        if (player == null) return;

        // �X�|�i�[���A�N�e�B�u�ȏꍇ�̂ݏ��������s
        if (isSpawningActive)
        {
            // �^�C�}�[����
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                //Debug.Log("Spawned");
                spawnTimer -= spawnInterval;
            }

            // ���Ԃ�420�b�ȏ�ɂȂ�����A�X�|�[�����~����
            if (timeManager.elapsedTime >= 419)
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
        if (ElitePrefab == null)
        {
            //Debug.LogError("Elite Prefab���ݒ肳��Ă��܂���B");
            return;
        }

        //Debug.Log("Spawning enemy near player at position: " + player.position);

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
        float scaledMaxHp = baseEliteMaxHp + (hpGrowthPerSecond * timeManager.elapsedTime);
        GameObject enemyInstance = Instantiate(ElitePrefab, spawnPosition, Quaternion.identity);
        EliteManager eliteManager = enemyInstance.GetComponent<EliteManager>();
        if (eliteManager != null)
        {
            eliteManager.InitializeStats(scaledMaxHp);
        }
    }
}
