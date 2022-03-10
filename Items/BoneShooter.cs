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
            item.width = 20;
            item.height = 20;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 0f;
            item.rare = ItemRarityID.Expert;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<BoneProjectile>();
            item.shootSpeed = 16f;
            item.useAmmo = ItemID.Bone;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 14);
        }
    }
}
