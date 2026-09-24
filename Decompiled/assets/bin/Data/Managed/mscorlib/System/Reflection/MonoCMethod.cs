using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020001D9 RID: 473
	internal class MonoCMethod : ConstructorInfo, ISerializable
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x0004299C File Offset: 0x00040B9C
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000429AC File Offset: 0x00040BAC
		public override ParameterInfo[] GetParameters()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06001188 RID: 4488
		[MethodImpl(4096)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x06001189 RID: 4489 RVA: 0x000429BC File Offset: 0x00040BBC
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			ParameterInfo[] parameters2 = this.GetParameters();
			if ((parameters == null && parameters2.Length != 0) || (parameters != null && parameters.Length != parameters2.Length))
			{
				throw new TargetParameterCountException("parameters do not match signature");
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.Default)
			{
				if (!Binder.ConvertArgs(binder, parameters, parameters2, culture))
				{
					throw new ArgumentException("failed to convert parameters");
				}
			}
			else
			{
				for (int i = 0; i < parameters2.Length; i++)
				{
					if (parameters[i].GetType() != parameters2[i].ParameterType)
					{
						throw new ArgumentException("parameters do not match signature");
					}
				}
			}
			if (obj == null && this.DeclaringType.ContainsGenericParameters)
			{
				throw new MemberAccessException("Cannot create an instance of " + this.DeclaringType + " because Type.ContainsGenericParameters is true.");
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default && this.DeclaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format("Cannot create an instance of {0} because it is an abstract class", this.DeclaringType));
			}
			Exception ex = null;
			object obj2 = null;
			try
			{
				obj2 = this.InternalInvoke(obj, parameters, out ex);
			}
			catch (MethodAccessException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new TargetInvocationException(inner);
			}
			if (ex != null)
			{
				throw ex;
			}
			return (obj != null) ? null : obj2;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00042B2C File Offset: 0x00040D2C
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.Invoke(null, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00042B3C File Offset: 0x00040D3C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00042B4C File Offset: 0x00040D4C
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00042B5C File Offset: 0x00040D5C
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00042B6C File Offset: 0x00040D6C
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00042B74 File Offset: 0x00040D74
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00042B84 File Offset: 0x00040D84
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return MonoMethod.get_name(this);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00042BA0 File Offset: 0x00040DA0
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00042BAC File Offset: 0x00040DAC
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00042BB8 File Offset: 0x00040DB8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00042BC4 File Offset: 0x00040DC4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Void ");
			stringBuilder.Append(this.Name);
			stringBuilder.Append("(");
			ParameterInfo[] parameters = this.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(parameters[i].ParameterType.Name);
			}
			if (this.CallingConvention == CallingConventions.Any)
			{
				stringBuilder.Append(", ...");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00042C68 File Offset: 0x00040E68
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Constructor);
		}

		// Token: 0x040008FF RID: 2303
		internal IntPtr mhandle;

		// Token: 0x04000900 RID: 2304
		private string name;

		// Token: 0x04000901 RID: 2305
		private Type reftype;
	}
}
