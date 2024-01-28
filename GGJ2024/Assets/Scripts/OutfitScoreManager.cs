using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using static UnityEditor.Progress;

public class OutfitScoreManager : MonoBehaviour
{
    [SerializeField] private ItemSlot hat;
    [SerializeField] private ItemSlot cloth;
    [SerializeField] private ItemSlot shoe;

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dressupClothes;

    public struct Outfit
    {
        public string name;
        public int[] scoreMini;
    }

    private static List<Outfit> outfitsData = new List<Outfit>();
    private static bool loaded = false;
    private static int currentDateNum = 0;

    private int totalScore;

    bool partChanged;

    private const int MINIGAME_NUM = 4;
    private const int PART_NUM = 3;

    private string[] partName = new string[PART_NUM];

    private void Awake()
    {
        ReadData();
        Debug.Log("outfits score data size:" + outfitsData.Count);

    }

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;

        partChanged = false;

        currentDateNum++;

        for (int i = 0; i < partName.Length; i++)
        {
            partName[i] = "";
        }
        if(currentDateNum > MINIGAME_NUM)
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        SetPartName(hat,0);
        SetPartName(cloth,1);
        SetPartName(shoe,2);

        CalculateScore();
    }

    private void ReadData()
    {
        if (loaded) return;

        TextAsset csvFile;

        List<string[]> csvDatas = new List<string[]>();

        int row = 0;

        csvFile = Resources.Load("OutfitScoreData") as TextAsset;
        // read text changes to string
        StringReader reader = new StringReader(csvFile.text);
        // while end of file
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
            row++;
        }

        for (int i = 1; i < row; i++)
        {
            // temporaly data
            Outfit temp;
            temp.scoreMini = new int[MINIGAME_NUM];

            temp.name = csvDatas[i][0];
            temp.scoreMini[0] = int.Parse(csvDatas[i][1]);
            temp.scoreMini[1] = int.Parse(csvDatas[i][2]);
            temp.scoreMini[2] = int.Parse(csvDatas[i][3]);
            temp.scoreMini[3] = int.Parse(csvDatas[i][4]);

            outfitsData.Add(temp);

        }

        loaded = true;
    }

    void SetPartName(ItemSlot item, int partNum)
    {
        if (item.GetCurrentItem() == null) return;
        if (item.GetCurrentItem().name == partName[partNum]) return;
        
        partName[partNum] = item.GetCurrentItem().name;
        partChanged = true;
    }

    void CalculateScore()
    {
        if (!partChanged) return;

        audioSource.clip = dressupClothes;
        audioSource.Play();
        
        int[] outfitsIndex = new int[3]; 
        for (int i = 0; i < PART_NUM; i++)
        {
            for (int j = 0; j < outfitsData.Count; j++) 
            {
                if (outfitsData[j].name + "(Clone)" == partName[i])
                {
                    //Debug.Log("correct name");
                    outfitsIndex[i] = j;
                    break;
                }

            }
        }

        int hatScore   = 0;
        int clothScore = 0;
        int shoesScore = 0;

        for (int i = 0; i < partName.Length; i++)
        {
            Debug.Log(partName[i]);
        }

        if (partName[0] != "")
        {
            hatScore = outfitsData[outfitsIndex[0]].scoreMini[currentDateNum - 1];
        }
        if (partName[1] != "")
        {
            clothScore = outfitsData[outfitsIndex[1]].scoreMini[currentDateNum - 1];

        }
        if (partName[2] != "")
        {
            shoesScore = outfitsData[outfitsIndex[2]].scoreMini[currentDateNum - 1];
        }

        Debug.Log("HatScore:" + hatScore);
        Debug.Log("ClothScore:" + clothScore);
        Debug.Log("ShoesScore:" + shoesScore);

        totalScore = hatScore + clothScore + shoesScore;

        scoreText.text = "Rizz-meter: " + totalScore.ToString();


        partChanged = false;
    }

    public int GetScore()
    {
        return totalScore;
    }
}
