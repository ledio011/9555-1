using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	// Token: 0x02000380 RID: 896
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		// Token: 0x06001A48 RID: 6728 RVA: 0x00061948 File Offset: 0x0005FB48
		public SecurityException() : base(Locale.GetText("A security error has been detected."))
		{
			base.HResult = -2146233078;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00061968 File Offset: 0x0005FB68
		public SecurityException(string message) : base(message)
		{
			base.HResult = -2146233078;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0006197C File Offset: 0x0005FB7C
		protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			base.HResult = -2146233078;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "PermissionState")
				{
					this.permissionState = (string)enumerator.Value;
					break;
				}
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x000619E0 File Offset: 0x0005FBE0
		[ComVisible(false)]
		public object Demanded
		{
			get
			{
				return this._demanded;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x000619E8 File Offset: 0x0005FBE8
		public IPermission FirstPermissionThatFailed
		{
			get
			{
				return this._firstperm;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x000619F0 File Offset: 0x0005FBF0
		public string PermissionState
		{
			get
			{
				return this.permissionState;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x000619F8 File Offset: 0x0005FBF8
		public Type PermissionType
		{
			get
			{
				return this.permissionType;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00061A00 File Offset: 0x0005FC00
		public string GrantedSet
		{
			get
			{
				return this._granted;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00061A08 File Offset: 0x0005FC08
		public string RefusedSet
		{
			get
			{
				return this._refused;
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00061A10 File Offset: 0x0005FC10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			try
			{
				info.AddValue("PermissionState", this.PermissionState);
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00061A54 File Offset: 0x0005FC54
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			try
			{
				if (this.permissionType != null)
				{
					stringBuilder.AppendFormat("{0}Type: {1}", Environment.NewLine, this.PermissionType);
				}
				if (this._method != null)
				{
					string text = this._method.ToString();
					int startIndex = text.IndexOf(" ") + 1;
					stringBuilder.AppendFormat("{0}Method: {1} {2}.{3}", new object[]
					{
						Environment.NewLine,
						this._method.ReturnType.Name,
						this._method.ReflectedType,
						text.Substring(startIndex)
					});
				}
				if (this.permissionState != null)
				{
					stringBuilder.AppendFormat("{0}State: {1}", Environment.NewLine, this.PermissionState);
				}
				if (this._granted != null && this._granted.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Granted: {1}", Environment.NewLine, this.GrantedSet);
				}
				if (this._refused != null && this._refused.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Refused: {1}", Environment.NewLine, this.RefusedSet);
				}
				if (this._demanded != null)
				{
					stringBuilder.AppendFormat("{0}Demanded: {1}", Environment.NewLine, this.Demanded);
				}
				if (this._firstperm != null)
				{
					stringBuilder.AppendFormat("{0}Failed Permission: {1}", Environment.NewLine, this.FirstPermissionThatFailed);
				}
				if (this._evidence != null)
				{
					stringBuilder.AppendFormat("{0}Evidences:", Environment.NewLine);
					foreach (object obj in this._evidence)
					{
						if (!(obj is Hash))
						{
							stringBuilder.AppendFormat("{0}\t{1}", Environment.NewLine, obj);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000E79 RID: 3705
		private string permissionState;

		// Token: 0x04000E7A RID: 3706
		private Type permissionType;

		// Token: 0x04000E7B RID: 3707
		private string _granted;

		// Token: 0x04000E7C RID: 3708
		private string _refused;

		// Token: 0x04000E7D RID: 3709
		private object _demanded;

		// Token: 0x04000E7E RID: 3710
		private IPermission _firstperm;

		// Token: 0x04000E7F RID: 3711
		private MethodInfo _method;

		// Token: 0x04000E80 RID: 3712
		private Evidence _evidence;

		// Token: 0x04000E81 RID: 3713
		private SecurityAction _action;

		// Token: 0x04000E82 RID: 3714
		private object _denyset;

		// Token: 0x04000E83 RID: 3715
		private object _permitset;

		// Token: 0x04000E84 RID: 3716
		private AssemblyName _assembly;

		// Token: 0x04000E85 RID: 3717
		private string _url;

		// Token: 0x04000E86 RID: 3718
		private SecurityZone _zone;
	}
}
