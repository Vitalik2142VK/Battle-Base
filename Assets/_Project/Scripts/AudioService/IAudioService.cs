namespace BattleBase.AudioService
{
    public interface IAudioService
    {
        Music Music { get; }

        Sfx Sfx { get; }
    }
}