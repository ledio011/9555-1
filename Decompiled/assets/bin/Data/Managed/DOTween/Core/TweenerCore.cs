using System;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Core
{
	// Token: 0x0200001E RID: 30
	public class TweenerCore<T1, T2, TPlugOptions> : Tweener where TPlugOptions : struct, IPlugOptions
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000330C File Offset: 0x0000150C
		internal TweenerCore()
		{
			this.typeofT1 = typeof(T1);
			this.typeofT2 = typeof(T2);
			this.typeofTPlugOptions = typeof(TPlugOptions);
			this.tweenType = TweenType.Tweener;
			this.Reset();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000335C File Offset: 0x0000155C
		internal sealed override void Reset()
		{
			base.Reset();
			if (this.tweenPlugin != null)
			{
				this.tweenPlugin.Reset(this);
			}
			this.plugOptions.Reset();
			this.getter = null;
			this.setter = null;
			this.hasManuallySetStartValue = false;
			this.isFromAllowed = true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000033B0 File Offset: 0x000015B0
		internal override float UpdateDelay(float elapsed)
		{
			return Tweener.DoUpdateDelay<T1, T2, TPlugOptions>(this, elapsed);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000033BC File Offset: 0x000015BC
		internal override bool Startup()
		{
			return Tweener.DoStartup<T1, T2, TPlugOptions>(this);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000033C4 File Offset: 0x000015C4
		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			float elapsed = useInversePosition ? (this.duration - this.position) : this.position;
			if (DOTween.useSafeMode)
			{
				try
				{
					this.tweenPlugin.EvaluateAndApply(this.plugOptions, this, this.isRelative, this.getter, this.setter, elapsed, this.startValue, this.changeValue, this.duration, useInversePosition, updateNotice);
					return false;
				}
				catch
				{
					return true;
				}
			}
			this.tweenPlugin.EvaluateAndApply(this.plugOptions, this, this.isRelative, this.getter, this.setter, elapsed, this.startValue, this.changeValue, this.duration, useInversePosition, updateNotice);
			return false;
		}

		// Token: 0x04000068 RID: 104
		public T2 startValue;

		// Token: 0x04000069 RID: 105
		public T2 endValue;

		// Token: 0x0400006A RID: 106
		public T2 changeValue;

		// Token: 0x0400006B RID: 107
		public TPlugOptions plugOptions;

		// Token: 0x0400006C RID: 108
		public DOGetter<T1> getter;

		// Token: 0x0400006D RID: 109
		public DOSetter<T1> setter;

		// Token: 0x0400006E RID: 110
		internal ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

		// Token: 0x0400006F RID: 111
		private const string _TxtCantChangeSequencedValues = "You cannot change the values of a tween contained inside a Sequence";
	}
}
