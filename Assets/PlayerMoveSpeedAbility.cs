using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSpeedAbility : MonoBehaviour
{
    [Header("�u�[�X�g�ݒ�")]
    [SerializeField] private float moveSpeedBoost = 1.5f; // ���x�̑����{��

    [Header("�`���[�W�ݒ�")]
    [SerializeField] private float chargeMax = 30.0f;     // �`���[�W�̍ő�l
    [SerializeField] private float regenerationDelay = 3.0f;  // �񕜂��n�܂�܂ł̑ҋ@����
    [SerializeField] private float regenerationRate = 1.0f;   // 1�b������̉񕜗�
    [SerializeField] private float drainRate = 0.2f;      // 1�b������̍ő�`���[�W�ɑ΂��錸���� (20%)
    // ���ǉ�: �O�����猻�݂̃`���[�W�ʂ�ǂݎ�邽�߂̃v���p�e�B
    public float CurrentCharge => charge;
    // ���ǉ�: �O������ő�`���[�W�ʂ�ǂݎ�邽�߂̃v���p�e�B
    public float MaxCharge => chargeMax;

    private float charge;              // ���݂̃`���[�W��
    private bool isBoosting = false;   // ���݃u�[�X�g�����ǂ����̃t���O
    private float timeOfLastChargeChange; // �Ō�Ƀ`���[�W���ω���������

    private Player playerComponent; // �v���C���[�R���|�[�l���g�ւ̎Q��

    private GameManager gameManager;

    void Start()
    {
        // �ŏ��Ƀv���C���[��T���ăR���|�[�l���g��ێ����Ă����i�������j
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerComponent = playerObj.GetComponent<Player>();
        }
        else
        {
            //Debug.LogError("Player�I�u�W�F�N�g��������܂���ł����B");
            this.enabled = false; // �v���C���[�����Ȃ��Ȃ�X�N���v�g�𖳌���
            return;
        }

        charge = chargeMax; // �ŏ��̓`���[�W���^��
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // --- 1. E�L�[���͂̏��� ---
        if (!gameManager.IsPaused && Input.GetKeyDown(KeyCode.LeftShift))
        {
            // ���݃u�[�X�g���Ȃ��~�A�����łȂ���ΊJ�n�����݂�
            if (isBoosting)
            {
                StopBoost();
            }
            else
            {
                StartBoost();
            }
        }

        // --- 2. �u�[�X�g���̏��� ---
        if (isBoosting)
        {
            // �`���[�W������������
            charge -= (chargeMax * drainRate) * Time.deltaTime;
            timeOfLastChargeChange = Time.time; // �`���[�W���ω������������X�V

            // �`���[�W��0�ɂȂ����狭���I�Ƀu�[�X�g���I��
            if (charge <= 0)
            {
                //Debug.Log("�`���[�W�؂�I�u�[�X�g���I�����܂��B");
                StopBoost();
            }
        }
        // --- 3. ��u�[�X�g���̉񕜏��� ---
        else if (charge < chargeMax)
        {
            // �Ō�Ƀ`���[�W���ω����Ă���w��b���o�߂�����A�񕜂��J�n
            if (Time.time >= timeOfLastChargeChange + regenerationDelay)
            {
                charge += regenerationRate * Time.deltaTime;
            }
        }

        // �`���[�W��0������ő�l�𒴂��Ȃ��悤�ɒl���ۂ߂�
        charge = Mathf.Clamp(charge, 0f, chargeMax);
    }

    /// <summary>
    /// �u�[�X�g���J�n���鏈��
    /// </summary>
    private void StartBoost()
    {
        // �v���C���[���������Ă��āA�`���[�W�������ł��c���Ă�����J�n
        if (playerComponent != null && charge > 0)
        {
            isBoosting = true;
            playerComponent.moveSpeed *= moveSpeedBoost;
            timeOfLastChargeChange = Time.time;
            //Debug.Log("�u�[�X�g�J�n�I ���݂̑��x: " + playerComponent.moveSpeed);
        }
    }

    /// <summary>
    /// �u�[�X�g���~���鏈��
    /// </summary>
    private void StopBoost()
    {
        if (playerComponent != null && isBoosting) // isBoosting�`�F�b�N�œ�d��~��h�~
        {
            isBoosting = false;
            playerComponent.moveSpeed /= moveSpeedBoost;
            timeOfLastChargeChange = Time.time;
            //Debug.Log("�u�[�X�g��~�B ���݂̑��x: " + playerComponent.moveSpeed);
        }
    }
}