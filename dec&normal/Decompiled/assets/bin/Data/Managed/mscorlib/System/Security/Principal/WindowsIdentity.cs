using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	// Token: 0x02000377 RID: 887
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : IDisposable, IDeserializationCallback, ISerializable, IIdentity
	{
		// Token: 0x06001A15 RID: 6677 RVA: 0x00060C30 File Offset: 0x0005EE30
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
		{
			this._type = type;
			this._account = acctType;
			this._authenticated = isAuthenticated;
			this._name = null;
			this.SetToken(userToken);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00060C5C File Offset: 0x0005EE5C
		public WindowsIdentity(SerializationInfo info, StreamingContext context)
		{
			this._info = info;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00060C78 File Offset: 0x0005EE78
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this._token = (IntPtr)this._info.GetValue("m_userToken", typeof(IntPtr));
			this._name = this._info.GetString("m_name");
			if (this._name != null)
			{
				string tokenName = WindowsIdentity.GetTokenName(this._token);
				if (tokenName != this._name)
				{
					throw new SerializationException("Token-Name mismatch.");
				}
			}
			else
			{
				this._name = WindowsIdentity.GetTokenName(this._token);
				if (this._name == string.Empty || this._name == null)
				{
					throw new SerializationException("Token doesn't match a user.");
				}
			}
			this._type = this._info.GetString("m_type");
			this._account = (WindowsAccountType)((int)this._info.GetValue("m_acctType", typeof(WindowsAccountType)));
			this._authenticated = this._info.GetBoolean("m_isAuthenticated");
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00060D88 File Offset: 0x0005EF88
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_userToken", this._token);
			info.AddValue("m_name", this._name);
			info.AddValue("m_type", this._type);
			info.AddValue("m_acctType", this._account);
			info.AddValue("m_isAuthenticated", this._authenticated);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00060DF4 File Offset: 0x0005EFF4
		[ComVisible(false)]
		public void Dispose()
		{
			this._token = IntPtr.Zero;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00060E04 File Offset: 0x0005F004
		public static WindowsIdentity GetCurrent()
		{
			return new WindowsIdentity(WindowsIdentity.GetCurrentToken(), null, WindowsAccountType.Normal, true);
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00060E14 File Offset: 0x0005F014
		public virtual string Name
		{
			get
			{
				if (this._name == null)
				{
					this._name = WindowsIdentity.GetTokenName(this._token);
				}
				return this._name;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00060E38 File Offset: 0x0005F038
		private static bool IsPosix
		{
			get
			{
				int platform = (int)Environment.Platform;
				return platform == 128 || platform == 4 || platform == 6;
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00060E64 File Offset: 0x0005F064
		private void SetToken(IntPtr token)
		{
			if (WindowsIdentity.IsPosix)
			{
				this._token = token;
				if (this._type == null)
				{
					this._type = "POSIX";
				}
				if (this._token == IntPtr.Zero)
				{
					this._account = WindowsAccountType.System;
				}
			}
			else
			{
				if (token == WindowsIdentity.invalidWindows && this._account != WindowsAccountType.Anonymous)
				{
					throw new ArgumentException("Invalid token");
				}
				this._token = token;
				if (this._type == null)
				{
					this._type = "NTLM";
				}
			}
		}

		// Token: 0x06001A1F RID: 6687
		[MethodImpl(4096)]
		internal static extern IntPtr GetCurrentToken();

		// Token: 0x06001A20 RID: 6688
		[MethodImpl(4096)]
		private static extern string GetTokenName(IntPtr token);

		// Token: 0x04000E52 RID: 3666
		private IntPtr _token;

		// Token: 0x04000E53 RID: 3667
		private string _type;

		// Token: 0x04000E54 RID: 3668
		private WindowsAccountType _account;

		// Token: 0x04000E55 RID: 3669
		private bool _authenticated;

		// Token: 0x04000E56 RID: 3670
		private string _name;

		// Token: 0x04000E57 RID: 3671
		private SerializationInfo _info;

		// Token: 0x04000E58 RID: 3672
		private static IntPtr invalidWindows = IntPtr.Zero;
	}
}
