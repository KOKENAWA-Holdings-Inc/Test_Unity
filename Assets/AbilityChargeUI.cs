using UnityEngine;
using UnityEngine.UI; // UI���������߂ɕK�v

public class AbilityChargeUI : MonoBehaviour
{
    [Header("UI�Q��")]
    [Tooltip("�u�[�X�g�̃`���[�W�ʂ�\������X���C�_�[")]
    [SerializeField] private Slider chargeSlider;

    // �Ď��Ώۂ̃X�N���v�g���i�[����ϐ�
    private PlayerMoveSpeedAbility speedAbility;

    void Start()
    {
        // �V�[�����ɑ��݂���PlayerMoveSpeedAbility�R���|�[�l���g��T��
        speedAbility = FindObjectOfType<PlayerMoveSpeedAbility>();

        // �X�N���v�g�܂��̓X���C�_�[��������Ȃ��ꍇ�̓G���[���o��
        if (speedAbility == null)
        {
            Debug.LogError("�V�[����PlayerMoveSpeedAbility�R���|�[�l���g��������܂���I");
            this.enabled = false; // �X�N���v�g�𖳌���
            return;
        }
        if (chargeSlider == null)
        {
            Debug.LogError("Charge Slider��Inspector����ݒ肳��Ă��܂���I");
            this.enabled = false;
            return;
        }

        // �X���C�_�[�̍ő�l�������ݒ�
        chargeSlider.maxValue = speedAbility.MaxCharge;
    }

    void Update()
    {
        // �X�N���v�g�ƃX���C�_�[������ɑ��݂���ꍇ�̂݁A���t���[���l���X�V
        if (speedAbility != null && chargeSlider != null)
        {
            // �X���C�_�[�̌��݂̒l���A�u�[�X�g�̌��݂̃`���[�W�ʂɍ��킹��
            chargeSlider.value = speedAbility.CurrentCharge;
        }
    }
}