using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class RealTime : MonoBehaviour
{
	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001B3AC File Offset: 0x000195AC
	public static float time
	{
		get
		{
			if (RealTime.mInst == null)
			{
				RealTime.Spawn();
			}
			return RealTime.mInst.mRealTime;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001B3D0 File Offset: 0x000195D0
	public static float deltaTime
	{
		get
		{
			if (RealTime.mInst == null)
			{
				RealTime.Spawn();
			}
			return RealTime.mInst.mRealDelta;
		}
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001B3F4 File Offset: 0x000195F4
	private static void Spawn()
	{
		GameObject gameObject = new GameObject("_RealTime");
		Object.DontDestroyOnLoad(gameObject);
		RealTime.mInst = gameObject.AddComponent<RealTime>();
		RealTime.mInst.mRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001B42C File Offset: 0x0001962C
	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.mRealDelta = Mathf.Clamp01(realtimeSinceStartup - this.mRealTime);
		this.mRealTime = realtimeSinceStartup;
	}

	// Token: 0x0400036F RID: 879
	private static RealTime mInst;

	// Token: 0x04000370 RID: 880
	private float mRealTime;

	// Token: 0x04000371 RID: 881
	private float mRealDelta;
}
