using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectDupe : MonoBehaviour
{
    [SerializeField] private GameObject transitionScreen;
    [SerializeField] private GameObject finishText;
    [SerializeField] private int correctNum = 3;
    [SerializeField] private GameTimer gameTimer;

    [Header("Zoom Setting")]
    [SerializeField] private float zoomInSize = 3f;
    [SerializeField] private float zoomOutSize = 5f;
    [SerializeField] private float zoomTime = 0.1f;


    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correct, wrong;


    private GameObject hitObject;
    private Spawner spawner;
    private bool finished;

    private int correctCount;

    private Camera camera;

    private float zoomTimer;

    private CountDown countDown;


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
        finished = false;
        hitObject = null;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        countDown = this.GetComponent<CountDown>();
        finishText.SetActive(false);
        camera = Camera.main;

        state = State.Idle;
        zoomTimer = 0f;

        transitionScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MiniGameManager.miniGameStarted) return;

        switch (state)
        {
            case State.Idle:
                {

                    if (finished)
                    {
                        finishText.SetActive(true);
                        GameFinsished();

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
                Destroy(hitObject);
                audioSource.clip = wrong;
                audioSource.Play();
            }
            else
            {
                correctCount++;
                if (correctCount == correctNum)
                {
                    gameTimer.SetPaused(true);
                }

                audioSource.clip = correct;
                audioSource.Play();

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

        zoomTimer += Time.deltaTime;

        if (zoomTimer > 2.0f)
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
            zoomTimer = 0f;

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

        zoomTimer += Time.deltaTime;

        if (zoomTimer > 0.5f)
        {
            state = State.Idle;
            zoomTimer = 0f;

        }
    }

    private void GameFinsished()
    {
        GameManager.rizzed[1] = true;

        StartCoroutine(GameManager.BackToOutfitSelector(transitionScreen));
    }

}
