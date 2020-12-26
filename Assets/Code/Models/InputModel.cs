using UnityEngine;

namespace ProjectPrikol
{
    public class InputModel
    {
        #region Fields

        public IInputAxisChange Horizontal { get; }
        public IInputAxisChange Vertical { get; }
        public IInputAxisChange MouseX { get; }
        public IInputAxisChange MouseY { get; }
        public IInputKeyPress StartCrouch { get; }
        public IInputKeyRelease StopCrouch { get; }
        public IInputKeyHold Jump { get; }

        #endregion


        public InputModel(InputData inputData)
        {
            Horizontal = new PCInputAxis(AxisNameStorage.HORIZONTAL);
            Vertical = new PCInputAxis(AxisNameStorage.VERTICAL);
            MouseX = new PCInputAxis(AxisNameStorage.MOUSE_X);
            MouseY = new PCInputAxis(AxisNameStorage.MOUSE_Y);
            StartCrouch = new PCInputKeyDown(inputData.Crouch);
            StopCrouch = new PCInputKeyUp(inputData.Crouch);
            Jump = new PCInputKeyHold(inputData.Jump);
        }
    }
}