using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureRoomController : MonoBehaviour
{
    private DungeonSceneController _dungeonSceneController;

    void Awake()
    {
        _dungeonSceneController = GameObject
            .Find("GameController")
            .GetComponent<DungeonSceneController>();
    }

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        _dungeonSceneController.OnMoveNextTarget(
            this.GetComponent<RectTransform>().localPosition,
            null
        );
    }
}
