using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitEnhanceUIManager : MonoBehaviour
{
    [Header("UI�Q��")]
    [Tooltip("�N�[���_�E����\������X���C�_�[")]
    [SerializeField] private Slider cooldownSlider;

    // �Ď��Ώۂ̃X�N���v�g���i�[����ϐ�
    private OrbitEnhancer orbitEnhancer;

    void Start()
    {
        // �V�[�����ɑ��݂���OrbitEnhancer�R���|�[�l���g��T��
        orbitEnhancer = FindObjectOfType<OrbitEnhancer>();

        if (orbitEnhancer == null)
        {
            Debug.LogError("�V�[����OrbitEnhancer�R���|�[�l���g��������܂���I");
            this.enabled = false;
            return;
        }
        if (cooldownSlider == null)
        {
            Debug.LogError("Cooldown Slider��Inspector����ݒ肳��Ă��܂���I");
            this.enabled = false;
            return;
        }

        
    }

    void Update()
    {
        if (orbitEnhancer == null || cooldownSlider == null) return;

        // ���ݎ������A�A�r���e�B���g�p�\�ɂȂ鎞�����O���H�i���N�[���_�E�������H�j
        if (Time.time < orbitEnhancer.AbilityReadyTime)
        {
            

            // �N�[���_�E���̎c�莞�Ԃ��v�Z
            float timeRemaining = orbitEnhancer.AbilityReadyTime - Time.time;
            // �S�̂̃N�[���_�E�����Ԃɑ΂���i�������v�Z�i0.0�`1.0�j
            float progress = (orbitEnhancer.CooldownDuration - timeRemaining) / orbitEnhancer.CooldownDuration;

            // �X���C�_�[�̒l���X�V
            cooldownSlider.value = progress;
        }
        
    }
}
