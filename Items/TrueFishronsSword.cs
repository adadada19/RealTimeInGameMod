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

namespace RealTimeInGameMod.Items
{
    class TrueFishronsSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Fishron's Sword");
            Tooltip.SetDefault("Is that a shark there?");
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.melee = true;
            item.width = 36;
            item.height = 38;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 6f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 15;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = player.Center + new Vector2(0f, -12f);
            var oldVelocity2 = new Vector2(player.Center.X - Main.MouseWorld.X, player.Center.Y - Main.MouseWorld.Y);
            var oldVelocity1 = Vector2.Negate(oldVelocity2);
            var velocity = Vector2.Normalize(oldVelocity1) * 5f;
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30));
                Projectile.NewProjectile(position, perturbedSpeed, ModContent.ProjectileType<DetonatingBubble2>(), damage, knockBack, player.whoAmI);
            }
            if (Main.rand.Next(5) == 4)
            {
                Projectile.NewProjectile(position, velocity, ModContent.ProjectileType<Sharkron>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
