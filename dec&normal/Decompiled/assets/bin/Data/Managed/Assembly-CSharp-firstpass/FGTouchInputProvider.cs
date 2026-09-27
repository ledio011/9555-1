using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class FGTouchInputProvider : FGInputProvider
{
	// Token: 0x060001A3 RID: 419 RVA: 0x00007418 File Offset: 0x00005618
	private void Start()
	{
		this.finger2touchMap = new int[this.maxTouches];
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000742C File Offset: 0x0000562C
	private void Update()
	{
		this.UpdateFingerTouchMap();
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00007434 File Offset: 0x00005634
	private void UpdateFingerTouchMap()
	{
		for (int i = 0; i < this.finger2touchMap.Length; i++)
		{
			this.finger2touchMap[i] = -1;
		}
		if (this.fixAndroidTouchIdBug && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
		{
			this.touchIdOffset = Input.touches[0].fingerId;
		}
		for (int j = 0; j < Input.touchCount; j++)
		{
			int num = Input.touches[j].fingerId - this.touchIdOffset;
			if (num < this.finger2touchMap.Length)
			{
				this.finger2touchMap[num] = j;
			}
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000074EC File Offset: 0x000056EC
	private bool HasValidTouch(int fingerIndex)
	{
		return this.finger2touchMap[fingerIndex] != -1;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x000074FC File Offset: 0x000056FC
	private Touch GetTouch(int fingerIndex)
	{
		int num = this.finger2touchMap[fingerIndex];
		if (num == -1)
		{
			return this.nullTouch;
		}
		return Input.touches[num];
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007530 File Offset: 0x00005730
	public override int MaxSimultaneousFingers
	{
		get
		{
			return this.maxTouches;
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00007538 File Offset: 0x00005738
	public override void GetInputState(int fingerIndex, out bool down, out Vector2 position)
	{
		down = false;
		position = Vector2.zero;
		if (this.HasValidTouch(fingerIndex))
		{
			Touch touch = this.GetTouch(fingerIndex);
			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				down = false;
			}
			else
			{
				down = true;
				position = touch.position;
			}
		}
	}

	// Token: 0x04000113 RID: 275
	public int maxTouches = 5;

	// Token: 0x04000114 RID: 276
	public bool fixAndroidTouchIdBug = true;

	// Token: 0x04000115 RID: 277
	private int touchIdOffset;

	// Token: 0x04000116 RID: 278
	private Touch nullTouch = default(Touch);

	// Token: 0x04000117 RID: 279
	private int[] finger2touchMap;
}
