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
    private AudioSource _audioSource;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("AudioManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
    }

    public void PlayBtnClick()
    {
        _audioSource.PlayOneShot(_btnClickClip);
    }

    public void PlayPageFlip()
    {
        _audioSource.PlayOneShot(_pageFlipClip);
    }
}
