using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool[] rizzed = new bool[4];
    private static int nextDate = 2;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MoveToNext()
    {
        StartCoroutine(MoveToNextDate());
    }
    private IEnumerator MoveToNextDate()
    {
        // Play transition for 5s
        yield return new WaitForSeconds(4f);

        // Load next date
        SceneManager.LoadScene(nextDate++);
        StopAllCoroutines();
    }
    public static IEnumerator BackToOutfitSelector(GameObject transition)
    {
        // Play transition for 5s
        transition.SetActive(true);
        yield return new WaitForSeconds(4f);

        // Move back to outfitSelector
        
        SceneManager.LoadScene("OutfitSelector");
    }

    private void ResetVariables()
    {
        
    }
}
