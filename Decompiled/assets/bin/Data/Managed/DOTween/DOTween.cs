using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000021 RID: 33
	public class DOTween
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000046B8 File Offset: 0x000028B8
		static DOTween()
		{
			DOTween.isUnityEditor = Application.isEditor;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004748 File Offset: 0x00002948
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00004750 File Offset: 0x00002950
		public static LogBehaviour logBehaviour
		{
			get
			{
				return DOTween._logBehaviour;
			}
			set
			{
				DOTween._logBehaviour = value;
				Debugger.SetLogPriority(DOTween._logBehaviour);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004764 File Offset: 0x00002964
		private static void AutoInit()
		{
			DOTween.Init(Resources.Load("DOTweenSettings") as DOTweenSettings, null, null, null);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000047A4 File Offset: 0x000029A4
		private static IDOTweenInit Init(DOTweenSettings settings, bool? recycleAllByDefault, bool? useSafeMode, LogBehaviour? logBehaviour)
		{
			DOTween.initialized = true;
			if (recycleAllByDefault != null)
			{
				DOTween.defaultRecyclable = recycleAllByDefault.Value;
			}
			if (useSafeMode != null)
			{
				DOTween.useSafeMode = useSafeMode.Value;
			}
			if (logBehaviour != null)
			{
				DOTween.logBehaviour = logBehaviour.Value;
			}
			DOTweenComponent.Create();
			if (settings != null)
			{
				if (useSafeMode == null)
				{
					DOTween.useSafeMode = settings.useSafeMode;
				}
				if (logBehaviour == null)
				{
					DOTween.logBehaviour = settings.logBehaviour;
				}
				if (recycleAllByDefault == null)
				{
					DOTween.defaultRecyclable = settings.defaultRecyclable;
				}
				DOTween.timeScale = settings.timeScale;
				DOTween.useSmoothDeltaTime = settings.useSmoothDeltaTime;
				DOTween.maxSmoothUnscaledTime = settings.maxSmoothUnscaledTime;
				DOTween.defaultRecyclable = ((recycleAllByDefault == null) ? settings.defaultRecyclable : recycleAllByDefault.Value);
				DOTween.showUnityEditorReport = settings.showUnityEditorReport;
				DOTween.drawGizmos = settings.drawGizmos;
				DOTween.defaultAutoPlay = settings.defaultAutoPlay;
				DOTween.defaultUpdateType = settings.defaultUpdateType;
				DOTween.defaultTimeScaleIndependent = settings.defaultTimeScaleIndependent;
				DOTween.defaultEaseType = settings.defaultEaseType;
				DOTween.defaultEaseOvershootOrAmplitude = settings.defaultEaseOvershootOrAmplitude;
				DOTween.defaultEasePeriod = settings.defaultEasePeriod;
				DOTween.defaultAutoKill = settings.defaultAutoKill;
				DOTween.defaultLoopType = settings.defaultLoopType;
			}
			if (Debugger.logPriority >= 2)
			{
				Debugger.Log(string.Concat(new object[]
				{
					"DOTween initialization (useSafeMode: ",
					DOTween.useSafeMode.ToString(),
					", recycling: ",
					DOTween.defaultRecyclable ? "ON" : "OFF",
					", logBehaviour: ",
					DOTween.logBehaviour,
					")"
				}));
			}
			return DOTween.instance;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004964 File Offset: 0x00002B64
		public static TweenerCore<float, float, FloatOptions> To(DOGetter<float> getter, DOSetter<float> setter, float endValue, float duration)
		{
			return DOTween.ApplyTo<float, float, FloatOptions>(getter, setter, endValue, duration, null);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004970 File Offset: 0x00002B70
		public static TweenerCore<Vector3, Vector3, VectorOptions> To(DOGetter<Vector3> getter, DOSetter<Vector3> setter, Vector3 endValue, float duration)
		{
			return DOTween.ApplyTo<Vector3, Vector3, VectorOptions>(getter, setter, endValue, duration, null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000497C File Offset: 0x00002B7C
		public static TweenerCore<Quaternion, Vector3, QuaternionOptions> To(DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, Vector3 endValue, float duration)
		{
			return DOTween.ApplyTo<Quaternion, Vector3, QuaternionOptions>(getter, setter, endValue, duration, null);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004988 File Offset: 0x00002B88
		public static TweenerCore<Color, Color, ColorOptions> To(DOGetter<Color> getter, DOSetter<Color> setter, Color endValue, float duration)
		{
			return DOTween.ApplyTo<Color, Color, ColorOptions>(getter, setter, endValue, duration, null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004994 File Offset: 0x00002B94
		internal static int CompleteAndReturnKilledTot(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Complete, FilterType.TargetOrId, targetOrId, true, 0f, null, null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000049AC File Offset: 0x00002BAC
		public static int Kill(object targetOrId, bool complete = false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return (complete ? DOTween.CompleteAndReturnKilledTot(targetOrId) : 0) + TweenManager.FilteredOperation(OperationType.Despawn, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000049DC File Offset: 0x00002BDC
		private static void InitCheck()
		{
			if (DOTween.initialized || !Application.isPlaying || DOTween.isQuitting)
			{
				return;
			}
			DOTween.AutoInit();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000049FC File Offset: 0x00002BFC
		private static TweenerCore<T1, T2, TPlugOptions> ApplyTo<T1, T2, TPlugOptions>(DOGetter<T1> getter, DOSetter<T1> setter, T2 endValue, float duration, ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null) where TPlugOptions : struct, IPlugOptions
		{
			DOTween.InitCheck();
			TweenerCore<T1, T2, TPlugOptions> tweener = TweenManager.GetTweener<T1, T2, TPlugOptions>();
			if (!Tweener.Setup<T1, T2, TPlugOptions>(tweener, getter, setter, endValue, duration, plugin))
			{
				TweenManager.Despawn(tweener, true);
				return null;
			}
			return tweener;
		}

		// Token: 0x04000093 RID: 147
		public static readonly string Version = "1.1.555";

		// Token: 0x04000094 RID: 148
		public static bool useSafeMode = true;

		// Token: 0x04000095 RID: 149
		public static bool showUnityEditorReport = false;

		// Token: 0x04000096 RID: 150
		public static float timeScale = 1f;

		// Token: 0x04000097 RID: 151
		public static bool useSmoothDeltaTime;

		// Token: 0x04000098 RID: 152
		public static float maxSmoothUnscaledTime = 0.15f;

		// Token: 0x04000099 RID: 153
		private static LogBehaviour _logBehaviour = LogBehaviour.ErrorsOnly;

		// Token: 0x0400009A RID: 154
		public static bool drawGizmos = true;

		// Token: 0x0400009B RID: 155
		public static UpdateType defaultUpdateType = UpdateType.Normal;

		// Token: 0x0400009C RID: 156
		public static bool defaultTimeScaleIndependent = false;

		// Token: 0x0400009D RID: 157
		public static AutoPlay defaultAutoPlay = AutoPlay.All;

		// Token: 0x0400009E RID: 158
		public static bool defaultAutoKill = true;

		// Token: 0x0400009F RID: 159
		public static LoopType defaultLoopType = LoopType.Restart;

		// Token: 0x040000A0 RID: 160
		public static bool defaultRecyclable;

		// Token: 0x040000A1 RID: 161
		public static Ease defaultEaseType = Ease.OutQuad;

		// Token: 0x040000A2 RID: 162
		public static float defaultEaseOvershootOrAmplitude = 1.70158f;

		// Token: 0x040000A3 RID: 163
		public static float defaultEasePeriod = 0f;

		// Token: 0x040000A4 RID: 164
		internal static DOTweenComponent instance;

		// Token: 0x040000A5 RID: 165
		internal static bool isUnityEditor;

		// Token: 0x040000A6 RID: 166
		internal static bool isDebugBuild;

		// Token: 0x040000A7 RID: 167
		internal static int maxActiveTweenersReached;

		// Token: 0x040000A8 RID: 168
		internal static int maxActiveSequencesReached;

		// Token: 0x040000A9 RID: 169
		internal static readonly List<TweenCallback> GizmosDelegates = new List<TweenCallback>();

		// Token: 0x040000AA RID: 170
		internal static bool initialized;

		// Token: 0x040000AB RID: 171
		internal static bool isQuitting;
	}
}
