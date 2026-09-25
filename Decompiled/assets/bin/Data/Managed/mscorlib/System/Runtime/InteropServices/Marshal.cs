using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000246 RID: 582
	[SuppressUnmanagedCodeSecurity]
	public static class Marshal
	{
		// Token: 0x06001351 RID: 4945
		[MethodImpl(4096)]
		private static extern int AddRefInternal(IntPtr pUnk);

		// Token: 0x06001352 RID: 4946 RVA: 0x00045664 File Offset: 0x00043864
		public static int AddRef(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.AddRefInternal(pUnk);
		}

		// Token: 0x06001353 RID: 4947
		[MethodImpl(4096)]
		public static extern IntPtr AllocCoTaskMem(int cb);

		// Token: 0x06001354 RID: 4948
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(4096)]
		public static extern IntPtr AllocHGlobal(IntPtr cb);

		// Token: 0x06001355 RID: 4949 RVA: 0x0004568C File Offset: 0x0004388C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004569C File Offset: 0x0004389C
		[MonoTODO]
		public static object BindToMoniker(string monikerName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000456A4 File Offset: 0x000438A4
		[MonoTODO]
		public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001358 RID: 4952
		[MethodImpl(4096)]
		private static extern void copy_to_unmanaged(Array source, int startIndex, IntPtr destination, int length);

		// Token: 0x06001359 RID: 4953
		[MethodImpl(4096)]
		private static extern void copy_from_unmanaged(IntPtr source, int startIndex, Array destination, int length);

		// Token: 0x0600135A RID: 4954 RVA: 0x000456AC File Offset: 0x000438AC
		public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000456B8 File Offset: 0x000438B8
		public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000456C4 File Offset: 0x000438C4
		public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000456D0 File Offset: 0x000438D0
		public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000456DC File Offset: 0x000438DC
		public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000456E8 File Offset: 0x000438E8
		public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000456F4 File Offset: 0x000438F4
		public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00045700 File Offset: 0x00043900
		public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004570C File Offset: 0x0004390C
		public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00045718 File Offset: 0x00043918
		public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00045724 File Offset: 0x00043924
		public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00045730 File Offset: 0x00043930
		public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004573C File Offset: 0x0004393C
		public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00045748 File Offset: 0x00043948
		public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00045754 File Offset: 0x00043954
		public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00045760 File Offset: 0x00043960
		public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0004576C File Offset: 0x0004396C
		public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00045774 File Offset: 0x00043974
		public static object CreateWrapperOfType(object o, Type t)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException("o must derive from __ComObject", "o");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			Type[] interfaces = o.GetType().GetInterfaces();
			foreach (Type type in interfaces)
			{
				if (type.IsImport && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					throw new InvalidCastException();
				}
			}
			return ComInteropProxy.GetProxy(_ComObject.IUnknown, t).GetTransparentProxy();
		}

		// Token: 0x0600136C RID: 4972
		[ComVisible(true)]
		[MethodImpl(4096)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		// Token: 0x0600136D RID: 4973
		[MethodImpl(4096)]
		public static extern void FreeBSTR(IntPtr ptr);

		// Token: 0x0600136E RID: 4974
		[MethodImpl(4096)]
		public static extern void FreeCoTaskMem(IntPtr ptr);

		// Token: 0x0600136F RID: 4975
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern void FreeHGlobal(IntPtr hglobal);

		// Token: 0x06001370 RID: 4976 RVA: 0x00045814 File Offset: 0x00043A14
		private static void ClearBSTR(IntPtr ptr)
		{
			int num = Marshal.ReadInt32(ptr, -4);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteByte(ptr, i, 0);
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00045844 File Offset: 0x00043A44
		public static void ZeroFreeBSTR(IntPtr s)
		{
			Marshal.ClearBSTR(s);
			Marshal.FreeBSTR(s);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00045854 File Offset: 0x00043A54
		private static void ClearAnsi(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				Marshal.WriteByte(ptr, num, 0);
				num++;
			}
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00045880 File Offset: 0x00043A80
		private static void ClearUnicode(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadInt16(ptr, num) != 0)
			{
				Marshal.WriteInt16(ptr, num, 0);
				num += 2;
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000458AC File Offset: 0x00043AAC
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000458BC File Offset: 0x00043ABC
		public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000458CC File Offset: 0x00043ACC
		public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000458DC File Offset: 0x00043ADC
		public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000458EC File Offset: 0x00043AEC
		public static Guid GenerateGuidForType(Type type)
		{
			return type.GUID;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000458F4 File Offset: 0x00043AF4
		[MonoTODO]
		public static string GenerateProgIdForType(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000458FC File Offset: 0x00043AFC
		[MonoTODO]
		public static object GetActiveObject(string progID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600137B RID: 4987
		[MethodImpl(4096)]
		private static extern IntPtr GetCCW(object o, Type T);

		// Token: 0x0600137C RID: 4988 RVA: 0x00045904 File Offset: 0x00043B04
		private static IntPtr GetComInterfaceForObjectInternal(object o, Type T)
		{
			if (Marshal.IsComObject(o))
			{
				return ((__ComObject)o).GetInterface(T);
			}
			return Marshal.GetCCW(o, T);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00045928 File Offset: 0x00043B28
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			IntPtr comInterfaceForObjectInternal = Marshal.GetComInterfaceForObjectInternal(o, T);
			Marshal.AddRef(comInterfaceForObjectInternal);
			return comInterfaceForObjectInternal;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00045948 File Offset: 0x00043B48
		[MonoTODO]
		public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00045950 File Offset: 0x00043B50
		[MonoNotSupported("MSDN states user code should never need to call this method.")]
		public static object GetComObjectData(object obj, object key)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x06001380 RID: 4992
		[MethodImpl(4096)]
		private static extern int GetComSlotForMethodInfoInternal(MemberInfo m);

		// Token: 0x06001381 RID: 4993 RVA: 0x0004595C File Offset: 0x00043B5C
		public static int GetComSlotForMethodInfo(MemberInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			if (!(m is MethodInfo))
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			if (!m.DeclaringType.IsInterface)
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			return Marshal.GetComSlotForMethodInfoInternal(m);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000459BC File Offset: 0x00043BBC
		[MonoTODO]
		public static int GetEndComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000459C4 File Offset: 0x00043BC4
		[MonoTODO]
		public static int GetExceptionCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000459CC File Offset: 0x00043BCC
		[MonoTODO]
		[ComVisible(true)]
		public static IntPtr GetExceptionPointers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000459D4 File Offset: 0x00043BD4
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			return m.GetHINSTANCE();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000459F0 File Offset: 0x00043BF0
		[MonoTODO("SetErrorInfo")]
		public static int GetHRForException(Exception e)
		{
			return e.hresult;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000459F8 File Offset: 0x00043BF8
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetHRForLastWin32Error()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001388 RID: 5000
		[MethodImpl(4096)]
		private static extern IntPtr GetIDispatchForObjectInternal(object o);

		// Token: 0x06001389 RID: 5001 RVA: 0x00045A00 File Offset: 0x00043C00
		public static IntPtr GetIDispatchForObject(object o)
		{
			IntPtr idispatchForObjectInternal = Marshal.GetIDispatchForObjectInternal(o);
			Marshal.AddRef(idispatchForObjectInternal);
			return idispatchForObjectInternal;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00045A1C File Offset: 0x00043C1C
		[MonoTODO]
		public static IntPtr GetIDispatchForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00045A24 File Offset: 0x00043C24
		[MonoTODO]
		public static IntPtr GetITypeInfoForType(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600138C RID: 5004
		[MethodImpl(4096)]
		private static extern IntPtr GetIUnknownForObjectInternal(object o);

		// Token: 0x0600138D RID: 5005 RVA: 0x00045A2C File Offset: 0x00043C2C
		public static IntPtr GetIUnknownForObject(object o)
		{
			IntPtr iunknownForObjectInternal = Marshal.GetIUnknownForObjectInternal(o);
			Marshal.AddRef(iunknownForObjectInternal);
			return iunknownForObjectInternal;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00045A48 File Offset: 0x00043C48
		[MonoTODO]
		public static IntPtr GetIUnknownForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00045A50 File Offset: 0x00043C50
		[Obsolete("This method has been deprecated")]
		[MonoTODO]
		public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00045A58 File Offset: 0x00043C58
		[MonoTODO]
		public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00045A60 File Offset: 0x00043C60
		public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
		{
			Variant variant = default(Variant);
			variant.SetValue(obj);
			Marshal.StructureToPtr(variant, pDstNativeVariant, false);
		}

		// Token: 0x06001392 RID: 5010
		[MethodImpl(4096)]
		private static extern object GetObjectForCCW(IntPtr pUnk);

		// Token: 0x06001393 RID: 5011 RVA: 0x00045A8C File Offset: 0x00043C8C
		public static object GetObjectForIUnknown(IntPtr pUnk)
		{
			object obj = Marshal.GetObjectForCCW(pUnk);
			if (obj == null)
			{
				ComInteropProxy proxy = ComInteropProxy.GetProxy(pUnk, typeof(__ComObject));
				obj = proxy.GetTransparentProxy();
			}
			return obj;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00045AC0 File Offset: 0x00043CC0
		public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
		{
			return ((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue();
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00045AEC File Offset: 0x00043CEC
		public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			object[] array = new object[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00045B54 File Offset: 0x00043D54
		[MonoTODO]
		public static int GetStartComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00045B5C File Offset: 0x00043D5C
		[Obsolete("This method has been deprecated")]
		[MonoTODO]
		public static Thread GetThreadFromFiberCookie(int cookie)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00045B64 File Offset: 0x00043D64
		public static object GetTypedObjectForIUnknown(IntPtr pUnk, Type t)
		{
			ComInteropProxy comInteropProxy = new ComInteropProxy(pUnk, t);
			__ComObject _ComObject = (__ComObject)comInteropProxy.GetTransparentProxy();
			foreach (Type type in t.GetInterfaces())
			{
				if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.Import && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					return null;
				}
			}
			return _ComObject;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00045BD8 File Offset: 0x00043DD8
		[MonoTODO]
		public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00045BE0 File Offset: 0x00043DE0
		[Obsolete]
		[MonoTODO]
		public static string GetTypeInfoName(UCOMITypeInfo pTI)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00045BE8 File Offset: 0x00043DE8
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00045BF0 File Offset: 0x00043DF0
		[MonoTODO]
		[Obsolete]
		public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00045BF8 File Offset: 0x00043DF8
		[MonoTODO]
		public static Guid GetTypeLibGuid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00045C00 File Offset: 0x00043E00
		[MonoTODO]
		public static Guid GetTypeLibGuidForAssembly(Assembly asm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00045C08 File Offset: 0x00043E08
		[MonoTODO]
		[Obsolete]
		public static int GetTypeLibLcid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00045C10 File Offset: 0x00043E10
		[MonoTODO]
		public static int GetTypeLibLcid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00045C18 File Offset: 0x00043E18
		[MonoTODO]
		[Obsolete]
		public static string GetTypeLibName(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00045C20 File Offset: 0x00043E20
		[MonoTODO]
		public static string GetTypeLibName(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00045C28 File Offset: 0x00043E28
		[MonoTODO]
		public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00045C30 File Offset: 0x00043E30
		public static object GetUniqueObjectForIUnknown(IntPtr unknown)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00045C38 File Offset: 0x00043E38
		[MonoTODO]
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A6 RID: 5030
		[MethodImpl(4096)]
		public static extern bool IsComObject(object o);

		// Token: 0x060013A7 RID: 5031 RVA: 0x00045C40 File Offset: 0x00043E40
		[MonoTODO]
		public static bool IsTypeVisibleFromCom(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00045C48 File Offset: 0x00043E48
		[MonoTODO]
		public static int NumParamBytes(MethodInfo m)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013A9 RID: 5033
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern int GetLastWin32Error();

		// Token: 0x060013AA RID: 5034
		[MethodImpl(4096)]
		public static extern IntPtr OffsetOf(Type t, string fieldName);

		// Token: 0x060013AB RID: 5035
		[MethodImpl(4096)]
		public static extern void Prelink(MethodInfo m);

		// Token: 0x060013AC RID: 5036
		[MethodImpl(4096)]
		public static extern void PrelinkAll(Type c);

		// Token: 0x060013AD RID: 5037
		[MethodImpl(4096)]
		public static extern string PtrToStringAnsi(IntPtr ptr);

		// Token: 0x060013AE RID: 5038
		[MethodImpl(4096)]
		public static extern string PtrToStringAnsi(IntPtr ptr, int len);

		// Token: 0x060013AF RID: 5039 RVA: 0x00045C50 File Offset: 0x00043E50
		public static string PtrToStringAuto(IntPtr ptr)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.PtrToStringAnsi(ptr) : Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00045C70 File Offset: 0x00043E70
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.PtrToStringAnsi(ptr, len) : Marshal.PtrToStringUni(ptr, len);
		}

		// Token: 0x060013B1 RID: 5041
		[MethodImpl(4096)]
		public static extern string PtrToStringUni(IntPtr ptr);

		// Token: 0x060013B2 RID: 5042
		[MethodImpl(4096)]
		public static extern string PtrToStringUni(IntPtr ptr, int len);

		// Token: 0x060013B3 RID: 5043
		[MethodImpl(4096)]
		public static extern string PtrToStringBSTR(IntPtr ptr);

		// Token: 0x060013B4 RID: 5044
		[ComVisible(true)]
		[MethodImpl(4096)]
		public static extern void PtrToStructure(IntPtr ptr, object structure);

		// Token: 0x060013B5 RID: 5045
		[ComVisible(true)]
		[MethodImpl(4096)]
		public static extern object PtrToStructure(IntPtr ptr, Type structureType);

		// Token: 0x060013B6 RID: 5046
		[MethodImpl(4096)]
		private static extern int QueryInterfaceInternal(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

		// Token: 0x060013B7 RID: 5047 RVA: 0x00045C90 File Offset: 0x00043E90
		public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.QueryInterfaceInternal(pUnk, ref iid, out ppv);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00045CBC File Offset: 0x00043EBC
		public static byte ReadByte(IntPtr ptr)
		{
			return Marshal.ReadByte(ptr, 0);
		}

		// Token: 0x060013B9 RID: 5049
		[MethodImpl(4096)]
		public static extern byte ReadByte(IntPtr ptr, int ofs);

		// Token: 0x060013BA RID: 5050 RVA: 0x00045CC8 File Offset: 0x00043EC8
		[MonoTODO]
		public static byte ReadByte([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00045CD0 File Offset: 0x00043ED0
		public static short ReadInt16(IntPtr ptr)
		{
			return Marshal.ReadInt16(ptr, 0);
		}

		// Token: 0x060013BC RID: 5052
		[MethodImpl(4096)]
		public static extern short ReadInt16(IntPtr ptr, int ofs);

		// Token: 0x060013BD RID: 5053 RVA: 0x00045CDC File Offset: 0x00043EDC
		[MonoTODO]
		public static short ReadInt16([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00045CE4 File Offset: 0x00043EE4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int ReadInt32(IntPtr ptr)
		{
			return Marshal.ReadInt32(ptr, 0);
		}

		// Token: 0x060013BF RID: 5055
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern int ReadInt32(IntPtr ptr, int ofs);

		// Token: 0x060013C0 RID: 5056 RVA: 0x00045CF0 File Offset: 0x00043EF0
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int ReadInt32([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00045CF8 File Offset: 0x00043EF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long ReadInt64(IntPtr ptr)
		{
			return Marshal.ReadInt64(ptr, 0);
		}

		// Token: 0x060013C2 RID: 5058
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern long ReadInt64(IntPtr ptr, int ofs);

		// Token: 0x060013C3 RID: 5059 RVA: 0x00045D04 File Offset: 0x00043F04
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00045D0C File Offset: 0x00043F0C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			return Marshal.ReadIntPtr(ptr, 0);
		}

		// Token: 0x060013C5 RID: 5061
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern IntPtr ReadIntPtr(IntPtr ptr, int ofs);

		// Token: 0x060013C6 RID: 5062 RVA: 0x00045D18 File Offset: 0x00043F18
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MonoTODO]
		public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013C7 RID: 5063
		[MethodImpl(4096)]
		public static extern IntPtr ReAllocCoTaskMem(IntPtr pv, int cb);

		// Token: 0x060013C8 RID: 5064
		[MethodImpl(4096)]
		public static extern IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb);

		// Token: 0x060013C9 RID: 5065
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		private static extern int ReleaseInternal(IntPtr pUnk);

		// Token: 0x060013CA RID: 5066 RVA: 0x00045D20 File Offset: 0x00043F20
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Release(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.ReleaseInternal(pUnk);
		}

		// Token: 0x060013CB RID: 5067
		[MethodImpl(4096)]
		private static extern int ReleaseComObjectInternal(object co);

		// Token: 0x060013CC RID: 5068 RVA: 0x00045D48 File Offset: 0x00043F48
		public static int ReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentException("Value cannot be null.", "o");
			}
			if (!Marshal.IsComObject(o))
			{
				throw new ArgumentException("Value must be a Com object.", "o");
			}
			return Marshal.ReleaseComObjectInternal(o);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00045D84 File Offset: 0x00043F84
		[MonoTODO]
		[Obsolete]
		public static void ReleaseThreadCache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00045D8C File Offset: 0x00043F8C
		[MonoNotSupported("MSDN states user code should never need to call this method.")]
		public static bool SetComObjectData(object obj, object key, object data)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00045D98 File Offset: 0x00043F98
		[ComVisible(true)]
		public static int SizeOf(object structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		// Token: 0x060013D0 RID: 5072
		[MethodImpl(4096)]
		public static extern int SizeOf(Type t);

		// Token: 0x060013D1 RID: 5073
		[MethodImpl(4096)]
		public static extern IntPtr StringToBSTR(string s);

		// Token: 0x060013D2 RID: 5074 RVA: 0x00045DA8 File Offset: 0x00043FA8
		public static IntPtr StringToCoTaskMemAnsi(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			byte[] array = new byte[num];
			for (int i = 0; i < s.Length; i++)
			{
				array[i] = (byte)s[i];
			}
			array[s.Length] = 0;
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00045E04 File Offset: 0x00044004
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.StringToCoTaskMemAnsi(s) : Marshal.StringToCoTaskMemUni(s);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00045E24 File Offset: 0x00044024
		public static IntPtr StringToCoTaskMemUni(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * 2);
			char[] array = new char[num];
			s.CopyTo(0, array, 0, s.Length);
			array[s.Length] = '\0';
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		// Token: 0x060013D5 RID: 5077
		[MethodImpl(4096)]
		public static extern IntPtr StringToHGlobalAnsi(string s);

		// Token: 0x060013D6 RID: 5078 RVA: 0x00045E6C File Offset: 0x0004406C
		public static IntPtr StringToHGlobalAuto(string s)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.StringToHGlobalAnsi(s) : Marshal.StringToHGlobalUni(s);
		}

		// Token: 0x060013D7 RID: 5079
		[MethodImpl(4096)]
		public static extern IntPtr StringToHGlobalUni(string s);

		// Token: 0x060013D8 RID: 5080 RVA: 0x00045E8C File Offset: 0x0004408C
		public static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem((length + 1) * 2 + 4);
			byte[] array = null;
			Marshal.WriteInt32(intPtr, 0, length * 2);
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, 4 + i * 2, (short)((int)array[i * 2] << 8 | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, 4 + array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return (IntPtr)((long)intPtr + 4L);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00045F54 File Offset: 0x00044154
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(length + 1);
			byte[] array = new byte[length + 1];
			try
			{
				byte[] buffer = s.GetBuffer();
				int i = 0;
				int num = 0;
				while (i < length)
				{
					array[i] = buffer[num + 1];
					buffer[num] = 0;
					buffer[num + 1] = 0;
					i++;
					num += 2;
				}
				array[i] = 0;
				Marshal.copy_to_unmanaged(array, 0, intPtr, length + 1);
			}
			finally
			{
				int j = length;
				while (j > 0)
				{
					j--;
					array[j] = 0;
				}
			}
			return intPtr;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004600C File Offset: 0x0004420C
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(length * 2 + 2);
			byte[] array = null;
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, i * 2, (short)((int)array[i * 2] << 8 | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return intPtr;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000460B4 File Offset: 0x000442B4
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x000460D0 File Offset: 0x000442D0
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		// Token: 0x060013DD RID: 5085
		[ComVisible(true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(4096)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		// Token: 0x060013DE RID: 5086 RVA: 0x000460EC File Offset: 0x000442EC
		public static void ThrowExceptionForHR(int errorCode)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00046108 File Offset: 0x00044308
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode, errorInfo);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		// Token: 0x060013E0 RID: 5088
		[MethodImpl(4096)]
		public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

		// Token: 0x060013E1 RID: 5089 RVA: 0x00046128 File Offset: 0x00044328
		public static void WriteByte(IntPtr ptr, byte val)
		{
			Marshal.WriteByte(ptr, 0, val);
		}

		// Token: 0x060013E2 RID: 5090
		[MethodImpl(4096)]
		public static extern void WriteByte(IntPtr ptr, int ofs, byte val);

		// Token: 0x060013E3 RID: 5091 RVA: 0x00046134 File Offset: 0x00044334
		[MonoTODO]
		public static void WriteByte([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, byte val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004613C File Offset: 0x0004433C
		public static void WriteInt16(IntPtr ptr, short val)
		{
			Marshal.WriteInt16(ptr, 0, val);
		}

		// Token: 0x060013E5 RID: 5093
		[MethodImpl(4096)]
		public static extern void WriteInt16(IntPtr ptr, int ofs, short val);

		// Token: 0x060013E6 RID: 5094 RVA: 0x00046148 File Offset: 0x00044348
		[MonoTODO]
		public static void WriteInt16([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, short val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00046150 File Offset: 0x00044350
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, val);
		}

		// Token: 0x060013E8 RID: 5096
		[MonoTODO]
		[MethodImpl(4096)]
		public static extern void WriteInt16(IntPtr ptr, int ofs, char val);

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004615C File Offset: 0x0004435C
		[MonoTODO]
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00046164 File Offset: 0x00044364
		public static void WriteInt32(IntPtr ptr, int val)
		{
			Marshal.WriteInt32(ptr, 0, val);
		}

		// Token: 0x060013EB RID: 5099
		[MethodImpl(4096)]
		public static extern void WriteInt32(IntPtr ptr, int ofs, int val);

		// Token: 0x060013EC RID: 5100 RVA: 0x00046170 File Offset: 0x00044370
		[MonoTODO]
		public static void WriteInt32([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, int val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00046178 File Offset: 0x00044378
		public static void WriteInt64(IntPtr ptr, long val)
		{
			Marshal.WriteInt64(ptr, 0, val);
		}

		// Token: 0x060013EE RID: 5102
		[MethodImpl(4096)]
		public static extern void WriteInt64(IntPtr ptr, int ofs, long val);

		// Token: 0x060013EF RID: 5103 RVA: 0x00046184 File Offset: 0x00044384
		[MonoTODO]
		public static void WriteInt64([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, long val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004618C File Offset: 0x0004438C
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			Marshal.WriteIntPtr(ptr, 0, val);
		}

		// Token: 0x060013F1 RID: 5105
		[MethodImpl(4096)]
		public static extern void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val);

		// Token: 0x060013F2 RID: 5106 RVA: 0x00046198 File Offset: 0x00044398
		[MonoTODO]
		public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, IntPtr val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000461A0 File Offset: 0x000443A0
		public static Exception GetExceptionForHR(int errorCode)
		{
			return Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000461B0 File Offset: 0x000443B0
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			if (errorCode == -2147024882)
			{
				return new OutOfMemoryException();
			}
			if (errorCode == -2147024809)
			{
				return new ArgumentException();
			}
			if (errorCode < 0)
			{
				return new COMException(string.Empty, errorCode);
			}
			return null;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000461FC File Offset: 0x000443FC
		public static int FinalReleaseComObject(object o)
		{
			while (Marshal.ReleaseComObject(o) != 0)
			{
			}
			return 0;
		}

		// Token: 0x060013F6 RID: 5110
		[MethodImpl(4096)]
		private static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		// Token: 0x060013F7 RID: 5111 RVA: 0x00046210 File Offset: 0x00044410
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsSubclassOf(typeof(MulticastDelegate)) || t == typeof(MulticastDelegate))
			{
				throw new ArgumentException("Type is not a delegate", "t");
			}
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		// Token: 0x060013F8 RID: 5112
		[MethodImpl(4096)]
		private static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		// Token: 0x060013F9 RID: 5113 RVA: 0x00046288 File Offset: 0x00044488
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		// Token: 0x04000A06 RID: 2566
		public static readonly int SystemMaxDBCSCharSize = 2;

		// Token: 0x04000A07 RID: 2567
		public static readonly int SystemDefaultCharSize = (Environment.OSVersion.Platform != PlatformID.Win32NT) ? 1 : 2;
	}
}
