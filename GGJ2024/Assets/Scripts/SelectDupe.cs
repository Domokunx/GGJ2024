using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectDupe : MonoBehaviour
{
    [SerializeField] private GameObject finishText;
    [SerializeField] private int correctNum = 3;

    private GameObject hitObject;
    private Spawner spawner;

    private bool mistake;
    private bool finished;

    private int correctCount;

    // Start is called before the first frame update
    void Start()
    {
        mistake = false;
        finished = false;
        hitObject = null;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        finishText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            finishText.SetActive(true);
            return;
        }

        ClickDupes();


        //if(mistake)
        //{
        //    StartCoroutine(FadeOut(hitObject));
        //}
    }

    public IEnumerator FadeOut(GameObject gameObject)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(
            spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.05f);

        yield return new WaitForSeconds(2f);

        mistake = false;
        hitObject = null;
    }

    private void ClickDupes()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        RaycastHit2D hit = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);

            hitObject = hit.collider.gameObject;

            if (hitObject.tag != "Date")
            {
                mistake = true;
                Destroy(hitObject);
            }
            else
            {
                correctCount++;
                if (correctCount == correctNum)
                {
                    finished = true;
                    return;
                }
                spawner.SpawnDupes();

                // change date position
                Vector2 randomPosition = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 0f));
                hitObject.transform.position = new Vector3(randomPosition.x, randomPosition.y, 0);

            }
        }
    }
}
