namespace BattleBase.Services.Audio
{
    public interface IAudioService
    {
        Music Music { get; }

        Sfx Sfx { get; }
    }
}