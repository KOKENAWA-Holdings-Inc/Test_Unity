using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerRingAttack : MonoBehaviour
{
    [SerializeField] private GameObject ringPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireRing();
        }
    }

    private void FireRing()
    {
        GameObject nearestTarget = FindNearestTarget();
        if (nearestTarget == null)
        {
            Debug.Log("攻撃対象の敵が見つかりません。");
            return;
        }

        GameObject ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
        RingController ringController = ring.GetComponent<RingController>();

        if (ringController != null)
        {
            // ★変更点: Initializeに渡す引数から自身のtransformを削除
            ringController.Initialize(nearestTarget.transform);
        }
    }

    // (FindNearestTargetメソッドは変更ありません)
    private GameObject FindNearestTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var bosses = GameObject.FindGameObjectsWithTag("Boss");
        var allTargets = enemies.Concat(bosses).ToList();

        if (allTargets.Count == 0) return null;

        return allTargets.OrderBy(t => Vector2.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }
}