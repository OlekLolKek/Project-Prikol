namespace ProjectPrikol
{
    public class WeaponSafety
    {
        public bool IsSafetyOn { get; set; }

        public void SwitchSafety()
        {
            IsSafetyOn = !IsSafetyOn;
        }
    }
}