using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [Header("Attribute")]
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxItemsOnMap = 1;

    private void Start()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    private void SpawnItem()
    {
        if (itemPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        // Count existing items on map
        GameObject[] existingItems = GameObject.FindGameObjectsWithTag("Pickup");
        if (existingItems.Length >= maxItemsOnMap) return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject itemToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        Instantiate(itemToSpawn, randomSpawnPoint.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;
        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(spawnPoint.position, 0.2f);
        }
    }
}