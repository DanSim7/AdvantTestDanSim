using _Project.Scripts.Mono.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Ecs.Views
{
    public class BusinessView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TitleText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LevelText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI IncomeText { get; private set; }
        [field: SerializeField] public SlicedFilledImage ProgressBar { get; private set; }
        [field: SerializeField] public Button LevelUpButton { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LevelUpPriceText { get; private set; }
        [field: SerializeField] public Button Upgrade1Button { get; private set; }
        [field: SerializeField]  public TextMeshProUGUI Upgrade1Text { get; private set; }
        [field: SerializeField] public Button Upgrade2Button { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Upgrade2Text { get; private set; }
    }
}