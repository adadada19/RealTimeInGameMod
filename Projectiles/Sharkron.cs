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
    class Sharkron : ModProjectile
    {
        int timer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharkron");
        }
        public override void SetDefaults()
        {
            projectile.knockBack = 0f;
            projectile.width = 42;
            projectile.height = 42;
            projectile.friendly = true;
            projectile.damage = 5;
        }
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            timer++;
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
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;
            if (b != -1)
            {
                Vector2 vector132 = Vector2.Normalize(targetCenter - projectile.Center);
                projectile.velocity = (projectile.velocity * 40f + vector132 * 20f) / 41f;
            }
            else
            {
                projectile.velocity.Y += 0.1f;
            }
            if (Vector2.Distance(playerPos, pos) >= 1000f || timer == 240)
            {
                projectile.active = false;
                HitCollideDeathEffect();
                timer = 0;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.NPCHit, projectile.position, 25);
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y + 0.5f;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.active = false;
            HitCollideDeathEffect();
        }
        public void HitCollideDeathEffect()
        {
            Main.PlaySound(SoundID.NPCKilled, (int)projectile.Center.X, (int)projectile.Center.Y, 19);
            if (!projectile.active)
            {
                for (int num256 = 0; num256 < 75; num256++)
                {
                    int num257 = Dust.NewDust(projectile.Center - Vector2.One * 25f, 50, 50, 5, 2 * projectile.direction, -2f);
                    Dust dust = Main.dust[num257];
                    dust.velocity /= 2f;
                }
                Gore.NewGore(projectile.Center, projectile.velocity * 0.8f, 583);
                Gore.NewGore(projectile.Center, projectile.velocity * 0.8f, 577);
                Gore.NewGore(projectile.Center, projectile.velocity * 0.9f, 578);
                Gore.NewGore(projectile.Center, projectile.velocity, 579);
            }
        }
    }
}

