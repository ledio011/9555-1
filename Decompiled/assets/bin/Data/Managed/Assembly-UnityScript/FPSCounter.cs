using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
[Serializable]
public class FPSCounter : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	public FPSCounter()
	{
		this.updateInterval = 0.5f;
		this.x_location = 5;
		this.y_location = 5;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002110 File Offset: 0x00000310
	public virtual void Awake()
	{
		this.useGUILayout = false;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
	public virtual void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width - this.x_location), (float)(Screen.height - this.y_location), (float)100, (float)30), "FPS: " + this.fps.ToString("f2"));
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
	public virtual void Start()
	{
		this.lastInterval = (double)Time.realtimeSinceStartup;
		this.frames = 0;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002188 File Offset: 0x00000388
	public virtual void Update()
	{
		this.frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if ((double)realtimeSinceStartup > this.lastInterval + (double)this.updateInterval)
		{
			this.fps = (float)((double)this.frames / ((double)realtimeSinceStartup - this.lastInterval));
			this.frames = 0;
			this.lastInterval = (double)realtimeSinceStartup;
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000021E8 File Offset: 0x000003E8
	public virtual void Main()
	{
	}

	// Token: 0x04000001 RID: 1
	public float updateInterval;

	// Token: 0x04000002 RID: 2
	public int x_location;

	// Token: 0x04000003 RID: 3
	public int y_location;

	// Token: 0x04000004 RID: 4
	private double lastInterval;

	// Token: 0x04000005 RID: 5
	private int frames;

	// Token: 0x04000006 RID: 6
	private float fps;
}
