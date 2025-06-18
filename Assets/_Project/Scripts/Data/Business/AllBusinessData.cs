using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Data.Business
{
    [CreateAssetMenu(menuName = "All Business Data")]
    public class AllBusinessData : ScriptableObject
    {
        [SerializeField] private BusinessData[] _businessDatas;

        public IReadOnlyList<BusinessData> BusinessDatas => _businessDatas;
    }
}