using System.Collections;
using UnityEngine;

namespace SimpleRPG.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}