namespace ProjectPrikol
{
    public abstract class WeaponModification : IFire
    {
        private Weapon _weapon;

        protected abstract Weapon AddModification(Weapon weapon);

        protected abstract Weapon RemoveModification(Weapon weapon);

        public void ApplyModification(Weapon weapon)
        {
            _weapon = AddModification(weapon);
        }

        public void Fire()
        {
            _weapon.Fire();
        }
    }
}