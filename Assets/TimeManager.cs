using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{// Inspector����ݒ肷��e�L�X�gUI
    public TextMeshProUGUI timeText;

    public float elapsedTime; // �o�ߎ��Ԃ��L�^����ϐ�

    void Update()
    {
        // ���t���[���̎��Ԃ����Z���Ă���
        elapsedTime += Time.deltaTime;

        // �o�ߎ��Ԃ𕪂ƕb�ɕϊ�
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // �e�L�X�gUI�ɕ\�����镶����𐮌` (��: "02:05")
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
