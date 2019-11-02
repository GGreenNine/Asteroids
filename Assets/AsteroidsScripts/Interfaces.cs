namespace DefaultNamespace
{

    public interface IController
    {
        bool IsShooting { get; }
        bool IsHyperspacing { get; }
        float TurnAxis { get; }
        float FrwdAxis { get; }
        bool ChangeWeapon { get; }
    }
}