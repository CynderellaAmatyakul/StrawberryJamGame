using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlowTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject radiusIndicatior;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float slowSpeed = 0.5f;
    [SerializeField] private float freezeTime = 2f;
    [SerializeField] private float aps = 1.5f;

    private float timeUnitFire;

    private void Update()
    {
        timeUnitFire += Time.deltaTime;

        if (timeUnitFire >= 1f / aps)
        {
            //Debug.Log("<color=cyan>Freeze</color>");
            Freeze();
            timeUnitFire = 0f;
        }
    }

    private void Freeze()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        radiusIndicatior.SetActive(true);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(slowSpeed);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
        radiusIndicatior.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
