using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    Vector3 FireWeapon(Vector3 targetPositon);
    void Damage(float damageAmount, Vector3 targetHitPosition, GameAgent sender);
    void VisualizeFiring(Vector3 targetPositon);

}
