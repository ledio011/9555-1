using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace Mono.Interop
{
	// Token: 0x02000040 RID: 64
	internal class ComInteropProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00009264 File Offset: 0x00007464
		private ComInteropProxy(IntPtr pUnk) : this(pUnk, typeof(__ComObject))
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00009278 File Offset: 0x00007478
		internal ComInteropProxy(IntPtr pUnk, Type t) : base(t)
		{
			this.com_object = new __ComObject(pUnk);
			this.CacheProxy();
		}

		// Token: 0x060000E8 RID: 232
		[MethodImpl(4096)]
		private static extern void AddProxy(IntPtr pItf, ComInteropProxy proxy);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(4096)]
		internal static extern ComInteropProxy FindProxy(IntPtr pItf);

		// Token: 0x060000EA RID: 234 RVA: 0x0000929C File Offset: 0x0000749C
		private void CacheProxy()
		{
			ComInteropProxy.AddProxy(this.com_object.IUnknown, this);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000092B0 File Offset: 0x000074B0
		internal static ComInteropProxy GetProxy(IntPtr pItf, Type t)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			IntPtr intPtr;
			int errorCode = Marshal.QueryInterface(pItf, ref iid_IUnknown, out intPtr);
			Marshal.ThrowExceptionForHR(errorCode);
			ComInteropProxy comInteropProxy = ComInteropProxy.FindProxy(intPtr);
			if (comInteropProxy == null)
			{
				Marshal.Release(pItf);
				return new ComInteropProxy(intPtr);
			}
			Marshal.Release(pItf);
			Interlocked.Increment(ref comInteropProxy.ref_count);
			return comInteropProxy;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009304 File Offset: 0x00007504
		public override IMessage Invoke(IMessage msg)
		{
			Console.WriteLine("Invoke");
			Console.WriteLine(Environment.StackTrace);
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00009324 File Offset: 0x00007524
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000932C File Offset: 0x0000752C
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009338 File Offset: 0x00007538
		public bool CanCastTo(Type fromType, object o)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new NotSupportedException("Only RCWs are currently supported");
			}
			return (fromType.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic && !(_ComObject.GetInterface(fromType, false) == IntPtr.Zero);
		}

		// Token: 0x04000102 RID: 258
		private __ComObject com_object;

		// Token: 0x04000103 RID: 259
		private int ref_count = 1;

		// Token: 0x04000104 RID: 260
		private string type_name;
	}
}
