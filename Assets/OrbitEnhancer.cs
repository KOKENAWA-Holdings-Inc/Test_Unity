using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEnhancer : MonoBehaviour
{
    [Header("能力設定")]
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private float attackMultiplier = 1.5f;
    [SerializeField] private float speedMultiplier = 3.0f;

    // ★追加: 独立したクールダウン時間を設定する変数
    [SerializeField] private float cooldown = 30.0f;

    private OrbitManager orbitManager;
    private float abilityReadyTime = 0f;

    // ★変更: UI用のプロパティが新しいcooldown変数を返すようにする
    public float AbilityReadyTime => abilityReadyTime;
    public float CooldownDuration => cooldown;


    void Start()
    {
        orbitManager = GetComponent<OrbitManager>();
        if (orbitManager == null)
        {
<<<<<<< HEAD
            //Debug.LogError("OrbitManager�������I�u�W�F�N�g�ɃA�^�b�`����Ă��܂���I");
=======
            Debug.LogError("OrbitManagerが同じオブジェクトにアタッチされていません！");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.time >= abilityReadyTime && orbitManager.SpawnedObjects.Count > 0)
        {
            StartCoroutine(ActivateOrbitBuffCoroutine());
        }
    }

    private IEnumerator ActivateOrbitBuffCoroutine()
    {
        // --- 1. バフ開始処理 ---
        // ★変更: 次に使用可能になる時刻を「現在時刻 + クールダウン」に設定
        abilityReadyTime = Time.time + duration + cooldown;
<<<<<<< HEAD
        //Debug.Log("�I�[�r�b�g�����I");
=======
        Debug.Log("オービット強化！");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b

        float originalSpeed = orbitManager.orbitSpeed;
        orbitManager.orbitSpeed *= speedMultiplier;

        foreach (GameObject orb in orbitManager.SpawnedObjects)
        {
            OrbitStatus status = orb.GetComponent<OrbitStatus>();
            if (status != null)
            {
                status.OrbitAttack *= attackMultiplier;
            }
        }
        orbitManager.StopLifecycle();

        // --- 2. バフ継続時間だけ待機 ---
        yield return new WaitForSeconds(duration);

<<<<<<< HEAD
        // --- 3. �o�t�I������ ---
        //Debug.Log("�I�[�r�b�g�����I���B�N�[���_�E���J�n�B");
=======
        // --- 3. バフ終了処理 ---
        Debug.Log("オービット強化終了。クールダウン開始。");
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
        orbitManager.orbitSpeed = originalSpeed;
        orbitManager.RestartLifecycle();
    }
}
