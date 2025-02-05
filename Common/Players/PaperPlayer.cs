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
using PaperMarioItems.Common.UI;
using PaperMarioItems.Content;
using PaperMarioItems.Common.Configs;
using Terraria.ModLoader.IO;
using PaperMarioItems.Content.Items.ConsumablesSPM;
using System.CodeDom;

namespace PaperMarioItems.Common.Players
{
    // Custom functions are in the PaperPlayerCustom.cs file
    partial class PaperPlayer : ModPlayer
    {
        //setup
        public bool chargedEffect, dodgyEffect, hugeEffect, softEffect, electrifiedEffect, lifeShroomRevive, thunderOnce, thunderAll, earthquakeEffect, dizzyEffect, causeSoften, causeEarthquake;
        public bool shootingStarActive, inflictDizzyActive, thunderEffectActive, frightMaskActive, stopwatchActive, fireFlowerActive, ruinPowderActive, sleepySheepActive;
        public bool ultraStoneAccessory;
        public int chargedStack = 0, shootingStar = 0, screenSpinTimer = 0, frightMaskCooldown = 0, stopwatchCooldown = 0, fireFlower = 0, ruinPowderCooldown = 0, sleepySheepCooldown = 0, consumedHPPlus = 0;
        private int chargeCap, wte, ssmd, sst, wtt, bgFlashTime, swt, fft, eqt, dyt, sleept, alertTimer = -10, consumedPowerPlus;
        private bool bgFlash;
        private static readonly Color LuckyTextColor = new(255, 255, 0, 255);
        public const int postReviveProtect = 1, postReviveRegen = 2, fireFlowerDamage = 25, earthquakeDamage = 75;
        public readonly int preHardChargeCap = 10, skellyChargeCap = 12, hardChargeCap = 15, mechChargeCap = 18, mechAllChargeCap = 20, planteraChargeCap = 25, cultistChargeCap = 40;
        //localized text
        const string LocalTextPath = $"Mods.PaperMarioItems.Common.Players.";
        public readonly string LuckyEvade = Language.GetTextValue(LocalTextPath + $"LuckyEvade"),
            MoonLordStopwatch = Language.GetTextValue(LocalTextPath + $"MoonLordStopwatch"),
            LightningDeath = Language.GetTextValue(LocalTextPath + $"LightningDeath"),
            HPDrainDeath = Language.GetTextValue(LocalTextPath + $"HPDrainDeath"),
            EarthquakeDeath = Language.GetTextValue(LocalTextPath + $"EarthquakeDeath"),
            PowDeath = Language.GetTextValue(LocalTextPath + $"PowDeath"),
            PowerPlusMax = Language.GetTextValue(LocalTextPath + $"PowerPlusMax");
        //reset
        public override void ResetEffects()
        {
            dodgyEffect = false;
            hugeEffect = false;
            softEffect = false;
            electrifiedEffect = false;
            lifeShroomRevive = false;
            dizzyEffect = false;
            if (electrifiedEffect == false) Player.buffImmune[BuffID.Electrified] = false;
            chargedEffect = false;
            ultraStoneAccessory = false;
        }
        //on enter world
        public override void OnEnterWorld()
        {
            if (Main.dedServ)
            {
                return;
            }

            ModContent.GetInstance<PaperCookingSystem>().HideUI();
            ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = Vector2.Zero;
        }
        //detours
        public override void Load()
        {
            On_Player.BuyItem += On_Player_BuyItem;
            On_Player.KillMe += On_Player_KillMe;
            On_Player.AddBuff += On_Player_AddBuff;
        }
        //space food detour
        private static void On_Player_AddBuff(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
        {
            int buffType = type;
            int buffTime = timeToAdd;
            if (self.HasBuff(PMBuffID.Allergic) && !NotAllergicToBuffs.notAllergicToBuffs.Contains(type))
            {
                if (self.GetModPlayer<PaperPlayer>().chargedStack <= 0)
                {
                    buffType = 0;
                    buffTime = 0;
                    //return;
                }
            }
            orig(self, buffType, buffTime, quiet, foodHack);
        }

        //life shroom detour
        private static void On_Player_KillMe(On_Player.orig_KillMe orig, Player self, PlayerDeathReason damageSource, double dmg, int hitDirection, bool pvp)
        {
            if (self.creativeGodMode || self.dead || !self.HasItem(PMItemID.LifeMushroom)) orig(self, damageSource, dmg, hitDirection, pvp);
            else
            {
                self.GetModPlayer<PaperPlayer>().lifeShroomRevive = true;
                self.GetModPlayer<PaperPlayer>().LifeMushroomHeal(self, postReviveProtect * 60 * 60, postReviveRegen * 60 * 60);
                return;
            }
        }
        //npc (nurse) detour
        private static bool On_Player_BuyItem(On_Player.orig_BuyItem orig, Player self, long price, int customCurrency)
        {
            if (self.HasItemInInventoryOrOpenVoidBag(PMItemID.InnCoupon) && Main.npc[self.talkNPC].type == NPCID.Nurse)
            {
                self.ConsumeItem(PMItemID.InnCoupon, true, true);
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
                    SoundEngine.PlaySound(PMSoundID.dizzy, Player.Center);
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
                        Dust.NewDustPerfect(new(Player.Center.X, Player.Center.Y - 10), PMDustID.BowserScare, null, Player.direction, default, 0.1f);
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
                        if (fft > 3)
                        {
                            FireFlowerAttack(Player);
                            fft = 0;
                            if (fireFlower < 2) fireFlowerActive = false;
                            fireFlower--;
                        }
                        fft++;
                    }
                }
                //earthquake
                if (causeEarthquake)
                {
                    if (eqt == 34)
                    {
                        SetShakeTime(90);
                    }
                    if (eqt >= 124)
                    {
                        CauseEarthquake(Player, earthquakeDamage);
                        eqt = 0;
                        causeEarthquake = false;
                    }
                    else eqt++;
                }
                //ruin powder
                if (ruinPowderActive && ruinPowderCooldown > 0)
                {
                    if (ruinPowderCooldown < 2)
                    {
                        RuinPowderEffect(Player);
                        ruinPowderActive = false;
                    }
                    ruinPowderCooldown--;
                }
                //mr softener
                if (causeSoften)
                {
                    SoftenEveryone(Player);
                    causeSoften = false;
                }
                //stopwatch
                if (sleepySheepActive)
                {
                    sleepySheepCooldown--;
                    if (sleepySheepCooldown == 1) EveryoneSleepNow(Player);
                    if (sleepySheepCooldown <= 0)
                    {
                        sleepySheepActive = false;
                        sleepySheepCooldown = 0;
                    }
                }
                //charged
                if (chargedStack > 0)
                {
                    chargedEffect = true;
                    if (!NPC.downedMoonlord)
                    {
                        if (!Main.hardMode)
                        {
                            chargeCap = preHardChargeCap;
                            if (NPC.downedBoss3) chargeCap = skellyChargeCap;
                        }
                        else
                        {
                            chargeCap = hardChargeCap;
                            if (NPC.downedMechBossAny) chargeCap = mechChargeCap;
                            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) chargeCap = mechAllChargeCap;
                            if (NPC.downedPlantBoss) chargeCap = planteraChargeCap;
                            if (NPC.downedAncientCultist) chargeCap = cultistChargeCap;
                        }
                        if (chargedStack > chargeCap) chargedStack = chargeCap;
                    }
                }
                if (chargedEffect)
                {
                    Player.AddBuff(PMBuffID.Charged, 2);
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (ultraStoneAccessory)
            {
                if (!Player.HasBuff(PMBuffID.PartnerBuff))
                {
                    if (Player.HasItem(PMItemID.ShineSprite)) Player.AddBuff(PMBuffID.PartnerBuff, 2);
                }
                else if (!Player.HasItem(PMItemID.ShineSprite)) Player.ClearBuff(PMBuffID.PartnerBuff);
            }

            if (consumedPowerPlus > 0)
            {
                Player.GetDamage(DamageClass.Generic) += consumedPowerPlus * (5 / 100f);
            }
        }

        public override void PostUpdate()
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                if (ModContent.GetInstance<PaperClientConfigs>().HealthAlert)
                {
                    if ((!PaperMarioItems.isLoadedBadgeMod && Player.statLife < 50) ||
                        (PaperMarioItems.isLoadedBadgeMod && Player.statLife <= Player.statLifeMax2 / 4))
                    {
                        if (alertTimer % 60 == 0)
                        {
                            if ((!PaperMarioItems.isLoadedBadgeMod && Player.statLife > 10) ||
                                (PaperMarioItems.isLoadedBadgeMod && Player.statLife > Player.statLifeMax2 / 10))
                                SoundEngine.PlaySound(PMSoundID.danger, Player.Center);
                            else SoundEngine.PlaySound(PMSoundID.peril, Player.Center);
                            alertTimer = 1;
                        }
                        else alertTimer++;
                    }
                    else if (alertTimer > -10) alertTimer = -10;
                }
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            //player effects when visible
            if (drawInfo.shadow == 0f)
            {
                if (softEffect) r *= 0.20f;
                if (electrifiedEffect)
                {
                    Lighting.AddLight(Player.Center, Color.WhiteSmoke.ToVector3() * 0.9f);
                    if (wte == 0)
                    {
                        Vector2 dustRotation = Main.rand.NextVector2Unit();
                        Vector2 dustPosition = Player.Center + dustRotation * Player.height;
                        Dust.NewDust(dustPosition, 0, 0, PMDustID.ElectricDust);
                        wte++;
                    }
                    else
                    {
                        if (wte >= 5) wte = 0;
                        else wte++;
                    }
                }
                if (dizzyEffect)
                {
                    if (dyt == 1)
                    {
                        Dust.NewDust(Player.Center, 0, 0, PMDustID.DizzyDust, Main.rand.NextFloat(-4, 5), Main.rand.NextFloat(-4, 0));
                        dyt++;
                    }
                    else
                    {
                        if (dyt > 10) dyt = 0;
                        dyt++;
                    }
                }
                if (Player.HasBuff(PMBuffID.Timestop))
                {
                    if (swt >= 60)
                    {
                        Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                        Vector2 newPos = new(Player.Center.X - (36 / 2) + 4, Player.Center.Y - (40 / 2));
                        Dust.NewDustPerfect(newPos, PMDustID.StopwatchDust, null, 0, color);
                        swt = 0;
                    }
                    swt++;
                }
                else if (swt != 0) swt = 0;
                if (Player.HasBuff(PMBuffID.Sleep))
                {
                    if (sleept >= 30)
                    {
                        Rectangle currentLocation = Player.getRect();
                        CombatText.NewText(currentLocation, Color.White, "Z");
                        sleept = 0;
                    }
                    sleept++;
                }
            }
            //bg
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

        //deplete charged count
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            chargedStack = 0;
        }

        //permanent stat boosts

        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;
            health.Base = consumedHPPlus * HPPlus.HPPlusValue;
            mana = StatModifier.Default;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)PaperMarioItems.MessageType.StatIncreaseSync);
            packet.Write((byte)Player.whoAmI);
            packet.Write((byte)consumedHPPlus);
            packet.Write((byte)consumedPowerPlus);
            packet.Send(toWho, fromWho);
        }

        public void ReceivePlayerSync(BinaryReader reader)
        {
            consumedHPPlus = reader.ReadByte();
            consumedPowerPlus = reader.ReadByte();
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            var clone = (PaperPlayer)targetCopy;
            clone.consumedHPPlus = consumedHPPlus;
            clone.consumedPowerPlus = consumedPowerPlus;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            var clone = (PaperPlayer)clientPlayer;
            if (consumedHPPlus != clone.consumedHPPlus || consumedPowerPlus != clone.consumedPowerPlus)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["consumedHPPlus"] = consumedHPPlus;
            tag["consumedPowerPlus"] = consumedPowerPlus;
        }

        public override void LoadData(TagCompound tag)
        {
            consumedHPPlus = tag.GetInt("consumedHPPlus");
            consumedPowerPlus = tag.GetInt("consumedPowerPlus");
        }
    }
}