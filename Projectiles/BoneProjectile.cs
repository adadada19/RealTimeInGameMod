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
        bool firstFrame = true;
        public override void SetDefaults()
        {
            projectile.damage = 5;
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
        }
        public override void AI()
        {
            if (firstFrame)
            {
                firstFrame = false;
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            }
            else
            {
                projectile.velocity.Y += 0.1f;
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            }
        }
    }
}
