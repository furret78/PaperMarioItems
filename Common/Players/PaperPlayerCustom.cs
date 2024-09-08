using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using PaperMarioItems.Common.NPCs;
using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;
using PaperMarioItems.Content.Projectiles;
using PaperMarioItems.Content;

namespace PaperMarioItems.Common.Players
{
    partial class PaperPlayer : ModPlayer
    {
        //targeting IDs
        private const int shootingCase = 0, dizzyCase = 1, frightCase = 2, thunderCase = 3, timeCase = 4, hpCase = 5, quakeCase = 6, powCase = 7, ruinCase = 8, softCase = 9;
        //targeting conditions
        private bool TargetConditionCheck(Player player, NPC npc, Player vsplayer, int condition)
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
                    case timeCase:
                    case hpCase:
                    case powCase:
                    case softCase:
                    case shootingCase:
                        if (npc.type != NPCID.CultistDevote) return true;
                        else return false;
                    //special targeting
                    case dizzyCase:
                        if (npc.type == NPCID.CultistDevote || !(NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.boss)) return true;
                        else return false;
                    case ruinCase:
                    case frightCase:
                        if (!(npc.type == NPCID.CultistDevote || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.boss)) return true;
                        else return false;
                    case quakeCase:
                        if ((!npc.HasBuff<TimestopDebuff>() && npc.noGravity) || (npc.HasBuff<TimestopDebuff>() && npc.GetGlobalNPC<PaperNPC>().hadGravity)) return false;
                        if (npc.boss && !PaperMarioItems.IsGroundedBoss[npc.type]) return false;
                        int blockX = (int)(npc.position.X / 16), blockY = (int)(npc.position.Y / 16);
                        for (int x = blockX - 1; x < blockX + 3; x++)
                        {
                            if (WorldGen.SolidTile2(x, blockY + 3) || WorldGen.SolidTile2(x, blockY - 3)) return true;
                        }
                        return false;
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
                    case shootingCase:
                    case frightCase:
                    case thunderCase:
                    case timeCase:
                    case hpCase:
                    case powCase:
                    case ruinCase:
                    case softCase:
                    case dizzyCase: return true;
                    //special
                    case quakeCase:
                        int blockX = (int)(vsplayer.position.X / 16), blockY = (int)(vsplayer.position.Y / 16);
                        for (int x = blockX - 1; x < blockX + 3; x++)
                        {
                            if (WorldGen.SolidTile2(x, blockY - 3) || WorldGen.SolidTile2(x, blockY + 3)) return true;
                        }
                        return false;
                    //no such condition
                    default: return false;
                }
            }
            else return false;
        }
        public int GetDirection(Entity target, Player player)
        {
            if (target.position.X < player.position.X) return -1;
            return 1;
        }
        public void SetShakeTime(int time)
        {
            if (ScreenShakeSystem.screenShakeTime < time) ScreenShakeSystem.screenShakeTime = time;
        }
        //search for other players + couple's cake
        public bool SearchTeammate(Player player)
        {
            if (Main.myPlayer != player.whoAmI || player == null) return false;
            else foreach (var vsplayer in Main.ActivePlayers)
                {
                    if (!vsplayer.hostile && vsplayer != player) return true;
                }
            return false;
        }
        public void AddBuffCouplesCake(Player player, int time = 300)
        {
            if (Main.myPlayer != player.whoAmI || player == null) return;
            else
            {
                Player closestTeammate = null;
                float closestDist = 0;
                float distSq = 0;
                foreach (var teammate in Main.ActivePlayers)
                {
                    Vector2 playerToPlayer = new(player.Center.X - teammate.Center.X, player.Center.Y - teammate.Center.Y);
                    distSq = (float)Math.Sqrt(playerToPlayer.LengthSquared());
                    if ((closestTeammate == null || distSq < closestDist) && !teammate.hostile && teammate != player)
                    {
                        closestTeammate = teammate;
                        closestDist = distSq;
                    }
                }
                closestTeammate?.AddBuff(BuffID.Regeneration, time);
            }
        }
        //life mushroom effects
        private void LifeMushroomHeal(Player self, int num1, int num2)
        {
            if (self == null || !self.HasItem(PMItemID.LifeMushroom)) return;
            else
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (self.buffTime[l] > 0)
                    {
                        self.DelBuff(l);
                        l = -1;
                    }
                }
                self.statLife = 1;
                self.AddBuff(PMBuffID.Revived, num1);
                self.SetImmuneTimeForAllTypes(num1);
                self.AddBuff(BuffID.Regeneration, num2);
                SoundEngine.PlaySound(PaperMarioItems.healPM, self.Center);
                self.ConsumeItem(PMItemID.LifeMushroom, true, true);
            }
        }
        //shooting star loopable
        private void ShootingStarAttack(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, shootingCase))
                {
                    empty = false;
                    //calculating visual parameters
                    Vector2 playerPosition = new(player.Center.X + Main.rand.NextFloatDirection() * Main.rand.NextFloat(Main.screenWidth / 2), player.Center.Y - ((Main.screenHeight * 2) / 3));
                    if (!player.ZoneSkyHeight && !player.ZoneOverworldHeight) playerPosition.X = player.Center.X - player.direction * (Main.screenWidth / 2);
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
                if (TargetConditionCheck(player, null, vsplayer, shootingCase))
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
                Vector2 target = new(player.Center.X + 100 * player.direction, player.Center.Y - ((range / 2) - Main.rand.NextFloat(range)));
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
                    if (TargetConditionCheck(player, npc, null, dizzyCase))
                    {
                        empty = false;
                        npc?.AddBuff(PMBuffID.Dizzy, 10800);
                    }
                }
                foreach (var vsplayer in Main.ActivePlayers)
                {
                    if (TargetConditionCheck(player, null, vsplayer, dizzyCase))
                    {
                        empty = false;
                        vsplayer?.AddBuff(PMBuffID.Dizzy, 10800);
                    }
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.causeStatusPM, player.Center);
        }
        //fright mask
        private void InflictFrightOnAll(Player player)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, frightCase))
                {
                    npc.AddBuff(PMBuffID.Fright, 10);
                    npc.SimpleStrikeNPC(0, GetDirection(npc, player), false, 20);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, frightCase))
                {
                    vsplayer.Hurt(default, 0, GetDirection(vsplayer, player), true, false, 0, false, 0, 0, 20);
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
            Dust.NewDustDirect(npc.Center, 2, 2, ModContent.DustType<LightningDust>());
            if (npc != null)
            {
                npc.SimpleStrikeNPC(100, GetDirection(npc, player), Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic));
                npc.AddBuff(BuffID.Electrified, 1800);
            }
            vsplayer?.Hurt(PlayerDeathReason.ByCustomReason(vsplayer.name + " " + LightningDeath), 100, GetDirection(vsplayer, player), true);
        }
        public void BackgroundFlash()
        {
            bgFlashTime = 13;
            bgFlash = true;
        }
        //timestop
        private void InflictTimestop(Player player)
        {
            bool empty = true;
            bool moonLordDetected = false;
            if (Main.myPlayer == player.whoAmI)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (TargetConditionCheck(player, npc, null, timeCase))
                    {
                        Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                        Vector2 newPos = new(npc.Center.X, npc.Center.Y);
                        if (!npc.boss && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                        {
                            empty = false;
                            if (!npc.HasBuff<TimestopDebuff>()) Dust.NewDustPerfect(newPos, ModContent.DustType<StopwatchDust>(), null, 0, color);
                            npc.AddBuff(PMBuffID.Timestop, 10800);
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
                                    npc.AddBuff(PMBuffID.Timestop, 3600);
                                }
                            }
                        }
                    }
                }
                foreach (var vsplayer in Main.ActivePlayers)
                {
                    if (TargetConditionCheck(player, null, vsplayer, timeCase))
                    {
                        Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                        Vector2 newPos = new(vsplayer.Center.X, vsplayer.Center.Y);
                        if (Main.rand.NextBool(5))
                        {
                            empty = false;
                            if (vsplayer.HasBuff<TimestopDebuff>()) Dust.NewDustPerfect(newPos, ModContent.DustType<StopwatchDust>(), null, 0, color);
                            vsplayer.AddBuff(PMBuffID.Timestop, 10800);
                        }
                    }
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.causeStatusPM);
        }
        //hp drain
        public void HPDrain(Player player, int healAmount)
        {
            NPC closestNPC = null;
            Player closestPlayer = null;
            float closestDist = 0, distSq = 0;
            int finalDamage = healAmount;
            bool exist = false;
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, hpCase))
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
                if (TargetConditionCheck(player, npc, null, hpCase))
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
                    Dust.NewDustPerfect(closestNPC.Center, ModContent.DustType<HPDrainDust>());
                    SoundEngine.PlaySound(SoundID.NPCDeath4, closestNPC.Center);
                    //hit the npc and do server handling
                    var hit = closestNPC.CalculateHitInfo(healAmount + 2, GetDirection(closestNPC, player), Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic), 0, null, false);
                    closestNPC.StrikeNPC(hit);
                    if (Main.netMode != NetmodeID.SinglePlayer) NetMessage.SendStrikeNPC(closestNPC, hit);
                    finalDamage = hit.Damage;
                }
                if (closestPlayer != null)
                {
                    closestPlayer.Hurt(PlayerDeathReason.ByCustomReason(closestPlayer.name + " " + HPDrainDeath), healAmount, GetDirection(closestPlayer, player), true, false, -1, false);
                    Dust.NewDustPerfect(closestPlayer.Center, ModContent.DustType<HPDrainDust>());
                    SoundEngine.PlaySound(SoundID.NPCDeath4, closestPlayer.Center);
                }
            }
            if (exist)
            {
                player.Heal(finalDamage);
                //SoundEngine.PlaySound(PaperMarioItems.healPM, player.Center);
            }
        }
        //earthquake
        private void CauseEarthquake(Player player, int damage)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, quakeCase))
                {
                    SoundEngine.PlaySound(PaperMarioItems.damagePM, npc.Center);
                    npc.SimpleStrikeNPC(damage, GetDirection(npc, player), Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic));
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, quakeCase))
                {
                    SoundEngine.PlaySound(PaperMarioItems.damagePM, vsplayer.Center);
                    vsplayer.Hurt(PlayerDeathReason.ByCustomReason(vsplayer.name + " " + EarthquakeDeath), damage, GetDirection(vsplayer, player), true);
                }
            }
        }
        //pow block
        public void PowBlock(Player player, int damage)
        {
            SetShakeTime(10);
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, powCase))
                {
                    SoundEngine.PlaySound(PaperMarioItems.damagePM, npc.Center);
                    npc.SimpleStrikeNPC(damage, GetDirection(npc, player), Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic));
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, powCase))
                {
                    SoundEngine.PlaySound(PaperMarioItems.damagePM, vsplayer.Center);
                    vsplayer.Hurt(PlayerDeathReason.ByCustomReason(vsplayer.name + " " + PowDeath), damage, GetDirection(vsplayer, player), true, false, -1, true, 0, 0, 0);
                }
            }
        }
        //ruin powder
        public void RuinPowderEffect(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, ruinCase))
                {
                    empty = false;
                    npc.AddBuff(BuffID.Confused, 7200);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, ruinCase))
                {
                    empty = false;
                    vsplayer.AddBuff(BuffID.Confused, 7200);
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.causeStatusPM, player.Center);
        }
        //mr softener
        public void SoftenEveryone(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, softCase))
                {
                    empty = false;
                    npc.AddBuff(PMBuffID.Soft, 7200);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, softCase))
                {
                    empty = false;
                    vsplayer.AddBuff(PMBuffID.Soft, 7200);
                }
            }
            if (!empty) SoundEngine.PlaySound(PaperMarioItems.causeStatusPM, player.Center);
        }
    }
}