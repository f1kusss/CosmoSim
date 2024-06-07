using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public enum FlyweightDefType
    {
        ShootGun,
        ShootPlasma
    }

    public abstract class FlywaightDefinition : ScriptableObject
    {
        [SerializeField] public string ID = "";

        [TextArea] [SerializeField] public string Name = "";
        public FlyweightDefType DeffinitionType;
        public GameObject DeffinitionPref;

        public abstract Flyweight Create();

        public virtual void OnGet(Flyweight flyweight)
        {
            flyweight.gameObject.SetActive(true);
        }

        public virtual void OnRelease(Flyweight flyweight)
        {
            flyweight.gameObject.SetActive(false);
        }

        public virtual void OnDestroyPooledObj(Flyweight flyweight)
        {
            Destroy(flyweight.gameObject);
        }


        protected void OnValidate()
        {
            if (ID == "")
            {
                ID = Guid.NewGuid().ToString();
            }
        }
    }
}