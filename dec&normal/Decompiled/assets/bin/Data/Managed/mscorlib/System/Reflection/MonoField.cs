using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001DC RID: 476
	[Serializable]
	internal class MonoField : FieldInfo, ISerializable
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00042DF4 File Offset: 0x00040FF4
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00042DFC File Offset: 0x00040FFC
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00042E04 File Offset: 0x00041004
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060011A8 RID: 4520
		[MethodImpl(4096)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00042E0C File Offset: 0x0004100C
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00042E18 File Offset: 0x00041018
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00042E24 File Offset: 0x00041024
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00042E2C File Offset: 0x0004102C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00042E38 File Offset: 0x00041038
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00042E44 File Offset: 0x00041044
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060011AF RID: 4527
		[MethodImpl(4096)]
		internal override extern int GetFieldOffset();

		// Token: 0x060011B0 RID: 4528
		[MethodImpl(4096)]
		private extern object GetValueInternal(object obj);

		// Token: 0x060011B1 RID: 4529 RVA: 0x00042E50 File Offset: 0x00041050
		public override object GetValue(object obj)
		{
			if (!this.IsStatic && obj == null)
			{
				throw new TargetException("Non-static field requires a target");
			}
			if (!this.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00042E88 File Offset: 0x00041088
		public override string ToString()
		{
			return string.Format("{0} {1}", this.type, this.name);
		}

		// Token: 0x060011B3 RID: 4531
		[MethodImpl(4096)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x060011B4 RID: 4532 RVA: 0x00042EA0 File Offset: 0x000410A0
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!this.IsStatic && obj == null)
			{
				throw new TargetException("Non-static field requires a target");
			}
			if (this.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				object obj2 = binder.ChangeType(val, this.type, culture);
				if (obj2 == null)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Object type ",
						val.GetType(),
						" cannot be converted to target type: ",
						this.type
					}), "val");
				}
				val = obj2;
			}
			MonoField.SetValueInternal(this, obj, val);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00042F54 File Offset: 0x00041154
		internal MonoField Clone(string newName)
		{
			return new MonoField
			{
				name = newName,
				type = this.type,
				attrs = this.attrs,
				klass = this.klass,
				fhandle = this.fhandle
			};
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00042FA0 File Offset: 0x000411A0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00042FBC File Offset: 0x000411BC
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x0400090C RID: 2316
		internal IntPtr klass;

		// Token: 0x0400090D RID: 2317
		internal RuntimeFieldHandle fhandle;

		// Token: 0x0400090E RID: 2318
		private string name;

		// Token: 0x0400090F RID: 2319
		private Type type;

		// Token: 0x04000910 RID: 2320
		private FieldAttributes attrs;
	}
}
