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
            page.transform.Find("HPNum").GetComponent<TMP_Text>().text = adventurer.hp.ToString();
            page.transform.Find("ATKNum").GetComponent<TMP_Text>().text = adventurer.atk.ToString();
            page.transform.Find("DEFNum").GetComponent<TMP_Text>().text = adventurer.def.ToString();
            page.transform.Find("SPEEDNum").GetComponent<TMP_Text>().text =
                adventurer.speed.ToString();
            for (int j = 0; j < adventurer.preferenceImgs.Count; j++)
            {
                page.transform.Find("PreferenceItem" + (j)).GetComponent<Image>().sprite =
                    adventurer.preferenceImgs[j];
                page.transform.Find("PreferenceItem" + (j)).GetComponent<Image>().enabled = true;
            }
            _pages.Add(page);
        }
    }

    public void OnNext()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        SceneManager.LoadScene("ShopScene");
    }
}
