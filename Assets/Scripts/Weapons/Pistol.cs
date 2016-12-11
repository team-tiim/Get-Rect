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
            sprite = "PistolAttackAnimation";
            projectileSprite = "weapons 1";
            projectileSpriteIndex = 7;
            projectileSpeed = 10;
            animationName = "projectile_pistol";
            gravityScale = 0.5f;
            attackCooldown = 0.1f;
        }
    }
}
