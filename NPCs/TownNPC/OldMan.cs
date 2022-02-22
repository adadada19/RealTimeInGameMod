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

namespace RealTimeInGameMod.NPCs.TownNPC
{
    class OldMan : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            
            if (npc.type == NPCID.OldMan && Main.dayTime)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        chat = Lang.dialog(90, false);
                        break;
                    case 1:
                        chat = Lang.dialog(91, false);
                        break;
                    case 2:
                        chat = Lang.dialog(92, false);
                        break;
                }
            }
        }
        public override bool PreChatButtonClicked(NPC npc, bool firstButton)
        {

            if (npc.type == NPCID.OldMan)
            {

                if (firstButton == false)
                {
                    NPC.SpawnSkeletron();
                }
            }
            else
            {
                return base.PreChatButtonClicked(npc, firstButton);
            }
            return false;
        }
    }
}
