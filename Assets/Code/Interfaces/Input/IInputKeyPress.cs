using System;


namespace ProjectPrikol
{
    public interface IInputKeyPress : IInput
    {
        event Action OnKeyPressed;
        void GetKeyDown();
    }
}