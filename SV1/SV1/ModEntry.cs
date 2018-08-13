using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Astra
{
    // IAssetLoader is needed for Load<T>, IAssetEditor for Edit<T>
    public class ModEntry : Mod, IAssetLoader, IAssetEditor
    {
        // The entry point
        public override void Entry(IModHelper helper)
        {
            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
        }

        // Preparing to edit these assets
        public bool CanEdit<T>(IAssetInfo asset)
        {
            if (asset.AssetNameEquals("Data/NPCDispositions"))
                return true;
            if (asset.AssetNameEquals("Data/NPCGiftTastes"))
                return true;
            if (asset.AssetNameEquals("Characters/Dialogue/rainy"))
                return true;
            if (asset.AssetNameEquals("Data/EngagementDialogue"))
                return true;
            return false;
        }

        // Actually editing said asset data
        public void Edit<T>(IAssetData asset)
        {
            if (asset.AssetNameEquals("Data/NPCDispositions"))
            {
                asset.AsDictionary<string, string>().Data["Astra"] = "adult/neutral/neutral/negative/female/datable/null/Town/summer 2/Maru ''/Saloon 33 6/Astra";
            }
            if (asset.AssetNameEquals("Data/NPCGiftTastes"))
            {
                IDictionary<string, string> NPCGiftTastes = asset.AsDictionary<string, string>().Data;
                NPCGiftTastes["Astra"] = "I'm not sure what I've done to deserve this, but thank you very much. This is amazing./206 211 395" +
                    "/For me? That is very nice of you, thank you./109 305 578" +
                    "/You're giving this to me? I suppose I could find a purpose for it.../346 773" +
                    "/I'd rather you not give that to me. The garbage can is over there./60 548 560" +
                    "/Oh, thank you./203 303 348 388 390 403 459/";
            }
            if (asset.AssetNameEquals("Characters/Dialogue/rainy"))
            {
                IDictionary<string, string> rainy = asset.AsDictionary<string, string>().Data;
                rainy["Astra"] = "Ah, rain.#$e#What a beautiful sign of a healthy planet.";
            }
            if(asset.AssetNameEquals("Data/EngagementDialogue"))
            {
                IDictionary<string, string> EngagementDialogue = asset.AsDictionary<string, string>().Data;
                EngagementDialogue["Astra"] = "You want to get married? To me?#$e#Yes. Yes, I accept. I just never thought...#$e#It doesn't matter. Don't worry, @, I'll take care of everything. We'll marry in 3 days time.";
            }

        }

        // Preparing to load external assets
        public bool CanLoad<T>(IAssetInfo asset)
        {
            if (asset.AssetNameEquals("Characters/Dialogue/Astra"))
                return true;
            if (asset.AssetNameEquals("Characters/Schedules/Astra"))
                return true;
            if (asset.AssetNameEquals("Characters/Astra"))
                return true;
            if (asset.AssetNameEquals("Portraits/Astra"))
                return true;
            return false;
        }

        // Actually load said external assets
        public T Load<T>(IAssetInfo asset)
        {
            if (asset.AssetNameEquals("Characters/Dialogue/Astra"))
            {
                return Helper.Content.Load<T>("assets/Astra_Dialogue.xnb", ContentSource.ModFolder);
            }
            if (asset.AssetNameEquals("Characters/Schedules/Astra"))
            {
                return Helper.Content.Load<T>("assets/Astra_Schedule.xnb", ContentSource.ModFolder);
            }
            if (asset.AssetNameEquals("Characters/Astra"))
            {
                return Helper.Content.Load<T>("assets/texture.png", ContentSource.ModFolder);
            }
            if (asset.AssetNameEquals("Portraits/Astra"))
            {
                return Helper.Content.Load<T>("assets/portrait.png", ContentSource.ModFolder);
            }

            throw new InvalidOperationException($"Unexpected asset '{asset.AssetName}'.");
        }
        
        // The event to spawn the NPC
        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            Texture2D portrait = Helper.Content.Load<Texture2D>("assets/portrait.png", ContentSource.ModFolder);

            NPC AstraNPC = new NPC(null, new Vector2(33, 6), "Saloon", 0, "Astra", true, null, portrait);
            
            //For debugging purposes
            //Monitor.Log($"Astra shoulda spawned at {AstraNPC.Position.X},{AstraNPC.Position.Y}");
        }
    }
}