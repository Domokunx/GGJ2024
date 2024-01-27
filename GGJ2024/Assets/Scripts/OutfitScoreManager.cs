using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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

    bool isEquipHat;
    bool isEquipCloth;
    bool isEquipShoe;

    private const int MINIGAME_NUM = 4;
    // Start is called before the first frame update
    void Start()
    {
        ReadData();
        currentDateNum = GameManager.nextDate - 1;

        totalScore = 0;

        isEquipHat = false;
        isEquipCloth = false;
        isEquipShoe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEquipHat)
        {
            if (ScoreCount(hat)) isEquipHat = true;
            else isEquipHat = false;
        }
        
        //if (!isEquipCloth)
        //{
        //    if (ScoreCount(cloth)) isEquipCloth = true;
        //    else isEquipCloth = false;
        //}

        //if (!isEquipShoe)
        //{
        //    if (ScoreCount(hat)) isEquipShoe = true;
        //    else isEquipShoe = false;
        //}

        scoreText.text = "Score: " + totalScore.ToString();
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

    bool ScoreCount(ItemSlot item)
    {
        if (item.GetCurrentItem() == null) return false;

        Debug.Log(item.GetCurrentItem().name);
        int outfitIndex = 0;
        for (int i = 0; i < outfitsData.Count; i++)
        {
            if (outfitsData[i].name + "(Clone)" == item.GetCurrentItem().name)
            {
                Debug.Log("correct name");
                outfitIndex = i;
                break;
            }
        }

        totalScore += outfitsData[outfitIndex].scoreMini[currentDateNum - 1];

        return true;
    }
}
