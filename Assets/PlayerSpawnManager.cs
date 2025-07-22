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
            // プレイヤーを座標(0, 0, 0)に、回転なしで生成する
            // Vector3.zero は (0, 0, 0) と同じ
            // Quaternion.identity は回転がない状態を指す
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

