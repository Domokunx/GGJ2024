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
        SpawnChicken();
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
            Spawn(dupeAPrefab);
        }

        // Spawn DupeB
        for (int i = 0; i < amountDupeB; i++)
        {
            Spawn(dupeBPrefab);
        }
    }

    public void SpawnChicken()
    {
        Spawn(datePrefab);
    }

    public void Spawn(GameObject prefab) 
    {
        GameObject gameObject = Instantiate(prefab);

        // spawn random position
        Vector2 randomPosition = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 1f));
        gameObject.transform.position = new Vector3(randomPosition.x, randomPosition.y, 0);

        // flip
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int flip = Random.Range(0, 2);

        Debug.Log(flip);

        if (flip == 0) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }
}
