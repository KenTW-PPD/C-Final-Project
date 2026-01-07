using UnityEngine;
public class UpgradeOption
{
    public string Name;
    public string Description;
    public System.Action OnSelect;
    public Sprite icon;
}

public interface Prize
{
    UpgradeOption GenerateOption(int rarity);
}