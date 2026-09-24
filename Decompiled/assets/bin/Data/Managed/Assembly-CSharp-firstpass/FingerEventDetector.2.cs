using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public abstract class FingerEventDetector : MonoBehaviour
{
	// Token: 0x06000025 RID: 37
	protected abstract void ProcessFinger(FingerGestures.Finger finger);

	// Token: 0x06000026 RID: 38
	public abstract Type GetEventType();

	// Token: 0x06000027 RID: 39 RVA: 0x00002890 File Offset: 0x00000A90
	protected virtual void Awake()
	{
		if (!this.Raycaster)
		{
			this.Raycaster = base.GetComponent<ScreenRaycaster>();
		}
		if (!this.MessageTarget)
		{
			this.MessageTarget = base.gameObject;
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000028D8 File Offset: 0x00000AD8
	protected virtual void Start()
	{
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000028DC File Offset: 0x00000ADC
	protected virtual void Update()
	{
		this.ProcessFingers();
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000028E4 File Offset: 0x00000AE4
	protected virtual void ProcessFingers()
	{
		if (this.FingerIndexFilter >= 0 && this.FingerIndexFilter < FingerGestures.Instance.MaxFingers)
		{
			this.ProcessFinger(FingerGestures.GetFinger(this.FingerIndexFilter));
		}
		else
		{
			for (int i = 0; i < FingerGestures.Instance.MaxFingers; i++)
			{
				this.ProcessFinger(FingerGestures.GetFinger(i));
			}
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002950 File Offset: 0x00000B50
	protected void TrySendMessage(FingerEvent eventData)
	{
		FingerGestures.FireEvent(eventData);
		if (this.UseSendMessage)
		{
			this.MessageTarget.SendMessage(eventData.Name, eventData, SendMessageOptions.DontRequireReceiver);
			if (this.SendMessageToSelection && eventData.Selection && eventData.Selection != this.MessageTarget)
			{
				eventData.Selection.SendMessage(eventData.Name, eventData, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600002C RID: 44 RVA: 0x000029C8 File Offset: 0x00000BC8
	internal ScreenRaycastData Raycast
	{
		get
		{
			return this.lastRaycast;
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000029D0 File Offset: 0x00000BD0
	public GameObject PickObject(Vector2 screenPos)
	{
		if (!this.Raycaster || !this.Raycaster.enabled)
		{
			return null;
		}
		if (!this.Raycaster.Raycast(screenPos, out this.lastRaycast))
		{
			return null;
		}
		return this.lastRaycast.GameObject;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002A24 File Offset: 0x00000C24
	protected void UpdateSelection(FingerEvent e)
	{
		e.Selection = this.PickObject(e.Position);
		e.Raycast = this.Raycast;
	}

	// Token: 0x04000011 RID: 17
	public int FingerIndexFilter = -1;

	// Token: 0x04000012 RID: 18
	public ScreenRaycaster Raycaster;

	// Token: 0x04000013 RID: 19
	public bool UseSendMessage = true;

	// Token: 0x04000014 RID: 20
	public bool SendMessageToSelection = true;

	// Token: 0x04000015 RID: 21
	public GameObject MessageTarget;

	// Token: 0x04000016 RID: 22
	private FingerGestures.Finger activeFinger;

	// Token: 0x04000017 RID: 23
	private ScreenRaycastData lastRaycast = default(ScreenRaycastData);
}
