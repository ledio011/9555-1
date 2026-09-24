using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public sealed class AndroidJNIHelper
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00004D40 File Offset: 0x00002F40
		private AndroidJNIHelper()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000DB RID: 219
		// (set) Token: 0x060000DC RID: 220
		public static extern bool debug { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060000DD RID: 221 RVA: 0x00004D48 File Offset: 0x00002F48
		[ExcludeFromDocs]
		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			string empty = string.Empty;
			return AndroidJNIHelper.GetConstructorID(javaClass, empty);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004D64 File Offset: 0x00002F64
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("\"\"")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D70 File Offset: 0x00002F70
		[ExcludeFromDocs]
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, string signature)
		{
			bool isStatic = false;
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004D88 File Offset: 0x00002F88
		[ExcludeFromDocs]
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			bool isStatic = false;
			string empty = string.Empty;
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, empty, isStatic);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("\"\"")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004DB4 File Offset: 0x00002FB4
		[ExcludeFromDocs]
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, string signature)
		{
			bool isStatic = false;
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004DCC File Offset: 0x00002FCC
		[ExcludeFromDocs]
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			bool isStatic = false;
			string empty = string.Empty;
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, empty, isStatic);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004DEC File Offset: 0x00002FEC
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("\"\"")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x060000E6 RID: 230
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr CreateJavaProxy(AndroidJavaProxy proxy);

		// Token: 0x060000E7 RID: 231 RVA: 0x00004E00 File Offset: 0x00003000
		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004E08 File Offset: 0x00003008
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return _AndroidJNIHelper.CreateJNIArgArray(args);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E10 File Offset: 0x00003010
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004E1C File Offset: 0x0000301C
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004E28 File Offset: 0x00003028
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004E34 File Offset: 0x00003034
		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004E3C File Offset: 0x0000303C
		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004E44 File Offset: 0x00003044
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004E4C File Offset: 0x0000304C
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E58 File Offset: 0x00003058
		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004E64 File Offset: 0x00003064
		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}
	}
}
