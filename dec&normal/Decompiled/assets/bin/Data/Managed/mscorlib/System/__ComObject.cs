using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.Interop;

namespace System
{
	// Token: 0x0200004F RID: 79
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000C788 File Offset: 0x0000A988
		public __ComObject()
		{
			this.Initialize(base.GetType());
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C79C File Offset: 0x0000A99C
		internal __ComObject(Type t)
		{
			this.Initialize(t);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		internal __ComObject(IntPtr pItf)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			int errorCode = Marshal.QueryInterface(pItf, ref iid_IUnknown, out this.iunknown);
			Marshal.ThrowExceptionForHR(errorCode);
		}

		// Token: 0x0600016F RID: 367
		[MethodImpl(4096)]
		internal static extern __ComObject CreateRCW(Type t);

		// Token: 0x06000170 RID: 368
		[MethodImpl(4096)]
		private extern void ReleaseInterfaces();

		// Token: 0x06000171 RID: 369 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		~__ComObject()
		{
			this.ReleaseInterfaces();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C80C File Offset: 0x0000AA0C
		internal void Initialize(Type t)
		{
			if (this.iunknown != IntPtr.Zero)
			{
				return;
			}
			ObjectCreationDelegate objectCreationCallback = ExtensibleClassFactory.GetObjectCreationCallback(t);
			if (objectCreationCallback != null)
			{
				this.iunknown = objectCreationCallback(IntPtr.Zero);
				if (this.iunknown == IntPtr.Zero)
				{
					throw new COMException(string.Format("ObjectCreationDelegate for type {0} failed to return a valid COM object", t));
				}
			}
			else
			{
				int errorCode = __ComObject.CoCreateInstance(__ComObject.GetCLSID(t), IntPtr.Zero, 21U, __ComObject.IID_IUnknown, out this.iunknown);
				Marshal.ThrowExceptionForHR(errorCode);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C89C File Offset: 0x0000AA9C
		private static Guid GetCLSID(Type t)
		{
			if (t.IsImport)
			{
				return t.GUID;
			}
			for (Type baseType = t.BaseType; baseType != typeof(object); baseType = baseType.BaseType)
			{
				if (baseType.IsImport)
				{
					return baseType.GUID;
				}
			}
			throw new COMException("Could not find base COM type for type " + t.ToString());
		}

		// Token: 0x06000174 RID: 372
		[MethodImpl(4096)]
		internal extern IntPtr GetInterfaceInternal(Type t, bool throwException);

		// Token: 0x06000175 RID: 373 RVA: 0x0000C908 File Offset: 0x0000AB08
		internal IntPtr GetInterface(Type t, bool throwException)
		{
			this.CheckIUnknown();
			return this.GetInterfaceInternal(t, throwException);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C918 File Offset: 0x0000AB18
		internal IntPtr GetInterface(Type t)
		{
			return this.GetInterface(t, true);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000C924 File Offset: 0x0000AB24
		private void CheckIUnknown()
		{
			if (this.iunknown == IntPtr.Zero)
			{
				throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000C948 File Offset: 0x0000AB48
		internal IntPtr IUnknown
		{
			get
			{
				if (this.iunknown == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return this.iunknown;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000C970 File Offset: 0x0000AB70
		internal IntPtr IDispatch
		{
			get
			{
				IntPtr @interface = this.GetInterface(typeof(IDispatch));
				if (@interface == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return @interface;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		internal static Guid IID_IUnknown
		{
			get
			{
				return new Guid("00000000-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		internal static Guid IID_IDispatch
		{
			get
			{
				return new Guid("00020400-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public override bool Equals(object obj)
		{
			this.CheckIUnknown();
			if (obj == null)
			{
				return false;
			}
			__ComObject _ComObject = obj as __ComObject;
			return _ComObject != null && this.iunknown == _ComObject.IUnknown;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public override int GetHashCode()
		{
			this.CheckIUnknown();
			return this.iunknown.ToInt32();
		}

		// Token: 0x0600017E RID: 382
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, out IntPtr pUnk);

		// Token: 0x04000144 RID: 324
		private IntPtr iunknown;

		// Token: 0x04000145 RID: 325
		private IntPtr hash_table;
	}
}
