using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Weapons
{
    class Knife : Weapon
    {
        public Knife()
        {
            _damage = 10;
            _idleAnimation = "weapon_sword_idle";
            _swingLength = 1.2f;
            _swingRadius = 0.2f;
            attackCooldown = 0.5f;
        }
    }
}
