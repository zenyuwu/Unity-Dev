using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class COR : MonoBehaviour
{
    [SerializeField] float time = 3;
    [SerializeField] bool go = false;

    Coroutine timerCoroutine;

    private void Start()
    {
        timerCoroutine = StartCoroutine(Timer(time));
        StartCoroutine("StoryTime");
        StartCoroutine(WaitAction());
    }

    private void Update()
    {
        //time -= Time.deltaTime;
        //if(time <= 0)
        //{
        //    time = 3;
        //    print("hello");
        //}
    }

    public IEnumerator Timer(float time)
    {
        for (; ; )
        {
            yield return new WaitForSeconds(time);
            print("hello00000");
        }
    }

    IEnumerator StoryTime()
    {
        print("hello again");
        yield return new WaitForSeconds(1);
        print("welcome!");
        yield return new WaitForSeconds(1);
        print("time to die");

        StopCoroutine(timerCoroutine);

        yield return null;
    }

    IEnumerator WaitAction()
    {
        yield return new WaitUntil(() => go);
        print("go");
        yield return null;
    }
}