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
            Debug.Log("�U���Ώۂ̓G��������܂���B");
            return;
        }

        GameObject ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
        RingController ringController = ring.GetComponent<RingController>();

        if (ringController != null)
        {
            // ���ύX�_: Initialize�ɓn���������玩�g��transform���폜
            ringController.Initialize(nearestTarget.transform);
        }
    }

    // (FindNearestTarget���\�b�h�͕ύX����܂���)
    private GameObject FindNearestTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var bosses = GameObject.FindGameObjectsWithTag("Boss");
        var allTargets = enemies.Concat(bosses).ToList();

        if (allTargets.Count == 0) return null;

        return allTargets.OrderBy(t => Vector2.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }
}