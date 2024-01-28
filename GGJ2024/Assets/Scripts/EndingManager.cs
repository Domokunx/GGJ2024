using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in sprites)
        {
            go.SetActive(false);
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            if (PickYourDateManager.selected[i])
            {
                sprites[i].gameObject.SetActive(true);
            }
        }
    }

}
