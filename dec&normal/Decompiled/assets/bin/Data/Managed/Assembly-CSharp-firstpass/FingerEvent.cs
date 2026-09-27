using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class FingerEvent
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000026E0 File Offset: 0x000008E0
	// (set) Token: 0x0600000F RID: 15 RVA: 0x000026E8 File Offset: 0x000008E8
	public string Name
	{
		get
		{
			return this.name;
		}
		internal set
		{
			this.name = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000026F4 File Offset: 0x000008F4
	// (set) Token: 0x06000011 RID: 17 RVA: 0x000026FC File Offset: 0x000008FC
	public FingerEventDetector Detector
	{
		get
		{
			return this.detector;
		}
		internal set
		{
			this.detector = value;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002708 File Offset: 0x00000908
	// (set) Token: 0x06000013 RID: 19 RVA: 0x00002710 File Offset: 0x00000910
	public FingerGestures.Finger Finger
	{
		get
		{
			return this.finger;
		}
		internal set
		{
			this.finger = value;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000014 RID: 20 RVA: 0x0000271C File Offset: 0x0000091C
	// (set) Token: 0x06000015 RID: 21 RVA: 0x0000272C File Offset: 0x0000092C
	public virtual Vector2 Position
	{
		get
		{
			return this.finger.Position;
		}
		internal set
		{
			throw new NotSupportedException("Setting position is not supported on " + base.GetType());
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000016 RID: 22 RVA: 0x00002744 File Offset: 0x00000944
	// (set) Token: 0x06000017 RID: 23 RVA: 0x0000274C File Offset: 0x0000094C
	public GameObject Selection
	{
		get
		{
			return this.selection;
		}
		internal set
		{
			this.selection = value;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000018 RID: 24 RVA: 0x00002758 File Offset: 0x00000958
	// (set) Token: 0x06000019 RID: 25 RVA: 0x00002760 File Offset: 0x00000960
	public ScreenRaycastData Raycast
	{
		get
		{
			return this.raycast;
		}
		internal set
		{
			this.raycast = value;
		}
	}

	// Token: 0x0400000B RID: 11
	private FingerEventDetector detector;

	// Token: 0x0400000C RID: 12
	private FingerGestures.Finger finger;

	// Token: 0x0400000D RID: 13
	private string name = string.Empty;

	// Token: 0x0400000E RID: 14
	private GameObject selection;

	// Token: 0x0400000F RID: 15
	private ScreenRaycastData raycast = default(ScreenRaycastData);
}
