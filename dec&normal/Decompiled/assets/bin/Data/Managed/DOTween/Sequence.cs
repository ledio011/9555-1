using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	// Token: 0x02000048 RID: 72
	public sealed class Sequence : Tween
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00007BD4 File Offset: 0x00005DD4
		internal override void Reset()
		{
			base.Reset();
			this.sequencedTweens.Clear();
			this._sequencedObjs.Clear();
			this.lastTweenInsertTime = 0f;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007C00 File Offset: 0x00005E00
		internal override bool Startup()
		{
			return Sequence.DoStartup(this);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007C08 File Offset: 0x00005E08
		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			return Sequence.DoApplyTween(this, prevPosition, prevCompletedLoops, newCompletedSteps, useInversePosition, updateMode);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007C18 File Offset: 0x00005E18
		internal static bool DoStartup(Sequence s)
		{
			if (s.sequencedTweens.Count == 0 && s._sequencedObjs.Count == 0 && s.onComplete == null && s.onKill == null && s.onPause == null && s.onPlay == null && s.onRewind == null && s.onStart == null && s.onStepComplete == null && s.onUpdate == null)
			{
				return false;
			}
			s.startupDone = true;
			s.fullDuration = ((s.loops > -1) ? (s.duration * (float)s.loops) : float.PositiveInfinity);
			s._sequencedObjs.Sort(new Comparison<ABSSequentiable>(Sequence.SortSequencedObjs));
			if (s.isRelative)
			{
				int count = s.sequencedTweens.Count;
				for (int i = 0; i < count; i++)
				{
					Tween tween = s.sequencedTweens[i];
					if (!s.isBlendable)
					{
						s.sequencedTweens[i].isRelative = true;
					}
				}
			}
			return true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007D0C File Offset: 0x00005F0C
		internal static bool DoApplyTween(Sequence s, float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode)
		{
			float num = prevPosition;
			float num2 = s.position;
			if (s.easeType != Ease.Linear)
			{
				num = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, num, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
				num2 = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, num2, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
			}
			float num3 = 0f;
			bool flag = s.loopType == LoopType.Yoyo && ((num < s.duration) ? (prevCompletedLoops % 2 != 0) : (prevCompletedLoops % 2 == 0));
			if (s.isBackwards)
			{
				flag = !flag;
			}
			float num5;
			if (newCompletedSteps > 0)
			{
				int completedLoops = s.completedLoops;
				float position = s.position;
				int num4 = newCompletedSteps;
				int i = 0;
				num5 = num;
				if (updateMode == UpdateMode.Update)
				{
					while (i < num4)
					{
						if (i > 0)
						{
							num5 = num3;
						}
						else if (flag && !s.isBackwards)
						{
							num5 = s.duration - num5;
						}
						num3 = (flag ? 0f : s.duration);
						if (Sequence.ApplyInternalCycle(s, num5, num3, updateMode, useInversePosition, flag, true))
						{
							return true;
						}
						i++;
						if (s.loopType == LoopType.Yoyo)
						{
							flag = !flag;
						}
					}
					if (completedLoops != s.completedLoops || Math.Abs(position - s.position) > 1E-45f)
					{
						return !s.active;
					}
				}
				else
				{
					if (s.loopType == LoopType.Yoyo && newCompletedSteps % 2 != 0)
					{
						flag = !flag;
						num = s.duration - num;
					}
					newCompletedSteps = 0;
				}
			}
			if (newCompletedSteps == 1 && s.isComplete)
			{
				return false;
			}
			if (newCompletedSteps > 0 && !s.isComplete)
			{
				num5 = (useInversePosition ? s.duration : 0f);
				if (s.loopType == LoopType.Restart && num3 > 0f)
				{
					Sequence.ApplyInternalCycle(s, s.duration, 0f, UpdateMode.Goto, false, false, false);
				}
			}
			else
			{
				num5 = (useInversePosition ? (s.duration - num) : num);
			}
			return Sequence.ApplyInternalCycle(s, num5, useInversePosition ? (s.duration - num2) : num2, updateMode, useInversePosition, flag, false);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007F10 File Offset: 0x00006110
		private static bool ApplyInternalCycle(Sequence s, float fromPos, float toPos, UpdateMode updateMode, bool useInverse, bool prevPosIsInverse, bool multiCycleStep = false)
		{
			if (toPos < fromPos)
			{
				for (int i = s._sequencedObjs.Count - 1; i > -1; i--)
				{
					if (!s.active)
					{
						return true;
					}
					ABSSequentiable abssequentiable = s._sequencedObjs[i];
					if (abssequentiable.sequencedEndPosition >= toPos && abssequentiable.sequencedPosition <= fromPos)
					{
						if (abssequentiable.tweenType == TweenType.Callback)
						{
							if (updateMode == UpdateMode.Update && prevPosIsInverse)
							{
								Tween.OnTweenCallback(abssequentiable.onStart);
							}
						}
						else
						{
							float num = toPos - abssequentiable.sequencedPosition;
							if (num < 0f)
							{
								num = 0f;
							}
							Tween tween = (Tween)abssequentiable;
							if (tween.startupDone)
							{
								tween.isBackwards = true;
								if (TweenManager.Goto(tween, num, false, updateMode))
								{
									return true;
								}
								if (multiCycleStep && tween.tweenType == TweenType.Sequence)
								{
									if (s.position <= 0f && s.completedLoops == 0)
									{
										tween.position = 0f;
									}
									else
									{
										bool flag = s.completedLoops == 0 || (s.isBackwards && (s.completedLoops < s.loops || s.loops == -1));
										if (tween.isBackwards)
										{
											flag = !flag;
										}
										if (useInverse)
										{
											flag = !flag;
										}
										if (s.isBackwards && !useInverse && !prevPosIsInverse)
										{
											flag = !flag;
										}
										tween.position = (flag ? 0f : tween.duration);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				int count = s._sequencedObjs.Count;
				for (int j = 0; j < count; j++)
				{
					if (!s.active)
					{
						return true;
					}
					ABSSequentiable abssequentiable2 = s._sequencedObjs[j];
					if (abssequentiable2.sequencedPosition <= toPos && abssequentiable2.sequencedEndPosition >= fromPos)
					{
						if (abssequentiable2.tweenType == TweenType.Callback)
						{
							if (updateMode == UpdateMode.Update && ((!s.isBackwards && !useInverse && !prevPosIsInverse) || (s.isBackwards && useInverse && !prevPosIsInverse)))
							{
								Tween.OnTweenCallback(abssequentiable2.onStart);
							}
						}
						else
						{
							float num2 = toPos - abssequentiable2.sequencedPosition;
							if (num2 < 0f)
							{
								num2 = 0f;
							}
							Tween tween2 = (Tween)abssequentiable2;
							if (toPos >= abssequentiable2.sequencedEndPosition)
							{
								if (!tween2.startupDone)
								{
									TweenManager.ForceInit(tween2, true);
								}
								if (num2 < tween2.fullDuration)
								{
									num2 = tween2.fullDuration;
								}
							}
							tween2.isBackwards = false;
							if (TweenManager.Goto(tween2, num2, false, updateMode))
							{
								return true;
							}
							if (multiCycleStep && tween2.tweenType == TweenType.Sequence)
							{
								if (s.position <= 0f && s.completedLoops == 0)
								{
									tween2.position = 0f;
								}
								else
								{
									bool flag2 = s.completedLoops == 0 || (!s.isBackwards && (s.completedLoops < s.loops || s.loops == -1));
									if (tween2.isBackwards)
									{
										flag2 = !flag2;
									}
									if (useInverse)
									{
										flag2 = !flag2;
									}
									if (s.isBackwards && !useInverse && !prevPosIsInverse)
									{
										flag2 = !flag2;
									}
									tween2.position = (flag2 ? 0f : tween2.duration);
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008250 File Offset: 0x00006450
		private static int SortSequencedObjs(ABSSequentiable a, ABSSequentiable b)
		{
			if (a.sequencedPosition > b.sequencedPosition)
			{
				return 1;
			}
			if (a.sequencedPosition < b.sequencedPosition)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04000112 RID: 274
		internal readonly List<Tween> sequencedTweens;

		// Token: 0x04000113 RID: 275
		private readonly List<ABSSequentiable> _sequencedObjs;

		// Token: 0x04000114 RID: 276
		internal float lastTweenInsertTime;
	}
}
