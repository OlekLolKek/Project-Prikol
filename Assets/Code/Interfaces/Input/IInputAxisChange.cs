using System;

namespace ProjectPrikol
{
    public interface IInputAxisChange
    {
        event Action<float> OnAxisChanged;
        void GetAxis();
    }
}