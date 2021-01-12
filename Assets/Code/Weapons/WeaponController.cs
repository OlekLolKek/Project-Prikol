using UnityEngine;

namespace ProjectPrikol
{
    public class WeaponController : IExecutable, ICleanable
    {
        private readonly IInputKeyPress _primary;
        private readonly IInputKeyPress _secondary;
        private readonly IInputKeyPress _melee;
        private readonly IInputKeyPress _fire;
        private readonly IInputKeyPress _changeMod;
        private readonly IInputAxisChange _mouseXInput;
        private readonly IInputAxisChange _mouseYInput;
        private readonly WeaponInventory _inventory;
        private readonly WeaponModification _modification;

        private float _mouseX;
        private float _mouseY;
        
        public WeaponController(InputModel inputModel, WeaponData data,
            CameraModel cameraModel)
        {
            _inventory = new WeaponInventory();
            var factory = new WeaponFactory();
            var silencerFactory = new BarrelAttachmentFactory(data.AssaultRifleSilencerData);
            var scopeFactory = new ScopeFactory(data.AssaultRifleScopeData);

            _primary = inputModel.Weapon1;
            _secondary = inputModel.Weapon2;
            _melee = inputModel.Weapon3;
            _mouseXInput = inputModel.MouseX;
            _mouseYInput = inputModel.MouseY;
            _changeMod = inputModel.ChangeMod;
            _fire = inputModel.Fire;

            _primary.OnKeyPressed += SelectPrimaryWeapon;
            _secondary.OnKeyPressed += SelectSecondaryWeapon;
            _melee.OnKeyPressed += SelectMeleeWeapon;
            _fire.OnKeyPressed += Shoot;
            _changeMod.OnKeyPressed += ChangeModification;
            _mouseXInput.OnAxisChanged += MouseXChange;
            _mouseYInput.OnAxisChanged += MouseYChange;
            

            
            var weapon = new Weapon(factory, data.AssaultRifleData, cameraModel);
            _inventory.AddWeapon(weapon);
            
            var silencer = new BarrelAttachment(silencerFactory, weapon);
            var scope = new Scope(scopeFactory, weapon);
            _modification = new ModificationsController(silencer, scope);
        }

        public void Execute(float deltaTime)
        {
            var weapon = _inventory.ActiveWeapon;
            weapon.Rotate(_mouseX, _mouseY);
            weapon.Execute(deltaTime);
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

        private void Shoot()
        {
            _inventory.ActiveWeapon.Fire();
        }

        public void Cleanup()
        {
            _primary.OnKeyPressed -= SelectPrimaryWeapon;
            _secondary.OnKeyPressed -= SelectSecondaryWeapon;
            _melee.OnKeyPressed -= SelectMeleeWeapon;
            _fire.OnKeyPressed -= Shoot;
            _changeMod.OnKeyPressed -= ChangeModification;
            _mouseXInput.OnAxisChanged -= MouseXChange;
            _mouseYInput.OnAxisChanged -= MouseYChange;
        }

        private void ChangeModification()
        {
            _modification.SwitchModifications(_inventory.ActiveWeapon);
        }
    }
}