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
            item.knockBack = 0f;
            item.width = 66;
            item.height = 50;
            item.maxStack = 1;
            item.rare = ItemRarityID.Expert;
            item.melee = false;
            item.damage = 30;
            item.magic = false;
            item.ranged = true;
            item.mana = 0;
            item.useAmmo = 154;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 10;
            item.shootSpeed = 16f;
            item.shootSpeed = 10.0f;
            item.shoot = ModContent.ProjectileType<BoneProjectile>();
            item.autoReuse = true;
        }
    }
}
