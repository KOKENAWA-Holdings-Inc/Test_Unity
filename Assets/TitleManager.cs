using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI titletext;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        titletext.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // Boss�̐ÓI�C�x���g�ɁA������UI�\�����\�b�h��o�^�i�w�ǁj����
        Boss.OnBossDied += TitleUI;
    }

    // ���̃I�u�W�F�N�g�������ɂȂ������ɌĂ΂��
    void OnDisable()
    {
        // �o�^����������i���������[�N�h�~�̂��ߏd�v�j
        Boss.OnBossDied -= TitleUI;
    }

    public void TitleUI() 
    {
        titletext.enabled = true;
    }
    public void TitleButto()
    {
        GameManager.ResumeGame();
        //Debug.Log("ok");
        SceneManager.LoadScene("Start Scene");

    }
}
