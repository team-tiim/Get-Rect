using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Weapons
{
    class Pistol : ProjectileWeapon
    {
        public Pistol()
        {
            damage = 5;
            idleAnimation = "weapon_pistol_idle";
            gravityScale = 0.5f;
            attackCooldown = 0.2f;
            knockback = 30;
        }
    }
}
