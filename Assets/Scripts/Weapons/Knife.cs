using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Weapons
{
    class Knife : MeleeWeapon
    {
        public Knife()
        {
            damage = 10;
            idleAnimation = "weapon_sword_idle";
            swingLength = 1.2f;
            swingRadius = 0.2f;
            attackCooldown = 0.5f;
        }
    }
}
