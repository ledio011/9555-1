using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020001E2 RID: 482
	[Serializable]
	internal class MonoProperty : PropertyInfo, ISerializable
	{
		// Token: 0x06001212 RID: 4626 RVA: 0x00044630 File Offset: 0x00042830
		private void CachePropertyInfo(PInfo flags)
		{
			if ((this.cached & flags) != flags)
			{
				MonoPropertyInfo.get_property_info(this, ref this.info, flags);
				this.cached |= flags;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004465C File Offset: 0x0004285C
		public override PropertyAttributes Attributes
		{
			get
			{
				this.CachePropertyInfo(PInfo.Attributes);
				return this.info.attrs;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00044670 File Offset: 0x00042870
		public override bool CanRead
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod);
				return this.info.get_method != null;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0004468C File Offset: 0x0004288C
		public override bool CanWrite
		{
			get
			{
				this.CachePropertyInfo(PInfo.SetMethod);
				return this.info.set_method != null;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x000446A8 File Offset: 0x000428A8
		public override Type PropertyType
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
				if (this.info.get_method != null)
				{
					return this.info.get_method.ReturnType;
				}
				ParameterInfo[] parameters = this.info.set_method.GetParameters();
				return parameters[parameters.Length - 1].ParameterType;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000446FC File Offset: 0x000428FC
		public override Type ReflectedType
		{
			get
			{
				this.CachePropertyInfo(PInfo.ReflectedType);
				return this.info.parent;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00044710 File Offset: 0x00042910
		public override Type DeclaringType
		{
			get
			{
				this.CachePropertyInfo(PInfo.DeclaringType);
				return this.info.parent;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00044728 File Offset: 0x00042928
		public override string Name
		{
			get
			{
				this.CachePropertyInfo(PInfo.Name);
				return this.info.name;
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00044740 File Offset: 0x00042940
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.GetMethod);
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				return this.info.get_method;
			}
			return null;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0004478C File Offset: 0x0004298C
		public override ParameterInfo[] GetIndexParameters()
		{
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			ParameterInfo[] array;
			if (this.info.get_method != null)
			{
				array = this.info.get_method.GetParameters();
			}
			else
			{
				if (this.info.set_method == null)
				{
					return new ParameterInfo[0];
				}
				ParameterInfo[] parameters = this.info.set_method.GetParameters();
				array = new ParameterInfo[parameters.Length - 1];
				Array.Copy(parameters, array, array.Length);
			}
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo pinfo = array[i];
				array[i] = new ParameterInfo(pinfo, this);
			}
			return array;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0004482C File Offset: 0x00042A2C
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				return this.info.set_method;
			}
			return null;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00044878 File Offset: 0x00042A78
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, false);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00044884 File Offset: 0x00042A84
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00044890 File Offset: 0x00042A90
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0004489C File Offset: 0x00042A9C
		public override object GetValue(object obj, object[] index)
		{
			if (index == null || index.Length == 0)
			{
			}
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000448B8 File Offset: 0x00042AB8
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			object result = null;
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Get Method not found for '" + this.Name + "'");
			}
			try
			{
				if (index == null || index.Length == 0)
				{
					result = getMethod.Invoke(obj, invokeAttr, binder, null, culture);
				}
				else
				{
					result = getMethod.Invoke(obj, invokeAttr, binder, index, culture);
				}
			}
			catch (SecurityException inner)
			{
				throw new TargetInvocationException(inner);
			}
			return result;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00044944 File Offset: 0x00042B44
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException("Set Method not found for '" + this.Name + "'");
			}
			object[] array;
			if (index == null || index.Length == 0)
			{
				array = new object[]
				{
					value
				};
			}
			else
			{
				int num = index.Length;
				array = new object[num + 1];
				index.CopyTo(array, 0);
				array[num] = value;
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000449C4 File Offset: 0x00042BC4
		public override string ToString()
		{
			return this.PropertyType.ToString() + " " + this.Name;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000449E4 File Offset: 0x00042BE4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Property);
		}

		// Token: 0x04000921 RID: 2337
		internal IntPtr klass;

		// Token: 0x04000922 RID: 2338
		internal IntPtr prop;

		// Token: 0x04000923 RID: 2339
		private MonoPropertyInfo info;

		// Token: 0x04000924 RID: 2340
		private PInfo cached;

		// Token: 0x04000925 RID: 2341
		private MonoProperty.GetterAdapter cached_getter;

		// Token: 0x020001E3 RID: 483
		// (Invoke) Token: 0x06001226 RID: 4646
		private delegate object GetterAdapter(object _this);
	}
}
