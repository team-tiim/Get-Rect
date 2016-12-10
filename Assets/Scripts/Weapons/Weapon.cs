using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        public float damage;
        public string sprite;
        public GameObject projectilePrefab;

        public virtual void Attack()
        {
            Debug.Log("Regular weapon");
        }

        public GameObject getProjectile()
        {
            return Instantiate(projectilePrefab);
        }
    }
}
