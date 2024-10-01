using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace PaperMarioItems.Common.Configs
{
	public class PaperClientConfigs : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;
		[Header("AudiovisualTweaks")]
		[DefaultValue(false)]
		public bool HealthAlert;
	}
}
