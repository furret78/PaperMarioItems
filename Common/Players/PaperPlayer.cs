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
    public class PaperPlayer : ModPlayer
    {
        //setup
        public bool dodgyEffect, hugeEffect, softEffect, electrifiedEffect, lifeShroomRevive;
        public bool inflictDizzyActive, thunderEffectActive, frightMaskActive, shootingStarActive;
        public bool thunderOnce, thunderAll;
        public int screenSpinTimer = 0;
        public int shootingStar = 0;
        public int frightMaskCooldown = 0;
        private int shootingStarMaxDelay = 0;
        private int shootingStarTimer = 0;
        private int waitTimeElectric = 0;
        private int waitTimeThunder = 0;
        private int bgFlashTime = 0;
        private bool bgFlash;
        private static SoundStyle loudThunder = SoundID.Thunder with { Variants = new System.ReadOnlySpan<int>(new int[] { 0 }) };
        public static readonly Color LuckyTextColor = new Color(255, 255, 0, 255);
        public const int postReviveProtect = 1;
        public const int postReviveRegen = 2;
        //localized text
        public readonly string LuckyEvade = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.LuckyEvade");
        //reset
        public override void ResetEffects()
        {
            dodgyEffect = false;
            hugeEffect = false;
            softEffect = false;
            electrifiedEffect = false;
            lifeShroomRevive = false;
            if (electrifiedEffect == false) Player.buffImmune[BuffID.Electrified] = false;
        }
        //detours
        public override void Load()
        {
            On_Player.BuyItem += On_Player_BuyItem;
            On_Player.KillMe += On_Player_KillMe;
        }
        //life shroom detour
        private void On_Player_KillMe(On_Player.orig_KillMe orig, Player self, PlayerDeathReason damageSource, double dmg, int hitDirection, bool pvp)
        {
            int num1 = postReviveProtect * 60 * 60;
            int num2 = postReviveRegen * 60 * 60;
            if (!self.creativeGodMode && !self.dead && self.HasItem(ModContent.ItemType<LifeMushroom>()))
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (self.buffTime[l] > 0)
                    {
                        self.DelBuff(l);
                        l = -1;
                    }
                }
                lifeShroomRevive = true;
                self.statLife = 1;
                self.AddBuff(ModContent.BuffType<RevivedBuff>(), num1);
                self.SetImmuneTimeForAllTypes(num1);
                self.AddBuff(BuffID.Regeneration, num2);
                SoundEngine.PlaySound(PaperMarioItems.healPM);
                self.ConsumeItem(ModContent.ItemType<LifeMushroom>(), true, true);
                return;
            }
            else orig(self, damageSource, dmg, hitDirection, pvp);
        }
        //npc (nurse) detour
        private static bool On_Player_BuyItem(On_Player.orig_BuyItem orig, Player self, long price, int customCurrency)
        {
            if (self.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<InnCoupon>()) && Main.npc[self.talkNPC].type == NPCID.Nurse)
            {
                self.ConsumeItem(ModContent.ItemType<InnCoupon>(), true, true);
                return true;
            }
            else return orig(self, price, customCurrency);
        }
        public override void PreUpdate()
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                //shooting star timer
                if (shootingStar > 0)
                {
                    shootingStarMaxDelay = (int)(shootingStar * 3.5f + 1);
                    if (!shootingStarActive)
                    {
                        shootingStarActive = true;
                    }
                    else
                    {
                        if (shootingStarTimer == shootingStarMaxDelay)
                        {
                            ShootingStarAttack(Player);
                            shootingStar--;
                            shootingStarTimer = -1;
                        }
                        shootingStarTimer++;
                    }
                }
                else
                {
                    shootingStarMaxDelay = 0;
                    shootingStarTimer = 0;
                    shootingStarActive = false;
                }
                //dizzy timer
                if (inflictDizzyActive)
                {
                    if (screenSpinTimer <= 0)
                    {
                        InflictDizzyOnEnemies(Player);
                        inflictDizzyActive = false;
                        screenSpinTimer = 0;
                    }
                    //code for screen spin
                    screenSpinTimer--;
                }
                //thunder bolt timer
                if (thunderOnce || thunderAll)
                {
                    if (waitTimeThunder <= 0) waitTimeThunder = 60;
                    thunderEffectActive = true;
                }
                if (thunderEffectActive)
                {
                    if (waitTimeThunder == 15)
                    {
                        BackgroundFlash();
                        waitTimeThunder--;
                    }
                    if (waitTimeThunder == 1)
                    {
                        if (thunderOnce && !thunderAll)
                        {
                            StrikeOneEnemy(Player);
                            thunderOnce = false;
                        }
                        if (thunderAll)
                        {
                            StrikeAllEnemies(Player);
                            thunderAll = false;
                        }
                        waitTimeThunder--;
                    }
                    else if (waitTimeThunder <= 0)
                    {
                        thunderAll = false;
                        thunderOnce = false;
                        thunderEffectActive = false;
                        waitTimeThunder = 0;
                    }
                    else waitTimeThunder--;
                }
                //fright mask timer
                if (frightMaskActive && frightMaskCooldown >= -20)
                {
                    frightMaskCooldown--;
                    if (frightMaskCooldown == 17)
                    {
                        Vector2 spawnPos = new Vector2(Player.Center.X, Player.Center.Y - 10);
                        Dust.NewDustPerfect(spawnPos, ModContent.DustType<BowserScare>(), null, Player.direction, default, 0.1f);
                        InflictFrightOnAll(Player);
                        SoundEngine.PlaySound(SoundID.ForceRoar, Player.Center);
                    }
                    if (frightMaskCooldown <= -20)
                    {
                        frightMaskActive = false;
                        frightMaskCooldown = 0;
                    }
                }
            }
        }
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
        //soft debuff
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (softEffect)
            {
                g *= 0.5f;
                r *= 0.75f;
            }
            if (electrifiedEffect)
            {
                if (waitTimeElectric == 0)
                {
                    Vector2 dustRotation = Main.rand.NextVector2Unit();
                    Vector2 dustPosition = Player.Center + dustRotation * Player.height;
                    Dust.NewDust(dustPosition, 0, 0, ModContent.DustType<ElectricDust>());
                    waitTimeElectric++;
                }
                else
                {
                    if (waitTimeElectric >= 5) waitTimeElectric = 0;
                    else waitTimeElectric++;
                }
            }
            if (bgFlash && Main.gamePaused == false)
            {
                if (bgFlashTime <= 0) bgFlash = false;
                Texture2D value = TextureAssets.MagicPixel.Value;
                Color color = Color.White;
                Main.spriteBatch.Draw(value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
                bgFlashTime--;
            }
        }
        //dodgy effects
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (dodgyEffect && Main.rand.NextBool(2) == false)
            {
                RepelDodge();
                return true;
            }
            return false;
        }
        public void RepelDodge()
        {
            if (Player.whoAmI != Main.myPlayer) return;
            Player.NinjaDodge();
            SoundEngine.PlaySound(PaperMarioItems.luckyPM);
            Rectangle currentLocation = Player.getRect();
            CombatText.NewText(currentLocation, LuckyTextColor, LuckyEvade);
        }
        public static void HandleDodgeMessage(BinaryReader reader, int whoAmI)
        {
            int player = reader.ReadByte();
            if (Main.netMode == NetmodeID.Server)
            {
                player = whoAmI;
            }

            Main.player[player].GetModPlayer<PaperPlayer>().RepelDodge();

            if (Main.netMode == NetmodeID.Server)
            {
                SendDodgeMessage(player);
            }
        }
        public static void SendDodgeMessage(int whoAmI)
        {
            ModPacket packet = ModContent.GetInstance<PaperMarioItems>().GetPacket();
            packet.Write((byte)PaperMarioItems.MessageType.RepelDodgeMessage);
            packet.Write((byte)whoAmI);
            packet.Send(ignoreClient: whoAmI);
        }
        //on hit electrified
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (electrifiedEffect)
            {
                ref StatModifier knockback = ref modifiers.Knockback;
                knockback *= 0f;
                ref StatModifier finalDamage = ref modifiers.FinalDamage;
                finalDamage *= 0f;
            }
        }
    }
}