using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _btnClickClip;

    [SerializeField]
    private AudioClip _pageFlipClip;

    [SerializeField]
    private AudioClip _menuBGMClip;

    [SerializeField]
    private AudioClip _bookBGMClip;

    [SerializeField]
    private AudioClip _shopBGMClip;

    [SerializeField]
    private AudioClip _tradeBGMClip;

    [SerializeField]
    private AudioClip _settlementBGMClip;

    [SerializeField]
    private AudioClip _dungeonBGMClip;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.3f;
        _audioSource.loop = true;
    }

    public void PlayBtnClick()
    {
        _audioSource.PlayOneShot(_btnClickClip);
    }

    public void PlayPageFlip()
    {
        _audioSource.PlayOneShot(_pageFlipClip);
    }

    public void PlayMenuBGM()
    {
        _audioSource.clip = _menuBGMClip;
        _audioSource.Play();
    }

    public void PlayBookBGM()
    {
        _audioSource.clip = _bookBGMClip;
        _audioSource.Play();
    }

    public void PlayShopBGM()
    {
        _audioSource.clip = _shopBGMClip;
        _audioSource.Play();
    }

    public void PlayTradeBGM()
    {
        _audioSource.clip = _tradeBGMClip;
        _audioSource.Play();
    }

    public void PlaySettlementBGM()
    {
        _audioSource.clip = _settlementBGMClip;
        _audioSource.Play();
    }

    public void PlayDungeonBGM()
    {
        _audioSource.clip = _dungeonBGMClip;
        _audioSource.Play();
    }
}
