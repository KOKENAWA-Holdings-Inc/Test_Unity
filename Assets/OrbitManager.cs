using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    [Header("�^�[�Q�b�g�ݒ�")]
    private Transform playerTransform; // �Ǐ]����v���C���[

    [Header("����I�u�W�F�N�g�ݒ�")]
    [SerializeField] private GameObject orbitingObjectPrefab;
    [SerializeField] private int numberOfObjects = 8;
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private float orbitSpeed = 50.0f;

    [Header("���Ԑݒ�")]
    [SerializeField] private float initialSpawnDelay = 2.0f;
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] private float respawnDelay = 5.0f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    // �� �ύX�_: Start�ł͏������R���[�`�����J�n���邾��
    void Start()
    {
        // �v���C���[��T���āA���������烁�C���������n�߂�R���[�`�����J�n
        StartCoroutine(InitializeCoroutine());
    }

    // �� �ǉ�: �v���C���[��T���ď��������s���R���[�`��
    private IEnumerator InitializeCoroutine()
    {
        // playerTransform��������܂Ń��[�v��������
        while (playerTransform == null)
        {
            // "Player"�^�O�𗊂�Ƀv���C���[�I�u�W�F�N�g��T��
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                // ����������Q�Ƃ�ۑ�
                playerTransform = playerObj.transform;
            }

            // �v���C���[��������Ȃ���΁A���̃t���[���܂őҋ@
            yield return null;
        }

        // --- �v���C���[������������A�������牺�̏��������s����� ---

        // �����Ə��ł��J��Ԃ����C���̃T�C�N�����J�n
        StartCoroutine(LifecycleCoroutine());
    }

    void LateUpdate()
    {
        if (playerTransform != null && spawnedObjects.Count > 0)
        {
            Orbit();
        }
    }

    private void Orbit()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            float angle = i * (360f / numberOfObjects);
            float currentAngle = angle + Time.time * orbitSpeed;
            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRadians) * radius;
            float y = Mathf.Sin(angleInRadians) * radius;
            Vector3 offset = new Vector3(x, y, 0);
            spawnedObjects[i].transform.position = playerTransform.position + offset;
        }
    }

    private IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        while (true)
        {
            SpawnObjects();
            yield return new WaitForSeconds(lifeTime);
            DespawnObjects();
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void SpawnObjects()
    {
        if (spawnedObjects.Count > 0) DespawnObjects();
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject obj = Instantiate(orbitingObjectPrefab, transform.position, Quaternion.identity);
            spawnedObjects.Add(obj);
        }
    }

    private void DespawnObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}