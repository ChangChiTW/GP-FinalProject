using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _monsterRoomPrefab;

    [SerializeField]
    private GameObject _treasureRoomPrefab;
    private DungeonSceneController _dungeonSceneController;
    private float _scale;
    private int _maxLevel = 10;
    private List<List<int>> _map = new List<List<int>>();
    private float[][] _yPositions = new float[][]
    {
        new float[] { 0f },
        new float[] { 200f, -200f },
        new float[] { 350f, 0f, -350f }
    };

    private List<int> _roomCount = new List<int>();

    void Awake()
    {
        _dungeonSceneController = GetComponent<DungeonSceneController>();
        _scale = _dungeonSceneController.GetScale();
    }

    void Start()
    {
        // Create the map
        for (int i = 0; i < _maxLevel; i++)
        {
            List<int> level = new List<int>();
            level.Add(Random.Range(1, 4));
            for (int j = 0; j < 2; j++)
            {
                level.Add(Random.Range(0, 3));
            }
            _map.Add(level);
        }
        // Create the dungeon
        for (int i = 0; i < _maxLevel; i++)
        {
            List<int> monsterList = new List<int>();
            List<int> treasureList = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                if (_map[i][j] == 1)
                {
                    treasureList.Add(j);
                }
                else if (_map[i][j] > 1)
                {
                    monsterList.Add(j);
                }
            }
            _roomCount.Add(monsterList.Count + treasureList.Count);
            int index = 0;
            for (int j = 0; j < 3 && index < _roomCount[i]; j++)
            {
                Vector3 position = new Vector3(i * 500f, _yPositions[_roomCount[i] - 1][index], 0);
                if (treasureList.Contains(j))
                {
                    GameObject treasureRoom = Instantiate(_treasureRoomPrefab);
                    treasureRoom.transform.SetParent(GameObject.Find("Map").transform, false);
                    treasureRoom.transform.localPosition = position;
                    index++;
                }
                else if (monsterList.Contains(j))
                {
                    GameObject monsterRoom = Instantiate(_monsterRoomPrefab);
                    monsterRoom.transform.SetParent(GameObject.Find("Map").transform, false);
                    monsterRoom.transform.localPosition = position;
                    index++;
                }
            }
        }
    }

    void Update()
    {
        if (_dungeonSceneController.IsArrived())
        {
            int level = _dungeonSceneController.GetLevel();
            Vector2 teamPosition = _dungeonSceneController.GetTeamPosition();
            // draw line to the next level
            if (level < _maxLevel - 1)
            {
                for (int i = 0; i < _roomCount[level + 1]; i++)
                {
                    float x = (level + 1) * 500f * _scale;
                    float y = _yPositions[_roomCount[level + 1] - 1][i] * _scale;
                    Vector2 nextLevelPosition = new Vector2(x, y);
                    if (Vector2.Distance(teamPosition, nextLevelPosition) < 6f)
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            teamPosition,
                            nextLevelPosition
                        );
                }
            }
        }
    }
}
