using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200004E RID: 78
	[StructLayout(0)]
	public sealed class Event
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x000083E0 File Offset: 0x000065E0
		public Event()
		{
			this.Init();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000083F0 File Offset: 0x000065F0
		public Event(Event other)
		{
			if (other == null)
			{
				throw new ArgumentException("Event to copy from is null.");
			}
			this.InitCopy(other);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00008410 File Offset: 0x00006610
		private Event(IntPtr ptr)
		{
			this.InitPtr(ptr);
		}

		// Token: 0x060003B8 RID: 952
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x060003B9 RID: 953 RVA: 0x00008420 File Offset: 0x00006620
		~Event()
		{
			this.Cleanup();
		}

		// Token: 0x060003BA RID: 954
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060003BB RID: 955
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InitCopy(Event other);

		// Token: 0x060003BC RID: 956
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InitPtr(IntPtr ptr);

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003BD RID: 957
		public extern EventType rawType { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003BE RID: 958
		// (set) Token: 0x060003BF RID: 959
		public extern EventType type { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060003C0 RID: 960
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern EventType GetTypeForControl(int controlID);

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00008450 File Offset: 0x00006650
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00008468 File Offset: 0x00006668
		public Vector2 mousePosition
		{
			get
			{
				Vector2 result;
				this.Internal_GetMousePosition(out result);
				return result;
			}
			set
			{
				this.Internal_SetMousePosition(value);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00008474 File Offset: 0x00006674
		private void Internal_SetMousePosition(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMousePosition(this, ref value);
		}

		// Token: 0x060003C4 RID: 964
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_SetMousePosition(Event self, ref Vector2 value);

		// Token: 0x060003C5 RID: 965
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_GetMousePosition(out Vector2 value);

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00008480 File Offset: 0x00006680
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00008498 File Offset: 0x00006698
		public Vector2 delta
		{
			get
			{
				Vector2 result;
				this.Internal_GetMouseDelta(out result);
				return result;
			}
			set
			{
				this.Internal_SetMouseDelta(value);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000084A4 File Offset: 0x000066A4
		private void Internal_SetMouseDelta(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMouseDelta(this, ref value);
		}

		// Token: 0x060003C9 RID: 969
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_SetMouseDelta(Event self, ref Vector2 value);

		// Token: 0x060003CA RID: 970
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_GetMouseDelta(out Vector2 value);

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000084B0 File Offset: 0x000066B0
		// (set) Token: 0x060003CC RID: 972 RVA: 0x000084C4 File Offset: 0x000066C4
		[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
		public Ray mouseRay
		{
			get
			{
				return new Ray(Vector3.up, Vector3.up);
			}
			set
			{
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003CD RID: 973
		// (set) Token: 0x060003CE RID: 974
		public extern int button { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003CF RID: 975
		// (set) Token: 0x060003D0 RID: 976
		public extern EventModifiers modifiers { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003D1 RID: 977
		// (set) Token: 0x060003D2 RID: 978
		public extern float pressure { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D3 RID: 979
		// (set) Token: 0x060003D4 RID: 980
		public extern int clickCount { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D5 RID: 981
		// (set) Token: 0x060003D6 RID: 982
		public extern char character { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003D7 RID: 983
		// (set) Token: 0x060003D8 RID: 984
		public extern string commandName { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003D9 RID: 985
		// (set) Token: 0x060003DA RID: 986
		public extern KeyCode keyCode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000084C8 File Offset: 0x000066C8
		// (set) Token: 0x060003DC RID: 988 RVA: 0x000084D8 File Offset: 0x000066D8
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00008504 File Offset: 0x00006704
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00008514 File Offset: 0x00006714
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Control;
				}
				else
				{
					this.modifiers |= EventModifiers.Control;
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00008540 File Offset: 0x00006740
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00008550 File Offset: 0x00006750
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Alt;
				}
				else
				{
					this.modifiers |= EventModifiers.Alt;
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000857C File Offset: 0x0000677C
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000858C File Offset: 0x0000678C
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Command;
				}
				else
				{
					this.modifiers |= EventModifiers.Command;
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000085B8 File Offset: 0x000067B8
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x000085CC File Offset: 0x000067CC
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.CapsLock;
				}
				else
				{
					this.modifiers |= EventModifiers.CapsLock;
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000085F8 File Offset: 0x000067F8
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000860C File Offset: 0x0000680C
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) != EventModifiers.None;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00008638 File Offset: 0x00006838
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) != EventModifiers.None;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000864C File Offset: 0x0000684C
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00008654 File Offset: 0x00006854
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				if (value != null)
				{
					Event.s_Current = value;
				}
				else
				{
					Event.s_Current = Event.s_MasterEvent;
				}
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x060003EA RID: 1002
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x060003EB RID: 1003 RVA: 0x00008680 File Offset: 0x00006880
		private static void Internal_MakeMasterEventCurrent()
		{
			if (Event.s_MasterEvent == null)
			{
				Event.s_MasterEvent = new Event();
			}
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x060003EC RID: 1004
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Use();

		// Token: 0x060003ED RID: 1005
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool PopEvent(Event outEvent);

		// Token: 0x060003EE RID: 1006
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetEventCount();

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000086B0 File Offset: 0x000068B0
		public bool isKey
		{
			get
			{
				EventType type = this.type;
				return type == EventType.KeyDown || type == EventType.KeyUp;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000086D4 File Offset: 0x000068D4
		public bool isMouse
		{
			get
			{
				EventType type = this.type;
				return type == EventType.MouseMove || type == EventType.MouseDown || type == EventType.MouseUp || type == EventType.MouseDrag;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00008704 File Offset: 0x00006904
		public static Event KeyboardEvent(string key)
		{
			Event @event = new Event();
			@event.type = EventType.KeyDown;
			if (key == null || key == string.Empty)
			{
				return @event;
			}
			int num = 0;
			bool flag;
			do
			{
				flag = true;
				if (num >= key.Length)
				{
					break;
				}
				char c = key[num];
				switch (c)
				{
				case '#':
					@event.modifiers |= EventModifiers.Shift;
					num++;
					break;
				default:
					if (c != '^')
					{
						flag = false;
					}
					else
					{
						@event.modifiers |= EventModifiers.Control;
						num++;
					}
					break;
				case '%':
					@event.modifiers |= EventModifiers.Command;
					num++;
					break;
				case '&':
					@event.modifiers |= EventModifiers.Alt;
					num++;
					break;
				}
			}
			while (flag);
			string text = key.Substring(num, key.Length - num).ToLower();
			string text2 = text;
			switch (text2)
			{
			case "[0]":
				@event.character = '0';
				@event.keyCode = KeyCode.Keypad0;
				return @event;
			case "[1]":
				@event.character = '1';
				@event.keyCode = KeyCode.Keypad1;
				return @event;
			case "[2]":
				@event.character = '2';
				@event.keyCode = KeyCode.Keypad2;
				return @event;
			case "[3]":
				@event.character = '3';
				@event.keyCode = KeyCode.Keypad3;
				return @event;
			case "[4]":
				@event.character = '4';
				@event.keyCode = KeyCode.Keypad4;
				return @event;
			case "[5]":
				@event.character = '5';
				@event.keyCode = KeyCode.Keypad5;
				return @event;
			case "[6]":
				@event.character = '6';
				@event.keyCode = KeyCode.Keypad6;
				return @event;
			case "[7]":
				@event.character = '7';
				@event.keyCode = KeyCode.Keypad7;
				return @event;
			case "[8]":
				@event.character = '8';
				@event.keyCode = KeyCode.Keypad8;
				return @event;
			case "[9]":
				@event.character = '9';
				@event.keyCode = KeyCode.Keypad9;
				return @event;
			case "[.]":
				@event.character = '.';
				@event.keyCode = KeyCode.KeypadPeriod;
				return @event;
			case "[/]":
				@event.character = '/';
				@event.keyCode = KeyCode.KeypadDivide;
				return @event;
			case "[-]":
				@event.character = '-';
				@event.keyCode = KeyCode.KeypadMinus;
				return @event;
			case "[+]":
				@event.character = '+';
				@event.keyCode = KeyCode.KeypadPlus;
				return @event;
			case "[=]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[equals]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[enter]":
				@event.character = '\n';
				@event.keyCode = KeyCode.KeypadEnter;
				return @event;
			case "up":
				@event.keyCode = KeyCode.UpArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "down":
				@event.keyCode = KeyCode.DownArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "left":
				@event.keyCode = KeyCode.LeftArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "right":
				@event.keyCode = KeyCode.RightArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "insert":
				@event.keyCode = KeyCode.Insert;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "home":
				@event.keyCode = KeyCode.Home;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "end":
				@event.keyCode = KeyCode.End;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgup":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page up":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgdown":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page down":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "backspace":
				@event.keyCode = KeyCode.Backspace;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "delete":
				@event.keyCode = KeyCode.Delete;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "tab":
				@event.keyCode = KeyCode.Tab;
				return @event;
			case "f1":
				@event.keyCode = KeyCode.F1;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f2":
				@event.keyCode = KeyCode.F2;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f3":
				@event.keyCode = KeyCode.F3;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f4":
				@event.keyCode = KeyCode.F4;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f5":
				@event.keyCode = KeyCode.F5;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f6":
				@event.keyCode = KeyCode.F6;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f7":
				@event.keyCode = KeyCode.F7;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f8":
				@event.keyCode = KeyCode.F8;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f9":
				@event.keyCode = KeyCode.F9;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f10":
				@event.keyCode = KeyCode.F10;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f11":
				@event.keyCode = KeyCode.F11;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f12":
				@event.keyCode = KeyCode.F12;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f13":
				@event.keyCode = KeyCode.F13;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f14":
				@event.keyCode = KeyCode.F14;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f15":
				@event.keyCode = KeyCode.F15;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "[esc]":
				@event.keyCode = KeyCode.Escape;
				return @event;
			case "return":
				@event.character = '\n';
				@event.keyCode = KeyCode.Return;
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			case "space":
				@event.keyCode = KeyCode.Space;
				@event.character = ' ';
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			}
			if (text.Length != 1)
			{
				try
				{
					@event.keyCode = (KeyCode)((int)Enum.Parse(typeof(KeyCode), text, true));
				}
				catch (ArgumentException)
				{
					Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[]
					{
						text
					}));
				}
			}
			else
			{
				@event.character = text.ToLower()[0];
				@event.keyCode = (KeyCode)@event.character;
				if (@event.modifiers != EventModifiers.None)
				{
					@event.character = '\0';
				}
			}
			return @event;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00009198 File Offset: 0x00007398
		public override int GetHashCode()
		{
			int num = 1;
			if (this.isKey)
			{
				num = (int)((ushort)this.keyCode);
			}
			if (this.isMouse)
			{
				num = this.mousePosition.GetHashCode();
			}
			return num * 37 | (int)this.modifiers;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000091E4 File Offset: 0x000073E4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			Event @event = (Event)obj;
			if (this.type != @event.type || this.modifiers != @event.modifiers)
			{
				return false;
			}
			if (this.isKey)
			{
				return this.keyCode == @event.keyCode && this.modifiers == @event.modifiers;
			}
			return this.isMouse && this.mousePosition == @event.mousePosition;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00009294 File Offset: 0x00007494
		public override string ToString()
		{
			if (this.isKey)
			{
				if (this.character == '\0')
				{
					return UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[]
					{
						this.type,
						this.modifiers,
						this.keyCode
					});
				}
				return UnityString.Format(string.Concat(new object[]
				{
					"Event:",
					this.type,
					"   Character:",
					(int)this.character,
					"   Modifiers:",
					this.modifiers,
					"   KeyCode:",
					this.keyCode
				}), new object[0]);
			}
			else
			{
				if (this.isMouse)
				{
					return UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[]
					{
						this.type,
						this.mousePosition,
						this.modifiers
					});
				}
				if (this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand)
				{
					return UnityString.Format("Event: {0}  \"{1}\"", new object[]
					{
						this.type,
						this.commandName
					});
				}
				return string.Empty + this.type;
			}
		}

		// Token: 0x04000093 RID: 147
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000094 RID: 148
		private static Event s_Current;

		// Token: 0x04000095 RID: 149
		private static Event s_MasterEvent;
	}
}
