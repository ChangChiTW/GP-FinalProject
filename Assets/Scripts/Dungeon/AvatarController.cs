using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    [SerializeField]
    private GameObject _img;

    [SerializeField]
    private GameObject _hpBar;

    [SerializeField]
    private GameObject _info;

    [SerializeField]
    private TMP_Text _nameText;

    [SerializeField]
    private TMP_Text _hpText;

    [SerializeField]
    private TMP_Text _atkText;

    [SerializeField]
    private TMP_Text _defText;

    [SerializeField]
    private TMP_Text _speedText;

    private DungeonSceneController _dungeonSceneController;
    private string _name;
    private float _hp;
    private float _maxHp;
    private float _atk;
    private float _def;
    private float _speed;

    void Awake()
    {
        _dungeonSceneController = GameObject
            .Find("GameController")
            .GetComponent<DungeonSceneController>();
    }

    public void SetAdventurer(AdventurerInfo adventurer)
    {
        _name = adventurer.name;
        _img.GetComponent<Image>().sprite = adventurer.img;
        _hp = adventurer.hp;
        _maxHp = adventurer.hp;
        _atk = adventurer.atk;
        _def = adventurer.def;
        _speed = adventurer.speed;
        _hpBar.GetComponent<Image>().fillAmount = 1;
    }

    public void UpdateAdventurerHp(float hp)
    {
        _hp = hp;
        _hpBar.GetComponent<Image>().fillAmount = _hp / _maxHp;
        if (_hp <= 0)
        {
            _img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Adventurer/Dead");
        }
    }

    public void OnCheckAdventurerInfo()
    {
        _nameText.text = _name;
        _hpText.text = _hp.ToString();
        _atkText.text = _atk.ToString();
        _defText.text = _def.ToString();
        _speedText.text = _speed.ToString();
        _info.SetActive(true);
    }

    public void OnCloseAdventurerInfo()
    {
        _info.SetActive(false);
    }
}
