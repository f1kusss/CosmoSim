using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Cosmosim
{
    public class RotateAround : MonoBehaviour
    {
        [ContextMenuItem("StartSunCor", "StartCoroutineMoveSolarSystem")]
        [ContextMenuItem("StopSunCor", "StopCoroutineMoveSolarSystem")]
        [ContextMenuItem("StartRotCor", "StartCoroutineRotator")]
        [ContextMenuItem("StopRotCor", "StopCoroutineRotator")]
        public bool CorMenu;

        [SerializeField] private Transform target; // the object to rotate around

        [SerializeField] private float speed; // the speed of rotation
        [SerializeField] private float forwardSpeed = 4.5f;
        [SerializeField] private float delay = 0.1f;
        private WaitForSeconds _waitForSeconds;
        private IEnumerator _coroutineMoveSolarSystem;
        private IEnumerator _coroutineRotator;

        void Start()
        {
            if (target == null)
            {
                target = this.gameObject.transform;
                Debug.Log("RotateAround target not specified. Defaulting to parent GameObject");
            }

            _waitForSeconds = new WaitForSeconds(delay);
            StartCoroutineMoveSolarSystem();
            StartCoroutineRotator();
        }

        void Update()
        {
        }

//---------------------------------------------------funk--------------------------------------------------------------
        private void Rotator()
        {
            transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
        }

        private void MoveSolarSystem()
        {
            if (gameObject.name == "Sun")
                transform.Translate(Vector3.forward * (forwardSpeed * Time.deltaTime));
        }
//---------------------------------------------------funk--------------------------------------------------------------


//---------------------------------------------------StartCorFunk------------------------------------------------------
        public void StartCoroutineMoveSolarSystem()
        {
            _coroutineMoveSolarSystem = MoveSolarSystemCor();
            StartCoroutine(_coroutineMoveSolarSystem);
        }

        public void StartCoroutineRotator()
        {
            _coroutineRotator = RotatorCor();
            StartCoroutine(_coroutineRotator);
        }
//---------------------------------------------------StartCorFunk------------------------------------------------------


//---------------------------------------------------StopCorFunk-------------------------------------------------------
        public void StopCoroutineMoveSolarSystem()
        {
            StopCoroutine(_coroutineMoveSolarSystem);
        }

        public void StopCoroutineRotator()
        {
            StopCoroutine(_coroutineRotator);
        }
//---------------------------------------------------StopCorFunk-------------------------------------------------------


//---------------------------------------------------Coroutines--------------------------------------------------------
        private IEnumerator MoveSolarSystemCor()
        {
            while (true)
            {
                yield return _waitForSeconds;
                MoveSolarSystem();
            }
        }

        private IEnumerator RotatorCor()
        {
            while (true)
            {
                yield return _waitForSeconds;
                Rotator();
            }
        }
//---------------------------------------------------Coroutines--------------------------------------------------------
    }
}