using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    public GameObject damagePopupPrefab;
    private Transform playerTransform; // �v���C���[�Ǐ]�p��Transform
    public float EnemyHP = 10f;
    public float EnemyMaxHP = 10f;
    public float Attack = 1f;
    public int EnemyExperience = 1;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private UnityEngine.UI.Slider healthSlider;

    // ���ǉ�: HP�̕ω������m���邽�߂ɁA���O��HP��ۑ�����ϐ�
    private float previousHP;

    public static event Action OnEnemyDied;

    public void InitializeStats(float newMaxHp)
    {
        EnemyMaxHP = newMaxHp;
        EnemyHP = EnemyMaxHP; // HP���ő�l�ɐݒ�
    }
    private void Awake()
    {
        EnemyHP = EnemyMaxHP;
    }

    void Start()
    {
        // �Ǐ]���邽�߂Ƀv���C���[��Transform��ێ�
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        // ���ǉ�: ���O��HP��������
        previousHP = EnemyHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = EnemyMaxHP;
            healthSlider.value = EnemyHP;
            // ������Ԃł͔�\���ɂ���
            healthSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ���ύX�_: HP���ω��������ǂ������`�F�b�N
        if (EnemyHP != previousHP)
        {
            // HP�o�[����\���Ȃ�A�ŏ��ɕ\������
            if (healthSlider != null && !healthSlider.gameObject.activeSelf)
            {
                healthSlider.gameObject.SetActive(true);
            }

            // Slider�̒l���X�V
            if (healthSlider != null)
            {
                healthSlider.value = EnemyHP;
            }
            if (damagePopupPrefab != null)
            {
                float damage = previousHP - EnemyHP;

                // ���ύX: �G�̓���ɒ��ڐ�������
                Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // �G��1���j�b�g��ɕ\��
                GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);

                // ������ ���̌����Ȃ̂ŁA���̍s�����S�ɍ폜 ������
                // popup.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

                // ���������|�b�v�A�b�v�Ƀ_���[�W�ʂ�ݒ�
                popup.GetComponent<DamagePopup>().Setup(damage);
            }


            if (damagePopupPrefab != null)
            {
                float damage = previousHP - EnemyHP;
                // �_���[�W�|�b�v�A�b�v�𐶐�
                GameObject popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
                popup.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false); // Canvas�̎q�ɂ���
                popup.GetComponent<DamagePopup>().Setup(damage);
            }

            // ���݂�HP���u���O��HP�v�Ƃ��ĕۑ����A����̃t���[���Ŕ�r�ł���悤�ɂ���
            previousHP = EnemyHP;
        }

        // ���g��HP��0�ȉ��ɂȂ������𖈃t���[���Ď�����
        if (EnemyHP <= 0)
        {
            Die(); // �C�x���g�𔭍s

            // �V�[������ "Player" �^�O�̃I�u�W�F�N�g��T��
            GameObject playerToReward = GameObject.FindGameObjectWithTag("Player");

            // �v���C���[�����������ꍇ�̂݌o���l��^����
            if (playerToReward != null)
            {
                Player playerComponent = playerToReward.GetComponent<Player>();
                if (playerComponent != null)
                {
                    playerComponent.ExperiencePoint += EnemyExperience;
                    playerComponent.ExperienceTotal += EnemyExperience;
                }
            }

            // ���g��j�󂷂�
            Destroy(this.gameObject);
        }
    }

    // ���ǉ�: HP�o�[�̈ʒu��G�ɒǏ]�����邽�߂̏���
    private void LateUpdate()
    {
        // HP�o�[���\������Ă���ꍇ�̂݁A�ʒu���X�V����
        if (healthSlider != null && healthSlider.gameObject.activeSelf)
        {
            // �G�{�̂̈ʒu����^���0.5���炵���ʒu��HP�o�[��z�u����
            healthSlider.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    private void FixedUpdate()
    {
        // �v���C���[���������Ă���ꍇ�̂ݒǏ]
        if (playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    void Die()
    {
        OnEnemyDied?.Invoke();
    }
}
