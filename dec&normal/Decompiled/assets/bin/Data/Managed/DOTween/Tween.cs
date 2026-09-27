using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	// Token: 0x02000052 RID: 82
	public abstract class Tween : ABSSequentiable
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00008694 File Offset: 0x00006894
		internal virtual void Reset()
		{
			this.timeScale = 1f;
			this.isBackwards = false;
			this.id = null;
			this.isIndependentUpdate = false;
			this.onStart = (this.onPlay = (this.onRewind = (this.onUpdate = (this.onComplete = (this.onStepComplete = (this.onKill = null))))));
			this.onWaypointChange = null;
			this.target = null;
			this.isFrom = false;
			this.isBlendable = false;
			this.isSpeedBased = false;
			this.duration = 0f;
			this.loops = 1;
			this.delay = 0f;
			this.isRelative = false;
			this.customEase = null;
			this.isSequenced = false;
			this.sequenceParent = null;
			this.specialStartupMode = SpecialStartupMode.None;
			this.creationLocked = (this.startupDone = (this.playedOnce = false));
			this.position = (this.fullDuration = (float)(this.completedLoops = 0));
			this.isPlaying = (this.isComplete = false);
			this.elapsedDelay = 0f;
			this.delayComplete = true;
			this.miscInt = -1;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000087C0 File Offset: 0x000069C0
		internal virtual float UpdateDelay(float elapsed)
		{
			return 0f;
		}

		// Token: 0x06000140 RID: 320
		internal abstract bool Startup();

		// Token: 0x06000141 RID: 321
		internal abstract bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice);

		// Token: 0x06000142 RID: 322 RVA: 0x000087C8 File Offset: 0x000069C8
		internal static bool DoGoto(Tween t, float toPosition, int toCompletedLoops, UpdateMode updateMode)
		{
			if (!t.startupDone && !t.Startup())
			{
				return true;
			}
			if (!t.playedOnce && updateMode == UpdateMode.Update)
			{
				t.playedOnce = true;
				if (t.onStart != null)
				{
					Tween.OnTweenCallback(t.onStart);
					if (!t.active)
					{
						return true;
					}
				}
				if (t.onPlay != null)
				{
					Tween.OnTweenCallback(t.onPlay);
					if (!t.active)
					{
						return true;
					}
				}
			}
			float prevPosition = t.position;
			int num = t.completedLoops;
			t.completedLoops = toCompletedLoops;
			bool flag = t.position <= 0f && num <= 0;
			bool flag2 = t.isComplete;
			if (t.loops != -1)
			{
				t.isComplete = (t.completedLoops == t.loops);
			}
			int num2 = 0;
			if (updateMode == UpdateMode.Update)
			{
				if (t.isBackwards)
				{
					num2 = ((t.completedLoops < num) ? (num - t.completedLoops) : ((toPosition <= 0f && !flag) ? 1 : 0));
					if (flag2)
					{
						num2--;
					}
				}
				else
				{
					num2 = ((t.completedLoops > num) ? (t.completedLoops - num) : 0);
				}
			}
			else if (t.tweenType == TweenType.Sequence)
			{
				num2 = num - toCompletedLoops;
				if (num2 < 0)
				{
					num2 = -num2;
				}
			}
			t.position = toPosition;
			if (t.position > t.duration)
			{
				t.position = t.duration;
			}
			else if (t.position <= 0f)
			{
				if (t.completedLoops > 0 || t.isComplete)
				{
					t.position = t.duration;
				}
				else
				{
					t.position = 0f;
				}
			}
			bool flag3 = t.isPlaying;
			if (t.isPlaying)
			{
				if (!t.isBackwards)
				{
					t.isPlaying = !t.isComplete;
				}
				else
				{
					t.isPlaying = (t.completedLoops != 0 || t.position > 0f);
				}
			}
			bool useInversePosition = t.loopType == LoopType.Yoyo && ((t.position < t.duration) ? (t.completedLoops % 2 != 0) : (t.completedLoops % 2 == 0));
			UpdateNotice updateNotice = (!flag && ((t.loopType == LoopType.Restart && t.completedLoops != num) || (t.position <= 0f && t.completedLoops <= 0))) ? UpdateNotice.RewindStep : UpdateNotice.None;
			if (t.ApplyTween(prevPosition, num, num2, useInversePosition, updateMode, updateNotice))
			{
				return true;
			}
			if (t.onUpdate != null && updateMode != UpdateMode.IgnoreOnUpdate)
			{
				Tween.OnTweenCallback(t.onUpdate);
			}
			if (t.position <= 0f && t.completedLoops <= 0 && !flag && t.onRewind != null)
			{
				Tween.OnTweenCallback(t.onRewind);
			}
			if (num2 > 0 && updateMode == UpdateMode.Update && t.onStepComplete != null)
			{
				for (int i = 0; i < num2; i++)
				{
					Tween.OnTweenCallback(t.onStepComplete);
				}
			}
			if (t.isComplete && !flag2 && t.onComplete != null)
			{
				Tween.OnTweenCallback(t.onComplete);
			}
			if (!t.isPlaying && flag3 && (!t.isComplete || !t.autoKill) && t.onPause != null)
			{
				Tween.OnTweenCallback(t.onPause);
			}
			return t.autoKill && t.isComplete;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008AE0 File Offset: 0x00006CE0
		internal static bool OnTweenCallback(TweenCallback callback)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback();
					return true;
				}
				catch (Exception ex)
				{
					Debugger.LogWarning(string.Concat(new string[]
					{
						"An error inside a tween callback was silently taken care of > ",
						ex.Message,
						"\n\n",
						ex.StackTrace,
						"\n\n"
					}));
					return false;
				}
			}
			callback();
			return true;
		}

		// Token: 0x0400011F RID: 287
		public float timeScale;

		// Token: 0x04000120 RID: 288
		public bool isBackwards;

		// Token: 0x04000121 RID: 289
		public object id;

		// Token: 0x04000122 RID: 290
		public object target;

		// Token: 0x04000123 RID: 291
		internal UpdateType updateType;

		// Token: 0x04000124 RID: 292
		internal bool isIndependentUpdate;

		// Token: 0x04000125 RID: 293
		internal TweenCallback onPlay;

		// Token: 0x04000126 RID: 294
		internal TweenCallback onPause;

		// Token: 0x04000127 RID: 295
		internal TweenCallback onRewind;

		// Token: 0x04000128 RID: 296
		internal TweenCallback onUpdate;

		// Token: 0x04000129 RID: 297
		internal TweenCallback onStepComplete;

		// Token: 0x0400012A RID: 298
		internal TweenCallback onComplete;

		// Token: 0x0400012B RID: 299
		internal TweenCallback onKill;

		// Token: 0x0400012C RID: 300
		internal TweenCallback<int> onWaypointChange;

		// Token: 0x0400012D RID: 301
		internal bool isFrom;

		// Token: 0x0400012E RID: 302
		internal bool isBlendable;

		// Token: 0x0400012F RID: 303
		internal bool isRecyclable;

		// Token: 0x04000130 RID: 304
		internal bool isSpeedBased;

		// Token: 0x04000131 RID: 305
		internal bool autoKill;

		// Token: 0x04000132 RID: 306
		internal float duration;

		// Token: 0x04000133 RID: 307
		internal int loops;

		// Token: 0x04000134 RID: 308
		internal LoopType loopType;

		// Token: 0x04000135 RID: 309
		internal float delay;

		// Token: 0x04000136 RID: 310
		internal bool isRelative;

		// Token: 0x04000137 RID: 311
		internal Ease easeType;

		// Token: 0x04000138 RID: 312
		internal EaseFunction customEase;

		// Token: 0x04000139 RID: 313
		public float easeOvershootOrAmplitude;

		// Token: 0x0400013A RID: 314
		public float easePeriod;

		// Token: 0x0400013B RID: 315
		internal Type typeofT1;

		// Token: 0x0400013C RID: 316
		internal Type typeofT2;

		// Token: 0x0400013D RID: 317
		internal Type typeofTPlugOptions;

		// Token: 0x0400013E RID: 318
		internal bool active;

		// Token: 0x0400013F RID: 319
		internal bool isSequenced;

		// Token: 0x04000140 RID: 320
		internal Sequence sequenceParent;

		// Token: 0x04000141 RID: 321
		internal int activeId = -1;

		// Token: 0x04000142 RID: 322
		internal SpecialStartupMode specialStartupMode;

		// Token: 0x04000143 RID: 323
		internal bool creationLocked;

		// Token: 0x04000144 RID: 324
		internal bool startupDone;

		// Token: 0x04000145 RID: 325
		internal bool playedOnce;

		// Token: 0x04000146 RID: 326
		internal float position;

		// Token: 0x04000147 RID: 327
		internal float fullDuration;

		// Token: 0x04000148 RID: 328
		internal int completedLoops;

		// Token: 0x04000149 RID: 329
		internal bool isPlaying;

		// Token: 0x0400014A RID: 330
		internal bool isComplete;

		// Token: 0x0400014B RID: 331
		internal float elapsedDelay;

		// Token: 0x0400014C RID: 332
		internal bool delayComplete = true;

		// Token: 0x0400014D RID: 333
		internal int miscInt = -1;
	}
}
