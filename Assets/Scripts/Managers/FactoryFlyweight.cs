using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Cosmosim
{
    public class FactoryFlyweight : SingletonManager<FactoryFlyweight>
    {
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultCapacity = 50;
        [SerializeField] private int _maxSize = 150;

        private Dictionary<FlyweightDefType, IObjectPool<Flyweight>> _pools =
            new Dictionary<FlyweightDefType, IObjectPool<Flyweight>>();

        public void ReturnToPool(Flyweight flyweight)
        {
            GetPoolForDefinition(flyweight.Definition)?.Release(flyweight);
        }

        public Flyweight Spawn(FlywaightDefinition definition)
        {
            return GetPoolForDefinition(definition)?.Get();
        }

        public Flyweight Spawn(FlywaightDefinition definition, Vector3 position, Quaternion rotation)
        {
            var flywaight = GetPoolForDefinition(definition)?.Get();
            flywaight.transform.position = position;
            flywaight.transform.rotation = rotation;
            return flywaight;
        }

        private IObjectPool<Flyweight> GetPoolForDefinition(FlywaightDefinition definition)
        {
            IObjectPool<Flyweight> pool;
            if (_pools.TryGetValue(definition.DeffinitionType, out pool))
                return pool;
            pool = new ObjectPool<Flyweight>(
                definition.Create,
                definition.OnGet,
                definition.OnRelease,
                definition.OnDestroyPooledObj,
                _collectionCheck,
                _defaultCapacity,
                _maxSize);
            _pools.Add(definition.DeffinitionType, pool);
            return pool;
        }
    }
}