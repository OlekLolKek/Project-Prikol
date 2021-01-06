using UnityEngine;

namespace ProjectPrikol
{
    public class Weapon
    {
        private GameObject _instance;
        
        public float Damage { get; set; }
        public float ShootCooldown { get; set; }
        public float ReloadTime { get; set; }
        public bool IsFullAuto { get; set; }
        
        public Weapon(IFactory factory)
        {
            _instance = factory.Create();
        }
        
        public void SetActive(bool active)
        {
            _instance.SetActive(active);
        }
    }
}