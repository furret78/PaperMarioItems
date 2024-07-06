using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;
using System;
using PaperMarioItems.Content.Projectiles;
using Terraria.DataStructures;

namespace PaperMarioItems.Common.Players
{
    partial class PaperPlayer : ModPlayer
    {
        //targeting IDs
        private const int shootingStarCase = 0, dizzyDialCase = 1, frightMaskCase = 2, thunderCase = 3, timestopCase = 4, hpDrainCase = 5;
        //targeting conditions
        private static bool TargetConditionCheck(Player player, NPC npc, Player vsplayer, int condition)
        {
            if (Main.myPlayer != player.whoAmI || player == null) return false;
            if (npc != null &&
                !npc.friendly &&
                !NPCID.Sets.CountsAsCritter[npc.type] &&
                npc.type != NPCID.CultistTablet &&
                (npc.Center - player.Center).Length() < (Main.screenWidth / 2))
            {
                switch (condition)
                {
                    //projectile targeting
                    case thunderCase:
                    case timestopCase:
                    case hpDrainCase:
                    case shootingStarCase:
                        if (npc.type != NPCID.CultistDevote) return true;
                        else return false;
                    //special targeting
                    case dizzyDialCase:
                        if (npc.type == NPCID.CultistDevote || !(NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.boss)) return true;
                        else return false;
                    case frightMaskCase:
                        if (!(npc.type == NPCID.CultistDevote || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.boss)) return true;
                        else return false;
                    //no such condition
                    default: return false;
                }
            }
            if (vsplayer != null &&
                vsplayer.hostile &&
                (vsplayer.Center - player.Center).Length() < (Main.screenWidth / 2))
            {
                switch (condition)
                {
                    case shootingStarCase:
                    case frightMaskCase:
                    case thunderCase:
                    case timestopCase:
                    case hpDrainCase:
                    case dizzyDialCase: return true;
                    //no such condition
                    default: return false;
                }
            }
            else return false;
        }
        //shooting star loopable
        private void ShootingStarAttack(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, shootingStarCase))
                {
                    empty = false;
                    //calculating visual parameters
                    Vector2 playerPosition = new(player.Center.X + Main.rand.NextFloatDirection()*Main.rand.NextFloat(Main.screenWidth / 2), player.Center.Y - ((Main.screenHeight*2)/3));
                    if (!player.ZoneSkyHeight && !player.ZoneOverworldHeight) playerPosition.X = player.Center.X - player.direction*(Main.screenWidth / 2);
                    Vector2 defaultVelocity = new(Main.rand.NextFloat(17) + 11f);
                    Vector2 homingAngle = (npc.Center - playerPosition).SafeNormalize(Vector2.UnitX);
                    //calculating damage
                    int finalDamage = 300;
                    if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail) finalDamage = 15;
                    if (npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail) finalDamage = 8;
                    //spawning
                    Projectile.NewProjectile(player.GetSource_FromThis(), playerPosition, defaultVelocity * homingAngle, ProjectileID.Starfury, finalDamage, 10f, Main.myPlayer, 0, npc.Center.Y);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, shootingStarCase))
                {
                    empty = false;
                    //calculating visual parameters
                    Vector2 playerPosition = new(player.Center.X + Main.rand.NextFloatDirection() * Main.rand.NextFloat(Main.screenWidth / 2), player.Center.Y - ((Main.screenHeight * 2) / 3));
                    if (!player.ZoneSkyHeight && !player.ZoneOverworldHeight) playerPosition.X = player.Center.X - player.direction * (Main.screenWidth / 2);
                    Vector2 defaultVelocity = new(Main.rand.NextFloat(17) + 11f);
                    Vector2 homingAngle = (vsplayer.Center - playerPosition).SafeNormalize(Vector2.UnitX);
                    //spawning
                    Projectile.NewProjectile(player.GetSource_FromThis(), playerPosition, defaultVelocity * homingAngle, ProjectileID.Starfury, 100, 10f, Main.myPlayer, 0, vsplayer.Center.Y);
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.starPM, player.Center);
        }
        //fire flower shooting
        private void FireFlowerAttack(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                int range = 250;
                Vector2 position = new(player.Center.X + 10 * player.direction, player.Center.Y);
                Vector2 velocity = new(Main.rand.NextFloat(17) + 10);
                Vector2 target = new(player.Center.X + 100 * player.direction, player.Center.Y - ((range/2) - Main.rand.NextFloat(range)));
                Vector2 angle = (target - position).SafeNormalize(Vector2.UnitX);
                float stopper = player.Center.Y + (Main.screenHeight / 2);
                Projectile.NewProjectile(player.GetSource_FromThis(), position, velocity * angle, ModContent.ProjectileType<CustomFireball>(), fireFlowerDamage, 3f);
                SoundEngine.PlaySound(PaperMarioItems.fireFlowerPM, position);
            }
        }
        //inflict dizzy on enemies
        public void InflictDizzy()
        {
            inflictDizzyActive = true;
            screenSpinTimer = 90;
        }
        public void InflictDizzyOnEnemies(Player player)
        {
            bool empty = true;
            if (Main.myPlayer == player.whoAmI)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (TargetConditionCheck(player, npc, null, dizzyDialCase))
                    {
                        empty = false;
                        npc?.AddBuff(ModContent.BuffType<DizzyDebuff>(), 10800);
                    }
                }
                foreach (var vsplayer in Main.ActivePlayers)
                {
                    if (TargetConditionCheck(player, null, vsplayer, dizzyDialCase))
                    {
                        empty = false;
                        vsplayer?.AddBuff(ModContent.BuffType<DizzyDebuff>(), 10800);
                    }
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.causeStatusPM);
        }
        //fright mask
        public void InflictFrightOnAll(Player player)
        {
            int whichDirection;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, frightMaskCase))
                {
                    if (npc.Center.X < player.Center.X) whichDirection = -1;
                    else whichDirection = 1;
                    npc.AddBuff(ModContent.BuffType<FrightDebuff>(), 10);
                    npc.SimpleStrikeNPC(0, whichDirection, false, 20);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, frightMaskCase))
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
                if (TargetConditionCheck(player, null, vsplayer, thunderCase))
                {
                    Vector2 playerToEnemy = new(player.Center.X - vsplayer.Center.X, player.Center.Y - vsplayer.Center.Y);
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
                if (TargetConditionCheck(player, npc, null, thunderCase))
                {
                    Vector2 playerToEnemy = new(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
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
                    SoundEngine.PlaySound(PaperMarioItems.thunderPM, closestNPC.Center);
                }
                if (closestPlayer != null)
                {
                    StrikeLightning(player, null, closestPlayer);
                    SoundEngine.PlaySound(PaperMarioItems.thunderPM, closestPlayer.Center);
                }
            }
        }
        private void StrikeAllEnemies(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, thunderCase))
                {
                    empty = false;
                    StrikeLightning(player, npc, null);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, thunderCase))
                {
                    empty = false;
                    StrikeLightning(player, null, vsplayer);
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.thunderPM, player.Center);
        }
        public void StrikeLightning(Player player, NPC npc, Player vsplayer)
        {
            int whichDirection;
            Dust.NewDustDirect(npc.Center, 2, 2, ModContent.DustType<LightningDust>());
            if (npc != null)
            {
                if (npc.Center.X < player.Center.X) whichDirection = -1;
                else whichDirection = 1;
                npc.SimpleStrikeNPC(100, whichDirection, Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic));
                npc.AddBuff(BuffID.Electrified, 1800);
            }
            if (vsplayer != null)
            {
                if (vsplayer.Center.X < player.Center.X) whichDirection = -1;
                else whichDirection = 1;
                vsplayer.Hurt(default, 100, whichDirection, true);
            }
        }
        public void BackgroundFlash()
        {
            bgFlashTime = 13;
            bgFlash = true;
        }
        //timestop
        public void InflictTimestop(Player player)
        {
            bool empty = true;
            bool moonLordDetected = false;
            if (Main.myPlayer == player.whoAmI)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (TargetConditionCheck(player, npc, null, timestopCase))
                    {
                        Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                        Vector2 newPos = new(npc.Center.X, npc.Center.Y);
                        if (!npc.boss && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                        {
                            empty = false;
                            if (!npc.HasBuff<TimestopDebuff>()) Dust.NewDustPerfect(newPos, ModContent.DustType<StopwatchDust>(), null, 0, color);
                            npc.AddBuff(ModContent.BuffType<TimestopDebuff>(), 10800);
                        }
                        else
                        {
                            if ((npc.type == NPCID.MoonLordCore || npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordHead))
                            {
                                if (!moonLordDetected)
                                {
                                    Main.NewText(MoonLordStopwatch, Color.Cyan);
                                    moonLordDetected = true;
                                }
                            }
                            else
                            {
                                int timestopChance = 7;
                                if (npc.type == NPCID.MoonLordFreeEye || npc.type == NPCID.MoonLordLeechBlob) timestopChance = 16;
                                if (Main.rand.NextBool(timestopChance))
                                {
                                    empty = false;
                                    if (!npc.HasBuff<TimestopDebuff>()) Dust.NewDustPerfect(newPos, ModContent.DustType<StopwatchDust>(), null, 0, color);
                                    npc.AddBuff(ModContent.BuffType<TimestopDebuff>(), 3600);
                                }
                            }
                        }
                    }
                }
                foreach (var vsplayer in Main.ActivePlayers)
                {
                    if (TargetConditionCheck(player, null, vsplayer, timestopCase))
                    {
                        Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                        Vector2 newPos = new(vsplayer.Center.X, vsplayer.Center.Y);
                        if (Main.rand.NextBool(5))
                        {
                            empty = false;
                            if (vsplayer.HasBuff<TimestopDebuff>()) Dust.NewDustPerfect(newPos, ModContent.DustType<StopwatchDust>(), null, 0, color);
                            vsplayer.AddBuff(ModContent.BuffType<TimestopDebuff>(), 10800);
                        }
                    }
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.causeStatusPM);
        }
        //hp drain
        public void HPDrain(Player player, int healAmount, string deathReason)
        {
            NPC closestNPC = null;
            Player closestPlayer = null;
            float closestDist = 0, distSq = 0;
            int whichDirection;
            bool exist = false;
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, hpDrainCase))
                {
                    Vector2 playerToEnemy = new(player.Center.X - vsplayer.Center.X, player.Center.Y - vsplayer.Center.Y);
                    distSq = (float)Math.Sqrt(playerToEnemy.LengthSquared());
                    if (closestNPC == null || distSq < closestDist)
                    {
                        closestPlayer = vsplayer;
                        closestDist = distSq;
                        exist = true;
                    }
                }
            }
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, hpDrainCase))
                {
                    
                    Vector2 playerToEnemy = new(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                    distSq = (float)Math.Sqrt(playerToEnemy.LengthSquared());
                    if (closestNPC == null || distSq < closestDist)
                    {
                        closestNPC = npc;
                        closestDist = distSq;
                        exist = true;
                    }
                }
            }
            if ((closestNPC != null || closestPlayer != null) && closestDist <= distSq)
            {
                if (closestNPC != null && closestPlayer == null)
                {
                    if (closestNPC.Center.X < player.Center.X) whichDirection = -1;
                    else whichDirection = 1;
                    closestNPC.SimpleStrikeNPC(healAmount, whichDirection);
                    Dust.NewDustPerfect(closestNPC.Center, ModContent.DustType<HPDrainDust>());
                    SoundEngine.PlaySound(SoundID.NPCDeath4, closestNPC.Center);
                }
                if (closestPlayer != null)
                {
                    if (closestPlayer.Center.X < player.Center.X) whichDirection = -1;
                    else whichDirection = 1;
                    closestPlayer.Hurt(PlayerDeathReason.ByCustomReason(closestPlayer.name + " " + deathReason), healAmount, player.direction, true, false, -1, false);
                    Dust.NewDustPerfect(closestPlayer.Center, ModContent.DustType<HPDrainDust>());
                    SoundEngine.PlaySound(SoundID.NPCDeath4, closestPlayer.Center);
                }
            }
            if (exist)
            {
                player.Heal(healAmount);
                SoundEngine.PlaySound(PaperMarioItems.healPM, player.Center);
            }
        }
    }
}