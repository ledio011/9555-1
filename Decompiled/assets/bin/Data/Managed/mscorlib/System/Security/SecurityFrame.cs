using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Security
{
	// Token: 0x02000381 RID: 897
	internal struct SecurityFrame
	{
		// Token: 0x06001A53 RID: 6739 RVA: 0x00061C84 File Offset: 0x0005FE84
		internal SecurityFrame(RuntimeSecurityFrame frame)
		{
			this._domain = null;
			this._method = null;
			this._assert = null;
			this._deny = null;
			this._permitonly = null;
			this.InitFromRuntimeFrame(frame);
		}

		// Token: 0x06001A54 RID: 6740
		[MethodImpl(4096)]
		private static extern Array _GetSecurityStack(int skip);

		// Token: 0x06001A55 RID: 6741 RVA: 0x00061CB0 File Offset: 0x0005FEB0
		internal void InitFromRuntimeFrame(RuntimeSecurityFrame frame)
		{
			this._domain = frame.domain;
			this._method = frame.method;
			if (frame.assert.size > 0)
			{
				this._assert = SecurityManager.Decode(frame.assert.blob, frame.assert.size);
			}
			if (frame.deny.size > 0)
			{
				this._deny = SecurityManager.Decode(frame.deny.blob, frame.deny.size);
			}
			if (frame.permitonly.size > 0)
			{
				this._permitonly = SecurityManager.Decode(frame.permitonly.blob, frame.permitonly.size);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00061D6C File Offset: 0x0005FF6C
		public Assembly Assembly
		{
			get
			{
				return this._method.ReflectedType.Assembly;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00061D80 File Offset: 0x0005FF80
		public AppDomain Domain
		{
			get
			{
				return this._domain;
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00061D88 File Offset: 0x0005FF88
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Frame: {0}{1}", this._method, Environment.NewLine);
			stringBuilder.AppendFormat("\tAppDomain: {0}{1}", this.Domain, Environment.NewLine);
			stringBuilder.AppendFormat("\tAssembly: {0}{1}", this.Assembly, Environment.NewLine);
			if (this._assert != null)
			{
				stringBuilder.AppendFormat("\tAssert: {0}{1}", this._assert, Environment.NewLine);
			}
			if (this._deny != null)
			{
				stringBuilder.AppendFormat("\tDeny: {0}{1}", this._deny, Environment.NewLine);
			}
			if (this._permitonly != null)
			{
				stringBuilder.AppendFormat("\tPermitOnly: {0}{1}", this._permitonly, Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00061E4C File Offset: 0x0006004C
		public static ArrayList GetStack(int skipFrames)
		{
			Array array = SecurityFrame._GetSecurityStack(skipFrames + 2);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				object value = array.GetValue(i);
				if (value == null)
				{
					break;
				}
				arrayList.Add(new SecurityFrame((RuntimeSecurityFrame)value));
			}
			return arrayList;
		}

		// Token: 0x04000E87 RID: 3719
		private AppDomain _domain;

		// Token: 0x04000E88 RID: 3720
		private MethodInfo _method;

		// Token: 0x04000E89 RID: 3721
		private PermissionSet _assert;

		// Token: 0x04000E8A RID: 3722
		private PermissionSet _deny;

		// Token: 0x04000E8B RID: 3723
		private PermissionSet _permitonly;
	}
}
