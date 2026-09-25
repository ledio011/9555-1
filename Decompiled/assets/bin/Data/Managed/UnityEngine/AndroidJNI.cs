using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	public sealed class AndroidJNI
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004D28 File Offset: 0x00002F28
		private AndroidJNI()
		{
		}

		// Token: 0x0600004A RID: 74
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int AttachCurrentThread();

		// Token: 0x0600004B RID: 75
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int DetachCurrentThread();

		// Token: 0x0600004C RID: 76
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetVersion();

		// Token: 0x0600004D RID: 77
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr FindClass(string name);

		// Token: 0x0600004E RID: 78
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		// Token: 0x0600004F RID: 79
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr FromReflectedField(IntPtr refField);

		// Token: 0x06000050 RID: 80
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic);

		// Token: 0x06000051 RID: 81
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic);

		// Token: 0x06000052 RID: 82
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetSuperclass(IntPtr clazz);

		// Token: 0x06000053 RID: 83
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

		// Token: 0x06000054 RID: 84
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int Throw(IntPtr obj);

		// Token: 0x06000055 RID: 85
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int ThrowNew(IntPtr clazz, string message);

		// Token: 0x06000056 RID: 86
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ExceptionOccurred();

		// Token: 0x06000057 RID: 87
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ExceptionDescribe();

		// Token: 0x06000058 RID: 88
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ExceptionClear();

		// Token: 0x06000059 RID: 89
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void FatalError(string message);

		// Token: 0x0600005A RID: 90
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int PushLocalFrame(int capacity);

		// Token: 0x0600005B RID: 91
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr PopLocalFrame(IntPtr result);

		// Token: 0x0600005C RID: 92
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		// Token: 0x0600005D RID: 93
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DeleteGlobalRef(IntPtr obj);

		// Token: 0x0600005E RID: 94
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		// Token: 0x0600005F RID: 95
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DeleteLocalRef(IntPtr obj);

		// Token: 0x06000060 RID: 96
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		// Token: 0x06000061 RID: 97
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int EnsureLocalCapacity(int capacity);

		// Token: 0x06000062 RID: 98
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr AllocObject(IntPtr clazz);

		// Token: 0x06000063 RID: 99
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000064 RID: 100
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		// Token: 0x06000065 RID: 101
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool IsInstanceOf(IntPtr obj, IntPtr clazz);

		// Token: 0x06000066 RID: 102
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetMethodID(IntPtr clazz, string name, string sig);

		// Token: 0x06000067 RID: 103
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetFieldID(IntPtr clazz, string name, string sig);

		// Token: 0x06000068 RID: 104
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig);

		// Token: 0x06000069 RID: 105
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig);

		// Token: 0x0600006A RID: 106
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewStringUTF(string bytes);

		// Token: 0x0600006B RID: 107
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetStringUTFLength(IntPtr str);

		// Token: 0x0600006C RID: 108
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetStringUTFChars(IntPtr str);

		// Token: 0x0600006D RID: 109
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x0600006E RID: 110
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x0600006F RID: 111
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000070 RID: 112
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000071 RID: 113
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000072 RID: 114
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000073 RID: 115
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000074 RID: 116
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000075 RID: 117
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000076 RID: 118
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000077 RID: 119
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000078 RID: 120
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetStringField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000079 RID: 121
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetObjectField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600007A RID: 122
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetBooleanField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600007B RID: 123
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern byte GetByteField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600007C RID: 124
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern char GetCharField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600007D RID: 125
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern short GetShortField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600007E RID: 126
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetIntField(IntPtr obj, IntPtr fieldID);

		// Token: 0x0600007F RID: 127
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern long GetLongField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000080 RID: 128
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetFloatField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000081 RID: 129
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern double GetDoubleField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000082 RID: 130
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStringField(IntPtr obj, IntPtr fieldID, string val);

		// Token: 0x06000083 RID: 131
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val);

		// Token: 0x06000084 RID: 132
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val);

		// Token: 0x06000085 RID: 133
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetByteField(IntPtr obj, IntPtr fieldID, byte val);

		// Token: 0x06000086 RID: 134
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetCharField(IntPtr obj, IntPtr fieldID, char val);

		// Token: 0x06000087 RID: 135
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetShortField(IntPtr obj, IntPtr fieldID, short val);

		// Token: 0x06000088 RID: 136
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetIntField(IntPtr obj, IntPtr fieldID, int val);

		// Token: 0x06000089 RID: 137
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetLongField(IntPtr obj, IntPtr fieldID, long val);

		// Token: 0x0600008A RID: 138
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetFloatField(IntPtr obj, IntPtr fieldID, float val);

		// Token: 0x0600008B RID: 139
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetDoubleField(IntPtr obj, IntPtr fieldID, double val);

		// Token: 0x0600008C RID: 140
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x0600008D RID: 141
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x0600008E RID: 142
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x0600008F RID: 143
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000090 RID: 144
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000091 RID: 145
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000092 RID: 146
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000093 RID: 147
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000094 RID: 148
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000095 RID: 149
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000096 RID: 150
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000097 RID: 151
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetStaticStringField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000098 RID: 152
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000099 RID: 153
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600009A RID: 154
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern byte GetStaticByteField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600009B RID: 155
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern char GetStaticCharField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600009C RID: 156
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern short GetStaticShortField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600009D RID: 157
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetStaticIntField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600009E RID: 158
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern long GetStaticLongField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x0600009F RID: 159
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetStaticFloatField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000A0 RID: 160
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000A1 RID: 161
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val);

		// Token: 0x060000A2 RID: 162
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val);

		// Token: 0x060000A3 RID: 163
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val);

		// Token: 0x060000A4 RID: 164
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val);

		// Token: 0x060000A5 RID: 165
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val);

		// Token: 0x060000A6 RID: 166
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val);

		// Token: 0x060000A7 RID: 167
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val);

		// Token: 0x060000A8 RID: 168
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val);

		// Token: 0x060000A9 RID: 169
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val);

		// Token: 0x060000AA RID: 170
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val);

		// Token: 0x060000AB RID: 171
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToBooleanArray(bool[] array);

		// Token: 0x060000AC RID: 172
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToByteArray(byte[] array);

		// Token: 0x060000AD RID: 173
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToCharArray(char[] array);

		// Token: 0x060000AE RID: 174
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToShortArray(short[] array);

		// Token: 0x060000AF RID: 175
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToIntArray(int[] array);

		// Token: 0x060000B0 RID: 176
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToLongArray(long[] array);

		// Token: 0x060000B1 RID: 177
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToFloatArray(float[] array);

		// Token: 0x060000B2 RID: 178
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToDoubleArray(double[] array);

		// Token: 0x060000B3 RID: 179
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass);

		// Token: 0x060000B4 RID: 180 RVA: 0x00004D30 File Offset: 0x00002F30
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return AndroidJNI.ToObjectArray(array, IntPtr.Zero);
		}

		// Token: 0x060000B5 RID: 181
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool[] FromBooleanArray(IntPtr array);

		// Token: 0x060000B6 RID: 182
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern byte[] FromByteArray(IntPtr array);

		// Token: 0x060000B7 RID: 183
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern char[] FromCharArray(IntPtr array);

		// Token: 0x060000B8 RID: 184
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern short[] FromShortArray(IntPtr array);

		// Token: 0x060000B9 RID: 185
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int[] FromIntArray(IntPtr array);

		// Token: 0x060000BA RID: 186
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern long[] FromLongArray(IntPtr array);

		// Token: 0x060000BB RID: 187
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float[] FromFloatArray(IntPtr array);

		// Token: 0x060000BC RID: 188
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern double[] FromDoubleArray(IntPtr array);

		// Token: 0x060000BD RID: 189
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr[] FromObjectArray(IntPtr array);

		// Token: 0x060000BE RID: 190
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetArrayLength(IntPtr array);

		// Token: 0x060000BF RID: 191
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewBooleanArray(int size);

		// Token: 0x060000C0 RID: 192
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewByteArray(int size);

		// Token: 0x060000C1 RID: 193
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewCharArray(int size);

		// Token: 0x060000C2 RID: 194
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewShortArray(int size);

		// Token: 0x060000C3 RID: 195
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewIntArray(int size);

		// Token: 0x060000C4 RID: 196
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewLongArray(int size);

		// Token: 0x060000C5 RID: 197
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewFloatArray(int size);

		// Token: 0x060000C6 RID: 198
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewDoubleArray(int size);

		// Token: 0x060000C7 RID: 199
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		// Token: 0x060000C8 RID: 200
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetBooleanArrayElement(IntPtr array, int index);

		// Token: 0x060000C9 RID: 201
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern byte GetByteArrayElement(IntPtr array, int index);

		// Token: 0x060000CA RID: 202
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern char GetCharArrayElement(IntPtr array, int index);

		// Token: 0x060000CB RID: 203
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern short GetShortArrayElement(IntPtr array, int index);

		// Token: 0x060000CC RID: 204
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetIntArrayElement(IntPtr array, int index);

		// Token: 0x060000CD RID: 205
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern long GetLongArrayElement(IntPtr array, int index);

		// Token: 0x060000CE RID: 206
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetFloatArrayElement(IntPtr array, int index);

		// Token: 0x060000CF RID: 207
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern double GetDoubleArrayElement(IntPtr array, int index);

		// Token: 0x060000D0 RID: 208
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		// Token: 0x060000D1 RID: 209
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetBooleanArrayElement(IntPtr array, int index, byte val);

		// Token: 0x060000D2 RID: 210
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetByteArrayElement(IntPtr array, int index, sbyte val);

		// Token: 0x060000D3 RID: 211
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetCharArrayElement(IntPtr array, int index, char val);

		// Token: 0x060000D4 RID: 212
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetShortArrayElement(IntPtr array, int index, short val);

		// Token: 0x060000D5 RID: 213
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetIntArrayElement(IntPtr array, int index, int val);

		// Token: 0x060000D6 RID: 214
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetLongArrayElement(IntPtr array, int index, long val);

		// Token: 0x060000D7 RID: 215
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetFloatArrayElement(IntPtr array, int index, float val);

		// Token: 0x060000D8 RID: 216
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetDoubleArrayElement(IntPtr array, int index, double val);

		// Token: 0x060000D9 RID: 217
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);
	}
}
