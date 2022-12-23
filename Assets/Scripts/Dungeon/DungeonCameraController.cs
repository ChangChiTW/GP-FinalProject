using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class DungeonCameraController : MonoBehaviour
{
    [SerializeField]
    private RawImage _backgroundImage;

    [SerializeField]
    private GameObject _moveLeftButton;

    [SerializeField]
    private GameObject _moveRightButton;
    private bool _freeCamera = false;
    private int _maxX = 40;
    private float _speed = 10.0f;
    private bool _moveLeft = false;
    private bool _moveRight = false;

    void Update()
    {
        _backgroundImage.uvRect = new Rect(this.transform.position.x / 20, 0, 1, 1);
        if (_freeCamera)
        {
            if (_moveLeft)
            {
                this.transform.position = Vector3.MoveTowards(
                    this.transform.position,
                    new Vector3(
                        this.transform.position.x - _speed * Time.deltaTime,
                        this.transform.position.y,
                        this.transform.position.z
                    ),
                    _speed * Time.deltaTime
                );
            }
            if (_moveRight)
            {
                this.transform.position = Vector3.MoveTowards(
                    this.transform.position,
                    new Vector3(
                        this.transform.position.x + _speed * Time.deltaTime,
                        this.transform.position.y,
                        this.transform.position.z
                    ),
                    _speed * Time.deltaTime
                );
            }
        }
        else
        {
            Vector3 targetPosition = GameObject
                .Find("GameController")
                .GetComponent<DungeonSceneController>()
                .GetTeamPosition();
            this.transform.position = Vector3.MoveTowards(
                this.transform.position,
                new Vector3(
                    targetPosition.x + 5,
                    this.transform.position.y,
                    this.transform.position.z
                ),
                _speed * Time.deltaTime
            );
        }
        if (this.transform.position.x <= 0)
        {
            _moveLeftButton.SetActive(false);
            _moveLeft = false;
        }
        else
        {
            _moveLeftButton.SetActive(true);
        }
        if (this.transform.position.x >= _maxX)
        {
            _moveRightButton.SetActive(false);
            _moveRight = false;
        }
        else
        {
            _moveRightButton.SetActive(true);
        }
    }

    public void OnMoveLeftClick()
    {
        _freeCamera = true;
        _moveLeft = true;
    }

    public void OnMoveLeftRelease()
    {
        _moveLeft = false;
    }

    public void OnMoveRightClick()
    {
        _freeCamera = true;
        _moveRight = true;
    }

    public void OnMoveRightRelease()
    {
        _moveRight = false;
    }

    public void OnMoveToTeam()
    {
        _freeCamera = false;
    }

    public void FreeCamera(bool freeCamera)
    {
        _freeCamera = freeCamera;
    }
}
