namespace ProjectPrikol
{
    public class InputController : IExecutable
    {
        #region Fields

        private readonly IInputAxisChange _horizontal;
        private readonly IInputAxisChange _vertical;
        private readonly IInputAxisChange _mouseX;
        private readonly IInputAxisChange _mouseY;
        private readonly IInputKeyPress _startCrouch;
        private readonly IInputKeyPress _weapon1;
        private readonly IInputKeyPress _weapon2;
        private readonly IInputKeyPress _weapon3;
        private readonly IInputKeyRelease _endCrouch;
        private readonly IInputKeyHold _jump;

        #endregion

        public InputController(IInputAxisChange inputHorizontal, IInputAxisChange inputVertical,
            IInputAxisChange mouseX, IInputAxisChange mouseY,
            IInputKeyPress startCrouch, IInputKeyRelease endCrouch,
            IInputKeyHold jump, IInputKeyPress weapon1,
            IInputKeyPress weapon2, IInputKeyPress weapon3)
        {
            _horizontal = inputHorizontal;
            _vertical = inputVertical;
            _mouseX = mouseX;
            _mouseY = mouseY;
            _startCrouch = startCrouch;
            _endCrouch = endCrouch;
            _jump = jump;
            _weapon1 = weapon1;
            _weapon2 = weapon2;
            _weapon3 = weapon3;
        }
        
        
        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
            _mouseX.GetAxis();
            _mouseY.GetAxis();
            
            _startCrouch.GetKeyDown();
            _endCrouch.GetKeyUp();
            _jump.GetKey();
            
            _weapon1.GetKeyDown();
            _weapon2.GetKeyDown();
            _weapon3.GetKeyDown();
        }
    }
}