using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.instance.path[pathIndex];
    }

    private void Update()
    {
        if (target == null) return;

        // Move directly towards target
        Vector3 moveDirection = (target.position - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check if reached target waypoint
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == LevelManager.instance.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            target = LevelManager.instance.path[pathIndex];
        }

        // Rotate to face movement direction
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}