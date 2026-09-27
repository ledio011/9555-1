using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Camera Info")]
[Serializable]
public class CameraInfo : MonoBehaviour
{
	// Token: 0x0600005A RID: 90 RVA: 0x00005628 File Offset: 0x00003828
	public virtual void Main()
	{
	}

	// Token: 0x040000CF RID: 207
	public DepthTextureMode currentDepthMode;

	// Token: 0x040000D0 RID: 208
	public RenderingPath currentRenderPath;

	// Token: 0x040000D1 RID: 209
	public int recognizedPostFxCount;
}
