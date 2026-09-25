using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000095 RID: 149
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060003BF RID: 959 RVA: 0x0001B700 File Offset: 0x00019900
	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001B708 File Offset: 0x00019908
	public static BetterList<UIDrawCall> activeList
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001B710 File Offset: 0x00019910
	public static BetterList<UIDrawCall> inactiveList
	{
		get
		{
			return UIDrawCall.mInactiveList;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001B718 File Offset: 0x00019918
	// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001B720 File Offset: 0x00019920
	public int renderQueue
	{
		get
		{
			return this.mRenderQueue;
		}
		set
		{
			if (this.mRenderQueue != value)
			{
				this.mRenderQueue = value;
				if (this.mDynamicMat != null)
				{
					this.mDynamicMat.renderQueue = value;
				}
			}
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001B760 File Offset: 0x00019960
	// (set) Token: 0x060003C5 RID: 965 RVA: 0x0001B790 File Offset: 0x00019990
	public int sortingOrder
	{
		get
		{
			return (!(this.mRenderer != null)) ? 0 : this.mRenderer.sortingOrder;
		}
		set
		{
			if (this.mRenderer != null && this.mRenderer.sortingOrder != value)
			{
				this.mRenderer.sortingOrder = value;
			}
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001B7CC File Offset: 0x000199CC
	public int finalRenderQueue
	{
		get
		{
			return (!(this.mDynamicMat != null)) ? this.mRenderQueue : this.mDynamicMat.renderQueue;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001B7F8 File Offset: 0x000199F8
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001B820 File Offset: 0x00019A20
	// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001B828 File Offset: 0x00019A28
	public Material baseMaterial
	{
		get
		{
			return this.mMaterial;
		}
		set
		{
			if (this.mMaterial != value)
			{
				this.mMaterial = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060003CA RID: 970 RVA: 0x0001B84C File Offset: 0x00019A4C
	public Material dynamicMaterial
	{
		get
		{
			return this.mDynamicMat;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060003CB RID: 971 RVA: 0x0001B854 File Offset: 0x00019A54
	// (set) Token: 0x060003CC RID: 972 RVA: 0x0001B85C File Offset: 0x00019A5C
	public Texture mainTexture
	{
		get
		{
			return this.mTexture;
		}
		set
		{
			this.mTexture = value;
			if (this.mDynamicMat != null)
			{
				this.mDynamicMat.mainTexture = value;
			}
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060003CD RID: 973 RVA: 0x0001B890 File Offset: 0x00019A90
	// (set) Token: 0x060003CE RID: 974 RVA: 0x0001B898 File Offset: 0x00019A98
	public Shader shader
	{
		get
		{
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				this.mShader = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060003CF RID: 975 RVA: 0x0001B8BC File Offset: 0x00019ABC
	public int triangles
	{
		get
		{
			return (!(this.mMesh != null)) ? 0 : this.mTriangles;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001B8DC File Offset: 0x00019ADC
	public bool isClipped
	{
		get
		{
			return this.mClipCount != 0;
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0001B8EC File Offset: 0x00019AEC
	private void CreateMaterial()
	{
		string text = (!(this.mShader != null)) ? ((!(this.mMaterial != null)) ? "Unlit/Transparent Colored" : this.mMaterial.shader.name) : this.mShader.name;
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text.get_Chars(text.Length - 2) == ' ')
		{
			int num = (int)text.get_Chars(text.Length - 1);
			if (num > 48 && num <= 57)
			{
				text = text.Substring(0, text.Length - 2);
			}
		}
		if (text.StartsWith("Hidden/"))
		{
			text = text.Substring(7);
		}
		text = text.Replace(" (SoftClip)", string.Empty);
		this.mLegacyShader = false;
		this.mClipCount = this.panel.clipCount;
		Shader shader;
		if (this.mClipCount != 0)
		{
			shader = Shader.Find(string.Concat(new object[]
			{
				"Hidden/",
				text,
				" ",
				this.mClipCount
			}));
			if (shader == null)
			{
				Shader.Find(text + " " + this.mClipCount);
			}
			if (shader == null && this.mClipCount == 1)
			{
				this.mLegacyShader = true;
				shader = Shader.Find(text + " (SoftClip)");
			}
		}
		else
		{
			shader = Shader.Find(text);
		}
		if (this.mMaterial != null)
		{
			this.mDynamicMat = new Material(this.mMaterial);
			this.mDynamicMat.hideFlags = 12;
			this.mDynamicMat.CopyPropertiesFromMaterial(this.mMaterial);
			string[] shaderKeywords = this.mMaterial.shaderKeywords;
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				this.mDynamicMat.EnableKeyword(shaderKeywords[i]);
			}
		}
		else
		{
			this.mDynamicMat = new Material(shader);
			this.mDynamicMat.hideFlags = 12;
		}
		if (shader != null)
		{
			this.mDynamicMat.shader = shader;
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				text,
				" shader doesn't have a clipped shader version for ",
				this.mClipCount,
				" clip regions"
			}));
		}
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0001BB70 File Offset: 0x00019D70
	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.CreateMaterial();
		this.mDynamicMat.renderQueue = this.mRenderQueue;
		if (this.mTexture != null)
		{
			this.mDynamicMat.mainTexture = this.mTexture;
		}
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[]
			{
				this.mDynamicMat
			};
		}
		return this.mDynamicMat;
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001BBF4 File Offset: 0x00019DF4
	private void UpdateMaterials()
	{
		if (this.mRebuildMat || this.mDynamicMat == null || this.mClipCount != this.panel.clipCount)
		{
			this.RebuildMaterial();
			this.mRebuildMat = false;
		}
		else if (this.mRenderer.sharedMaterial != this.mDynamicMat)
		{
			this.mRenderer.sharedMaterials = new Material[]
			{
				this.mDynamicMat
			};
		}
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0001BC7C File Offset: 0x00019E7C
	public void UpdateGeometry()
	{
		int size = this.verts.size;
		if (size > 0 && size == this.uvs.size && size == this.cols.size && size % 4 == 0)
		{
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.verts.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (this.mMesh == null)
				{
					this.mMesh = new Mesh();
					this.mMesh.hideFlags = 4;
					this.mMesh.name = ((!(this.mMaterial != null)) ? "Mesh" : this.mMaterial.name);
					this.mMesh.MarkDynamic();
					flag = true;
				}
				bool flag2 = this.uvs.buffer.Length != this.verts.buffer.Length || this.cols.buffer.Length != this.verts.buffer.Length || (this.norms.buffer != null && this.norms.buffer.Length != this.verts.buffer.Length) || (this.tans.buffer != null && this.tans.buffer.Length != this.verts.buffer.Length);
				if (!flag2 && this.panel.renderQueue != UIPanel.RenderQueue.Automatic)
				{
					flag2 = (this.mMesh == null || this.mMesh.vertexCount != this.verts.buffer.Length);
				}
				if (!flag2 && this.verts.size << 1 < this.verts.buffer.Length)
				{
					flag2 = true;
				}
				this.mTriangles = this.verts.size >> 1;
				if (flag2 || this.verts.buffer.Length > 65000)
				{
					if (flag2 || this.mMesh.vertexCount != this.verts.size)
					{
						this.mMesh.Clear();
						flag = true;
					}
					this.mMesh.vertices = this.verts.ToArray();
					this.mMesh.uv = this.uvs.ToArray();
					this.mMesh.colors32 = this.cols.ToArray();
					if (this.norms != null)
					{
						this.mMesh.normals = this.norms.ToArray();
					}
					if (this.tans != null)
					{
						this.mMesh.tangents = this.tans.ToArray();
					}
				}
				else
				{
					if (this.mMesh.vertexCount != this.verts.buffer.Length)
					{
						this.mMesh.Clear();
						flag = true;
					}
					this.mMesh.vertices = this.verts.buffer;
					this.mMesh.uv = this.uvs.buffer;
					this.mMesh.colors32 = this.cols.buffer;
					if (this.norms != null)
					{
						this.mMesh.normals = this.norms.buffer;
					}
					if (this.tans != null)
					{
						this.mMesh.tangents = this.tans.buffer;
					}
				}
				if (flag)
				{
					this.mIndices = this.GenerateCachedIndexBuffer(size, num);
					this.mMesh.triangles = this.mIndices;
				}
				if (flag2 || !this.alwaysOnScreen)
				{
					this.mMesh.RecalculateBounds();
				}
				this.mFilter.mesh = this.mMesh;
			}
			else
			{
				this.mTriangles = 0;
				if (this.mFilter.mesh != null)
				{
					this.mFilter.mesh.Clear();
				}
				Debug.LogError("Too many vertices on one panel: " + this.verts.size);
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.AddComponent<MeshRenderer>();
			}
			this.UpdateMaterials();
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
		}
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.norms.Clear();
		this.tans.Clear();
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
	private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
	{
		int i = 0;
		int count = UIDrawCall.mCache.Count;
		while (i < count)
		{
			int[] array = UIDrawCall.mCache[i];
			if (array != null && array.Length == indexCount)
			{
				return array;
			}
			i++;
		}
		int[] array2 = new int[indexCount];
		int num = 0;
		for (int j = 0; j < vertexCount; j += 4)
		{
			array2[num++] = j;
			array2[num++] = j + 1;
			array2[num++] = j + 2;
			array2[num++] = j + 2;
			array2[num++] = j + 3;
			array2[num++] = j;
		}
		if (UIDrawCall.mCache.Count > 10)
		{
			UIDrawCall.mCache.RemoveAt(0);
		}
		UIDrawCall.mCache.Add(array2);
		return array2;
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0001C290 File Offset: 0x0001A490
	private void OnWillRenderObject()
	{
		this.UpdateMaterials();
		if (this.onRender != null)
		{
			this.onRender(this.mDynamicMat ?? this.mMaterial);
		}
		if (this.mDynamicMat == null || this.mClipCount == 0)
		{
			return;
		}
		if (!this.mLegacyShader)
		{
			UIPanel parentPanel = this.panel;
			int num = 0;
			while (parentPanel != null)
			{
				if (parentPanel.hasClipping)
				{
					float angle = 0f;
					Vector4 drawCallClipRange = parentPanel.drawCallClipRange;
					if (parentPanel != this.panel)
					{
						Vector3 vector = parentPanel.cachedTransform.InverseTransformPoint(this.panel.cachedTransform.position);
						drawCallClipRange.x -= vector.x;
						drawCallClipRange.y -= vector.y;
						Vector3 eulerAngles = this.panel.cachedTransform.rotation.eulerAngles;
						Vector3 eulerAngles2 = parentPanel.cachedTransform.rotation.eulerAngles;
						Vector3 vector2 = eulerAngles2 - eulerAngles;
						vector2.x = NGUIMath.WrapAngle(vector2.x);
						vector2.y = NGUIMath.WrapAngle(vector2.y);
						vector2.z = NGUIMath.WrapAngle(vector2.z);
						if (Mathf.Abs(vector2.x) > 0.001f || Mathf.Abs(vector2.y) > 0.001f)
						{
							Debug.LogWarning("Panel can only be clipped properly if X and Y rotation is left at 0", this.panel);
						}
						angle = vector2.z;
					}
					this.SetClipping(num++, drawCallClipRange, parentPanel.clipSoftness, angle);
				}
				parentPanel = parentPanel.parentPanel;
			}
		}
		else
		{
			Vector2 clipSoftness = this.panel.clipSoftness;
			Vector4 drawCallClipRange2 = this.panel.drawCallClipRange;
			Vector2 mainTextureOffset;
			mainTextureOffset..ctor(-drawCallClipRange2.x / drawCallClipRange2.z, -drawCallClipRange2.y / drawCallClipRange2.w);
			Vector2 mainTextureScale;
			mainTextureScale..ctor(1f / drawCallClipRange2.z, 1f / drawCallClipRange2.w);
			Vector2 vector3;
			vector3..ctor(1000f, 1000f);
			if (clipSoftness.x > 0f)
			{
				vector3.x = drawCallClipRange2.z / clipSoftness.x;
			}
			if (clipSoftness.y > 0f)
			{
				vector3.y = drawCallClipRange2.w / clipSoftness.y;
			}
			this.mDynamicMat.mainTextureOffset = mainTextureOffset;
			this.mDynamicMat.mainTextureScale = mainTextureScale;
			this.mDynamicMat.SetVector("_ClipSharpness", vector3);
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0001C548 File Offset: 0x0001A748
	private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
	{
		angle *= -0.017453292f;
		Vector2 vector;
		vector..ctor(1000f, 1000f);
		if (soft.x > 0f)
		{
			vector.x = cr.z / soft.x;
		}
		if (soft.y > 0f)
		{
			vector.y = cr.w / soft.y;
		}
		if (index < UIDrawCall.ClipRange.Length)
		{
			this.mDynamicMat.SetVector(UIDrawCall.ClipRange[index], new Vector4(-cr.x / cr.z, -cr.y / cr.w, 1f / cr.z, 1f / cr.w));
			this.mDynamicMat.SetVector(UIDrawCall.ClipArgs[index], new Vector4(vector.x, vector.y, Mathf.Sin(angle), Mathf.Cos(angle)));
		}
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001C650 File Offset: 0x0001A850
	private void OnEnable()
	{
		this.mRebuildMat = true;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001C65C File Offset: 0x0001A85C
	private void OnDisable()
	{
		this.depthStart = int.MaxValue;
		this.depthEnd = int.MinValue;
		this.panel = null;
		this.manager = null;
		this.mMaterial = null;
		this.mTexture = null;
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.mDynamicMat = null;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return UIDrawCall.Create(null, panel, mat, tex, shader);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x0001C6CC File Offset: 0x0001A8CC
	private static UIDrawCall Create(string name, UIPanel pan, Material mat, Texture tex, Shader shader)
	{
		UIDrawCall uidrawCall = UIDrawCall.Create(name);
		uidrawCall.gameObject.layer = pan.cachedGameObject.layer;
		uidrawCall.baseMaterial = mat;
		uidrawCall.mainTexture = tex;
		uidrawCall.shader = shader;
		uidrawCall.renderQueue = pan.startingRenderQueue;
		uidrawCall.sortingOrder = pan.sortingOrder;
		uidrawCall.manager = pan;
		return uidrawCall;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0001C72C File Offset: 0x0001A92C
	private static UIDrawCall Create(string name)
	{
		if (UIDrawCall.mInactiveList.size > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList.Pop();
			UIDrawCall.mActiveList.Add(uidrawCall);
			if (name != null)
			{
				uidrawCall.name = name;
			}
			NGUITools.SetActive(uidrawCall.gameObject, true);
			return uidrawCall;
		}
		GameObject gameObject = new GameObject(name);
		Object.DontDestroyOnLoad(gameObject);
		UIDrawCall uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
		UIDrawCall.mActiveList.Add(uidrawCall2);
		return uidrawCall2;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0001C79C File Offset: 0x0001A99C
	public static void ClearAll()
	{
		bool isPlaying = Application.isPlaying;
		int i = UIDrawCall.mActiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mActiveList[--i];
			if (uidrawCall)
			{
				if (isPlaying)
				{
					NGUITools.SetActive(uidrawCall.gameObject, false);
				}
				else
				{
					NGUITools.DestroyImmediate(uidrawCall.gameObject);
				}
			}
		}
		UIDrawCall.mActiveList.Clear();
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0001C810 File Offset: 0x0001AA10
	public static void ReleaseAll()
	{
		UIDrawCall.ClearAll();
		UIDrawCall.ReleaseInactive();
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001C81C File Offset: 0x0001AA1C
	public static void ReleaseInactive()
	{
		int i = UIDrawCall.mInactiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList[--i];
			if (uidrawCall)
			{
				NGUITools.DestroyImmediate(uidrawCall.gameObject);
			}
		}
		UIDrawCall.mInactiveList.Clear();
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001C870 File Offset: 0x0001AA70
	public static int Count(UIPanel panel)
	{
		int num = 0;
		for (int i = 0; i < UIDrawCall.mActiveList.size; i++)
		{
			if (UIDrawCall.mActiveList[i].manager == panel)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0001C8BC File Offset: 0x0001AABC
	public static void Destroy(UIDrawCall dc)
	{
		if (dc)
		{
			if (Application.isPlaying)
			{
				if (UIDrawCall.mActiveList.Remove(dc))
				{
					NGUITools.SetActive(dc.gameObject, false);
					UIDrawCall.mInactiveList.Add(dc);
				}
			}
			else
			{
				UIDrawCall.mActiveList.Remove(dc);
				NGUITools.DestroyImmediate(dc.gameObject);
			}
		}
	}

	// Token: 0x04000379 RID: 889
	private const int maxIndexBufferCache = 10;

	// Token: 0x0400037A RID: 890
	public UIDrawCall.OnRenderCallback onRender;

	// Token: 0x0400037B RID: 891
	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	// Token: 0x0400037C RID: 892
	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	// Token: 0x0400037D RID: 893
	[HideInInspector]
	[NonSerialized]
	public int depthStart = int.MaxValue;

	// Token: 0x0400037E RID: 894
	[HideInInspector]
	[NonSerialized]
	public int depthEnd = int.MinValue;

	// Token: 0x0400037F RID: 895
	[HideInInspector]
	[NonSerialized]
	public UIPanel manager;

	// Token: 0x04000380 RID: 896
	[HideInInspector]
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x04000381 RID: 897
	[HideInInspector]
	[NonSerialized]
	public bool alwaysOnScreen;

	// Token: 0x04000382 RID: 898
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x04000383 RID: 899
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector3> norms = new BetterList<Vector3>();

	// Token: 0x04000384 RID: 900
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector4> tans = new BetterList<Vector4>();

	// Token: 0x04000385 RID: 901
	[HideInInspector]
	[NonSerialized]
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x04000386 RID: 902
	[HideInInspector]
	[NonSerialized]
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x04000387 RID: 903
	private Material mMaterial;

	// Token: 0x04000388 RID: 904
	private Texture mTexture;

	// Token: 0x04000389 RID: 905
	private Shader mShader;

	// Token: 0x0400038A RID: 906
	private int mClipCount;

	// Token: 0x0400038B RID: 907
	private Transform mTrans;

	// Token: 0x0400038C RID: 908
	private Mesh mMesh;

	// Token: 0x0400038D RID: 909
	private MeshFilter mFilter;

	// Token: 0x0400038E RID: 910
	private MeshRenderer mRenderer;

	// Token: 0x0400038F RID: 911
	private Material mDynamicMat;

	// Token: 0x04000390 RID: 912
	private int[] mIndices;

	// Token: 0x04000391 RID: 913
	private bool mRebuildMat = true;

	// Token: 0x04000392 RID: 914
	private bool mLegacyShader;

	// Token: 0x04000393 RID: 915
	private int mRenderQueue = 3000;

	// Token: 0x04000394 RID: 916
	private int mTriangles;

	// Token: 0x04000395 RID: 917
	[NonSerialized]
	public bool isDirty;

	// Token: 0x04000396 RID: 918
	private static List<int[]> mCache = new List<int[]>(10);

	// Token: 0x04000397 RID: 919
	private static string[] ClipRange = new string[]
	{
		"_ClipRange0",
		"_ClipRange1",
		"_ClipRange2",
		"_ClipRange4"
	};

	// Token: 0x04000398 RID: 920
	private static string[] ClipArgs = new string[]
	{
		"_ClipArgs0",
		"_ClipArgs1",
		"_ClipArgs2",
		"_ClipArgs3"
	};

	// Token: 0x02000096 RID: 150
	public enum Clipping
	{
		// Token: 0x0400039A RID: 922
		None,
		// Token: 0x0400039B RID: 923
		SoftClip = 3,
		// Token: 0x0400039C RID: 924
		ConstrainButDontClip
	}

	// Token: 0x02000A9D RID: 2717
	// (Invoke) Token: 0x06004EFD RID: 20221
	public delegate void OnRenderCallback(Material mat);
}
