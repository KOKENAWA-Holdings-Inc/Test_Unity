using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    // カメラのTransform
    private Transform cameraTransform;

    // スプライトの幅と高さ
    private float spriteWidth;
    private float spriteHeight;

    // グリッドの大きさ（今回は3x3）
    private const int GridSize = 3;

    void Start()
    {
        // メインカメラのTransformを取得
        cameraTransform = Camera.main.transform;

        // このオブジェクトのスプライトの幅と高さを取得
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
        spriteHeight = spriteRenderer.sprite.bounds.size.y;
    }

    void LateUpdate()
    {
        // === 水平方向のループチェック ===
        // カメラがこの背景よりも右に大きく移動したら
        if (transform.position.x + spriteWidth < cameraTransform.position.x)
        {
            // 背景を右端に移動させる
            transform.position += new Vector3(spriteWidth * GridSize, 0, 0);
        }
        // カメラがこの背景よりも左に大きく移動したら
        else if (transform.position.x - spriteWidth > cameraTransform.position.x)
        {
            // 背景を左端に移動させる
            transform.position -= new Vector3(spriteWidth * GridSize, 0, 0);
        }

        // === 垂直方向のループチェック (追加) ===
        // カメラがこの背景よりも上に大きく移動したら
        if (transform.position.y + spriteHeight < cameraTransform.position.y)
        {
            // 背景を上端に移動させる
            transform.position += new Vector3(0, spriteHeight * GridSize, 0);
        }
        // カメラがこの背景よりも下に大きく移動したら
        else if (transform.position.y - spriteHeight > cameraTransform.position.y)
        {
            // 背景を下端に移動させる
            transform.position -= new Vector3(0, spriteHeight * GridSize, 0);
        }
    }
}