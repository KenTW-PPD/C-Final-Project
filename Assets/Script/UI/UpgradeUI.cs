using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class UpgradeUI : MonoBehaviour
{
    // 三選一的介面
    public List<Weapons> WeaponList;
    public Character character;
    public List<OptionUI> options;
    private Reword reword = new Reword();
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false; 
    }
    public void BuildUpgradeUI()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        WeaponList = character.WeaponList;


        // 新增並定義一升級表單
        reword.allPrize = new List<Prize>();
        reword.allPrize.Add(character);
        foreach (Weapons w in WeaponList)
            reword.allPrize.Add(w);
        // 得到3個升級選項
        List<UpgradeOption> upgradeOptions = reword.GetRandomChoices(3);
        
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("Upgrade Options Generated" + upgradeOptions[i].Name);
            //Debug.Log("Upgrade Options Generated" + upgradeOptions[i].Description);
            options[i].BuildOption(upgradeOptions[i]);
        }
    }


}
