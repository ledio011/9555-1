using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class MountCarMeshRoot : MonoBehaviour
{
	// Token: 0x06000A0E RID: 2574 RVA: 0x00049868 File Offset: 0x00047A68
	public void ChangeColor(ColorData colordata)
	{
		if (this.mCarMaterial == null)
		{
			this.mCarMaterial = new Material(this.CarBodyRoot.gameObject.renderer.material);
			Shader shader = Shader.Find(this.mCarMaterial.shader.name);
			if (shader != null)
			{
				this.mCarMaterial.shader = shader;
			}
			else
			{
				Debug.Log("unable to refresh shader: in material " + this.mCarMaterial.name);
			}
			this.CarBodyRoot.gameObject.renderer.sharedMaterial = this.mCarMaterial;
			for (int i = 0; i < this.CarBodyRoot.transform.childCount; i++)
			{
				this.CarBodyRoot.transform.GetChild(i).gameObject.renderer.sharedMaterial = this.mCarMaterial;
			}
			if (this.QLWheel.gameObject.renderer != null)
			{
				this.QLWheel.gameObject.renderer.sharedMaterial = this.mCarMaterial;
				this.QRWheel.gameObject.renderer.sharedMaterial = this.mCarMaterial;
				this.HLWheel.gameObject.renderer.sharedMaterial = this.mCarMaterial;
				this.HRWheel.gameObject.renderer.sharedMaterial = this.mCarMaterial;
			}
		}
		if (this.mCarMaterial != null)
		{
			this.mCarMaterial.SetColor("_Color", colordata.CShaderColor);
			this.mCarMaterial.SetColor("_RimColor", colordata.CShaderRimColor);
			this.mCarMaterial.SetFloat("_ReflAmount", colordata.ShaderReflAmount);
			this.mCarMaterial.SetFloat("_RimPower", colordata.ShaderRimPower);
		}
		else
		{
			Debug.Log("car material is null");
		}
	}

	// Token: 0x040008DE RID: 2270
	public Transform PlayerRoot;

	// Token: 0x040008DF RID: 2271
	public Transform QLWheel;

	// Token: 0x040008E0 RID: 2272
	public Transform QRWheel;

	// Token: 0x040008E1 RID: 2273
	public Transform HLWheel;

	// Token: 0x040008E2 RID: 2274
	public Transform HRWheel;

	// Token: 0x040008E3 RID: 2275
	public Transform CarBodyRoot;

	// Token: 0x040008E4 RID: 2276
	public GameObject CarShadow;

	// Token: 0x040008E5 RID: 2277
	public float WheelRadius;

	// Token: 0x040008E6 RID: 2278
	public Vector3 ColliderCenter;

	// Token: 0x040008E7 RID: 2279
	public Vector3 ColliderSize;

	// Token: 0x040008E8 RID: 2280
	private Material mCarMaterial;
}
