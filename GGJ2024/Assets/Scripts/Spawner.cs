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
    // Start is called before the first frame update
    void Start()
    {
        // Spawn DupeA
        for(int i = 0; i < amountDupeA; i++)
        {
            GameObject dupeA = Instantiate(dupeAPrefab);

            Vector2 randomPositionA = new Vector2(Random.Range(-9f, 9f) , Random.Range(-5f, 0f));

            dupeA.transform.position = new Vector3(randomPositionA.x, randomPositionA.y, 0);
        }

        // Spawn DupeB
        for(int i = 0; i < amountDupeB; i++)
        {
            GameObject dupeB = Instantiate(dupeBPrefab);

            Vector2 randomPositionB = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 0f));

            dupeB.transform.position = new Vector3(randomPositionB.x, randomPositionB.y, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
