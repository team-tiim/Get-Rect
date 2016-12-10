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
            sprite = "weapons1";
            projectileSpeed = 10;
            projectileSprite = "weapons1";
        }
    }
}
