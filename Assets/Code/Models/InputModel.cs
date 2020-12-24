namespace ProjectPrikol
{
    public class InputModel
    {
        #region Fields

        public IInputAxisChange Horizontal { get; }
        public IInputAxisChange Vertical { get; }
        public IInputAxisChange MouseX { get; }
        public IInputAxisChange MouseY { get; }
        public IInputKeyPress Crouch { get; }
        public IInputKeyPress Jump { get; }

        #endregion


        public InputModel(InputData inputData)
        {
            Horizontal = new PCInputAxis(AxisNameStorage.HORIZONTAL);
            Vertical = new PCInputAxis(AxisNameStorage.VERTICAL);
            MouseX = new PCInputAxis(AxisNameStorage.MOUSE_X);
            MouseY = new PCInputAxis(AxisNameStorage.MOUSE_Y);
            Crouch = new PCInputKey(inputData.Crouch);
            Jump = new PCInputKey(inputData.Jump);
        }
    }
}