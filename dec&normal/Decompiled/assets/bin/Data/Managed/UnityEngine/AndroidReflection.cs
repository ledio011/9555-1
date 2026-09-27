using System;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	internal class AndroidReflection
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00006348 File Offset: 0x00004548
		private static IntPtr GetStaticMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNISafe.GetStaticMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return staticMethodID;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006388 File Offset: 0x00004588
		public static IntPtr GetConstructorMember(IntPtr jclass, string signature)
		{
			jvalue[] array = new jvalue[2];
			IntPtr result;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewStringUTF(signature);
				result = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetConstructorID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000063FC File Offset: 0x000045FC
		public static IntPtr GetMethodMember(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			IntPtr result;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewStringUTF(methodName);
				array[2].l = AndroidJNISafe.NewStringUTF(signature);
				array[3].z = isStatic;
				result = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetMethodID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000064A0 File Offset: 0x000046A0
		public static IntPtr GetFieldMember(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			IntPtr result;
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewStringUTF(fieldName);
				array[2].l = AndroidJNISafe.NewStringUTF(signature);
				array[3].z = isStatic;
				result = AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperGetFieldID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006544 File Offset: 0x00004744
		public static IntPtr NewProxyInstance(int delegateHandle, IntPtr interfaze)
		{
			jvalue[] array = new jvalue[2];
			array[0].i = delegateHandle;
			array[1].l = interfaze;
			return AndroidJNISafe.CallStaticObjectMethod(AndroidReflection.s_ReflectionHelperClass, AndroidReflection.s_ReflectionHelperNewProxyInstance, array);
		}

		// Token: 0x0400000A RID: 10
		private const string RELECTION_HELPER_CLASS_NAME = "com/unity3d/player/ReflectionHelper";

		// Token: 0x0400000B RID: 11
		private static IntPtr s_ReflectionHelperClass = AndroidJNI.NewGlobalRef(AndroidJNISafe.FindClass("com/unity3d/player/ReflectionHelper"));

		// Token: 0x0400000C RID: 12
		private static IntPtr s_ReflectionHelperGetConstructorID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getConstructorID", "(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/reflect/Constructor;");

		// Token: 0x0400000D RID: 13
		private static IntPtr s_ReflectionHelperGetMethodID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getMethodID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Method;");

		// Token: 0x0400000E RID: 14
		private static IntPtr s_ReflectionHelperGetFieldID = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Field;");

		// Token: 0x0400000F RID: 15
		private static IntPtr s_ReflectionHelperNewProxyInstance = AndroidReflection.GetStaticMethodID("com/unity3d/player/ReflectionHelper", "newProxyInstance", "(ILjava/lang/Class;)Ljava/lang/Object;");
	}
}
