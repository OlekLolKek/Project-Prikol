﻿namespace ProjectPrikol
{
    public interface ILateExecutable : IControllable
    {
        void LateExecute(float deltaTime);
    }
}