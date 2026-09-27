using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public abstract class Gesture
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000030 RID: 48 RVA: 0x00002A94 File Offset: 0x00000C94
	// (remove) Token: 0x06000031 RID: 49 RVA: 0x00002AB0 File Offset: 0x00000CB0
	public event Gesture.EventHandler OnStateChanged;

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000032 RID: 50 RVA: 0x00002ACC File Offset: 0x00000CCC
	// (set) Token: 0x06000033 RID: 51 RVA: 0x00002AD4 File Offset: 0x00000CD4
	public FingerGestures.FingerList Fingers
	{
		get
		{
			return this.fingers;
		}
		internal set
		{
			this.fingers = value;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000034 RID: 52 RVA: 0x00002AE0 File Offset: 0x00000CE0
	// (set) Token: 0x06000035 RID: 53 RVA: 0x00002AE8 File Offset: 0x00000CE8
	public GestureRecognizer Recognizer
	{
		get
		{
			return this.recognizer;
		}
		internal set
		{
			this.recognizer = value;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AF4 File Offset: 0x00000CF4
	// (set) Token: 0x06000037 RID: 55 RVA: 0x00002AFC File Offset: 0x00000CFC
	public float StartTime
	{
		get
		{
			return this.startTime;
		}
		internal set
		{
			this.startTime = value;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000038 RID: 56 RVA: 0x00002B08 File Offset: 0x00000D08
	// (set) Token: 0x06000039 RID: 57 RVA: 0x00002B10 File Offset: 0x00000D10
	public Vector2 StartPosition
	{
		get
		{
			return this.startPosition;
		}
		internal set
		{
			this.startPosition = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00002B1C File Offset: 0x00000D1C
	// (set) Token: 0x0600003B RID: 59 RVA: 0x00002B24 File Offset: 0x00000D24
	public Vector2 Position
	{
		get
		{
			return this.position;
		}
		internal set
		{
			this.position = value;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B30 File Offset: 0x00000D30
	// (set) Token: 0x0600003D RID: 61 RVA: 0x00002B38 File Offset: 0x00000D38
	public GestureRecognitionState State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state != value)
			{
				this.prevState = this.state;
				this.state = value;
				if (this.OnStateChanged != null)
				{
					this.OnStateChanged(this);
				}
			}
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600003E RID: 62 RVA: 0x00002B7C File Offset: 0x00000D7C
	public GestureRecognitionState PreviousState
	{
		get
		{
			return this.prevState;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B84 File Offset: 0x00000D84
	public float ElapsedTime
	{
		get
		{
			return Time.time - this.StartTime;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B94 File Offset: 0x00000D94
	// (set) Token: 0x06000041 RID: 65 RVA: 0x00002B9C File Offset: 0x00000D9C
	public GameObject StartSelection
	{
		get
		{
			return this.startSelection;
		}
		internal set
		{
			this.startSelection = value;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BA8 File Offset: 0x00000DA8
	// (set) Token: 0x06000043 RID: 67 RVA: 0x00002BB0 File Offset: 0x00000DB0
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

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000044 RID: 68 RVA: 0x00002BBC File Offset: 0x00000DBC
	// (set) Token: 0x06000045 RID: 69 RVA: 0x00002BC4 File Offset: 0x00000DC4
	public ScreenRaycastData Raycast
	{
		get
		{
			return this.lastRaycast;
		}
		internal set
		{
			this.lastRaycast = value;
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002BD0 File Offset: 0x00000DD0
	internal GameObject PickObject(ScreenRaycaster raycaster, Vector2 screenPos)
	{
		if (!raycaster || !raycaster.enabled)
		{
			return null;
		}
		if (!raycaster.Raycast(screenPos, out this.lastRaycast))
		{
			return null;
		}
		return this.lastRaycast.GameObject;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002C14 File Offset: 0x00000E14
	internal void PickStartSelection(ScreenRaycaster raycaster)
	{
		this.StartSelection = this.PickObject(raycaster, this.StartPosition);
		this.Selection = this.StartSelection;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002C40 File Offset: 0x00000E40
	internal void PickSelection(ScreenRaycaster raycaster)
	{
		this.Selection = this.PickObject(raycaster, this.Position);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002C58 File Offset: 0x00000E58
	public static implicit operator bool(Gesture gesture)
	{
		return gesture != null;
	}

	// Token: 0x04000024 RID: 36
	internal int ClusterId;

	// Token: 0x04000025 RID: 37
	private GestureRecognizer recognizer;

	// Token: 0x04000026 RID: 38
	private float startTime;

	// Token: 0x04000027 RID: 39
	private Vector2 startPosition = Vector2.zero;

	// Token: 0x04000028 RID: 40
	private Vector2 position = Vector2.zero;

	// Token: 0x04000029 RID: 41
	private GestureRecognitionState state;

	// Token: 0x0400002A RID: 42
	private GestureRecognitionState prevState;

	// Token: 0x0400002B RID: 43
	private FingerGestures.FingerList fingers = new FingerGestures.FingerList();

	// Token: 0x0400002C RID: 44
	private GameObject startSelection;

	// Token: 0x0400002D RID: 45
	private GameObject selection;

	// Token: 0x0400002E RID: 46
	private ScreenRaycastData lastRaycast = default(ScreenRaycastData);

	// Token: 0x02000061 RID: 97
	// (Invoke) Token: 0x06000277 RID: 631
	public delegate void EventHandler(Gesture gesture);
}
