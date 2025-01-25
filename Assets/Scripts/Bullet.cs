using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Vector3 direction;
    private Vector3 targetInitialPosition;

    public void SetTarget(Transform _target)
    {
        targetInitialPosition = _target.position;
    }

    private void Start()
    {
        direction = (targetInitialPosition - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        if (gameObject.name != "SniperBoba(Clone)")
        {
            Destroy(gameObject);
        }
    }
}