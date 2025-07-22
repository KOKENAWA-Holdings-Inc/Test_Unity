using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    

    // �v���C���[�I�u�W�F�N�g
    public Transform player;

    // �X�|�[��������G�̃v���n�u
    public GameObject BossPrefab;

    // �X�|�[���������`�̈�̃T�C�Y�i���S����̋����j
    public Vector2 spawnArea = new Vector2(9.5f, 5.5f);

    public TimeManager timeManager;

    

    //private bool hasBossSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnBoss", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (((int)timeManager.elapsedTime) == 420 && !hasBossSpawned) 
        {
            SpawnBoss();
            hasBossSpawned = true;
        }*/

        
        
    }

    void SpawnBoss() 
    {
        Vector2 offset = Vector2.zero; // �X�|�[���ʒu�̃I�t�Z�b�g��������

        // 0:��, 1:��, 2:�E, 3:�� ��4�̕ӂ���1�������_���ɑI��
        int side = Random.Range(0, 4);

        switch (side)
        {
            // ��ӂɃX�|�[��
            case 0:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y);
                break;
            // ���ӂɃX�|�[��
            case 1:
                offset = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -spawnArea.y);
                break;
            // �E�ӂɃX�|�[��
            case 2:
                offset = new Vector2(spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
            // ���ӂɃX�|�[��
            case 3:
                offset = new Vector2(-spawnArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                break;
        }

        // �v���C���[�̈ʒu�Ɍv�Z�����I�t�Z�b�g�����Z
        Vector2 spawnPosition = (Vector2)player.position + offset;

        // �G�𐶐�
        Instantiate(BossPrefab, spawnPosition, Quaternion.identity);
    }

    
}
