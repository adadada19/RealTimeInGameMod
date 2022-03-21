using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealTimeInGameMod.Buffs;

namespace RealTimeInGameMod.Projectiles
{
    class DetonatingBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Detonating Bubble");
        }
        public override void SetDefaults()
        {
            projectile.knockBack = 0f;
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.damage = 5;
            projectile.scale = (float)Main.rand.Next(75, 129) / 100f;
        }
        public override void AI()
        {
			Player player = Main.LocalPlayer;
            int b = -1;
            var pos = projectile.position;
            var playerPos = player.position;
            float distanceFromTarget = 100f;
            Vector2 targetCenter = pos;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, pos);
                    bool closest = Vector2.Distance(pos, targetCenter) > between;
                    bool inRange = between < distanceFromTarget;
                    if ((closest && inRange) || b == -1)
                    {
                        b = i;
                        targetCenter = npc.Center;
                    }
                }
            }
			Vector2 vector132 = Vector2.Normalize(targetCenter - projectile.Center);
			projectile.velocity = (projectile.velocity * 40f + vector132 * 20f) / 41f;
			projectile.velocity.X = (projectile.velocity.X * 50f + Main.rand.Next(-10, 11) * 0.1f) / 51f;
			projectile.velocity.Y = (projectile.velocity.Y * 50f + -0.25f + Main.rand.Next(-10, 11) * 0.2f) / 51f;
            if (Vector2.Distance(playerPos, pos) >= 1000f)
            {
                projectile.active = false;
            }
		}
    }
}
