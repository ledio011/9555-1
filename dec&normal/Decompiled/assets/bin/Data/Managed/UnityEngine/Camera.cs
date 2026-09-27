using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	public sealed class Camera : Behaviour
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002A4 RID: 676
		// (set) Token: 0x060002A5 RID: 677
		public extern float fieldOfView { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002A6 RID: 678
		// (set) Token: 0x060002A7 RID: 679
		public extern float nearClipPlane { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A8 RID: 680
		// (set) Token: 0x060002A9 RID: 681
		public extern float farClipPlane { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002AA RID: 682
		public extern bool hdr { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002AB RID: 683
		// (set) Token: 0x060002AC RID: 684
		public extern float orthographicSize { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002AD RID: 685
		// (set) Token: 0x060002AE RID: 686
		public extern bool orthographic { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002AF RID: 687
		// (set) Token: 0x060002B0 RID: 688
		public extern TransparencySortMode transparencySortMode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00007724 File Offset: 0x00005924
		public bool isOrthoGraphic
		{
			get
			{
				return this.orthographic;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002B2 RID: 690
		// (set) Token: 0x060002B3 RID: 691
		public extern float depth { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002B4 RID: 692
		// (set) Token: 0x060002B5 RID: 693
		public extern float aspect { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002B6 RID: 694
		// (set) Token: 0x060002B7 RID: 695
		public extern int cullingMask { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B8 RID: 696
		// (set) Token: 0x060002B9 RID: 697
		public extern int eventMask { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000076 RID: 118
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000772C File Offset: 0x0000592C
		public Color backgroundColor
		{
			set
			{
				this.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x060002BB RID: 699
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007738 File Offset: 0x00005938
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00007750 File Offset: 0x00005950
		public Rect rect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rect(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rect(ref value);
			}
		}

		// Token: 0x060002BE RID: 702
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x060002BF RID: 703
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_rect(ref Rect value);

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000775C File Offset: 0x0000595C
		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_pixelRect(out result);
				return result;
			}
		}

		// Token: 0x060002C1 RID: 705
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002C2 RID: 706
		// (set) Token: 0x060002C3 RID: 707
		public extern RenderTexture targetTexture { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00007774 File Offset: 0x00005974
		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_projectionMatrix(out result);
				return result;
			}
		}

		// Token: 0x060002C5 RID: 709
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_projectionMatrix(out Matrix4x4 value);

		// Token: 0x060002C6 RID: 710 RVA: 0x0000778C File Offset: 0x0000598C
		public void ResetAspect()
		{
			Camera.INTERNAL_CALL_ResetAspect(this);
		}

		// Token: 0x060002C7 RID: 711
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_ResetAspect(Camera self);

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002C8 RID: 712
		// (set) Token: 0x060002C9 RID: 713
		public extern CameraClearFlags clearFlags { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060002CA RID: 714 RVA: 0x00007794 File Offset: 0x00005994
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_WorldToScreenPoint(this, ref position);
		}

		// Token: 0x060002CB RID: 715
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_WorldToScreenPoint(Camera self, ref Vector3 position);

		// Token: 0x060002CC RID: 716 RVA: 0x000077A0 File Offset: 0x000059A0
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_WorldToViewportPoint(this, ref position);
		}

		// Token: 0x060002CD RID: 717
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_WorldToViewportPoint(Camera self, ref Vector3 position);

		// Token: 0x060002CE RID: 718 RVA: 0x000077AC File Offset: 0x000059AC
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ViewportToWorldPoint(this, ref position);
		}

		// Token: 0x060002CF RID: 719
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_ViewportToWorldPoint(Camera self, ref Vector3 position);

		// Token: 0x060002D0 RID: 720 RVA: 0x000077B8 File Offset: 0x000059B8
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenToWorldPoint(this, ref position);
		}

		// Token: 0x060002D1 RID: 721
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_ScreenToWorldPoint(Camera self, ref Vector3 position);

		// Token: 0x060002D2 RID: 722 RVA: 0x000077C4 File Offset: 0x000059C4
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenToViewportPoint(this, ref position);
		}

		// Token: 0x060002D3 RID: 723
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_ScreenToViewportPoint(Camera self, ref Vector3 position);

		// Token: 0x060002D4 RID: 724 RVA: 0x000077D0 File Offset: 0x000059D0
		public Ray ViewportPointToRay(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ViewportPointToRay(this, ref position);
		}

		// Token: 0x060002D5 RID: 725
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Ray INTERNAL_CALL_ViewportPointToRay(Camera self, ref Vector3 position);

		// Token: 0x060002D6 RID: 726 RVA: 0x000077DC File Offset: 0x000059DC
		public Ray ScreenPointToRay(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenPointToRay(this, ref position);
		}

		// Token: 0x060002D7 RID: 727
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Ray INTERNAL_CALL_ScreenPointToRay(Camera self, ref Vector3 position);

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002D8 RID: 728
		public static extern Camera main { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002D9 RID: 729
		public static extern int allCamerasCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060002DA RID: 730
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetAllCameras(Camera[] cameras);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000077E8 File Offset: 0x000059E8
		[Obsolete("use Camera.main instead.")]
		public static Camera mainCamera
		{
			get
			{
				return Camera.main;
			}
		}

		// Token: 0x060002DC RID: 732
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002DD RID: 733
		// (set) Token: 0x060002DE RID: 734
		public extern float[] layerCullDistances { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000080 RID: 128
		// (set) Token: 0x060002DF RID: 735
		public extern bool layerCullSpherical { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002E0 RID: 736
		// (set) Token: 0x060002E1 RID: 737
		public extern DepthTextureMode depthTextureMode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
