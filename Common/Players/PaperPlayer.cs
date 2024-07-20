using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent;
using PaperMarioItems.Content.Items.Consumables;
using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;

namespace PaperMarioItems.Common.Players
{
    // Custom functions are in the PaperPlayerCustom.cs file
    partial class PaperPlayer : ModPlayer
    {
        //setup
        public bool dodgyEffect, hugeEffect, softEffect, electrifiedEffect, lifeShroomRevive, thunderOnce, thunderAll, earthquakeEffect;
        public bool shootingStarActive, inflictDizzyActive, thunderEffectActive, frightMaskActive, stopwatchActive, fireFlowerActive, causeEarthquake, ruinPowderActive;
        public int shootingStar = 0, screenSpinTimer = 0, frightMaskCooldown = 0, stopwatchCooldown = 0, fireFlower = 0, ruinPowderCooldown = 0;
        private int wte = 0, ssmd, sst = 0, wtt = 0, bgFlashTime = 0, stopwatchTimer = 0, fireFlowerTimer = 0, earthquakeTimer = 0;
        private bool bgFlash;
        private static readonly Color LuckyTextColor = new(255, 255, 0, 255);
        public const int postReviveProtect = 1, postReviveRegen = 2, fireFlowerDamage = 25, earthquakeDamage = 75;
        //localized text
        public readonly string LuckyEvade = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.LuckyEvade"),
            MoonLordStopwatch = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.MoonLordStopwatch"),
            LightningDeath = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.LightningDeath"),
            HPDrainDeath = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.HPDrainDeath"),
            EarthquakeDeath = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.EarthquakeDeath"),
            PowDeath = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.PowDeath");
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
            if (self.creativeGodMode || self.dead || !self.HasItem(ModContent.ItemType<LifeMushroom>())) orig(self, damageSource, dmg, hitDirection, pvp);
            else
            {
                lifeShroomRevive = true;
                LifeMushroomHeal(self, postReviveProtect * 60 * 60, postReviveRegen * 60 * 60);
                return;
            }
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
                //timestop dust
                if (Player.HasBuff<TimestopDebuff>())
                {
                    if (stopwatchTimer >= 60)
                    {
                        Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                        Vector2 newPos = new(Player.Center.X, Player.Center.Y);
                        Dust.NewDustPerfect(newPos, ModContent.DustType<StopwatchDust>(), null, 0, color);
                        stopwatchTimer = 0;
                    }
                    stopwatchTimer++;
                }
                else if (stopwatchTimer != 0) stopwatchTimer = 0;
                //shooting star timer
                if (shootingStar > 0)
                {
                    ssmd = (int)(shootingStar * 3.5f + 1);
                    if (!shootingStarActive) shootingStarActive = true;
                    else
                    {
                        if (sst == ssmd)
                        {
                            ShootingStarAttack(Player);
                            shootingStar--;
                            sst = -1;
                        }
                        sst++;
                    }
                }
                else
                {
                    ssmd = 0;
                    sst = 0;
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
                    //insert code for screen spin
                    SoundEngine.PlaySound(PaperMarioItems.dizzyPM, Player.Center);
                    screenSpinTimer--;
                }
                //thunder bolt timer
                if (thunderOnce || thunderAll)
                {
                    if (wtt <= 0) wtt = 60;
                    thunderEffectActive = true;
                }
                if (thunderEffectActive)
                {
                    if (wtt == 15) BackgroundFlash();
                    if (wtt == 1)
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
                    }
                    if (wtt <= 0)
                    {
                        thunderAll = false;
                        thunderOnce = false;
                        thunderEffectActive = false;
                        wtt = 0;
                    }
                    else wtt--;
                }
                //fright mask timer
                if (frightMaskActive && frightMaskCooldown >= -20)
                {
                    frightMaskCooldown--;
                    if (frightMaskCooldown == 17)
                    {
                        Vector2 spawnPos = new(Player.Center.X, Player.Center.Y - 10);
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
                //stopwatch
                if (stopwatchActive)
                {
                    stopwatchCooldown--;
                    if (stopwatchCooldown == 1) InflictTimestop(Player);
                    if (stopwatchCooldown <= 0)
                    {
                        stopwatchActive = false;
                        stopwatchCooldown = 0;
                    }
                }
                //fire flower shoot
                if (fireFlower > 0)
                {
                    if (!fireFlowerActive) fireFlowerActive = true;
                    else
                    {
                        if (fireFlowerTimer > 3)
                        {
                            FireFlowerAttack(Player);
                            fireFlowerTimer = 0;
                            if (fireFlower < 2) fireFlowerActive = false;
                            fireFlower--;
                        }
                        fireFlowerTimer++;
                    }
                }
                //earthquake
                if (causeEarthquake)
                {
                    if (earthquakeTimer == 34)
                    {
                        SetShakeTime(90);
                    }
                    if (earthquakeTimer >= 124)
                    {
                        CauseEarthquake(Player, earthquakeDamage);
                        earthquakeTimer = 0;
                        causeEarthquake = false;
                    }
                    else earthquakeTimer++;
                }
                //ruin powder
                if (ruinPowderActive && ruinPowderCooldown > 0)
                {
                    if (ruinPowderCooldown < 2)
                    {

                    }
                    ruinPowderCooldown--;
                }
            }
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
                if (wte == 0)
                {
                    Vector2 dustRotation = Main.rand.NextVector2Unit();
                    Vector2 dustPosition = Player.Center + dustRotation * Player.height;
                    Dust.NewDust(dustPosition, 0, 0, ModContent.DustType<ElectricDust>());
                    wte++;
                }
                else
                {
                    if (wte >= 5) wte = 0;
                    else wte++;
                }
            }
            if (bgFlash)
            {
                Texture2D value = TextureAssets.MagicPixel.Value;
                Color color = Color.LightGray;
                if (!Main.dayTime) color = Color.Gray;
                Main.spriteBatch.Draw(value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
                if (!Main.gamePaused)
                {
                    bgFlashTime--;
                    if (bgFlashTime <= 0) bgFlash = false;
                }
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
            SoundEngine.PlaySound(PaperMarioItems.luckyPM, Player.Center);
            Rectangle currentLocation = Player.getRect();
            CombatText.NewText(currentLocation, LuckyTextColor, LuckyEvade);
        }
        public static void HandleDodgeMessage(BinaryReader reader, int whoAmI)
        {
            int player = reader.ReadByte();
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                player = whoAmI;
            }

            Main.player[player].GetModPlayer<PaperPlayer>().RepelDodge();

            if (Main.netMode != NetmodeID.SinglePlayer)
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