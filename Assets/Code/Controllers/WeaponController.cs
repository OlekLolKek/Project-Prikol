namespace ProjectPrikol
{
    public class WeaponController
    {
        private readonly Controllers _controllers;
        private readonly WeaponInventory _inventory;
        private readonly WeaponFactory _factory;
        
        public WeaponController(InputModel inputModel, WeaponData data)
        {
            _controllers = new Controllers();
            _inventory = new WeaponInventory();
            _factory = new WeaponFactory(data.AssaultRifleData);
            
            _inventory.AddWeapon(new Weapon(_factory));
        }
    }
}