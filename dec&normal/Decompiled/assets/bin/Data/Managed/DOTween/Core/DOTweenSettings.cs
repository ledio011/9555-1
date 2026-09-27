using System;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x02000014 RID: 20
	public class DOTweenSettings : ScriptableObject
	{
		// Token: 0x0400002F RID: 47
		public const string AssetName = "DOTweenSettings";

		// Token: 0x04000030 RID: 48
		public bool useSafeMode = true;

		// Token: 0x04000031 RID: 49
		public float timeScale = 1f;

		// Token: 0x04000032 RID: 50
		public bool useSmoothDeltaTime;

		// Token: 0x04000033 RID: 51
		public float maxSmoothUnscaledTime = 0.15f;

		// Token: 0x04000034 RID: 52
		public bool showUnityEditorReport;

		// Token: 0x04000035 RID: 53
		public LogBehaviour logBehaviour = LogBehaviour.ErrorsOnly;

		// Token: 0x04000036 RID: 54
		public bool drawGizmos = true;

		// Token: 0x04000037 RID: 55
		public bool defaultRecyclable;

		// Token: 0x04000038 RID: 56
		public AutoPlay defaultAutoPlay = AutoPlay.All;

		// Token: 0x04000039 RID: 57
		public UpdateType defaultUpdateType;

		// Token: 0x0400003A RID: 58
		public bool defaultTimeScaleIndependent;

		// Token: 0x0400003B RID: 59
		public Ease defaultEaseType = Ease.OutQuad;

		// Token: 0x0400003C RID: 60
		public float defaultEaseOvershootOrAmplitude = 1.70158f;

		// Token: 0x0400003D RID: 61
		public float defaultEasePeriod;

		// Token: 0x0400003E RID: 62
		public bool defaultAutoKill = true;

		// Token: 0x0400003F RID: 63
		public LoopType defaultLoopType;

		// Token: 0x04000040 RID: 64
		public DOTweenSettings.SettingsLocation storeSettingsLocation;

		// Token: 0x02000015 RID: 21
		public enum SettingsLocation
		{
			// Token: 0x04000042 RID: 66
			AssetsDirectory,
			// Token: 0x04000043 RID: 67
			DOTweenDirectory,
			// Token: 0x04000044 RID: 68
			DemigiantDirectory
		}
	}
}
