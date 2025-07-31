using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld1 : MonoBehaviour
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
        PlayerLanceShooter lanceShooterComponent = playerObj.GetComponent<PlayerLanceShooter>();
        OrbitManager OrbitComponent = playerObj.GetComponent<OrbitManager>();
        if (playerComponent == null)
        {
            Debug.LogError("Player�I�u�W�F�N�g��Player�X�N���v�g���A�^�b�`����Ă��܂���B");
            return;
        }


        // ������ ����ȍ~�A'player' �̑���� 'playerComponent' ���g�� ������
        switch (GameManager.selectedItem2)
        {
            case "Attack+10%":
                playerComponent.Attack = playerComponent.Attack * 1.1f;
                break;
            case "Defence+5%":
                playerComponent.Defence = playerComponent.Defence * 1.05f;
                break;
            case "MaxHP+5%":
                // ���W�b�N�����P: �ő�HP�𑝂₵�A����HP���ő�l�ɂ���
                playerComponent.PlayerMAXHP = playerComponent.PlayerMAXHP * 1.05f;
                playerComponent.PlayerHP = playerComponent.PlayerMAXHP;
                break;
            case "MoveSpeed+0.1":
                playerComponent.moveSpeed += 0.1f;
                break;
            case "Cool Time-10%":
                OrbitComponent.respawnDelay = OrbitComponent.respawnDelay * 0.9f;
                lanceShooterComponent.shootCooldown = lanceShooterComponent.shootCooldown * 0.9f;
                break;
            case "Weapon Speed+30%":
                OrbitComponent.orbitSpeed = OrbitComponent.orbitSpeed * 1.3f;
                lanceShooterComponent.bulletSpeed = lanceShooterComponent.bulletSpeed * 1.3f;
                break;
        }

        GameManager.ResumeGame();
        GameManager.PassiveUI.SetActive(false);
        GameManager.PassiveUI1.SetActive(false);
    }
}