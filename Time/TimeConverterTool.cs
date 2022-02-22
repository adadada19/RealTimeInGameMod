using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealTimeInGameMod.Time
{
    class TimeConverterTool : ModItem
    {
        bool isTimeConverted = true;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Time Converter");
            DisplayName.SetDefault("Converts time to real and back");
        }
        public override void SetDefaults()
        {
            item.knockBack = 0f;
            item.width = 64;
            item.height = 64;
            item.maxStack = 1;
            item.rare = ItemRarityID.Expert;
            item.melee = false;
            item.damage = 0;
            item.magic = false;
            item.mana = 0;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 20;
            item.useAnimation = 20;
            item.UseSound = SoundID.Item119;
        }
        public override bool UseItem(Player player)
        {
            if (isTimeConverted == false)
            {
                
                isTimeConverted = true;
                ModContent.GetInstance<TimeConverter>().TimeConvert(isTimeConverted);
            }
            else if (isTimeConverted)
            {
                
                isTimeConverted = false;
                ModContent.GetInstance<TimeConverter>().TimeConvert(isTimeConverted);
            }
            return true;
        }
    }
}
