namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSiteIdCreator : IBuildingSiteIdCreator
    {
        public int _currentId;

        public BuildingSiteIdCreator()
        {
            _currentId = 0;
        }

        public int Create() =>
            _currentId++;
    }
}