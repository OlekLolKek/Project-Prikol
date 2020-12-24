namespace ProjectPrikol
{
    public class CameraController : IExecutable, ICleanable
    {
        private CameraModel _cameraModel;
        private PlayerModel _playerModel;
        
        public CameraController(CameraModel cameraModel, PlayerModel playerModel)
        {
            _cameraModel = cameraModel;
            _playerModel = playerModel;
        }
        
        public void Execute(float deltaTime)
        {
            
        }

        public void Cleanup()
        {
            
        }
    }
}