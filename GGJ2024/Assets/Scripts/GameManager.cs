using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static IEnumerator BackToOutfitSelector(GameObject transition)
    {
        // Play transition for 5s
        transition.SetActive(true);
        yield return new WaitForSeconds(4f);

        // Move back to outfitSelector
        
        SceneManager.LoadScene(1);
    }
}
