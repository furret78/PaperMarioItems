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

namespace PaperMarioItems.Common.Players
{
    public class PaperPlayer : ModPlayer
    {
        //setup
        public bool dodgyEffect;
        public bool hugeEffect;
        public bool softEffect;
        public bool electrifiedEffect;
        public bool lifeShroomRevive;
        public bool inflictDizzyActive;
        public int screenSpinTimer = 0;
        public int shootingStar = 0;
        public bool shootingStarActive;
        private int shootingStarMaxDelay = 0; //for internal use
        private int shootingStarTimer = 0; //for internal use
        public static readonly Color LuckyTextColor = new Color(255, 255, 0, 255);
        public const int postReviveProtect = 1;
        public const int postReviveRegen = 2;
        int waitTimeElectric = 0;
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
                    int num25 = self.buffType[l];
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
        //shooting star
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
                    
                    screenSpinTimer--;
                }
            }
        }
        public void ShootingStarAttack(Player player)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly)
                {
                    Vector2 playerPosition = new Vector2(player.Center.X + Main.rand.NextFloatDirection()*Main.rand.NextFloat(Main.screenWidth / 2), player.Center.Y - ((Main.screenHeight*2)/3));
                    if (!player.ZoneSkyHeight && !player.ZoneOverworldHeight) playerPosition.X = player.Center.X - player.direction*(Main.screenWidth / 2);
                    Vector2 defaultVelocity = new Vector2(Main.rand.NextFloat(17) + 11f);
                    Vector2 homingAngle = (npc.Center - playerPosition).SafeNormalize(Vector2.UnitX);
                    int finalDamage = 300;
                    if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail) finalDamage = 15;
                    if (npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail) finalDamage = 5;
                    SoundEngine.PlaySound(PaperMarioItems.starPM);
                    Projectile.NewProjectile(player.GetSource_FromThis(), playerPosition, defaultVelocity * homingAngle, ProjectileID.Starfury, finalDamage, 10f, Main.myPlayer, 0, npc.Center.Y);
                }
            }
        }
        //inflict dizzy on enemies
        public void InflictDizzy(Player player)
        {
            inflictDizzyActive = true;
            screenSpinTimer = 90;
        }
        public void InflictDizzyOnEnemies(Player player)
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (Main.myPlayer == player.whoAmI && !npc.friendly && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type] && !npc.boss)
                {
                    npc.AddBuff(ModContent.BuffType<DizzyDebuff>(), 10800);
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
                if (waitTimeElectric == 0)
                {
                    Vector2 dustRotation = Main.rand.NextVector2Unit();
                    Vector2 dustPosition = Player.Center + dustRotation * Player.height;
                    Dust.NewDust(dustPosition, 0, 0, ModContent.DustType<ElectricDust>());
                    waitTimeElectric++;
                }
                else
                {
                    if (waitTimeElectric == 5) waitTimeElectric = 0;
                    else waitTimeElectric++;
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