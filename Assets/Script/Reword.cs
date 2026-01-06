using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Reword
{
    public List<Prize> allPrize = new List<Prize>();

    // 抽獎機制
    public int RarityLevel()
    {
        int r = Random.Range(1, 101);
        // 分五個等級 common 45%, ucommon  25%, rare 15% , epic 10% , legendary 5%
        switch (r)
        {
            case >= 95:
                return 5;
            case >= 85:
                return 4;
            case >= 70:
                return 3;
            case >= 45:
                return 2;
            default:
                return 1;
        }
    }
    public List<UpgradeOption> GetRandomChoices(int times)
    {
        List<UpgradeOption> choices = new List<UpgradeOption>();

        for (int i = 0; i < times; i++)
        {
            int rarity = RarityLevel();
            int randomIndex = Random.Range(0, allPrize.Count);

            UpgradeOption opt = allPrize[randomIndex].GenerateOption(rarity);
            choices.Add(opt);
        }
        return choices;
    }

}
