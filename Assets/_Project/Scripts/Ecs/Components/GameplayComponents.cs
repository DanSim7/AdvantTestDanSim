using _Project.Scripts.Mono.UI;
using TMPro;
using UnityEngine.UI;

namespace _Project.Scripts.Ecs.Components
{
    public struct Wallet
    {
        public float Balance;
    }

    public struct Business
    {
        public int ID;
        public int Level;
        public float Progress;
        public bool IsPurchased;
        public bool Upgrade1Purchased;
        public bool Upgrade2Purchased;
    }

    public struct BusinessView
    {
        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI IncomeText;
        public SlicedFilledImage ProgressBar;
        public Button LevelUpButton;
        public TextMeshProUGUI LevelUpPriceText;
        public Button Upgrade1Button;
        public TextMeshProUGUI Upgrade1Text;
        public Button Upgrade2Button;
        public TextMeshProUGUI Upgrade2Text;
    }
}