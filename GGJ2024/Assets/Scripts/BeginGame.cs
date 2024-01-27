using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    void Start()
    {
        
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("OutfitSelector");
    }

    public void BeginToGame()
    {
        StartCoroutine(ChangeScene());
    }
}
