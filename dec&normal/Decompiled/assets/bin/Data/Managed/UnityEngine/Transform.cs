using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200012A RID: 298
	public class Transform : Component, IEnumerable
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001997C File Offset: 0x00017B7C
		protected Transform()
		{
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00019984 File Offset: 0x00017B84
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0001999C File Offset: 0x00017B9C
		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_position(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_position(ref value);
			}
		}

		// Token: 0x06000AA6 RID: 2726
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x06000AA7 RID: 2727
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_position(ref Vector3 value);

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000199A8 File Offset: 0x00017BA8
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x000199C0 File Offset: 0x00017BC0
		public Vector3 localPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localPosition(ref value);
			}
		}

		// Token: 0x06000AAA RID: 2730
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localPosition(out Vector3 value);

		// Token: 0x06000AAB RID: 2731
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localPosition(ref Vector3 value);

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000199CC File Offset: 0x00017BCC
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x000199E8 File Offset: 0x00017BE8
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000199F8 File Offset: 0x00017BF8
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00019A10 File Offset: 0x00017C10
		public Vector3 localEulerAngles
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localEulerAngles(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localEulerAngles(ref value);
			}
		}

		// Token: 0x06000AB0 RID: 2736
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localEulerAngles(out Vector3 value);

		// Token: 0x06000AB1 RID: 2737
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localEulerAngles(ref Vector3 value);

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00019A1C File Offset: 0x00017C1C
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00019A30 File Offset: 0x00017C30
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00019A44 File Offset: 0x00017C44
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00019A58 File Offset: 0x00017C58
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00019A6C File Offset: 0x00017C6C
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00019A80 File Offset: 0x00017C80
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00019A90 File Offset: 0x00017C90
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00019AA8 File Offset: 0x00017CA8
		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_rotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rotation(ref value);
			}
		}

		// Token: 0x06000ABA RID: 2746
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x06000ABB RID: 2747
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_rotation(ref Quaternion value);

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00019AB4 File Offset: 0x00017CB4
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00019ACC File Offset: 0x00017CCC
		public Quaternion localRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_localRotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localRotation(ref value);
			}
		}

		// Token: 0x06000ABE RID: 2750
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localRotation(out Quaternion value);

		// Token: 0x06000ABF RID: 2751
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localRotation(ref Quaternion value);

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00019AD8 File Offset: 0x00017CD8
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public Vector3 localScale
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localScale(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localScale(ref value);
			}
		}

		// Token: 0x06000AC2 RID: 2754
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localScale(out Vector3 value);

		// Token: 0x06000AC3 RID: 2755
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localScale(ref Vector3 value);

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00019AFC File Offset: 0x00017CFC
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00019B04 File Offset: 0x00017D04
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				if (this is RectTransform)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000AC6 RID: 2758
		// (set) Token: 0x06000AC7 RID: 2759
		internal extern Transform parentInternal { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00019B24 File Offset: 0x00017D24
		public void SetParent(Transform parent)
		{
			this.SetParent(parent, true);
		}

		// Token: 0x06000AC9 RID: 2761
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetParent(Transform parent, bool worldPositionStays);

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00019B30 File Offset: 0x00017D30
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_worldToLocalMatrix(out result);
				return result;
			}
		}

		// Token: 0x06000ACB RID: 2763
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_worldToLocalMatrix(out Matrix4x4 value);

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00019B48 File Offset: 0x00017D48
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_localToWorldMatrix(out result);
				return result;
			}
		}

		// Token: 0x06000ACD RID: 2765
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localToWorldMatrix(out Matrix4x4 value);

		// Token: 0x06000ACE RID: 2766 RVA: 0x00019B60 File Offset: 0x00017D60
		[ExcludeFromDocs]
		public void Translate(Vector3 translation)
		{
			Space relativeTo = Space.Self;
			this.Translate(translation, relativeTo);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00019B78 File Offset: 0x00017D78
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.World)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00019BB0 File Offset: 0x00017DB0
		[ExcludeFromDocs]
		public void Translate(float x, float y, float z)
		{
			Space relativeTo = Space.Self;
			this.Translate(x, y, z, relativeTo);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00019BCC File Offset: 0x00017DCC
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			if (relativeTo)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00019C1C File Offset: 0x00017E1C
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00019C30 File Offset: 0x00017E30
		[ExcludeFromDocs]
		public void Rotate(Vector3 eulerAngles)
		{
			Space relativeTo = Space.Self;
			this.Rotate(eulerAngles, relativeTo);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00019C48 File Offset: 0x00017E48
		public void Rotate(Vector3 eulerAngles, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion rhs = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			if (relativeTo == Space.Self)
			{
				this.localRotation *= rhs;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * rhs * this.rotation;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00019CBC File Offset: 0x00017EBC
		[ExcludeFromDocs]
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			Space relativeTo = Space.Self;
			this.Rotate(xAngle, yAngle, zAngle, relativeTo);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00019CD8 File Offset: 0x00017ED8
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00019CEC File Offset: 0x00017EEC
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundInternal(this, ref axis, angle);
		}

		// Token: 0x06000AD9 RID: 2777
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_RotateAroundInternal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000ADA RID: 2778 RVA: 0x00019CF8 File Offset: 0x00017EF8
		[ExcludeFromDocs]
		public void Rotate(Vector3 axis, float angle)
		{
			Space relativeTo = Space.Self;
			this.Rotate(axis, angle, relativeTo);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00019D10 File Offset: 0x00017F10
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.Self)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00019D48 File Offset: 0x00017F48
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion rotation = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = rotation * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00019D94 File Offset: 0x00017F94
		[ExcludeFromDocs]
		public void LookAt(Transform target)
		{
			Vector3 up = Vector3.up;
			this.LookAt(target, up);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00019DB0 File Offset: 0x00017FB0
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			if (target)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00019DCC File Offset: 0x00017FCC
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref worldUp);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00019DD8 File Offset: 0x00017FD8
		[ExcludeFromDocs]
		public void LookAt(Vector3 worldPosition)
		{
			Vector3 up = Vector3.up;
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref up);
		}

		// Token: 0x06000AE1 RID: 2785
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_LookAt(Transform self, ref Vector3 worldPosition, ref Vector3 worldUp);

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public Vector3 TransformDirection(Vector3 direction)
		{
			return Transform.INTERNAL_CALL_TransformDirection(this, ref direction);
		}

		// Token: 0x06000AE3 RID: 2787
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_TransformDirection(Transform self, ref Vector3 direction);

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00019E04 File Offset: 0x00018004
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00019E14 File Offset: 0x00018014
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			return Transform.INTERNAL_CALL_InverseTransformDirection(this, ref direction);
		}

		// Token: 0x06000AE6 RID: 2790
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformDirection(Transform self, ref Vector3 direction);

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00019E20 File Offset: 0x00018020
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00019E30 File Offset: 0x00018030
		public Vector3 TransformVector(Vector3 vector)
		{
			return Transform.INTERNAL_CALL_TransformVector(this, ref vector);
		}

		// Token: 0x06000AE9 RID: 2793
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_TransformVector(Transform self, ref Vector3 vector);

		// Token: 0x06000AEA RID: 2794 RVA: 0x00019E3C File Offset: 0x0001803C
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00019E4C File Offset: 0x0001804C
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			return Transform.INTERNAL_CALL_InverseTransformVector(this, ref vector);
		}

		// Token: 0x06000AEC RID: 2796
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformVector(Transform self, ref Vector3 vector);

		// Token: 0x06000AED RID: 2797 RVA: 0x00019E58 File Offset: 0x00018058
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00019E68 File Offset: 0x00018068
		public Vector3 TransformPoint(Vector3 position)
		{
			return Transform.INTERNAL_CALL_TransformPoint(this, ref position);
		}

		// Token: 0x06000AEF RID: 2799
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_TransformPoint(Transform self, ref Vector3 position);

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00019E74 File Offset: 0x00018074
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00019E84 File Offset: 0x00018084
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			return Transform.INTERNAL_CALL_InverseTransformPoint(this, ref position);
		}

		// Token: 0x06000AF2 RID: 2802
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformPoint(Transform self, ref Vector3 position);

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00019E90 File Offset: 0x00018090
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AF4 RID: 2804
		public extern Transform root { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AF5 RID: 2805
		public extern int childCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000AF6 RID: 2806
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void DetachChildren();

		// Token: 0x06000AF7 RID: 2807
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetAsFirstSibling();

		// Token: 0x06000AF8 RID: 2808
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetAsLastSibling();

		// Token: 0x06000AF9 RID: 2809
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetSiblingIndex(int index);

		// Token: 0x06000AFA RID: 2810
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetSiblingIndex();

		// Token: 0x06000AFB RID: 2811
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Transform Find(string name);

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00019EA0 File Offset: 0x000180A0
		public Vector3 lossyScale
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_lossyScale(out result);
				return result;
			}
		}

		// Token: 0x06000AFD RID: 2813
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_lossyScale(out Vector3 value);

		// Token: 0x06000AFE RID: 2814
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool IsChildOf(Transform parent);

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AFF RID: 2815
		// (set) Token: 0x06000B00 RID: 2816
		public extern bool hasChanged { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000B01 RID: 2817 RVA: 0x00019EB8 File Offset: 0x000180B8
		public Transform FindChild(string name)
		{
			return this.Find(name);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00019EC4 File Offset: 0x000180C4
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00019ECC File Offset: 0x000180CC
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAround(this, ref axis, angle);
		}

		// Token: 0x06000B04 RID: 2820
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_RotateAround(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000B05 RID: 2821 RVA: 0x00019ED8 File Offset: 0x000180D8
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundLocal(this, ref axis, angle);
		}

		// Token: 0x06000B06 RID: 2822
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_RotateAroundLocal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000B07 RID: 2823
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Transform GetChild(int index);

		// Token: 0x06000B08 RID: 2824
		[WrapperlessIcall]
		[Obsolete("use Transform.childCount instead.")]
		[MethodImpl(4096)]
		public extern int GetChildCount();

		// Token: 0x0200012B RID: 299
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000B09 RID: 2825 RVA: 0x00019EE4 File Offset: 0x000180E4
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00019EFC File Offset: 0x000180FC
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x06000B0B RID: 2827 RVA: 0x00019F10 File Offset: 0x00018110
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				return ++this.currentIndex < childCount;
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x00019F40 File Offset: 0x00018140
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x040004CF RID: 1231
			private Transform outer;

			// Token: 0x040004D0 RID: 1232
			private int currentIndex = -1;
		}
	}
}
