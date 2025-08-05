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
    public float minSummonInterval = 2f;
    [Tooltip("���������s����Œ��Ԋu�i�b�j")]
    public float maxSummonInterval = 5f;

    void Start()
    {
        StartCoroutine(FindBossAndStartSummoning());
    }

    void Update()
    {
        // ���̃X�N���v�g�̓R���[�`���œ��삷�邽��Update�͕s�v
    }

    private IEnumerator FindBossAndStartSummoning()
    {
        while (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            yield return new WaitForSeconds(1f);
        }

        if (markerCanvas != null)
        {
            markerCanvas.gameObject.SetActive(false);
        }
        else
        {
            //Debug.LogError("Marker Canvas��Inspector����ݒ肳��Ă��܂���I");
        }

        StartCoroutine(SummoningLoopCoroutine());
    }

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

        // --- UI�}�[�J�[�̕\���ƒǏ]���� ---
        markerCanvas.gameObject.SetActive(true);
        //Debug.Log("�v���C���[���^�[�Q�b�g�ɐݒ肵�܂����B");

        float markerVisibleTime = 0.5f;
        float timer = 0f;
        Vector3 lastKnownPosition = playerObj.transform.position; // �Ō�̍��W��ۑ�����ϐ�

        // 0.5�b�ԁA�}�[�J�[���v���C���[�̈ʒu�ɒǏ]�����郋�[�v
        while (timer < markerVisibleTime)
        {
            // ���[�v���Ƀv���C���[���j�󂳂ꂽ�ꍇ�ɑΉ�
            if (playerObj == null)
            {
                markerCanvas.gameObject.SetActive(false);
                yield break; // �v���C���[�����Ȃ��Ȃ�����R���[�`���𒆒f
            }

            // �}�[�J�[�̈ʒu���v���C���[�̌��݈ʒu�ɍX�V���A���̍��W��ۑ�
            lastKnownPosition = playerObj.transform.position;
            markerCanvas.transform.position = lastKnownPosition;

            timer += Time.deltaTime;
            yield return null; // 1�t���[���ҋ@
        }

        // 0.5�b�o�ߌ�A�}�[�J�[���\���ɂ���
        markerCanvas.gameObject.SetActive(false);

        // --- ���̏������� ---
        // �����0.2�b�ҋ@ (���v0.7�b)
        yield return new WaitForSeconds(0.2f);

        // ���ύX: �}�[�J�[���Ō�ɂ��������W�ɋ�����������
        Instantiate(ballPrefab, lastKnownPosition, Quaternion.identity);
        //Debug.Log("�v���C���[�̈ʒu�ɋ����������܂����B");
    }
}