namespace ProjectPrikol
{
    public class WeaponController : IExecutable
    {
        private readonly Controllers _controllers;
        private readonly WeaponInventory _inventory;
        private readonly WeaponFactory _factory;

        private float _mouseX;
        private float _mouseY;
        
        public WeaponController(InputModel inputModel, WeaponData data,
            CameraModel cameraModel)
        {
            _controllers = new Controllers();
            _inventory = new WeaponInventory();
            _factory = new WeaponFactory();

            inputModel.Weapon1.OnKeyPressed += SelectPrimaryWeapon;
            inputModel.Weapon2.OnKeyPressed += SelectSecondaryWeapon;
            inputModel.Weapon3.OnKeyPressed += SelectMeleeWeapon;
            inputModel.MouseX.OnAxisChanged += MouseXChange;
            inputModel.MouseY.OnAxisChanged += MouseYChange;
            
            var weapon = new Weapon(_factory, data.AssaultRifleData, cameraModel);
            weapon.Deactivate();
            _inventory.AddWeapon(weapon);
        }

        public void Execute(float deltaTime)
        {
            var weapon = _inventory.ActiveWeapon;
            weapon.Rotate(_mouseX, _mouseY);
        }

        private void SelectPrimaryWeapon()
        {
            _inventory.SwitchWeapons(0);
        }
        
        private void SelectSecondaryWeapon()
        {
            _inventory.SwitchWeapons(1);
        }
        
        private void SelectMeleeWeapon()
        {
            _inventory.SwitchWeapons(2);
        }

        private void MouseXChange(float value)
        {
            _mouseX = value;
        }

        private void MouseYChange(float value)
        {
            _mouseY = value;
        }
    }
}