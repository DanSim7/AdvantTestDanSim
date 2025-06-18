using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Data.Localization
{
    [CreateAssetMenu(menuName = "Localization Data")]
    public class LocalizationData : ScriptableObject
    {
        [SerializeField] private LocalizationBusiness[] _businesses;
        
        public IReadOnlyList<LocalizationBusiness> Businesses => _businesses;
    }

    [Serializable]
    public class LocalizationBusiness
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Upgrade1Name { get; private set; }
        [field: SerializeField] public string Upgrade2Name { get; private set; }
    }
}