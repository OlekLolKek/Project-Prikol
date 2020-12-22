namespace ProjectPrikol
{
    public interface IExecutable : IControllable
    {
        void Execute(float deltaTime);
    }
}