using System;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public class AndroidJavaObject : IDisposable
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003530 File Offset: 0x00001730
		internal AndroidJavaObject(IntPtr jobject) : this()
		{
			if (jobject == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(jobject);
			this.m_jobject = AndroidJNI.NewGlobalRef(jobject);
			this.m_jclass = AndroidJNI.NewGlobalRef(objectClass);
			AndroidJNISafe.DeleteLocalRef(objectClass);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003584 File Offset: 0x00001784
		internal AndroidJavaObject()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000358C File Offset: 0x0000178C
		public AndroidJavaObject(string className, params object[] args) : this()
		{
			this._AndroidJavaObject(className, args);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000035A0 File Offset: 0x000017A0
		protected void DebugPrint(string msg)
		{
			if (!AndroidJavaObject.enableDebugPrints)
			{
				return;
			}
			Debug.Log(msg);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000035B4 File Offset: 0x000017B4
		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
			if (!AndroidJavaObject.enableDebugPrints)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in args)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append((obj != null) ? obj.GetType().ToString() : "<null>");
			}
			Debug.Log(string.Concat(new string[]
			{
				call,
				"(\"",
				methodName,
				"\"",
				stringBuilder.ToString(),
				") = ",
				signature
			}));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003658 File Offset: 0x00001858
		private void _AndroidJavaObject(string className, params object[] args)
		{
			this.DebugPrint("Creating AndroidJavaObject from " + className);
			if (args == null)
			{
				args = new object[1];
			}
			using (AndroidJavaObject androidJavaObject = AndroidJavaObject.FindClass(className))
			{
				this.m_jclass = AndroidJNI.NewGlobalRef(androidJavaObject.GetRawObject());
				jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
				try
				{
					IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.m_jclass, args);
					IntPtr intPtr = AndroidJNISafe.NewObject(this.m_jclass, constructorID, array);
					this.m_jobject = AndroidJNI.NewGlobalRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr);
				}
				finally
				{
					AndroidJNIHelper.DeleteJNIArgArray(args, array);
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000370C File Offset: 0x0000190C
		~AndroidJavaObject()
		{
			this.Dispose(true);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000373C File Offset: 0x0000193C
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_disposed = true;
			AndroidJNISafe.DeleteGlobalRef(this.m_jobject);
			AndroidJNISafe.DeleteGlobalRef(this.m_jclass);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003768 File Offset: 0x00001968
		protected void _Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003778 File Offset: 0x00001978
		protected void _Call(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallVoidMethod(this.m_jobject, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000037D4 File Offset: 0x000019D4
		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType result;
			try
			{
				if (typeof(ReturnType).IsPrimitive)
				{
					if (typeof(ReturnType) == typeof(int))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallIntMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(bool))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallBooleanMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(byte))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallByteMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(short))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallShortMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(long))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallLongMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(float))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallFloatMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(double))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallDoubleMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(char))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallCharMethod(this.m_jobject, methodID, array));
					}
					else
					{
						result = default(ReturnType);
					}
				}
				else if (typeof(ReturnType) == typeof(string))
				{
					result = (ReturnType)((object)AndroidJNISafe.CallStringMethod(this.m_jobject, methodID, array));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaClass))
				{
					IntPtr jclass = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
					result = (ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(jclass));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaObject))
				{
					IntPtr jobject = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
					result = (ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(jobject));
				}
				else
				{
					if (!typeof(Array).IsAssignableFrom(typeof(ReturnType)))
					{
						throw new Exception("JNI: Unknown return type '" + typeof(ReturnType) + "'");
					}
					IntPtr array2 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
					result = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(array2));
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003B1C File Offset: 0x00001D1C
		protected FieldType _Get<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					return (FieldType)((object)AndroidJNISafe.GetIntField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(bool))
				{
					return (FieldType)((object)AndroidJNISafe.GetBooleanField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(byte))
				{
					return (FieldType)((object)AndroidJNISafe.GetByteField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(short))
				{
					return (FieldType)((object)AndroidJNISafe.GetShortField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(long))
				{
					return (FieldType)((object)AndroidJNISafe.GetLongField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(float))
				{
					return (FieldType)((object)AndroidJNISafe.GetFloatField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(double))
				{
					return (FieldType)((object)AndroidJNISafe.GetDoubleField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(char))
				{
					return (FieldType)((object)AndroidJNISafe.GetCharField(this.m_jobject, fieldID));
				}
				return default(FieldType);
			}
			else
			{
				if (typeof(FieldType) == typeof(string))
				{
					return (FieldType)((object)AndroidJNISafe.GetStringField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(AndroidJavaClass))
				{
					IntPtr objectField = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(objectField));
				}
				if (typeof(FieldType) == typeof(AndroidJavaObject))
				{
					IntPtr objectField2 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectField2));
				}
				if (typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					IntPtr objectField3 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
					return (FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(objectField3));
				}
				throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003DC4 File Offset: 0x00001FC4
		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					AndroidJNISafe.SetIntField(this.m_jobject, fieldID, (int)((object)val));
				}
				else if (typeof(FieldType) == typeof(bool))
				{
					AndroidJNISafe.SetBooleanField(this.m_jobject, fieldID, (bool)((object)val));
				}
				else if (typeof(FieldType) == typeof(byte))
				{
					AndroidJNISafe.SetByteField(this.m_jobject, fieldID, (byte)((object)val));
				}
				else if (typeof(FieldType) == typeof(short))
				{
					AndroidJNISafe.SetShortField(this.m_jobject, fieldID, (short)((object)val));
				}
				else if (typeof(FieldType) == typeof(long))
				{
					AndroidJNISafe.SetLongField(this.m_jobject, fieldID, (long)((object)val));
				}
				else if (typeof(FieldType) == typeof(float))
				{
					AndroidJNISafe.SetFloatField(this.m_jobject, fieldID, (float)((object)val));
				}
				else if (typeof(FieldType) == typeof(double))
				{
					AndroidJNISafe.SetDoubleField(this.m_jobject, fieldID, (double)((object)val));
				}
				else if (typeof(FieldType) == typeof(char))
				{
					AndroidJNISafe.SetCharField(this.m_jobject, fieldID, (char)((object)val));
				}
			}
			else if (typeof(FieldType) == typeof(string))
			{
				AndroidJNISafe.SetStringField(this.m_jobject, fieldID, (string)((object)val));
			}
			else if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, ((AndroidJavaClass)((object)val)).m_jclass);
			}
			else if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, ((AndroidJavaObject)((object)val)).m_jobject);
			}
			else
			{
				if (!typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
				}
				IntPtr val2 = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
				AndroidJNISafe.SetObjectField(this.m_jclass, fieldID, val2);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000040A4 File Offset: 0x000022A4
		protected void _CallStatic(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallStaticVoidMethod(this.m_jclass, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004100 File Offset: 0x00002300
		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType result;
			try
			{
				if (typeof(ReturnType).IsPrimitive)
				{
					if (typeof(ReturnType) == typeof(int))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticIntMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(bool))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticBooleanMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(byte))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticByteMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(short))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticShortMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(long))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticLongMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(float))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticFloatMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(double))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticDoubleMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(char))
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticCharMethod(this.m_jclass, methodID, array));
					}
					else
					{
						result = default(ReturnType);
					}
				}
				else if (typeof(ReturnType) == typeof(string))
				{
					result = (ReturnType)((object)AndroidJNISafe.CallStaticStringMethod(this.m_jclass, methodID, array));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaClass))
				{
					IntPtr jclass = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
					result = (ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(jclass));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaObject))
				{
					IntPtr jobject = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
					result = (ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(jobject));
				}
				else
				{
					if (!typeof(Array).IsAssignableFrom(typeof(ReturnType)))
					{
						throw new Exception("JNI: Unknown return type '" + typeof(ReturnType) + "'");
					}
					IntPtr array2 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
					result = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(array2));
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004448 File Offset: 0x00002648
		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticIntField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(bool))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticBooleanField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(byte))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticByteField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(short))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticShortField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(long))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticLongField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(float))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticFloatField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(double))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticDoubleField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(char))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticCharField(this.m_jclass, fieldID));
				}
				return default(FieldType);
			}
			else
			{
				if (typeof(FieldType) == typeof(string))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticStringField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(AndroidJavaClass))
				{
					IntPtr staticObjectField = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(staticObjectField));
				}
				if (typeof(FieldType) == typeof(AndroidJavaObject))
				{
					IntPtr staticObjectField2 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(staticObjectField2));
				}
				if (typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					IntPtr staticObjectField3 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
					return (FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(staticObjectField3));
				}
				throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000046F0 File Offset: 0x000028F0
		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					AndroidJNISafe.SetStaticIntField(this.m_jclass, fieldID, (int)((object)val));
				}
				else if (typeof(FieldType) == typeof(bool))
				{
					AndroidJNISafe.SetStaticBooleanField(this.m_jclass, fieldID, (bool)((object)val));
				}
				else if (typeof(FieldType) == typeof(byte))
				{
					AndroidJNISafe.SetStaticByteField(this.m_jclass, fieldID, (byte)((object)val));
				}
				else if (typeof(FieldType) == typeof(short))
				{
					AndroidJNISafe.SetStaticShortField(this.m_jclass, fieldID, (short)((object)val));
				}
				else if (typeof(FieldType) == typeof(long))
				{
					AndroidJNISafe.SetStaticLongField(this.m_jclass, fieldID, (long)((object)val));
				}
				else if (typeof(FieldType) == typeof(float))
				{
					AndroidJNISafe.SetStaticFloatField(this.m_jclass, fieldID, (float)((object)val));
				}
				else if (typeof(FieldType) == typeof(double))
				{
					AndroidJNISafe.SetStaticDoubleField(this.m_jclass, fieldID, (double)((object)val));
				}
				else if (typeof(FieldType) == typeof(char))
				{
					AndroidJNISafe.SetStaticCharField(this.m_jclass, fieldID, (char)((object)val));
				}
			}
			else if (typeof(FieldType) == typeof(string))
			{
				AndroidJNISafe.SetStaticStringField(this.m_jclass, fieldID, (string)((object)val));
			}
			else if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, ((AndroidJavaClass)((object)val)).m_jclass);
			}
			else if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, ((AndroidJavaObject)((object)val)).m_jobject);
			}
			else
			{
				if (!typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
				}
				IntPtr val2 = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
				AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, val2);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000049D0 File Offset: 0x00002BD0
		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			AndroidJavaObject result;
			try
			{
				result = new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004A08 File Offset: 0x00002C08
		internal static AndroidJavaClass AndroidJavaClassDeleteLocalRef(IntPtr jclass)
		{
			AndroidJavaClass result;
			try
			{
				result = new AndroidJavaClass(jclass);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004A40 File Offset: 0x00002C40
		protected IntPtr _GetRawObject()
		{
			return this.m_jobject;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004A48 File Offset: 0x00002C48
		protected IntPtr _GetRawClass()
		{
			return this.m_jclass;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004A50 File Offset: 0x00002C50
		protected static AndroidJavaObject FindClass(string name)
		{
			return AndroidJavaObject.JavaLangClass.CallStatic<AndroidJavaObject>("forName", new object[]
			{
				name.Replace('/', '.')
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00004A80 File Offset: 0x00002C80
		protected static AndroidJavaClass JavaLangClass
		{
			get
			{
				if (AndroidJavaObject.s_JavaLangClass == null)
				{
					AndroidJavaObject.s_JavaLangClass = new AndroidJavaClass(AndroidJNISafe.FindClass("java/lang/Class"));
				}
				return AndroidJavaObject.s_JavaLangClass;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public void Dispose()
		{
			this._Dispose();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public void Call(string methodName, params object[] args)
		{
			this._Call(methodName, args);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004ABC File Offset: 0x00002CBC
		public void CallStatic(string methodName, params object[] args)
		{
			this._CallStatic(methodName, args);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public FieldType Get<FieldType>(string fieldName)
		{
			return this._Get<FieldType>(fieldName);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public void Set<FieldType>(string fieldName, FieldType val)
		{
			this._Set<FieldType>(fieldName, val);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return this._GetStatic<FieldType>(fieldName);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004AEC File Offset: 0x00002CEC
		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
			this._SetStatic<FieldType>(fieldName, val);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public IntPtr GetRawObject()
		{
			return this._GetRawObject();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004B00 File Offset: 0x00002D00
		public IntPtr GetRawClass()
		{
			return this._GetRawClass();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004B08 File Offset: 0x00002D08
		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._Call<ReturnType>(methodName, args);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004B14 File Offset: 0x00002D14
		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodName, args);
		}

		// Token: 0x04000003 RID: 3
		private static bool enableDebugPrints;

		// Token: 0x04000004 RID: 4
		private bool m_disposed;

		// Token: 0x04000005 RID: 5
		protected IntPtr m_jobject;

		// Token: 0x04000006 RID: 6
		protected IntPtr m_jclass;

		// Token: 0x04000007 RID: 7
		private static AndroidJavaClass s_JavaLangClass;
	}
}
