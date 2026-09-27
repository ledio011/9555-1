using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001C7 RID: 455
	[ComDefaultInterface(typeof(_FieldInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	public abstract class FieldInfo : MemberInfo, _FieldInfo
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001117 RID: 4375
		public abstract FieldAttributes Attributes { get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001118 RID: 4376
		public abstract RuntimeFieldHandle FieldHandle { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001119 RID: 4377
		public abstract Type FieldType { get; }

		// Token: 0x0600111A RID: 4378
		public abstract object GetValue(object obj);

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000420C4 File Offset: 0x000402C4
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x000420C8 File Offset: 0x000402C8
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x000420DC File Offset: 0x000402DC
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x000420F0 File Offset: 0x000402F0
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00042100 File Offset: 0x00040300
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.NotSerialized;
			}
		}

		// Token: 0x06001120 RID: 4384
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x06001121 RID: 4385 RVA: 0x00042118 File Offset: 0x00040318
		[DebuggerStepThrough]
		[DebuggerHidden]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, null);
		}

		// Token: 0x06001122 RID: 4386
		[MethodImpl(4096)]
		private static extern FieldInfo internal_from_handle_type(IntPtr field_handle, IntPtr type_handle);

		// Token: 0x06001123 RID: 4387 RVA: 0x00042128 File Offset: 0x00040328
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return FieldInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0004215C File Offset: 0x0004035C
		internal virtual int GetFieldOffset()
		{
			throw new SystemException("This method should not be called");
		}

		// Token: 0x06001125 RID: 4389
		[MethodImpl(4096)]
		private extern UnmanagedMarshal GetUnmanagedMarshal();

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00042168 File Offset: 0x00040368
		internal virtual UnmanagedMarshal UMarshal
		{
			get
			{
				return this.GetUnmanagedMarshal();
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00042170 File Offset: 0x00040370
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			UnmanagedMarshal umarshal = this.UMarshal;
			if (umarshal != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new NonSerializedAttribute();
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				array[num++] = new FieldOffsetAttribute(this.GetFieldOffset());
			}
			if (umarshal != null)
			{
				array[num++] = umarshal.ToMarshalAsAttribute();
			}
			return array;
		}
	}
}
