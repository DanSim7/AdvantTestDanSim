using System;
using UnityEngine;

namespace _Project.Scripts.Data.Business
{
    [CreateAssetMenu(menuName = "Business Data")]
    public class BusinessData : ScriptableObject
    {
        [field: SerializeField] public float IncomeDelay { get; private set; }
        [field: SerializeField] public float BaseCost { get; private set; }
        [field: SerializeField] public float BaseIncome { get; private set; }
        [field: SerializeField] public UpgradeData Upgrade1 { get; private set; }
        [field: SerializeField] public UpgradeData Upgrade2 { get; private set; }
    }
    
    [Serializable]
    public struct UpgradeData
    {
        [field: SerializeField] public float Cost { get; private set; }
        [field: SerializeField] public float IncomeMultiplier { get; private set; }
    }
}