using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000125 RID: 293
	public sealed class TouchScreenKeyboard
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x00019780 File Offset: 0x00017980
		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments touchScreenKeyboard_InternalConstructorHelperArguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			touchScreenKeyboard_InternalConstructorHelperArguments.keyboardType = Convert.ToUInt32(keyboardType);
			touchScreenKeyboard_InternalConstructorHelperArguments.autocorrection = Convert.ToUInt32(autocorrection);
			touchScreenKeyboard_InternalConstructorHelperArguments.multiline = Convert.ToUInt32(multiline);
			touchScreenKeyboard_InternalConstructorHelperArguments.secure = Convert.ToUInt32(secure);
			touchScreenKeyboard_InternalConstructorHelperArguments.alert = Convert.ToUInt32(alert);
			this.TouchScreenKeyboard_InternalConstructorHelper(ref touchScreenKeyboard_InternalConstructorHelperArguments, text, textPlaceholder);
		}

		// Token: 0x06000A90 RID: 2704
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Destroy();

		// Token: 0x06000A91 RID: 2705 RVA: 0x000197F0 File Offset: 0x000179F0
		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x06000A92 RID: 2706
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder);

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00019820 File Offset: 0x00017A20
		public static bool isSupported
		{
			get
			{
				switch (Application.platform)
				{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
				case RuntimePlatform.WP8Player:
				case RuntimePlatform.BB10Player:
				case RuntimePlatform.TizenPlayer:
					return true;
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
					return false;
				}
				return false;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00019888 File Offset: 0x00017A88
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			string empty = string.Empty;
			bool alert = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000198AC File Offset: 0x00017AAC
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			string empty = string.Empty;
			bool alert = false;
			bool secure = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000198D0 File Offset: 0x00017AD0
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder);
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A97 RID: 2711
		// (set) Token: 0x06000A98 RID: 2712
		public extern string text { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700025D RID: 605
		// (set) Token: 0x06000A99 RID: 2713
		public static extern bool hideInput { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A9A RID: 2714
		// (set) Token: 0x06000A9B RID: 2715
		public extern bool active { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A9C RID: 2716
		public extern bool done { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A9D RID: 2717
		public extern bool wasCanceled { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x040004BF RID: 1215
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
