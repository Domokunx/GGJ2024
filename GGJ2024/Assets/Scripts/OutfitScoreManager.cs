using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using static UnityEditor.Progress;

public class OutfitScoreManager : MonoBehaviour
{
    [SerializeField] private ItemSlot hat;
    [SerializeField] private ItemSlot cloth;
    [SerializeField] private ItemSlot shoe;

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    public struct Outfit
    {
        public string name;
        public int[] scoreMini;
    }

    public static List<Outfit> outfitsData = new List<Outfit>();

    private int totalScore;
    private int currentDateNum;

    bool partChanged;

    private const int MINIGAME_NUM = 4;
    private const int PART_NUM = 3;

    private string[] partName = new string[PART_NUM];
    // Start is called before the first frame update
    void Start()
    {
        ReadData();
        currentDateNum = GameManager.nextDate - 1;

        totalScore = 0;

        partChanged = false;
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

        for(int i = 0; i < MINIGAME_NUM; i++)
        {
            Debug.Log(outfitsData[0].scoreMini[i]);
        }
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
        
        int[] outfitsIndex = new int[3]; 
        for (int i = 0; i < PART_NUM; i++)
        {
            for (int j = 0; j < outfitsData.Count; j++) 
            {
                if (outfitsData[j].name + "(Clone)" == partName[i])
                {
                    Debug.Log("correct name");
                    outfitsIndex[i] = j;
                    break;
                }

            }
        }
        int hatScore = outfitsData[outfitsIndex[0]].scoreMini[currentDateNum - 1];
        int clothScore = outfitsData[outfitsIndex[1]].scoreMini[currentDateNum - 1];
        int shoesScore = outfitsData[outfitsIndex[2]].scoreMini[currentDateNum - 1];

        totalScore = hatScore + clothScore + shoesScore;

        scoreText.text = "Score: " + totalScore.ToString();


        partChanged = false;
    }
}
