using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="MedicineItemData", menuName ="ItemData/Medicine Data")]
public class MedicineItemData : ItemData
{
    public float HealthAmount = 10f;
    public override void ItemFunction(GameObject obj)
    {
        if(obj.TryGetComponent<IDamageable>(out IDamageable component))
        {
            component.Healing(HealthAmount);
        }
    }
}