using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectDupe : MonoBehaviour
{
    [SerializeField] private GameObject finishText;
    [SerializeField] private int correctNum = 3;

    [Header("Zoom Setting")]
    [SerializeField] private float zoomInSize = 3f;
    [SerializeField] private float zoomOutSize = 5f;
    [SerializeField] private float zoomTime = 0.1f;

    private GameObject hitObject;
    private Spawner spawner;

    private bool isCorrect;
    private bool isMistake;
    private bool finished;

    private int correctCount;

    private Camera camera;

    private bool zoomed;

    private float timer;

    enum State
    {
        Idle,
        ZoomIn,
        ZoomOut,
    }

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        isMistake = false;
        isCorrect = false;
        finished = false;
        hitObject = null;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        finishText.SetActive(false);
        camera = Camera.main;
        zoomed = false;

        state = State.Idle;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                {

                    if (finished)
                    {
                        finishText.SetActive(true);
                        return;
                    }

                    ClickDupes();


                    break;
                }

            case State.ZoomIn:
                {
                    ZoomIn(hitObject);
                    //StartCoroutine(ZoomIn(hitObject));
                    break;
                }
            case State.ZoomOut:
                {
                    ZoomOut();
                    //StartCoroutine(ZoomOut());
                    break;
                }
        }
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
                isMistake = true;
                Destroy(hitObject);
            }
            else
            {
                isCorrect = true;
                correctCount++;

                // change state
                state = State.ZoomIn;
            }
        }
    }


    //public IEnumerator FadeOut(GameObject gameObject)
    //{
    //    SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    //    spriteRenderer.color = new Color(
    //        spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.05f);

    //    yield return new WaitForSeconds(2f);

    //    isMistake = false;
    //    hitObject = null;
    //}
    
    private void ZoomIn(GameObject gameObject)
    {
        Vector3 targetPos = gameObject.transform.position;
        targetPos.z = camera.transform.position.z;
        // camera zoom
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, zoomTime);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomInSize, zoomTime);

        timer += Time.deltaTime;

        if (timer > 2.0f)
        {
            Destroy(hitObject);

            if (correctCount == correctNum)
            {
                finished = true;
            }
            else
            {
                spawner.SpawnDupes();
                spawner.SpawnChicken();
            }

            state = State.ZoomOut;
            timer = 0f;

        }
    }

    //public IEnumerator ZoomIn(GameObject gameObject)
    //{
    //    Vector3 targetPos = gameObject.transform.position;
    //    targetPos.z = camera.transform.position.z;
    //    // camera zoom
    //    camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, zoomTime);
    //    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomInSize, zoomTime);

    //    yield return new WaitForSeconds(2f);

    //    // change chicken position
    //    Destroy(hitObject);

    //    if(state == State.ZoomIn)
    //    {
    //        spawner.SpawnDupes();
    //        spawner.SpawnChicken();
    //    }

    //    state = State.ZoomOut;

    //    hitObject = null;
    //}

    //public IEnumerator ZoomOut()
    //{
    //    camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0f, 0f, camera.transform.position.z), zoomTime);
    //    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomOutSize, zoomTime);

    //    yield return new WaitForSeconds(0.5f);

    //    state = State.Idle;
    //}
    private void ZoomOut()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0f, 0f, camera.transform.position.z), zoomTime);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomOutSize, zoomTime);

        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            state = State.Idle;
            timer = 0f;

        }
    }
}
