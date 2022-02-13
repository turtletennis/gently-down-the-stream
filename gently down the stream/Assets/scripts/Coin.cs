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

        public new void Collect()
        {
            PlayerStats.coins += value;
            base.Collect();
        }
    }
}
