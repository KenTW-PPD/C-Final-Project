using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class OptionUI : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image image;
    public UpgradeUI maneger;
    public Button selectButton;

    private UpgradeOption currentOption;
    public void BuildOption(UpgradeOption option)
    {
        currentOption = option;
        title.text = option.Name;
        description.text = option.Description;
        if(option.icon != null)
            image.sprite = option.icon;
        // 清除所有監聽器 防止重複執行
        selectButton.onClick.RemoveAllListeners();
        // 綁定按鈕按下事件
        selectButton.onClick.AddListener(() => {
            ExeuteOption();
        });
    }
    private void ExeuteOption()
    {
        if(currentOption != null)
        {
            currentOption.OnSelect?.Invoke();
            maneger.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1f;
        }
    }


}
