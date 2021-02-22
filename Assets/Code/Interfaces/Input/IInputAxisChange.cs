using System;

namespace ProjectPrikol
{
    public interface IInputAxisChange : IInput
    {
        event Action<float> OnAxisChanged;
        void GetAxis();
    }
}