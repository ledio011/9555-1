using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public abstract class GestureRecognizer : MonoBehaviour
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600006B RID: 107 RVA: 0x0000357C File Offset: 0x0000177C
	// (set) Token: 0x0600006C RID: 108 RVA: 0x00003584 File Offset: 0x00001784
	public virtual int RequiredFingerCount
	{
		get
		{
			return this.requiredFingerCount;
		}
		set
		{
			this.requiredFingerCount = value;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600006D RID: 109 RVA: 0x00003590 File Offset: 0x00001790
	public virtual bool SupportFingerClustering
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003594 File Offset: 0x00001794
	public virtual GestureResetMode GetDefaultResetMode()
	{
		return GestureResetMode.EndOfTouchSequence;
	}

	// Token: 0x0600006F RID: 111
	public abstract string GetDefaultEventMessageName();

	// Token: 0x06000070 RID: 112
	public abstract Type GetGestureType();

	// Token: 0x06000071 RID: 113 RVA: 0x00003598 File Offset: 0x00001798
	protected virtual void Awake()
	{
		if (string.IsNullOrEmpty(this.EventMessageName))
		{
			this.EventMessageName = this.GetDefaultEventMessageName();
		}
		if (this.ResetMode == GestureResetMode.Default)
		{
			this.ResetMode = this.GetDefaultResetMode();
		}
		if (!this.EventMessageTarget)
		{
			this.EventMessageTarget = base.gameObject;
		}
		if (!this.Raycaster)
		{
			this.Raycaster = base.GetComponent<ScreenRaycaster>();
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003610 File Offset: 0x00001810
	protected virtual void OnEnable()
	{
		if (FingerGestures.Instance)
		{
			FingerGestures.Register(this);
		}
		else
		{
			Debug.LogError("Failed to register gesture recognizer " + this + " - FingerGestures instance is not available.");
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003644 File Offset: 0x00001844
	protected virtual void OnDisable()
	{
		if (FingerGestures.Instance)
		{
			FingerGestures.Unregister(this);
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000365C File Offset: 0x0000185C
	protected void Acquire(FingerGestures.Finger finger)
	{
		if (!finger.GestureRecognizers.Contains(this))
		{
			finger.GestureRecognizers.Add(this);
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003688 File Offset: 0x00001888
	protected bool Release(FingerGestures.Finger finger)
	{
		return finger.GestureRecognizers.Remove(this);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003698 File Offset: 0x00001898
	protected virtual void Start()
	{
		if (!FingerGestures.Instance)
		{
			Debug.LogWarning("FingerGestures instance not found in current scene. Disabling recognizer: " + this);
			base.enabled = false;
			return;
		}
		if (!this.ClusterManager && this.SupportFingerClustering)
		{
			this.ClusterManager = base.GetComponent<FingerClusterManager>();
			if (!this.ClusterManager)
			{
				this.ClusterManager = FingerGestures.DefaultClusterManager;
			}
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003710 File Offset: 0x00001910
	protected bool Young(FingerGestures.IFingerList touches)
	{
		FingerGestures.Finger oldest = touches.GetOldest();
		if (oldest == null)
		{
			return false;
		}
		float num = Time.time - oldest.StarTime;
		return num < 0.25f;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003744 File Offset: 0x00001944
	public float ToPixels(float distance)
	{
		return distance.Convert(this.DistanceUnit, DistanceUnit.Pixels);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003754 File Offset: 0x00001954
	public float ToSqrPixels(float distance)
	{
		float num = this.ToPixels(distance);
		return num * num;
	}

	// Token: 0x04000034 RID: 52
	protected static readonly FingerGestures.IFingerList EmptyFingerList = new FingerGestures.FingerList();

	// Token: 0x04000035 RID: 53
	[SerializeField]
	private int requiredFingerCount = 1;

	// Token: 0x04000036 RID: 54
	public DistanceUnit DistanceUnit = DistanceUnit.Centimeters;

	// Token: 0x04000037 RID: 55
	public int MaxSimultaneousGestures = 1;

	// Token: 0x04000038 RID: 56
	public GestureResetMode ResetMode;

	// Token: 0x04000039 RID: 57
	public ScreenRaycaster Raycaster;

	// Token: 0x0400003A RID: 58
	public FingerClusterManager ClusterManager;

	// Token: 0x0400003B RID: 59
	public GestureRecognizerDelegate Delegate;

	// Token: 0x0400003C RID: 60
	public bool UseSendMessage = true;

	// Token: 0x0400003D RID: 61
	public string EventMessageName;

	// Token: 0x0400003E RID: 62
	public GameObject EventMessageTarget;

	// Token: 0x0400003F RID: 63
	public GestureRecognizer.SelectionType SendMessageToSelection;

	// Token: 0x04000040 RID: 64
	public bool IsExclusive;

	// Token: 0x02000010 RID: 16
	public enum SelectionType
	{
		// Token: 0x04000042 RID: 66
		Default,
		// Token: 0x04000043 RID: 67
		StartSelection,
		// Token: 0x04000044 RID: 68
		CurrentSelection,
		// Token: 0x04000045 RID: 69
		None
	}
}
