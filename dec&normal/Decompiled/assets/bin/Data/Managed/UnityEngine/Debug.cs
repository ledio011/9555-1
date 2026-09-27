using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000047 RID: 71
	public sealed class Debug
	{
		// Token: 0x0600038B RID: 907 RVA: 0x00008104 File Offset: 0x00006304
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00008128 File Offset: 0x00006328
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref white, duration, depthTest);
		}

		// Token: 0x0600038D RID: 909
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 start, ref Vector3 end, ref Color color, float duration, bool depthTest);

		// Token: 0x0600038E RID: 910
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Log(int level, string msg, [Writable] Object obj);

		// Token: 0x0600038F RID: 911
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_LogException(Exception exception, [Writable] Object obj);

		// Token: 0x06000390 RID: 912 RVA: 0x00008150 File Offset: 0x00006350
		public static void Log(object message)
		{
			Debug.Internal_Log(0, (message == null) ? "Null" : message.ToString(), null);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00008170 File Offset: 0x00006370
		public static void Log(object message, Object context)
		{
			Debug.Internal_Log(0, (message == null) ? "Null" : message.ToString(), context);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00008190 File Offset: 0x00006390
		public static void LogError(object message)
		{
			Debug.Internal_Log(2, (message == null) ? "Null" : message.ToString(), null);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000081B0 File Offset: 0x000063B0
		public static void LogError(object message, Object context)
		{
			Debug.Internal_Log(2, message.ToString(), context);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000081C0 File Offset: 0x000063C0
		public static void LogException(Exception exception)
		{
			Debug.Internal_LogException(exception, null);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000081CC File Offset: 0x000063CC
		public static void LogException(Exception exception, Object context)
		{
			Debug.Internal_LogException(exception, context);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000081D8 File Offset: 0x000063D8
		public static void LogWarning(object message)
		{
			Debug.Internal_Log(1, message.ToString(), null);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000081E8 File Offset: 0x000063E8
		public static void LogWarning(object message, Object context)
		{
			Debug.Internal_Log(1, message.ToString(), context);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000398 RID: 920
		public static extern bool isDebugBuild { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
