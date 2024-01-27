using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int nextDate = 2;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static IEnumerator MoveToNextDate()
    {
        // Play transition for 5s
        yield return new WaitForSeconds(4f);

        // Load next date
        SceneManager.LoadScene(nextDate++);
        
    }
    public static IEnumerator BackToOutfitSelector(GameObject transition)
    {
        // Play transition for 5s
        transition.SetActive(true);
        yield return new WaitForSeconds(4f);

        // Move back to outfitSelector
        
        SceneManager.LoadScene("OutfitSelector");
    }
}
