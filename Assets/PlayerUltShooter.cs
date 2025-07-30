using System.Collections;
using System.Collections.Generic;
using System; // event Action ���g�����߂ɕK�v
using UnityEngine;
using UnityEngine.UI; // UI�̃X���C�_�[���g���ꍇ�͕K�v

public class PlayerUltShooter : MonoBehaviour
{
    [Header("���ːݒ�")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 15f;

    [Header("�K�E�Z�`���[�W�ݒ�")]
    public float maxUltCharge = 500f; // �`���[�W�̍ő�l
    public float passiveChargeRate = 2f;  // 1�b������̎��R������
    public float hitChargeAmount = 3f;    // 1�q�b�g������̑�����
    public float currentUltCharge = 0f;  // ���݂̃`���[�W��

    private GameManager gameManager;


    // ���ǉ��F�G�ɍU�����q�b�g�������Ƃ�ʒm���邽�߂̃C�x���g
    public static event Action OnEnemyHit;

    // ���ǉ��F�O������C�x���g�𔭐������邽�߂̌��J���\�b�h
    public static void RaiseOnEnemyHit()
    {
        // ���\�b�h�̓�������Ȃ�C�x���g���Ăяo����
        OnEnemyHit?.Invoke();
    }


    private Camera mainCamera;

    // ���ǉ��F�C�x���g�̍w�ǂ��J�n
    private void OnEnable()
    {
        OnEnemyHit += HandleEnemyHit;
    }

    // ���ǉ��F�C�x���g�̍w�ǂ������i�I�u�W�F�N�g�j�����ɕK���s���j
    private void OnDisable()
    {
        OnEnemyHit -= HandleEnemyHit;
    }

    void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // --- 1. �`���[�W�𑝂₷ ---
        // ���Ԍo�߂Ń`���[�W
        currentUltCharge += passiveChargeRate * Time.deltaTime;
        // �ő�l�𒴂��Ȃ��悤�ɐ���
        currentUltCharge = Mathf.Min(currentUltCharge, maxUltCharge);

        // --- 2. ���ˏ������`�F�b�N ---
        // ���ύX�F�N���b�N�ɉ����āA�`���[�W�����^���ł��邩���m�F
        if (!gameManager.IsPaused && Input.GetMouseButtonDown(0) && currentUltCharge >= maxUltCharge)
        {
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            Shoot(targetPosition);

            // ���ǉ��F���˂�����`���[�W�����Z�b�g
            currentUltCharge = 0f;
        }

        // --- 3. UI���X�V�i�C�Ӂj ---
        /*if (ultChargeSlider != null)
        {
            ultChargeSlider.value = currentUltCharge;
        }*/
    }

    void Shoot(Vector3 target)
    {
        if (projectilePrefab == null) return;
        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        TargetedProjectile projectile = projectileObj.GetComponent<TargetedProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(target, projectileSpeed);
        }
    }

    // ���ǉ��F�C�x���g�������������ɌĂяo����郁�\�b�h
    private void HandleEnemyHit()
    {
        // �q�b�g�����̂Ń`���[�W��ǉ�
        currentUltCharge += hitChargeAmount;
    }
}