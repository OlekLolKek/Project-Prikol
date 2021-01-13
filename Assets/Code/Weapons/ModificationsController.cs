﻿using UnityEngine;

namespace ProjectPrikol
{
    public sealed class ModificationsController : WeaponModification
    {
        private readonly IBarrelAttachment _barrelAttachment;
        private readonly IScope _scope;

        public ModificationsController(IBarrelAttachment barrelAttachment, IScope scope)
        {
            _barrelAttachment = barrelAttachment;
            _scope = scope;
        }

        protected override Weapon AddModification(Weapon weapon)
        {
            _barrelAttachment.Activate();
            weapon.SetModdedValues(_barrelAttachment.AttachmentBarrel, 
                _barrelAttachment.AttachmentAudioSource);
            
            _scope.Activate();
            
            return weapon;
        }

        protected override Weapon RemoveModification(Weapon weapon)
        {
            _barrelAttachment.Deactivate();
            weapon.SetDefaultValues();
            
            _scope.Deactivate();

            return weapon;
        }
    }
}