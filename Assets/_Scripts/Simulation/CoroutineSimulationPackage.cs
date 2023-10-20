using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class CoroutineSimulationPackage
    {
        public float Priority { get; set; }
        public readonly List<Func<IEnumerator>> ExecuteEvents = new();

        
        
        public void AddToPackage(Action action)
        {
            ExecuteEvents.Add(ConvertToIEnumerator(action));
        }

        public void AddToPackage(Func<IEnumerator> coroutine)
        {
            ExecuteEvents.Add(coroutine);
        }

        public void AddToPackage(Tween tween)
        {
            ExecuteEvents.Add(ConvertToIEnumerator(tween));
        }

        private Func<IEnumerator> ConvertToIEnumerator(Action action)
        {
            return new Func<IEnumerator>(() =>
            {
                IEnumerator CoroutineWrapper()
                {
                    action();
                    yield return null; // Yielding once to make it a valid coroutine
                }

                return CoroutineWrapper();
            });
        }
        
        public Func<IEnumerator> ConvertToIEnumerator(Tween tween)
        {
            return new Func<IEnumerator>(() =>
            {
                IEnumerator CoroutineWrapper()
                {
                    while (!tween.IsComplete())
                    {
                        yield return null;
                    }
                }

                return CoroutineWrapper();
            });
        }
    }
}