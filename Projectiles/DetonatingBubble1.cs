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
    class DetonatingBubble1 : ModProjectile
    {
        int timer = 0;
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
            projectile.scale = (int)Main.rand.Next(69, 87) / 100f;
        }
        public override void AI()
        {
            timer++;
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
            if (b != -1)
            {
                Vector2 vector132 = Vector2.Normalize(targetCenter - projectile.Center);
                projectile.velocity = (projectile.velocity * 40f + vector132 * 20f) / 41f;
                projectile.velocity.X = (projectile.velocity.X * 50f + Main.rand.Next(-10, 11) * 0.1f) / 51f;
                projectile.velocity.Y = (projectile.velocity.Y * 50f + -0.25f + Main.rand.Next(-10, 11) * 0.2f) / 51f;
            }
            else
            {
                projectile.velocity.X = (projectile.velocity.X * 50f + Main.rand.Next(-10, 11) * 0.1f) / 51f;
                projectile.velocity.Y = (projectile.velocity.Y * 50f + -0.25f + Main.rand.Next(-10, 11) * 0.2f) / 51f;
            }
            if (Vector2.Distance(playerPos, pos) >= 1000f || timer == 120)
            {
                projectile.active = false;
                HitCollideDeathEffect();
                timer = 0;
            }
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            HitCollideDeathEffect();
        }
        public void HitCollideDeathEffect()
        {
            Main.PlaySound(4, (int)projectile.Center.X, (int)projectile.Center.Y, 3);
            if (!projectile.active)
            {
                Vector2 center = projectile.Center;
                for (int num258 = 0; num258 < 60; num258++)
                {
                    int num259 = 25;
                    Vector2 vector19 = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * Main.rand.Next(24, 41) / 8f;
                    int num260 = Dust.NewDust(projectile.Center - Vector2.One * num259, num259 * 2, num259 * 2, 212);
                    Dust dust60 = Main.dust[num260];
                    Vector2 vector20 = Vector2.Normalize(dust60.position - projectile.Center);
                    dust60.position = projectile.Center + vector20 * 25f * projectile.scale;
                    if (num258 < 30)
                    {
                        dust60.velocity = vector20 * dust60.velocity.Length();
                    }
                    else
                    {
                        dust60.velocity = vector20 * Main.rand.Next(45, 91) / 10f;
                    }
                    dust60.color = Main.hslToRgb((float)(0.40000000596046448 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
                    dust60.color = Color.Lerp(dust60.color, Color.White, 0.3f);
                    dust60.noGravity = true;
                    dust60.scale = 0.7f;
                }
            }
        }
    }
}
