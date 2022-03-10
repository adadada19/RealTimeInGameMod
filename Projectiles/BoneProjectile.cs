using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealTimeInGameMod.Projectiles;
using RealTimeInGameMod.Buffs;

namespace RealTimeInGameMod.Projectiles
{
    class BoneProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.damage = 5;
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 1;

        }
    }
}
