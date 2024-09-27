using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using PaperMarioItems.Common.NPCs;
using PaperMarioItems.Content;
using Terraria.Enums;

namespace PaperMarioItems.Common.Players
{
    partial class PaperPlayer : ModPlayer
    {
        //targeting IDs
        private const int shootingCase = 0, dizzyCase = 1, frightCase = 2, thunderCase = 3, timeCase = 4, hpCase = 5, quakeCase = 6, powCase = 7, ruinCase = 8, softCase = 9, sleepyCase = 10;
        //targeting conditions
        private bool TargetConditionCheck(Player player, NPC npc, Player vsplayer, int condition)
        {
            if (Main.myPlayer != player.whoAmI || player == null) return false;
            if (npc != null &&
                !npc.friendly &&
                !NPCID.Sets.CountsAsCritter[npc.type] &&
                npc.type != NPCID.CultistTablet &&
                IsWithinScreenRange(player, npc))
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
                    case sleepyCase:
                    case frightCase:
                        if (!(npc.type == NPCID.CultistDevote || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.boss)) return true;
                        else return false;
                    case quakeCase:
                        if ((!npc.HasBuff(PMBuffID.Timestop) && npc.noGravity) || (npc.HasBuff(PMBuffID.Timestop) && npc.GetGlobalNPC<PaperNPC>().hadGravity)) return false;
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
                vsplayer.hostile && DifferentTeamCheck(player, vsplayer) &&
                IsWithinScreenRange(player, vsplayer) &&
                vsplayer != player)
            {
                switch (condition)
                {
                    //special
                    case quakeCase:
                        int blockX = (int)(vsplayer.position.X / 16), blockY = (int)(vsplayer.position.Y / 16);
                        for (int x = blockX - 1; x < blockX + 3; x++)
                        {
                            if (WorldGen.SolidTile2(x, blockY - 3) || WorldGen.SolidTile2(x, blockY + 3)) return true;
                        }
                        return false;
                    //default
                    default: return true;
                }
            }
            else return false;
        }
        private int GetDirection(Entity target, Player player)
        {
            if (target.position.X < player.position.X) return -1;
            return 1;
        }
        private bool DifferentTeamCheck(Player player, Player otherplayer)
        {
            if (player.team == (int)Team.None || otherplayer.team == (int)Team.None) return true;
            return player.team == otherplayer.team;
        }
        private bool IsWithinScreenRange(Player player, Entity target) => (target.Center - player.Center).Length() < (Main.screenWidth / 2);
        public void SetShakeTime(int time)
        {
            if (ScreenShakeSystem.screenShakeTime < time) ScreenShakeSystem.screenShakeTime = time;
        }
        //life mushroom effects
        private void LifeMushroomHeal(Player self, int num1, int num2)
        {
            if (self == null || !self.HasItem(PMItemID.LifeMushroom)) return;
            else
            {
                if (self.HasBuff(BuffID.PotionSickness)) self.ClearBuff(BuffID.PotionSickness);
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
                SoundEngine.PlaySound(PMSoundID.heal, self.Center);
                self.ConsumeItem(PMItemID.LifeMushroom, true, true);
            }
        }
        //dodge
        private void RepelDodge()
        {
            if (Player.whoAmI != Main.myPlayer) return;
            Player.NinjaDodge();
            SoundEngine.PlaySound(PMSoundID.lucky, Player.Center);
            CombatText.NewText(Player.getRect(), LuckyTextColor, LuckyEvade);
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
            if (!empty) SoundEngine.PlaySound(PMSoundID.star, player.Center);
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
                Projectile.NewProjectile(player.GetSource_FromThis(), position, velocity * angle, PMProjID.Fireball, fireFlowerDamage, 3f);
                SoundEngine.PlaySound(PMSoundID.fireFlower, position);
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
            if (!empty) SoundEngine.PlaySound(PMSoundID.causeStatus, player.Center);
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
                    SoundEngine.PlaySound(PMSoundID.thunder, closestNPC.Center);
                }
                if (closestPlayer != null)
                {
                    StrikeLightning(player, null, closestPlayer);
                    SoundEngine.PlaySound(PMSoundID.thunder, closestPlayer.Center);
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
            if (!empty) SoundEngine.PlaySound(PMSoundID.thunder, player.Center);
        }
        public void StrikeLightning(Player player, NPC npc, Player vsplayer)
        {
            Dust.NewDustDirect(npc.Center, 2, 2, PMDustID.LightningDust);
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
            bool inflictedOnBoss = Main.rand.NextBool(7);
            bool moonLordDetected = false;
            if (Main.myPlayer == player.whoAmI)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (TargetConditionCheck(player, npc, null, timeCase))
                    {
                        Color color = new(220 + Main.rand.Next(35), 240 + Main.rand.Next(35), 240 + Main.rand.Next(35));
                        Vector2 newPos = new(npc.Center.X - (36 / 2) + 4, npc.Center.Y - (40 / 2));
                        if (!npc.boss && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                        {
                            empty = false;
                            if (!npc.HasBuff(PMBuffID.Timestop)) Dust.NewDustPerfect(newPos, PMDustID.StopwatchDust, null, 0, color);
                            npc.AddBuff(PMBuffID.Timestop, 10800);
                        }
                        else
                        {
                            if (npc.type >= NPCID.MoonLordHead && npc.type <= NPCID.MoonLordCore)
                            {
                                if (!moonLordDetected)
                                {
                                    Main.NewText(MoonLordStopwatch, Color.Cyan);
                                    moonLordDetected = true;
                                }
                            }
                            else if (inflictedOnBoss)
                            {
                                empty = false;
                                if (!npc.HasBuff(PMBuffID.Timestop)) Dust.NewDustPerfect(newPos, PMDustID.StopwatchDust, null, 0, color);
                                npc.AddBuff(PMBuffID.Timestop, 3600);
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
                            if (!vsplayer.HasBuff(PMBuffID.Timestop)) Dust.NewDustPerfect(newPos, PMDustID.StopwatchDust, null, 0, color);
                            vsplayer.AddBuff(PMBuffID.Timestop, 10800, false);
                        }
                    }
                }
            }
            if (!empty) SoundEngine.PlaySound(PMSoundID.stopwatch);
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
                    Dust.NewDustPerfect(closestNPC.Center, PMDustID.HPDrainDust);
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
                    Dust.NewDustPerfect(closestPlayer.Center, PMDustID.HPDrainDust);
                    SoundEngine.PlaySound(SoundID.NPCDeath4, closestPlayer.Center);
                }
            }
            if (exist)
            {
                player.Heal(finalDamage);
                //SoundEngine.PlaySound(PMSoundID.heal, player.Center);
            }
        }
        //earthquake
        private void CauseEarthquake(Player player, int damage)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, quakeCase))
                {
                    SoundEngine.PlaySound(PMSoundID.damage, npc.Center);
                    npc.SimpleStrikeNPC(damage, GetDirection(npc, player), Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic));
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, quakeCase))
                {
                    SoundEngine.PlaySound(PMSoundID.damage, vsplayer.Center);
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
                    SoundEngine.PlaySound(PMSoundID.damage, npc.Center);
                    npc.SimpleStrikeNPC(damage, GetDirection(npc, player), Main.rand.Next(100) <= player.GetTotalCritChance(DamageClass.Generic));
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, powCase))
                {
                    SoundEngine.PlaySound(PMSoundID.damage, vsplayer.Center);
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
                    vsplayer.AddBuff(BuffID.Confused, 7200, false);
                }
            }
            if (!empty) SoundEngine.PlaySound(PMSoundID.causeStatus, player.Center);
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
                    vsplayer.AddBuff(PMBuffID.Soft, 7200, false);
                }
            }
        }
        //sleepy sheep
        private void EveryoneSleepNow(Player player)
        {
            bool empty = true;
            foreach (var npc in Main.ActiveNPCs)
            {
                if (TargetConditionCheck(player, npc, null, sleepyCase))
                {
                    empty = false;
                    npc.AddBuff(PMBuffID.Sleep, 1200);
                }
            }
            foreach (var vsplayer in Main.ActivePlayers)
            {
                if (TargetConditionCheck(player, null, vsplayer, sleepyCase))
                {
                    empty = false;
                    vsplayer.AddBuff(PMBuffID.Sleep, 600, false);
                }
            }
            if (!empty) SoundEngine.PlaySound(PMSoundID.causeStatus, player.Center);
        }
    }
}