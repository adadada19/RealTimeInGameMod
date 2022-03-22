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
    class FishronsSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishron's Sword");
            Tooltip.SetDefault("Let the water flow");
        }
        public override void SetDefaults()
        {
            item.damage = 50;
            item.melee = true;
            item.width = 24;
            item.height = 36;
            item.useTime = 20;
            item.useAnimation = 20;
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
            Projectile.NewProjectile(position, velocity, ModContent.ProjectileType<DetonatingBubble1>(), damage, knockBack, player.whoAmI);
            Main.PlaySound(SoundID.Splash, position, 1);
            return false;
        }
    }
}
