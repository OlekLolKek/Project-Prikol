namespace ProjectPrikol
{
    public abstract class WeaponModification : IFire
    {
        private Weapon _weapon;
        private bool _isApplied;

        protected abstract Weapon AddModification(Weapon weapon);
        protected abstract Weapon RemoveModification(Weapon weapon);

        public void SwitchModifications(Weapon weapon)
        {
            if (_isApplied)
            {
                _weapon = RemoveModification(weapon);
                _isApplied = false;
            }
            else
            {
                _weapon = AddModification(weapon);
                _isApplied = true;
            }
        }
        
        public void Fire()
        {
            _weapon.Fire();
        }
    }
}