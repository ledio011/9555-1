using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000AF RID: 175
	public class Material : Object
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public Material(string contents)
		{
			Material.Internal_CreateWithString(this, contents);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public Material(Shader shader)
		{
			Material.Internal_CreateWithShader(this, shader);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00011A8C File Offset: 0x0000FC8C
		public Material(Material source)
		{
			Material.Internal_CreateWithMaterial(this, source);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000739 RID: 1849
		// (set) Token: 0x0600073A RID: 1850
		public extern Shader shader { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00011A9C File Offset: 0x0000FC9C
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public Color color
		{
			get
			{
				return this.GetColor("_Color");
			}
			set
			{
				this.SetColor("_Color", value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00011ABC File Offset: 0x0000FCBC
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00011ACC File Offset: 0x0000FCCC
		public Texture mainTexture
		{
			get
			{
				return this.GetTexture("_MainTex");
			}
			set
			{
				this.SetTexture("_MainTex", value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x00011ADC File Offset: 0x0000FCDC
		public Vector2 mainTextureOffset
		{
			set
			{
				this.SetTextureOffset("_MainTex", value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public Vector2 mainTextureScale
		{
			set
			{
				this.SetTextureScale("_MainTex", value);
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00011AFC File Offset: 0x0000FCFC
		public void SetColor(string propertyName, Color color)
		{
			this.SetColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public void SetColor(int nameID, Color color)
		{
			Material.INTERNAL_CALL_SetColor(this, nameID, ref color);
		}

		// Token: 0x06000743 RID: 1859
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetColor(Material self, int nameID, ref Color color);

		// Token: 0x06000744 RID: 1860 RVA: 0x00011B18 File Offset: 0x0000FD18
		public Color GetColor(string propertyName)
		{
			return this.GetColor(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000745 RID: 1861
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color GetColor(int nameID);

		// Token: 0x06000746 RID: 1862 RVA: 0x00011B28 File Offset: 0x0000FD28
		public void SetVector(string propertyName, Vector4 vector)
		{
			this.SetColor(propertyName, new Color(vector.x, vector.y, vector.z, vector.w));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00011B54 File Offset: 0x0000FD54
		public Vector4 GetVector(string propertyName)
		{
			Color color = this.GetColor(propertyName);
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00011B8C File Offset: 0x0000FD8C
		public void SetTexture(string propertyName, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(propertyName), texture);
		}

		// Token: 0x06000749 RID: 1865
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetTexture(int nameID, Texture texture);

		// Token: 0x0600074A RID: 1866 RVA: 0x00011B9C File Offset: 0x0000FD9C
		public Texture GetTexture(string propertyName)
		{
			return this.GetTexture(Shader.PropertyToID(propertyName));
		}

		// Token: 0x0600074B RID: 1867
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x0600074C RID: 1868 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public void SetTextureOffset(string propertyName, Vector2 offset)
		{
			Material.INTERNAL_CALL_SetTextureOffset(this, propertyName, ref offset);
		}

		// Token: 0x0600074D RID: 1869
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetTextureOffset(Material self, string propertyName, ref Vector2 offset);

		// Token: 0x0600074E RID: 1870 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		public void SetTextureScale(string propertyName, Vector2 scale)
		{
			Material.INTERNAL_CALL_SetTextureScale(this, propertyName, ref scale);
		}

		// Token: 0x0600074F RID: 1871
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetTextureScale(Material self, string propertyName, ref Vector2 scale);

		// Token: 0x06000750 RID: 1872 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public void SetMatrix(string propertyName, Matrix4x4 matrix)
		{
			this.SetMatrix(Shader.PropertyToID(propertyName), matrix);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public void SetMatrix(int nameID, Matrix4x4 matrix)
		{
			Material.INTERNAL_CALL_SetMatrix(this, nameID, ref matrix);
		}

		// Token: 0x06000752 RID: 1874
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetMatrix(Material self, int nameID, ref Matrix4x4 matrix);

		// Token: 0x06000753 RID: 1875 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		public void SetFloat(string propertyName, float value)
		{
			this.SetFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x06000754 RID: 1876
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float value);

		// Token: 0x06000755 RID: 1877 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		public float GetFloat(string propertyName)
		{
			return this.GetFloat(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000756 RID: 1878
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000757 RID: 1879 RVA: 0x00011C00 File Offset: 0x0000FE00
		public void SetInt(string propertyName, int value)
		{
			this.SetFloat(propertyName, (float)value);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00011C0C File Offset: 0x0000FE0C
		public bool HasProperty(string propertyName)
		{
			return this.HasProperty(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000759 RID: 1881
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool HasProperty(int nameID);

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600075A RID: 1882
		public extern int passCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600075B RID: 1883
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool SetPass(int pass);

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600075C RID: 1884
		// (set) Token: 0x0600075D RID: 1885
		public extern int renderQueue { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600075E RID: 1886
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateWithString([Writable] Material mono, string contents);

		// Token: 0x0600075F RID: 1887
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateWithShader([Writable] Material mono, Shader shader);

		// Token: 0x06000760 RID: 1888
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateWithMaterial([Writable] Material mono, Material source);

		// Token: 0x06000761 RID: 1889
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		// Token: 0x06000762 RID: 1890
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000763 RID: 1891
		public extern string[] shaderKeywords { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
