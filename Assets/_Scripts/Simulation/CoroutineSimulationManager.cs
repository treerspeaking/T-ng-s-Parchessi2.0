using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;
using UnityUtilities;

namespace _Scripts.Simulation
{
    public class CoroutineSimulationManager : SingletonMonoBehaviour<CoroutineSimulationManager>
    {
        private SimplePriorityQueue<CoroutineSimulationPackage> _simulationQueue = new();


        private bool _isExecuting;

        public void AddCoroutineSimulationObject(CoroutineSimulationPackage simulationPackage)
        {
            if (simulationPackage == null || _simulationQueue.Contains(simulationPackage))
            {
                return;
            }
            _simulationQueue.Enqueue(simulationPackage, simulationPackage.Priority);
        }

        public void RemoveCoroutineSimulationObject(CoroutineSimulationPackage simulationPackage)
        {
            if (simulationPackage == null || !_simulationQueue.Contains(simulationPackage))
            {
                return;
            }
            _simulationQueue.Remove(simulationPackage);
        }

        public void ClearAll()
        {
            _simulationQueue.Clear();
        }

        public void ExecuteAllCoroutineSimulations()
        {
            StartCoroutine(ExecuteAll());
        }
        
        
        public void ExecuteAllCoroutineSimulationsThenClear()
        {
            StartCoroutine(ExecuteAll());
            
            _simulationQueue.Clear();
        }

        IEnumerator ExecuteAll()
        {
            if (_isExecuting)
            {
                yield break;
            }

            _isExecuting = true;
            while (_simulationQueue.Count > 0)
            {
                var simulationObject = _simulationQueue.Dequeue();
                yield return StartCoroutine(ExecuteCoroutineSimulationConcurrent(simulationObject));
            }

            _isExecuting = false;
        }
        IEnumerator ExecuteCoroutineSimulationConcurrent(CoroutineSimulationPackage simulationPackage)
        {
            foreach (var enumerator in simulationPackage.ExecuteEvents)
            {
                yield return StartCoroutine(enumerator.Invoke());
            }

            yield return null;
}
        
        IEnumerator ExecuteCoroutineSimulationParallel(CoroutineSimulationPackage simulationPackage)
        {
            List<Coroutine> runningCoroutines = new List<Coroutine>();

            foreach (var enterEnumerator in simulationPackage.ExecuteEvents)
            {
                Coroutine newCoroutine = StartCoroutine(enterEnumerator.Invoke());
                runningCoroutines.Add(newCoroutine);
            }

            // Wait for all running coroutines to complete
            yield return WaitAllCoroutines(runningCoroutines);
        }

        IEnumerator WaitAllCoroutines(List<Coroutine> coroutines)
        {
            foreach (var coroutine in coroutines)
            {
                yield return coroutine;
            }
        }
    }
}

