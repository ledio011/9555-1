using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class RealTimeShadow : SingletonUnity<RealTimeShadow>
{
	// Token: 0x170003AC RID: 940
	// (get) Token: 0x0600117F RID: 4479 RVA: 0x000714B4 File Offset: 0x0006F6B4
	private Material shadowMaterial
	{
		get
		{
			if (this.m_ShadowMaterial == null)
			{
				this.m_ShadowMaterial = new Material(RealTimeShadow.shadowMatString);
			}
			return this.m_ShadowMaterial;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001180 RID: 4480 RVA: 0x000714E0 File Offset: 0x0006F6E0
	private Material projectorMaterial
	{
		get
		{
			if (this.m_ProjectorMaterial == null)
			{
				this.m_ProjectorMaterial = new Material(RealTimeShadow.projectorMatString);
			}
			return this.m_ProjectorMaterial;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001181 RID: 4481 RVA: 0x0007150C File Offset: 0x0006F70C
	private Material projectorNoFallOffMaterial
	{
		get
		{
			if (this.m_ProjectorNoFallOffMaterial == null)
			{
				this.m_ProjectorNoFallOffMaterial = new Material(RealTimeShadow.projectorMatNoFallOffString);
			}
			return this.m_ProjectorNoFallOffMaterial;
		}
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00071538 File Offset: 0x0006F738
	public void InitShadow()
	{
		this.mShadowPic = new RenderTexture(this.ShadowPicSizeW, this.ShadowPicSizeH, 0);
		if (SystemInfo.SupportsRenderTextureFormat(6))
		{
			this.mShadowPic.format = 6;
		}
		this.mShadowPic.useMipMap = false;
		GameObject gameObject = new GameObject("ShadowProjecter");
		this.mShadowProjectorTrans = gameObject.transform;
		this.mShadowProjectorTrans.rotation = Quaternion.Euler(this.ShadowAngle);
		this.mShadowShader = this.shadowMaterial.shader;
		this.mShadowCamera = gameObject.AddComponent<Camera>();
		this.mShadowCamera.clearFlags = 2;
		this.mShadowCamera.backgroundColor = Color.white;
		this.mShadowCamera.cullingMask = this.ShadowCasterLayer;
		this.mShadowCamera.orthographic = true;
		this.mShadowCamera.aspect = 1f;
		this.mShadowCamera.targetTexture = this.mShadowPic;
		this.mShadowCamera.SetReplacementShader(this.mShadowShader, "RenderType");
		Material material = (!(this.FalloffTex == null)) ? this.projectorMaterial : this.projectorNoFallOffMaterial;
		material.SetTexture("_ShadowTex", this.mShadowPic);
		if (this.FalloffTex != null)
		{
			material.SetTexture("_FalloffTex", this.FalloffTex);
		}
		this.mShadowProjector = gameObject.AddComponent<Projector>();
		this.mShadowProjector.orthographic = true;
		this.mShadowProjector.ignoreLayers = ~this.ShadowReceiverLayer;
		this.mShadowProjector.material = material;
		Shader.SetGlobalColor("_ShadowLightness", new Color(this.mLightNess, this.mLightNess, this.mLightNess, 0f));
		this.mShadowProjector.enabled = false;
		this.mShadowCamera.enabled = false;
		SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
		if (sceneManager != null && sceneManager.CurrentMapInofData != null)
		{
			this.mShadowProjector.transform.eulerAngles = sceneManager.CurrentMapInofData.ShadowDir;
		}
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x00071748 File Offset: 0x0006F948
	private new void Awake()
	{
		base.Awake();
		this.mRenderList = new List<Renderer>();
		this.InitShadow();
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00071764 File Offset: 0x0006F964
	public void Reset(GameObject targetObj)
	{
		this.TargetRenderObj = targetObj;
		this.mRenderList.Clear();
		Renderer[] componentsInChildren = this.TargetRenderObj.transform.GetChild(0).GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Debug.Log(string.Concat(new object[]
			{
				"renderMeshs :: ",
				i,
				" :: ",
				componentsInChildren[i].gameObject.name
			}));
			this.mRenderList.Add(componentsInChildren[i]);
		}
		Debug.Log("renderMeshs :: " + componentsInChildren.Length);
		Vector3 vector;
		Vector3 vector2;
		this.GetAllRendereBounds(out vector, out vector2);
		float num = vector2.x;
		if (vector2.y > num)
		{
			num = vector2.y;
		}
		if (vector2.z > num)
		{
			num = vector2.z;
		}
		this.mShadowCamera.orthographicSize = num;
		this.mShadowCamera.nearClipPlane = -num;
		this.mShadowCamera.farClipPlane = num;
		this.mShadowProjector.orthoGraphicSize = num;
		this.mShadowProjector.nearClipPlane = -num * 0.18f;
		this.mShadowProjector.farClipPlane = this.mShadowProjector.nearClipPlane + this.CastingDistance;
		this.EnableRealTimeShadow();
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x000718BC File Offset: 0x0006FABC
	public void Reset(List<GameObject> targetRenderObjList)
	{
		this.mRenderList.Clear();
		for (int i = 0; i < targetRenderObjList.Count; i++)
		{
			this.mRenderList.Add(targetRenderObjList[i].GetComponent<Renderer>());
		}
		Vector3 vector;
		Vector3 vector2;
		this.GetAllRendereBounds(out vector, out vector2);
		float num = vector2.x;
		if (vector2.y > num)
		{
			num = vector2.y;
		}
		if (vector2.z > num)
		{
			num = vector2.z;
		}
		this.mShadowCamera.orthographicSize = num;
		this.mShadowCamera.nearClipPlane = -num;
		this.mShadowCamera.farClipPlane = num;
		this.mShadowProjector.orthoGraphicSize = num;
		this.mShadowProjector.nearClipPlane = -num * 0.18f;
		this.mShadowProjector.farClipPlane = this.mShadowProjector.nearClipPlane + this.CastingDistance;
		this.EnableRealTimeShadow();
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x000719A8 File Offset: 0x0006FBA8
	public void EnableRealTimeShadow()
	{
		base.enabled = true;
		if (!this.mShadowProjector.enabled)
		{
			this.mShadowProjector.enabled = true;
		}
		if (!this.mShadowCamera.enabled)
		{
			this.mShadowCamera.enabled = true;
		}
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x000719F4 File Offset: 0x0006FBF4
	private void LateUpdate()
	{
		Vector3 position;
		Vector3 vector;
		if (this.GetAllRendereBounds(out position, out vector))
		{
			this.mShadowProjectorTrans.position = position;
		}
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00071A1C File Offset: 0x0006FC1C
	private bool GetAllRendereBounds(out Vector3 center, out Vector3 extents)
	{
		int count = this.mRenderList.Count;
		bool result = false;
		Vector3 vector;
		vector..ctor(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 vector2;
		vector2..ctor(float.MinValue, float.MinValue, float.MinValue);
		for (int i = 0; i < count; i++)
		{
			Renderer renderer = this.mRenderList[i];
			if (renderer != null)
			{
				Bounds bounds = renderer.bounds;
				Vector3 center2 = bounds.center;
				Vector3 extents2 = bounds.extents;
				float num = center2.x - extents2.x - this.extraWidth;
				if (num < vector.x)
				{
					vector.x = num;
				}
				float num2 = center2.x + extents2.x + this.extraWidth;
				if (num2 > vector2.x)
				{
					vector2.x = num2;
				}
				float num3 = center2.y - extents2.y - this.extraWidth;
				if (num3 < vector.y)
				{
					vector.y = num3;
				}
				float num4 = center2.y + extents2.y + this.extraWidth;
				if (num4 > vector2.y)
				{
					vector2.y = num4;
				}
				float num5 = center2.z + extents2.z + this.extraWidth;
				if (num5 > vector2.z)
				{
					vector2.z = num5;
				}
				float num6 = center2.z - extents2.z - this.extraWidth;
				if (num6 < vector.z)
				{
					vector.z = num6;
				}
				result = true;
			}
		}
		center.x = 0.5f * (vector2.x + vector.x);
		center.y = 0.5f * (vector2.y + vector.y);
		center.z = 0.5f * (vector2.z + vector.z);
		extents.x = 0.7f * (vector2.x - vector.x);
		extents.y = 0.7f * (vector2.y - vector.y);
		extents.z = 0.7f * (vector2.z - vector.z);
		return result;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x00071C70 File Offset: 0x0006FE70
	public void DisableRealTimeShadow()
	{
		base.enabled = false;
		if (this.mShadowProjector.enabled)
		{
			this.mShadowProjector.enabled = false;
		}
		if (this.mShadowCamera.enabled)
		{
			this.mShadowCamera.enabled = false;
		}
	}

	// Token: 0x04001752 RID: 5970
	private static string shadowMatString = "Shader \"Hidden/ShadowMat\" {\n\tProperties {\n\t\t_ShadowLightness (\"_ShadowLightness\", Color) = (0,0,0,0)\n\t}\n\tSubShader {\n\t\tTags { \"RenderType\" = \"RealTimeShadow\"}\n\t\tPass {\n\t\t\tCull Off ZWrite Off\n\t\t\tColor [_ShadowLightness]\n\t\t}\n\t}\n\tFallback off\n   }";

	// Token: 0x04001753 RID: 5971
	private Material m_ShadowMaterial;

	// Token: 0x04001754 RID: 5972
	private static string projectorMatString = "\tShader \"Hidden/ShadowProjectorMultiply\" { \n\tProperties {\n\t\t_ShadowTex (\"Cookie\", 2D) = \"white\" { TexGen ObjectLinear }\n\t\t_FalloffTex (\"FallOff\", 2D) = \"white\" { TexGen ObjectLinear\t}\n\t}\n\tSubshader {\n\t\tPass {\n\t\t\tZWrite off\n\t\t\tOffset -1, -1\n\t\t\tColorMask RGB\n\t\t\tBlend DstColor Zero\n\t\t\tSetTexture [_ShadowTex] {\n\t\t\t\tcombine texture\n\t\t\t\tMatrix [_Projector]\n\t\t\t}\n\t\t\tSetTexture [_FalloffTex] {\n\t\t\t\tconstantColor (1,1,1,0)\n\t\t\t\tcombine previous lerp (texture) constant\n\t\t\t\tMatrix [_ProjectorClip]\n\t\t\t}\n\t\t}\n\t}\n}";

	// Token: 0x04001755 RID: 5973
	private Material m_ProjectorMaterial;

	// Token: 0x04001756 RID: 5974
	private static string projectorMatNoFallOffString = "\tShader \"Hidden/ShadowProjectorMultiply\" { \n\tProperties {\n\t\t_ShadowTex (\"Cookie\", 2D) = \"white\" { TexGen ObjectLinear }\n\t\t_FalloffTex (\"FallOff\", 2D) = \"white\" { TexGen ObjectLinear\t}\n\t}\n\tSubshader {\n\t\tPass {\n\t\t\tZWrite off\n\t\t\tOffset -1, -1\n\t\t\tFog { Color (1, 1, 1) }\n\t\t\tColorMask RGB\n\t\t\tBlend DstColor Zero\n\t\t\tSetTexture [_ShadowTex] {\n\t\t\t\tcombine texture, ONE - texture\n\t\t\t\tMatrix [_Projector]\n\t\t\t}\n\t\t}\n\t}\n}";

	// Token: 0x04001757 RID: 5975
	private Material m_ProjectorNoFallOffMaterial;

	// Token: 0x04001758 RID: 5976
	public GameObject TargetRenderObj;

	// Token: 0x04001759 RID: 5977
	public LayerMask ShadowReceiverLayer = 8388608;

	// Token: 0x0400175A RID: 5978
	public LayerMask ShadowCasterLayer = int.MinValue;

	// Token: 0x0400175B RID: 5979
	public float CastingDistance = 10f;

	// Token: 0x0400175C RID: 5980
	public Texture FalloffTex;

	// Token: 0x0400175D RID: 5981
	private int ShadowPicSizeW = 256;

	// Token: 0x0400175E RID: 5982
	private int ShadowPicSizeH = 256;

	// Token: 0x0400175F RID: 5983
	private Vector3 ShadowAngle = new Vector3(55f, 70f, 0f);

	// Token: 0x04001760 RID: 5984
	private RenderTexture mShadowPic;

	// Token: 0x04001761 RID: 5985
	private Transform mShadowProjectorTrans;

	// Token: 0x04001762 RID: 5986
	private Shader mShadowShader;

	// Token: 0x04001763 RID: 5987
	private Camera mShadowCamera;

	// Token: 0x04001764 RID: 5988
	private Projector mShadowProjector;

	// Token: 0x04001765 RID: 5989
	private List<Renderer> mRenderList;

	// Token: 0x04001766 RID: 5990
	private float mLightNess = 0.7f;

	// Token: 0x04001767 RID: 5991
	private bool initFlag;

	// Token: 0x04001768 RID: 5992
	private float extraWidth = 0.6f;
}
