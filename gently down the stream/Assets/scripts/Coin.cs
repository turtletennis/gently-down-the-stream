using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Coin : Pickup
    {
        public int value = 10;

        public override void Collect()
        {
            Debug.Log("Picked up " + name);
            PlayerStats.coins += value;
            base.Collect();
        }
    }
}
