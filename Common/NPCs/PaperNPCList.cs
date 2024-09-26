using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public class PaperNPCList : ModSystem
    {
        public static List<int> EnemiesRotateWhileAsleep = new();
        public static List<int> NoGravityEnemies = new();

        public override void Unload()
        {
            EnemiesRotateWhileAsleep = null;
            NoGravityEnemies = null;
        }

        public override void SetStaticDefaults()
        {
            var EnemyRotateList = new List<int>()
            {

            };

            var NoGravList = new List<int>()
            {
                NPCID.Ghost, NPCID.Wraith
            };

            for (int i = 0; i < EnemyRotateList.Count; i++)
            {
                EnemiesRotateWhileAsleep.Add(EnemyRotateList[i]);
            }

            for (int i = 0; i < NoGravityEnemies.Count; i++)
            {
                NoGravityEnemies.Add(NoGravList[i]);
            }
        }
    }
}