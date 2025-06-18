namespace _Project.Scripts.Ecs.Components
{
    public struct LevelUpEvent
    {
        public int BusinessID;
    }

    public struct UpgradeEvent
    {
        public int BusinessID;
        public int UpgradeID;
    }
}