using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI�R���|�[�l���g���������߂ɕK�v

public class Summoner : MonoBehaviour
{
    [Header("��������I�u�W�F�N�g")]
    [Tooltip("�������鋅�̃v���n�u")]
    public GameObject ballPrefab;

    [Header("UI�ݒ�")]
    [Tooltip("�}�[�J�[�Ƃ��ĕ\������Canvas")]
    [SerializeField] private Canvas markerCanvas;

    [Header("�����^�C�~���O")]
    [Tooltip("���������s����ŒZ�Ԋu�i�b�j")]
    public float minSummonInterval = 5f;
    [Tooltip("���������s����Œ��Ԋu�i�b�j")]
    public float maxSummonInterval = 10f;

    void Start()
    {
        // ���ύX: �{�X��T���A���������烁�C���������J�n����R���[�`�����N��
        StartCoroutine(FindBossAndStartSummoning());
    }

    void Update()
    {
        // ���̃X�N���v�g�̓R���[�`���œ��삷�邽��Update�͕s�v
    }

    /// <summary>
    /// ���ǉ�: �{�X���o������܂őҋ@���A�o�������烁�C�����[�v���J�n����R���[�`��
    /// </summary>
    private IEnumerator FindBossAndStartSummoning()
    {
        //Debug.Log("�{�X��T���Ă��܂�...");

        // �{�X��������܂�1�b���ƂɒT��������
        while (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            yield return new WaitForSeconds(1f);
        }

        // --- �{�X������������A�������牺�̏����ݒ肪���s����� ---
        //Debug.Log("�{�X�𔭌��I�����������J�n���܂��B");

        if (markerCanvas != null)
        {
            markerCanvas.gameObject.SetActive(false);
        }
        else
        {
            //Debug.LogError("Marker Canvas��Inspector����ݒ肳��Ă��܂���I");
        }

        // ���C���̏������[�v���J�n����
        StartCoroutine(SummoningLoopCoroutine());
    }

    /// <summary>
    /// ���������������_���ȊԊu�Ŗ����ɌJ��Ԃ����߂̃��[�v
    /// </summary>
    private IEnumerator SummoningLoopCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(minSummonInterval, maxSummonInterval);
            yield return new WaitForSeconds(randomDelay);
            StartCoroutine(SummonSequenceCoroutine());
        }
    }

    private IEnumerator SummonSequenceCoroutine()
    {
        if (markerCanvas == null) yield break;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            //Debug.LogError("Player��������Ȃ��������߁A�����V�[�P���X�𒆒f���܂��B");
            yield break;
        }

        Vector3 summonPosition = playerObj.transform.position;

        // --- UI�}�[�J�[�̕\������ ---
        markerCanvas.transform.position = summonPosition;
        markerCanvas.gameObject.SetActive(true);
        //Debug.Log("�v���C���[���^�[�Q�b�g�ɐݒ肵�܂����B");

        yield return new WaitForSeconds(0.5f);

        markerCanvas.gameObject.SetActive(false);

        // --- ���̏������� ---
        yield return new WaitForSeconds(0.5f);

        Instantiate(ballPrefab, summonPosition, Quaternion.identity);
        //Debug.Log("�v���C���[�̈ʒu�ɋ����������܂����B");
    }
}