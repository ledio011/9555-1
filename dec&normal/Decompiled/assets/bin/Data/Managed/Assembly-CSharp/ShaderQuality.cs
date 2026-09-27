using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Shader Quality")]
public class ShaderQuality : MonoBehaviour
{
	// Token: 0x060000D1 RID: 209 RVA: 0x000063D8 File Offset: 0x000045D8
	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (this.mCurrent != num)
		{
			this.mCurrent = num;
			Shader.globalMaximumLOD = this.mCurrent;
		}
	}

	// Token: 0x040000ED RID: 237
	private int mCurrent = 600;
}
