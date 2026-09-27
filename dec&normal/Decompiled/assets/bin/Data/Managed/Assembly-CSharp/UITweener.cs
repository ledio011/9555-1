using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000AD RID: 173
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x170000BA RID: 186
	// (get) Token: 0x060004EB RID: 1259 RVA: 0x00021058 File Offset: 0x0001F258
	public float amountPerDelta
	{
		get
		{
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs((this.duration <= 0f) ? 1000f : (1f / this.duration));
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x060004EC RID: 1260 RVA: 0x000210BC File Offset: 0x0001F2BC
	// (set) Token: 0x060004ED RID: 1261 RVA: 0x000210C4 File Offset: 0x0001F2C4
	public float tweenFactor
	{
		get
		{
			return this.mFactor;
		}
		set
		{
			this.mFactor = Mathf.Clamp01(value);
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x060004EE RID: 1262 RVA: 0x000210D4 File Offset: 0x0001F2D4
	public Direction direction
	{
		get
		{
			return (this.mAmountPerDelta >= 0f) ? Direction.Forward : Direction.Reverse;
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x000210F0 File Offset: 0x0001F2F0
	private void Reset()
	{
		if (!this.mStarted)
		{
			this.SetStartToCurrentValue();
			this.SetEndToCurrentValue();
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0002110C File Offset: 0x0001F30C
	protected virtual void Start()
	{
		this.Update();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00021114 File Offset: 0x0001F314
	private void Update()
	{
		float num = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
		float num2 = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
		if (!this.mStarted)
		{
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += this.amountPerDelta * num;
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		if (this.style == UITweener.Style.Once && (this.duration == 0f || this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			if (this.duration == 0f || (this.mFactor == 1f && this.mAmountPerDelta > 0f) || (this.mFactor == 0f && this.mAmountPerDelta < 0f))
			{
				base.enabled = false;
			}
			if (UITweener.current == null)
			{
				UITweener.current = this;
				if (this.onFinished != null)
				{
					this.mTemp = this.onFinished;
					this.onFinished = new List<EventDelegate>();
					EventDelegate.Execute(this.mTemp);
					for (int i = 0; i < this.mTemp.Count; i++)
					{
						EventDelegate eventDelegate = this.mTemp[i];
						if (eventDelegate != null)
						{
							EventDelegate.Add(this.onFinished, eventDelegate, eventDelegate.oneShot);
						}
					}
					this.mTemp = null;
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, 1);
				}
				UITweener.current = null;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x000213E8 File Offset: 0x0001F5E8
	public void SetOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000213F8 File Offset: 0x0001F5F8
	public void SetOnFinished(EventDelegate del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00021408 File Offset: 0x0001F608
	public void AddOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00021418 File Offset: 0x0001F618
	public void AddOnFinished(EventDelegate del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00021428 File Offset: 0x0001F628
	public void RemoveOnFinished(EventDelegate del)
	{
		if (this.onFinished != null)
		{
			this.onFinished.Remove(del);
		}
		if (this.mTemp != null)
		{
			this.mTemp.Remove(del);
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00021468 File Offset: 0x0001F668
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00021474 File Offset: 0x0001F674
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.5707964f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.5707964f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.2831855f) / 6.2831855f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve == null) ? num : this.animationCurve.Evaluate(num), isFinished);
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000215C8 File Offset: 0x0001F7C8
	private float BounceLogic(float val)
	{
		if (val < 0.363636f)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
		}
		else if (val < 0.90909f)
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
		}
		return val;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00021660 File Offset: 0x0001F860
	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0002166C File Offset: 0x0001F86C
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00021678 File Offset: 0x0001F878
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00021684 File Offset: 0x0001F884
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		base.enabled = true;
		this.Update();
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x000216B8 File Offset: 0x0001F8B8
	public void ResetToBeginning()
	{
		this.mStarted = false;
		this.mFactor = 0f;
		this.Sample(this.mFactor, false);
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000216DC File Offset: 0x0001F8DC
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x06000500 RID: 1280
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x06000501 RID: 1281 RVA: 0x00021724 File Offset: 0x0001F924
	public static T Begin<T>(GameObject go, float duration) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t != null && t.tweenGroup != 0)
		{
			t = (T)((object)null);
			T[] components = go.GetComponents<T>();
			int i = 0;
			int num = components.Length;
			while (i < num)
			{
				t = components[i];
				if (t != null && t.tweenGroup == 0)
				{
					break;
				}
				t = (T)((object)null);
				i++;
			}
		}
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.mStarted = false;
		t.duration = duration;
		t.mFactor = 0f;
		t.mAmountPerDelta = Mathf.Abs(t.mAmountPerDelta);
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.enabled = true;
		if (duration <= 0f)
		{
			t.Sample(1f, true);
			t.enabled = false;
		}
		return t;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x000218CC File Offset: 0x0001FACC
	public virtual void SetStartToCurrentValue()
	{
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000218D0 File Offset: 0x0001FAD0
	public virtual void SetEndToCurrentValue()
	{
	}

	// Token: 0x04000436 RID: 1078
	public static UITweener current;

	// Token: 0x04000437 RID: 1079
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x04000438 RID: 1080
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x04000439 RID: 1081
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x0400043A RID: 1082
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x0400043B RID: 1083
	[HideInInspector]
	public float delay;

	// Token: 0x0400043C RID: 1084
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x0400043D RID: 1085
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x0400043E RID: 1086
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x0400043F RID: 1087
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000440 RID: 1088
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000441 RID: 1089
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000442 RID: 1090
	private bool mStarted;

	// Token: 0x04000443 RID: 1091
	private float mStartTime;

	// Token: 0x04000444 RID: 1092
	private float mDuration;

	// Token: 0x04000445 RID: 1093
	[HideInInspector]
	public float mAmountPerDelta = 1000f;

	// Token: 0x04000446 RID: 1094
	private float mFactor;

	// Token: 0x04000447 RID: 1095
	private List<EventDelegate> mTemp;

	// Token: 0x020000AE RID: 174
	public enum Method
	{
		// Token: 0x04000449 RID: 1097
		Linear,
		// Token: 0x0400044A RID: 1098
		EaseIn,
		// Token: 0x0400044B RID: 1099
		EaseOut,
		// Token: 0x0400044C RID: 1100
		EaseInOut,
		// Token: 0x0400044D RID: 1101
		BounceIn,
		// Token: 0x0400044E RID: 1102
		BounceOut
	}

	// Token: 0x020000AF RID: 175
	public enum Style
	{
		// Token: 0x04000450 RID: 1104
		Once,
		// Token: 0x04000451 RID: 1105
		Loop,
		// Token: 0x04000452 RID: 1106
		PingPong
	}
}
