using System;
using System.Collections.Generic;

// Token: 0x02000009 RID: 9
public abstract class FingerEventDetector<T> : FingerEventDetector where T : FingerEvent, new()
{
	// Token: 0x0600001B RID: 27 RVA: 0x00002774 File Offset: 0x00000974
	protected virtual T CreateFingerEvent()
	{
		return Activator.CreateInstance<T>();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000277C File Offset: 0x0000097C
	public override Type GetEventType()
	{
		return typeof(T);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002788 File Offset: 0x00000988
	protected override void Start()
	{
		base.Start();
		FingerGestures.OnInputProviderChanged += this.FingerGestures_OnInputProviderChanged;
		this.Init();
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000027A8 File Offset: 0x000009A8
	protected virtual void OnDestroy()
	{
		FingerGestures.OnInputProviderChanged -= this.FingerGestures_OnInputProviderChanged;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000027BC File Offset: 0x000009BC
	private void FingerGestures_OnInputProviderChanged()
	{
		this.Init();
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000027C4 File Offset: 0x000009C4
	protected virtual void Init()
	{
		this.Init(FingerGestures.Instance.MaxFingers);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000027D8 File Offset: 0x000009D8
	protected virtual void Init(int fingersCount)
	{
		this.fingerEventsList = new List<T>(fingersCount);
		for (int i = 0; i < fingersCount; i++)
		{
			T item = this.CreateFingerEvent();
			item.Detector = this;
			item.Finger = FingerGestures.GetFinger(i);
			this.fingerEventsList.Add(item);
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002838 File Offset: 0x00000A38
	protected T GetEvent(FingerGestures.Finger finger)
	{
		return this.GetEvent(finger.Index);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002848 File Offset: 0x00000A48
	protected virtual T GetEvent(int fingerIndex)
	{
		return this.fingerEventsList[fingerIndex];
	}

	// Token: 0x04000010 RID: 16
	private List<T> fingerEventsList;

	// Token: 0x02000063 RID: 99
	// (Invoke) Token: 0x0600027F RID: 639
	public delegate void FingerEventHandler(T eventData);
}
