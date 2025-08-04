using System.Collections.Generic;
using System.Linq; // �O�̂��ߒǉ�
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    public bool IsPaused => isPaused;
    private int playerLvre;
    // public Player player; // �� �s�v�Ȃ̂ō폜
    private Player targetPlayer; // �� �����Ō��o���邽�߂̕ϐ�

    public GameObject PassiveUI; // ������passiveChoiceUIParent�ŊǗ�����
    public GameObject PassiveUI1; //
    public string selectedItem1;
    public string selectedItem2;

    [Header("UI�Q��")]
    public GameObject passiveChoiceUIParent;
    public TextMeshProUGUI RandomPassive1;
    public TextMeshProUGUI RandomPassive2;
    public TextMeshProUGUI Choice;

    [Header("�I�����̊Ǘ�")]
    public string[] masterItemChoices = { "Attack+10%", "Defence+5%", "MaxHP+5%", "MoveSpeed+0.1", "Cool Time-2%", "Weapon Speed+3%" };
    private List<string> remainingChoices;

    void Start()
    {
        // �ŏ���UI���\���ɂ��Ă���
        if (passiveChoiceUIParent != null)
        {
            passiveChoiceUIParent.SetActive(false);
        }  

        // �� �v���C���[��T���A�������烌�x���Ď����n�߂�R���[�`�����J�n
        StartCoroutine(InitializeAndMonitorPlayer());
    }

    // �� �v���C���[�̏������ƃ��x���Ď���S�čs���R���[�`��
    private IEnumerator InitializeAndMonitorPlayer()
    {
        // --- 1. �v���C���[��������܂őҋ@ ---
        while (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.GetComponent<Player>();
            }
            yield return null; // 1�t���[���҂��Ă���Č���
        }

        // --- 2. �v���C���[�����������珉���ݒ���s�� ---
        playerLvre = (int)targetPlayer.PlayerLv;
        ResetRemainingChoices();
        //Debug.Log("�v���C���[�����o���܂����B���x���Ď����J�n���܂��B");

        // --- 3. ���x���A�b�v�̊Ď����[�v�iUpdate�̑���j ---
        while (true)
        {
            // isPaused��false�̎��������x���A�b�v�����m����
            if (!isPaused)
            {
                if (targetPlayer.PlayerLv != playerLvre)
                {
                    DisplayUniqueChoices();
                    PauseGame();
                    playerLvre = (int)targetPlayer.PlayerLv;

                    if (passiveChoiceUIParent != null)
                    {
                        passiveChoiceUIParent.SetActive(true);
                        PassiveUI.SetActive(true);
                        PassiveUI1.SetActive(true);
                    }
                        
                }
            }
            yield return null; // 1�t���[���҂�
        }
    }

    // void Update() �̓R���[�`���ɓ������ꂽ���ߕs�v

    public void ResetRemainingChoices()
    {
        remainingChoices = new List<string>(masterItemChoices);
    }

    public void DisplayUniqueChoices()
    {
        ResetRemainingChoices();

        int randomIndex1 = Random.Range(0, remainingChoices.Count);
        selectedItem1 = remainingChoices[randomIndex1];
        remainingChoices.RemoveAt(randomIndex1);

        int randomIndex2 = Random.Range(0, remainingChoices.Count);
        selectedItem2 = remainingChoices[randomIndex2];

        RandomPassive1.text = selectedItem1;
        RandomPassive2.text = selectedItem2;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (passiveChoiceUIParent != null)
        {
            passiveChoiceUIParent.SetActive(false);
        }
            
    }
}