using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using PaperMarioItems.Content.Items.Consumables;
using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace PaperMarioItems.Common.Players
{
    partial class PaperPlayer : ModPlayer
    {
        //shooting star
        private void ShootingStarAttack(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly && npc.type != NPCID.CultistDevote && !NPCID.Sets.CountsAsCritter[npc.type] && (npc.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    empty = false;
                    Vector2 playerPosition = new Vector2(player.Center.X + Main.rand.NextFloatDirection()*Main.rand.NextFloat(Main.screenWidth / 2), player.Center.Y - ((Main.screenHeight*2)/3));
                    if (!player.ZoneSkyHeight && !player.ZoneOverworldHeight) playerPosition.X = player.Center.X - player.direction*(Main.screenWidth / 2);
                    Vector2 defaultVelocity = new Vector2(Main.rand.NextFloat(17) + 11f);
                    Vector2 homingAngle = (npc.Center - playerPosition).SafeNormalize(Vector2.UnitX);
                    int finalDamage = 300;
                    if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail) finalDamage = 15;
                    if (npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail) finalDamage = 8;
                    Projectile.NewProjectile(player.GetSource_FromThis(), playerPosition, defaultVelocity * homingAngle, ProjectileID.Starfury, finalDamage, 10f, Main.myPlayer, 0, npc.Center.Y);
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.starPM);
        }
        //inflict dizzy on enemies
        public void InflictDizzy()
        {
            inflictDizzyActive = true;
            screenSpinTimer = 90;
        }
        public void InflictDizzyOnEnemies(Player player)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly && npc.type != NPCID.CultistDevote && !NPCID.Sets.CountsAsCritter[npc.type] && (npc.Center - player.Center).Length() < (Main.screenWidth / 2) && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type] && !npc.boss)
                {
                    npc?.AddBuff(ModContent.BuffType<DizzyDebuff>(), 10800);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (Main.myPlayer == player.whoAmI && vsplayer.hostile && (vsplayer.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    vsplayer?.AddBuff(ModContent.BuffType<DizzyDebuff>(), 10800);
                }
            }
        }
        //fright mask
        public void InflictFrightOnAll(Player player)
        {
            int whichDirection;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly && npc.type != NPCID.CultistDevote && !NPCID.Sets.CountsAsCritter[npc.type] && (npc.Center - player.Center).Length() < (Main.screenWidth / 2) && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type] && !npc.boss)
                {
                    if (npc.Center.X < player.Center.X) whichDirection = -1;
                    else whichDirection = 1;
                    npc.AddBuff(ModContent.BuffType<FrightDebuff>(), 10);
                    npc.SimpleStrikeNPC(0, whichDirection, false, 20);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (Main.myPlayer == player.whoAmI && vsplayer.hostile && (vsplayer.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    if (vsplayer.Center.X < player.Center.X) whichDirection = -1;
                    else whichDirection = 1;
                    vsplayer.Hurt(default, 0, whichDirection, true, false, 0, false, 0, 0, 20);
                }
            }
        }
        //thunder bolt and rage
        private void StrikeOneEnemy(Player player)
        {
            NPC closestNPC = null;
            Player closestPlayer = null;
            float closestDist = 0;
            float distSq = 0;
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (Main.myPlayer == player.whoAmI && vsplayer.hostile && (vsplayer.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    Vector2 playerToEnemy = new Vector2(player.Center.X - vsplayer.Center.X, player.Center.Y - vsplayer.Center.Y);
                    distSq = (float)Math.Sqrt(playerToEnemy.LengthSquared());
                    if (closestNPC == null || distSq < closestDist)
                    {
                        closestPlayer = vsplayer;
                        closestDist = distSq;
                    }
                }
            }
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly && npc.type != NPCID.CultistDevote && !NPCID.Sets.CountsAsCritter[npc.type] && (npc.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    Vector2 playerToEnemy = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                    distSq = (float)Math.Sqrt(playerToEnemy.LengthSquared());
                    if (closestNPC == null || distSq < closestDist)
                    {
                        closestNPC = npc;
                        closestDist = distSq;
                    }
                }
            }
            if ((closestNPC != null || closestPlayer != null) && closestDist <= distSq)
            {
                if (closestNPC != null && closestPlayer == null)
                {
                    StrikeLightning(player, closestNPC, null);
                    SoundEngine.PlaySound(loudThunder, closestNPC.Center);
                }
                if (closestPlayer != null)
                {
                    SoundEngine.PlaySound(loudThunder, closestPlayer.Center);
                }

            }
        }
        private void StrikeAllEnemies(Player player)
        {
            NPC closestNPC = null;
            Player closestPlayer = null;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly && npc.type != NPCID.CultistDevote && !NPCID.Sets.CountsAsCritter[npc.type] && (npc.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    closestNPC = npc;
                    StrikeLightning(player, npc, null);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (Main.myPlayer == player.whoAmI && vsplayer.hostile && (vsplayer.Center - player.Center).Length() < (Main.screenWidth / 2))
                {
                    closestPlayer = vsplayer;
                    StrikeLightning(player, null, vsplayer);
                }
            }
            if (closestNPC != null || closestPlayer != null)
            {
                SoundEngine.PlaySound(loudThunder, player.Center);
            }
        }
        public void StrikeLightning(Player player, NPC npc, Player vsplayer)
        {
            int whichDirection;
            Dust.NewDustDirect(npc.Center, 2, 2, ModContent.DustType<LightningDust>());
            if (npc != null)
            {
                if (npc.Center.X < player.Center.X) whichDirection = -1;
                else whichDirection = 1;
                npc.SimpleStrikeNPC(100, whichDirection);
                npc.AddBuff(BuffID.Electrified, 1800);
            }
            if (vsplayer != null)
            {
                if (vsplayer.Center.X < player.Center.X) whichDirection = -1;
                else whichDirection = 1;
                vsplayer.Hurt(default, 100, player.direction);
            }
        }
        public void BackgroundFlash()
        {
            bgFlashTime = 13;
            bgFlash = true;
        }
    }
}