using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
    public GameManager GameManager;
    // public Player player; // �� �s�v�Ȃ̂ō폜

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Theworld()
    {
        // �� ���̃��\�b�h���Ă΂ꂽ�u�Ԃ� "Player" �^�O�Ńv���C���[��T��
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        // �v���C���[��������Ȃ������ꍇ�́A�G���[���O���o���ď����𒆒f����
        if (playerObj == null)
        {
            Debug.LogError("Player�I�u�W�F�N�g��������܂���ł����B");
            return;
        }

        // ���������I�u�W�F�N�g����Player�X�N���v�g���擾����
        Player playerComponent = playerObj.GetComponent<Player>();
        if (playerComponent == null)
        {
            Debug.LogError("Player�I�u�W�F�N�g��Player�X�N���v�g���A�^�b�`����Ă��܂���B");
            return;
        }


        // ������ ����ȍ~�A'player' �̑���� 'playerComponent' ���g�� ������
        switch (GameManager.selectedItem1)
        {
            case "attack":
                playerComponent.Attack++;
                break;
            case "defence":
                playerComponent.Defence++;
                break;
            case "HP":
                // ���W�b�N�����P: �ő�HP�𑝂₵�A����HP���ő�l�ɂ���
                playerComponent.PlayerMAXHP += 5;
                playerComponent.PlayerHP = playerComponent.PlayerMAXHP;
                break;
            case "luck":
                playerComponent.Luck++;
                break;
        }

        GameManager.ResumeGame();
        GameManager.PassiveUI.SetActive(false);
        GameManager.PassiveUI1.SetActive(false);
    }
}