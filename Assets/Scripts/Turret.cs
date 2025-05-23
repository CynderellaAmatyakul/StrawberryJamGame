using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;

    private Transform target;
    private float timeUnitFire;

    private void Update()
    {
        if (gameObject.tag == "Pickup") return;

        timeUnitFire += Time.deltaTime;
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            
            if (timeUnitFire >= 1f / bps)
            {
                Shoot();
                timeUnitFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        //Debug.Log("<color=green>Shoot</color>");
        GameObject bullet = Instantiate(bulletPrefab, firingPoint[0].position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (name == "Milo")
        {
            GameObject bullet1 = Instantiate(bulletPrefab, firingPoint[1].position, Quaternion.identity);
            GameObject bullet2 = Instantiate(bulletPrefab, firingPoint[2].position, Quaternion.identity);
            GameObject bullet3 = Instantiate(bulletPrefab, firingPoint[3].position, Quaternion.identity);
            Bullet bulletScript1 = bullet1.GetComponent<Bullet>();
            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            Bullet bulletScript3 = bullet3.GetComponent<Bullet>();

            //firingPoint[1].position += firingPoint[1].position * 5f * Time.deltaTime;

            bulletScript1.SetTarget(firingPoint[1]);
            bulletScript2.SetTarget(firingPoint[2]);
            bulletScript3.SetTarget(firingPoint[3]);
        }
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;

        Quaternion targetRotaion = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotaion, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
