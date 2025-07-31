using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour
{
    [Header("�o��������I�u�W�F�N�g")]
    [SerializeField]
    private GameObject objectPrefab;

    [Header("UI�ݒ�")]
    [SerializeField]
    private GameObject markerUiPrefab;

    [Header("�ݒ�")]
    [SerializeField]
    private int numberOfObjects = 12;
    [SerializeField]
    private float displayDuration = 0.1f;
    [SerializeField]
    private float markerDuration = 0.5f;
    [SerializeField]
    private float spawnDelayAfterMarker = 0.5f;

    [Header("���s�^�C�~���O")]
    [SerializeField]
    private float minWaitTime = 15f;
    [SerializeField]
    private float randomWaitTime = 20f;

    private Camera mainCamera;

    // ���ǉ�: �}�[�J�[UI�̃C���X�^���X��ۊǂ��Ă������߂̃��X�g�i�I�u�W�F�N�g�v�[���j
    private List<GameObject> markerPool = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;

        // ���ύX: �ŏ��ɕK�v�Ȑ��̃}�[�J�[�𐶐����A��\���ɂ��ăv�[�����Ă���
        if (markerUiPrefab != null)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                GameObject marker = Instantiate(markerUiPrefab);
                marker.SetActive(false); // ���������C���X�^���X���\����
                markerPool.Add(marker);
            }
        }

        StartCoroutine(FindBossAndStartSpawning());
    }

    private IEnumerator FindBossAndStartSpawning()
    {
        while (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("�{�X�𔭌��ITimedSpawner�̏������J�n���܂��B");
        StartCoroutine(SpawningLoopCoroutine());
    }

    private IEnumerator SpawningLoopCoroutine()
    {
        while (true)
        {
            float delay = minWaitTime + Random.Range(0f, randomWaitTime);
            yield return new WaitForSeconds(delay);
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        if (objectPrefab == null || markerUiPrefab == null)
        {
            Debug.LogError("Object Prefab �܂��� Marker Ui Prefab ���ݒ肳��Ă��܂���I");
            return;
        }

        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(0, Screen.width);
            float randomY = Random.Range(0, Screen.height);

            Vector3 screenPosition = new Vector3(randomX, randomY, 10f);
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            // ���ύX: �v�[���̃C���f�b�N�X�ƍ��W��n��
            StartCoroutine(SpawnWithMarkerCoroutine(i, worldPosition));
        }
    }

    // ���ύX: �v�[���̃C���f�b�N�X���󂯎��悤�Ɉ�����ύX
    private IEnumerator SpawnWithMarkerCoroutine(int poolIndex, Vector3 spawnPosition)
    {
        // --- UI�}�[�J�[�̕\������ ---
        // ���ύX: �v�[������}�[�J�[���擾
        GameObject markerInstance = markerPool[poolIndex];

        // ���ύX: ����(Instantiate)�ł͂Ȃ��A�ʒu���Z�b�g���ĕ\��(SetActive)
        markerInstance.transform.position = spawnPosition;
        markerInstance.SetActive(true);

        yield return new WaitForSeconds(markerDuration);

        // ���ύX: �j��(Destroy)�ł͂Ȃ��A��\��(SetActive)
        markerInstance.SetActive(false);

        // --- �I�u�W�F�N�g�̏������� ---
        yield return new WaitForSeconds(spawnDelayAfterMarker);

        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        Destroy(spawnedObject, displayDuration);
    }
}