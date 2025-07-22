using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    // �w�肳�ꂽ�Q�[���I�u�W�F�N�g�iUI�p�l���j���A�N�e�B�u�ɂ��郁�\�b�h
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    // �w�肳�ꂽ�Q�[���I�u�W�F�N�g�iUI�p�l���j���A�N�e�B�u�ɂ��郁�\�b�h
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    // �w�肳�ꂽ�Q�[���I�u�W�F�N�g�iUI�p�l���j�̕\���E��\����؂�ւ��郁�\�b�h
    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}