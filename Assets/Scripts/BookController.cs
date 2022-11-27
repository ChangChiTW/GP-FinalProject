using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookController : MonoBehaviour
{
    public Animator book;
    public Canvas intro;
    public Canvas tutor;

    // Start is called before the first frame update
    void Start()
    {
        intro.enabled = true;
        tutor.enabled = false;
    }

    // Update is called once per frame
    void Update() { }

    public void turnPage(bool turnRight)
    {
        if (turnRight)
        {
            StartCoroutine(turnTutor());
        }
        else
        {
            StartCoroutine(turnIntro());
        }
    }

    public IEnumerator turnTutor()
    {
        intro.enabled = false;
        book.SetTrigger("right");
        yield return new WaitForSeconds(0.8f);
        tutor.enabled = true;
    }

    public IEnumerator turnIntro()
    {
        tutor.enabled = false;
        book.SetTrigger("left");
        yield return new WaitForSeconds(0.8f);
        intro.enabled = true;
    }

    public void start()
    {
        SceneManager.LoadScene("scenename");
    }
}
