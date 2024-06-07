using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class Asteriod : MonoBehaviour
    {
        [SerializeField] private float _minSize = 0.8f;
        [SerializeField] private float _maxSize = 3.0f;
        [SerializeField] private float _rotationOffset = 100f;
        private Vector3 _randomRotation;

        void Start()
        {
            Vector3 originalScale = transform.localScale;
            Vector3 newScale = new Vector3(originalScale.x * Random.Range(_minSize, _maxSize),
                originalScale.y * Random.Range(_minSize, _maxSize),
                originalScale.z * Random.Range(_minSize, _maxSize));

            transform.localScale = newScale;
            _randomRotation = new Vector3(Random.Range(-_rotationOffset, _rotationOffset),
                Random.Range(-_rotationOffset, _rotationOffset), Random.Range(-_rotationOffset, _rotationOffset));
        }


        void Update()
        {
            transform.Rotate(_randomRotation * Time.deltaTime);
        }
    }
}