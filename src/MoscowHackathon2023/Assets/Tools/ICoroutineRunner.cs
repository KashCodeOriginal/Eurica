using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator enumerator);
    void StopCoroutine(Coroutine coroutine);
}
