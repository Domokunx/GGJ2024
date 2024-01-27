using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("DupeA")]
    [SerializeField] private GameObject dupeAPrefab;
    [SerializeField] private int amountDupeA;

    [Header("DupeB")]
    [SerializeField] private GameObject dupeBPrefab;
    [SerializeField] private int amountDupeB;

    [Header("Data")]
    [SerializeField] private GameObject datePrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnDupes();
        SpawnDate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDupes()
    {
        // Spawn DupeA
        for (int i = 0; i < amountDupeA; i++)
        {
            GameObject dupeA = Instantiate(dupeAPrefab);

            Vector2 randomPositionA = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 0f));

            dupeA.transform.position = new Vector3(randomPositionA.x, randomPositionA.y, 0);
        }

        // Spawn DupeB
        for (int i = 0; i < amountDupeB; i++)
        {
            GameObject dupeB = Instantiate(dupeBPrefab);

            Vector2 randomPositionB = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 0f));

            dupeB.transform.position = new Vector3(randomPositionB.x, randomPositionB.y, 0);
        }
    }

    public void SpawnDate()
    {
        GameObject date = Instantiate(datePrefab);

        Vector2 randomPosition = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 0f));

        date.transform.position = new Vector3(randomPosition.x, randomPosition.y, 0);
    }
}
