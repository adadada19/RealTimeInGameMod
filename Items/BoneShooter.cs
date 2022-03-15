using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealTimeInGameMod.Projectiles;
using RealTimeInGameMod.Buffs;

namespace RealTimeInGameMod.Items
{
    class BoneShooter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Shooter");
            Tooltip.SetDefault("Shoot a bone");
        }
        public override void SetDefaults()
        {
            item.damage = 30;
            item.ranged = true;
            item.width = 64;
            item.height = 16;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0f;
            item.rare = ItemRarityID.Expert;
            item.UseSound = SoundID.Item38;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<BoneProjectile>();
            item.shootSpeed = 16f;
            item.useAmmo = ItemID.Bone;
            item.scale = 0.8f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            /*            if (player.position.X > Main.MouseWorld.X)
                        {
                            position += new Vector2(-50, -12);
                        }
                        else if (player.position.X < Main.MouseWorld.X)
                        {
                            position += new Vector2(50, -12);
                        }
                        else
                        {*/
            // position += new Vector2(0, -5);

            /*            }*/
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 60f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                                                                                                                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                                                                                                                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 3);
        }
    }
}
