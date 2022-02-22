using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI.Gamepad;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.UI;
using MonoMod;
using MonoMod.Cil;

namespace RealTimeInGameMod
{
	public class RealTimeInGameMod : Mod
	{
        public override void Load()
        {
            base.Load();
            On.Terraria.Main.DrawNPCChatButtons += Main_DrawNPCChatButtons;;
            IL.Terraria.Player.ItemCheck += Player_ItemCheck;
        }

        private void Player_ItemCheck(MonoMod.Cil.ILContext il)
        {
            var c = new ILCursor(il);
            //throw new System.NotImplementedException();
        }

        private void Main_DrawNPCChatButtons(On.Terraria.Main.orig_DrawNPCChatButtons orig, int superColor, Color chatColor, int numLines, string focusText, string focusText3)
        {
            
            if (Main.dayTime && Main.npc[Main.LocalPlayer.talkNPC].type == NPCID.OldMan)
            {
                focusText3 = Lang.inter[50].Value;
            }
            orig(superColor, chatColor, numLines, focusText, focusText3);
           
        }
    }
}