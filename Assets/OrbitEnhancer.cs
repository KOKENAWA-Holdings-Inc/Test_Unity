using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEnhancer : MonoBehaviour
{
    [Header("�\�͐ݒ�")]
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private float attackMultiplier = 1.5f;
    [SerializeField] private float speedMultiplier = 3.0f;

    // ���ǉ�: �Ɨ������N�[���_�E�����Ԃ�ݒ肷��ϐ�
    [SerializeField] private float cooldown = 30.0f;

    private OrbitManager orbitManager;
    private float abilityReadyTime = 0f;

    // ���ύX: UI�p�̃v���p�e�B���V����cooldown�ϐ���Ԃ��悤�ɂ���
    public float AbilityReadyTime => abilityReadyTime;
    public float CooldownDuration => cooldown;

    private GameManager gameManager;


    void Start()
    {
        orbitManager = GetComponent<OrbitManager>();
        if (orbitManager == null)
        {
            //Debug.LogError("OrbitManager�������I�u�W�F�N�g�ɃA�^�b�`����Ă��܂���I");
            this.enabled = false;
        }
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.IsPaused && Input.GetKey(KeyCode.Space) && Time.time >= abilityReadyTime && orbitManager.SpawnedObjects.Count > 0)
        {
            StartCoroutine(ActivateOrbitBuffCoroutine());
        }
    }

    private IEnumerator ActivateOrbitBuffCoroutine()
    {
        // --- 1. �o�t�J�n���� ---
        // ���ύX: ���Ɏg�p�\�ɂȂ鎞�����u���ݎ��� + �N�[���_�E���v�ɐݒ�
        abilityReadyTime = Time.time + duration + cooldown;
        //Debug.Log("�I�[�r�b�g�����I");

        float originalSpeed = orbitManager.orbitSpeed;
        orbitManager.orbitSpeed *= speedMultiplier;

        foreach (GameObject orb in orbitManager.SpawnedObjects)
        {
            OrbitStatus status = orb.GetComponent<OrbitStatus>();
            if (status != null)
            {
                status.OrbitAttack *= attackMultiplier;
            }
        }
        orbitManager.StopLifecycle();

        // --- 2. �o�t�p�����Ԃ����ҋ@ ---
        yield return new WaitForSeconds(duration);

        // --- 3. �o�t�I������ ---
        //Debug.Log("�I�[�r�b�g�����I���B�N�[���_�E���J�n�B");
        orbitManager.orbitSpeed = originalSpeed;
        orbitManager.RestartLifecycle();
    }
}