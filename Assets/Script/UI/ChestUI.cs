using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class ChestUI : MonoBehaviour
{
    public List<Weapons> Allweapons;
    public List<Weapons> ActiveWeapons;
    public Character character;
    private Reword reword = new Reword();
    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Button button;
    private void Start()
    {
        if(this.GetComponent<Canvas>().enabled == false) 
        { 
            this.GetComponent<Canvas>().enabled = true; 
        }
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        this.GetComponent<Canvas>().enabled = false;
        ActiveWeapons = character.WeaponList;
        Debug.Log(ActiveWeapons[0].isEquiped());
    }

    private void FindAllActiveWeapons()
    {

        var map = new Dictionary<string, Weapons>();
        foreach (var w in ActiveWeapons)
        {
            string n = w.getWeaponName();
            //Debug.Log($"[Step 2] 掃描場上武器: 物件名={w.name}, 變數WeaponName='{n}'");
            if (!string.IsNullOrEmpty(n)) map[n] = w;
        }

        for (int i = 0; i < Allweapons.Count; i++)
        {
            string weaponName = Allweapons[i].name;
            
            if (map.ContainsKey(weaponName)) 
            {
                Allweapons[i] = map[weaponName];
                //Debug.Log(map[weaponName].isEquiped());
                //Debug.Log(Allweapons[i].isEquiped());
            }
            else
            {
                //Debug.Log("沒有此武器");
            }
        }
    }
    public void BuildChestUI()
    {
        FindAllActiveWeapons();
        List<Prize> allPrize = new List<Prize>();
        allPrize.Add(character);
        foreach (Weapons w in Allweapons)
            allPrize.Add(w);

        reword.allPrize = allPrize;
        UpgradeOption option = reword.GetRandomChoices(1)[0];
        title.text = option.Name;
        description.text = option.Description;
        if(option.icon != null)
            image.sprite = option.icon;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            option.OnSelect();
            this.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1f;
        });
    }





}

