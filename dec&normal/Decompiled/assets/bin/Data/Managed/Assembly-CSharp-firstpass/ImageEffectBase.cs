using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public class ImageEffectBase : MonoBehaviour
{
	// Token: 0x06000255 RID: 597 RVA: 0x00009F68 File Offset: 0x00008168
	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000256 RID: 598 RVA: 0x00009FB0 File Offset: 0x000081B0
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.shader);
				this.m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00009FE8 File Offset: 0x000081E8
	protected virtual void OnDisable()
	{
		if (this.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x040001C7 RID: 455
	public Shader shader;

	// Token: 0x040001C8 RID: 456
	private Material m_Material;
}
