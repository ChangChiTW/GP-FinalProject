using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class BookAnimator : MonoBehaviour
{
    private Animator _bookAnimator;

    private void Awake()
    {
        TryGetComponent<Animator>(out _bookAnimator);
    }

    public void OpenBook()
    {
        _bookAnimator.SetTrigger("open");
    }

    public void CloseBook()
    {
        _bookAnimator.SetTrigger("close");
    }

    public void FlipRight()
    {
        _bookAnimator.SetTrigger("right");
    }

    public void FlipLeft()
    {
        _bookAnimator.SetTrigger("left");
    }
}
