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
            _damage = 5;
            _idleAnimation = "weapon_pistol_idle";
            projectileSprite = "weapons 1";
            projectileSpriteIndex = 7;
            projectileSpeed = 20;
            projectileAnimation = "projectile_pistol";
            gravityScale = 0.5f;
            attackCooldown = 0.001f;
        }
    }
}
