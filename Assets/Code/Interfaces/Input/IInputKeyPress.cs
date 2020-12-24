using System;


namespace ProjectPrikol
{
    public interface IInputKeyPress
    {
        event Action OnKeyPressed;
        void GetKey();
    }
}