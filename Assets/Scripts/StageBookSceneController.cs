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
        _pages[0].transform.Find("Day").GetComponent<TMP_Text>().text =
            "Day: " + _stateManager.GetDay().ToString();
        _pages[0].transform.Find("AmountBalance").GetComponent<TMP_Text>().text =
            "$" + _stateManager.GetBalance().ToString();
        _pages[0].transform.Find("InterestPayableBalance").GetComponent<TMP_Text>().text =
            "$" + _stateManager.GetDebt().ToString();
        _pages[0].transform.Find("DebtBalance").GetComponent<TMP_Text>().text = "$10000";
        _pages[0].transform.Find("ExpectedBalance").GetComponent<TMP_Text>().text =
            "$" + _stateManager.GetExpectedBalance().ToString();
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
