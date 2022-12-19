using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageBookSceneController : IntroBookSceneController
{
    [SerializeField]
    private GameObject _rangerPagePrefab;

    [SerializeField]
    private GameObject _backgroundImg;

    override protected void init()
    {
        _currentPage = _stateManager.GetStageBookPage();
        string backgroundImgPath =
            "StageBook/" + _stateManager.GetLastSceneToStageBookScene() + "BG";
        _backgroundImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(backgroundImgPath);
        int goldRatio = _stateManager.GetGoldRatio();
        _pages[0].transform.Find("GoldRatio").GetComponent<TMP_Text>().text =
            "Gold Ratio: " + goldRatio.ToString() + "%";
        List<string> specialConditions = _stateManager.GetSpecialConditions();
        if (specialConditions.Count > 0)
        {
            for (int i = 0; i < specialConditions.Count; i++)
            {
                _pages[0].transform.Find("SpecialConditionList").GetComponent<TMP_Text>().text +=
                    specialConditions[i] + "\n";
            }
        }
        else
        {
            _pages[0].transform.Find("SpecialConditionList").GetComponent<TMP_Text>().text = "None";
        }
        AdventurerInfo[] adventurerList = _adventurerManager.GetAdventurerList();
        for (int i = 0; i < adventurerList.Length; i++)
        {
            AdventurerInfo adventurer = adventurerList[i];
            GameObject page = Instantiate(_rangerPagePrefab);
            page.transform.SetParent(_book.transform);
            page.transform.position = _pages[0].transform.position;
            page.transform.localScale = new Vector3(1, 1, 1);
            page.transform.Find("Ranger").GetComponent<TMP_Text>().text = adventurer.name;
            page.transform.Find("RangerImg").GetComponent<Image>().sprite = adventurer.img;
            page.transform.Find("HP").GetComponent<TMP_Text>().text = "HP: " + adventurer.hp;
            page.transform.Find("ATK").GetComponent<TMP_Text>().text = "ATK: " + adventurer.atk;
            page.transform.Find("DEF").GetComponent<TMP_Text>().text = "SPD: " + adventurer.speed;
            for (int j = 0; j < adventurer.items.Length; j++)
            {
                ItemInfo item = adventurer.items[j];
                page.transform.Find("Cost" + j).GetComponent<TMP_Text>().text =
                    item.cost.ToString();
                page.transform.Find("BasePrice" + j).GetComponent<TMP_Text>().text =
                    item.basePrice.ToString();
            }
            page.transform
                .Find("ShopBtn")
                .GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    _stateManager.SetStageBookPage(_currentPage);
                    SceneManager.LoadScene("ShopScene");
                });
            _pages.Add(page);
        }
    }

    public override void OnCloseBook()
    {
        _stateManager.SetStageBookPage(_currentPage);
        base.OnCloseBook();
    }
}
