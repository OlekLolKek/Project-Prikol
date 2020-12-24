namespace ProjectPrikol
{
    public class InputController : IExecutable
    {
        #region Fields

        private readonly IInputAxisChange _horizontal;
        private readonly IInputAxisChange _vertical;
        private readonly IInputAxisChange _mouseX;
        private readonly IInputAxisChange _mouseY;
        private readonly IInputKeyPress _crouch;
        private readonly IInputKeyPress _jump;

        #endregion

        public InputController(IInputAxisChange inputHorizontal, IInputAxisChange inputVertical,
            IInputAxisChange mouseX, IInputAxisChange mouseY,
            IInputKeyPress crouch, IInputKeyPress jump)
        {
            _horizontal = inputHorizontal;
            _vertical = inputVertical;
            _mouseX = mouseX;
            _mouseY = mouseY;
            _crouch = crouch;
            _jump = jump;
        }
        
        
        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
            _mouseX.GetAxis();
            _mouseY.GetAxis();
            
            _crouch.GetKey();
            _jump.GetKey();
        }
    }
}