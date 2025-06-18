using TMPro;
using UnityEngine;

namespace _Project.Scripts.Ecs.Views
{
    public class MainView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI BalanceText { get; private set; }
        [field: SerializeField] public Transform BusinessesContainer { get; private set; }
        [field: SerializeField] public BusinessView BusinessViewPrefab { get; private set; }
    }
}