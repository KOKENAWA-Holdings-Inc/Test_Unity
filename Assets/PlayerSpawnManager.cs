using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {

        if (playerPrefab != null)
        {
            // �v���C���[�����W(0, 0, 0)�ɁA��]�Ȃ��Ő�������
            // Vector3.zero �� (0, 0, 0) �Ɠ���
            // Quaternion.identity �͉�]���Ȃ���Ԃ��w��
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

