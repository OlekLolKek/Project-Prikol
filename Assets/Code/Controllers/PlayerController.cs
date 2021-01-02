namespace ProjectPrikol
{
    public class PlayerController : IExecutable, ICleanable
    {
        #region Fields

        private readonly PlayerCollisionController _playerCollisionController;
        private readonly MoveController _moveController;
        private readonly JumpController _jumpController;

        #endregion
        

        public PlayerController(PlayerModel playerModel, InputModel inputModel,
            PlayerData playerData)
        {
            var collisionModel = new PlayerCollisionModel();
            _playerCollisionController = new PlayerCollisionController(playerModel, collisionModel,
                playerData);
            
            _moveController = new MoveController(playerModel, playerData, 
                inputModel, collisionModel);


            
            _jumpController = new JumpController(playerModel, playerData, 
                inputModel, collisionModel);
        }
        
        public void Execute(float deltaTime)
        {
            _moveController.Execute(deltaTime);
            _playerCollisionController.Execute(deltaTime);
            _jumpController.Execute(deltaTime);
        }

        public void Cleanup()
        {
            
        }
    }
}