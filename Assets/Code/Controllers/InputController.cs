namespace ProjectPrikol
{
    public class InputController : IExecutable
    {
        #region Fields

        private readonly IInputAxisChange _horizontal;
        private readonly IInputAxisChange _vertical;
        private readonly IInputAxisChange _mouseX;
        private readonly IInputAxisChange _mouseY;
        private readonly IInputKeyRelease _stopCrouch;
        private readonly IInputKeyPress _startCrouch;
        private readonly IInputKeyPress _weapon1;
        private readonly IInputKeyPress _weapon2;
        private readonly IInputKeyPress _weapon3;
        private readonly IInputKeyPress _changeMod;
        private readonly IInputKeyPress _fire;
        private readonly IInputKeyHold _jump;

        #endregion

        public InputController(InputModel inputModel)
        {
            _horizontal = inputModel.Horizontal;
            _vertical = inputModel.Vertical;
            _mouseX = inputModel.MouseX;
            _mouseY = inputModel.MouseY;
            _startCrouch = inputModel.StartCrouch;
            _stopCrouch = inputModel.StopCrouch;
            _jump = inputModel.Jump;
            _weapon1 = inputModel.Weapon1;
            _weapon2 = inputModel.Weapon2;
            _weapon3 = inputModel.Weapon3;
            _changeMod = inputModel.ChangeMod;
            _fire = inputModel.Fire;
        }
        
        
        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
            _mouseX.GetAxis();
            _mouseY.GetAxis();
            
            _startCrouch.GetKeyDown();
            _stopCrouch.GetKeyUp();
            _jump.GetKey();
            
            _weapon1.GetKeyDown();
            _weapon2.GetKeyDown();
            _weapon3.GetKeyDown();
            _changeMod.GetKeyDown();
            _fire.GetKeyDown();
        }
    }
}