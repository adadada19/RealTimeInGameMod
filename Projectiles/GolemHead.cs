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

    class GolemHead : ModProjectile
    {
        int Timer = new int();
        public override void SetStaticDefaults()
        {
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.damage = 5;
            projectile.knockBack = 2f;
            projectile.penetrate = -1;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            var damage = projectile.damage;
            var knockBack = projectile.knockBack;
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<GolemHeadBuff>());
                projectile.minionSlots = 1f;
                projectile.damage = 5;
                projectile.knockBack = 2f;
            }
            if (player.HasBuff(ModContent.BuffType<GolemHeadBuff>()))
            {
                projectile.timeLeft = 2;
            }
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f;
            float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
            idlePosition.X += minionPositionOffsetX;
            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }
            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                // Fix overlap with other minions
                Projectile other = Main.projectile[i];
                if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
                    else projectile.velocity.X += overlapVelocity;

                    if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
                    else projectile.velocity.Y += overlapVelocity;
                }
            }
            float speed = 8f;
            float inertia = 20f;
            if (distanceToIdlePosition > 600f)
            {
                // Speed up the minion if it's away from the player
                speed = 12f;
                inertia = 60f;
            }
            else
            {
                // Slow down the minion if closer to the player
                speed = 4f;
                inertia = 80f;
            }
            if (distanceToIdlePosition > 20f)
            {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            /*Vector2 idlePosition = new Vector2(player.position.X, player.position.Y);
            if (idlePosition.X > projectile.position.X)
            {
                Vector2 BackOfHead1 = new Vector2(player.Center.X - 40f, player.Center.Y - 20f);
                Vector2 MoveToHead1 = new Vector2(BackOfHead1.X - projectile.Center.X, BackOfHead1.Y - projectile.Center.Y);
                
                if (projectile.Center != BackOfHead1)
                {
                    MoveToHead1.Normalize();
                    projectile.velocity += MoveToHead1/10f;
                }
                else
                    projectile.velocity = Vector2.Zero;
            }
            else if (idlePosition.X < projectile.position.X)
            {
                Vector2 BackOfHead1 = new Vector2(player.Center.X + 20f, player.Center.Y - 20f);
                Vector2 MoveToHead1 = new Vector2(BackOfHead1.X - projectile.Center.X, BackOfHead1.Y - projectile.Center.Y);
                
                if (projectile.Center != BackOfHead1)
                {
                    MoveToHead1.Normalize();
                    projectile.velocity += MoveToHead1/10f;
                }
                else
                    projectile.velocity = Vector2.Zero;
            }*/
            int b = -1;
            var pos = projectile.position;
            Vector2 targetCenter = pos;
            // the max distance
            float distanceFromTarget = 700f;
            // loop over npc
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                // define npc
                NPC npc = Main.npc[i];
                // check if npc can be chased
                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, pos);
                    bool closest = Vector2.Distance(pos, targetCenter) > between;
                    bool inRange = between < distanceFromTarget;
                    // check if the npc is the closest npc or no target has been found
                    if ((closest && inRange) || b == -1)
                    {
                        //set targetcenter and b to the index
                        b = i;
                        targetCenter = npc.Center;
                    }
                }
            }
            // check if the index not -1
            if (b != -1)
            {
                //the shoot speed
                float insertsomefloathere = 10f;
                // use DirectionTo
                Vector2 velToEnemy = projectile.DirectionTo(targetCenter) * insertsomefloathere;
                Vector2 GolemLeftEye = new Vector2(projectile.Center.X + 10f, projectile.Center.Y + 10f);
                Vector2 GolemRightEye = new Vector2(projectile.Center.X + 15f, projectile.Center.Y + 10f);
                //call proj.newproj from using the velocity to enemy

                Timer++;
                if (Timer % 120 == 0)
                {
                    Main.PlaySound(SoundID.Item33);
                    Projectile.NewProjectile(GolemLeftEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                    Projectile.NewProjectile(GolemRightEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                }
                if (Timer % 615 == 0)
                {
                    Main.PlaySound(SoundID.Item33);
                    Projectile.NewProjectile(GolemLeftEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                    Projectile.NewProjectile(GolemRightEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                }
                if (Timer % 630 == 0)
                {
                    Main.PlaySound(SoundID.Item33);
                    Projectile.NewProjectile(GolemLeftEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                    Projectile.NewProjectile(GolemRightEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                }
                if (Timer % 645 == 0)
                {
                    Main.PlaySound(SoundID.Item33);
                    Projectile.NewProjectile(GolemLeftEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                    Projectile.NewProjectile(GolemRightEye, velToEnemy, ModContent.ProjectileType<EyeBeam>(), damage, knockBack, projectile.whoAmI);
                    Timer = 0;
                }
            }
        }
        }
}
