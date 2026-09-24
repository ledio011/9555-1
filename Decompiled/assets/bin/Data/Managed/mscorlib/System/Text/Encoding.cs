using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
	// Token: 0x020003A0 RID: 928
	[ComVisible(true)]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		// Token: 0x06001BB8 RID: 7096 RVA: 0x00067AF4 File Offset: 0x00065CF4
		protected Encoding()
		{
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00067B04 File Offset: 0x00065D04
		protected Encoding(int codePage)
		{
			this.windows_code_page = codePage;
			this.codePage = codePage;
			if (codePage != 1200 && codePage != 1201 && codePage != 12000 && codePage != 12001 && codePage != 65000 && codePage != 65001)
			{
				if (codePage != 20127 && codePage != 54936)
				{
					this.decoder_fallback = DecoderFallback.ReplacementFallback;
					this.encoder_fallback = EncoderFallback.ReplacementFallback;
				}
				else
				{
					this.decoder_fallback = DecoderFallback.ReplacementFallback;
					this.encoder_fallback = EncoderFallback.ReplacementFallback;
				}
			}
			else
			{
				this.decoder_fallback = DecoderFallback.StandardSafeFallback;
				this.encoder_fallback = EncoderFallback.StandardSafeFallback;
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00067DA8 File Offset: 0x00065FA8
		internal static string _(string arg)
		{
			return arg;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00067DAC File Offset: 0x00065FAC
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00067DB4 File Offset: 0x00065FB4
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x00067DBC File Offset: 0x00065FBC
		[ComVisible(false)]
		public DecoderFallback DecoderFallback
		{
			get
			{
				return this.decoder_fallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("This Encoding is readonly.");
				}
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.decoder_fallback = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00067DE8 File Offset: 0x00065FE8
		[ComVisible(false)]
		public EncoderFallback EncoderFallback
		{
			get
			{
				return this.encoder_fallback;
			}
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00067DF0 File Offset: 0x00065FF0
		internal void SetFallbackInternal(EncoderFallback e, DecoderFallback d)
		{
			if (e != null)
			{
				this.encoder_fallback = e;
			}
			if (d != null)
			{
				this.decoder_fallback = d;
			}
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00067E0C File Offset: 0x0006600C
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.codePage == encoding.codePage && this.DecoderFallback.Equals(encoding.DecoderFallback)) && this.EncoderFallback.Equals(encoding.EncoderFallback);
		}

		// Token: 0x06001BC2 RID: 7106
		public abstract int GetByteCount(char[] chars, int index, int count);

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00067E64 File Offset: 0x00066064
		public unsafe virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return 0;
			}
			fixed (char* chars = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				return this.GetByteCount(chars, s.Length);
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00067EA8 File Offset: 0x000660A8
		public virtual int GetByteCount(char[] chars)
		{
			if (chars != null)
			{
				return this.GetByteCount(chars, 0, chars.Length);
			}
			throw new ArgumentNullException("chars");
		}

		// Token: 0x06001BC5 RID: 7109
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00067EC8 File Offset: 0x000660C8
		public unsafe virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (charIndex < 0 || charIndex > s.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount < 0 || charIndex > s.Length - charCount)
			{
				throw new ArgumentOutOfRangeException("charCount", Encoding._("ArgRange_Array"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Encoding._("ArgRange_Array"));
			}
			if (charCount == 0 || bytes.Length == byteIndex)
			{
				return 0;
			}
			fixed (char* ptr = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (byte* ptr2 = ref (bytes != null && bytes.Length != 0) ? ref bytes[0] : ref *null)
				{
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, bytes.Length - byteIndex);
				}
			}
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00067FB8 File Offset: 0x000661B8
		public unsafe virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return new byte[0];
			}
			int byteCount = this.GetByteCount(s);
			if (byteCount == 0)
			{
				return new byte[0];
			}
			fixed (char* chars = s + RuntimeHelpers.OffsetToStringData / 2)
			{
				byte[] array = new byte[byteCount];
				fixed (byte* bytes = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
				{
					this.GetBytes(chars, s.Length, bytes, byteCount);
					return array;
				}
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00068040 File Offset: 0x00066240
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			int byteCount = this.GetByteCount(chars, index, count);
			byte[] array = new byte[byteCount];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0006806C File Offset: 0x0006626C
		public virtual byte[] GetBytes(char[] chars)
		{
			int byteCount = this.GetByteCount(chars, 0, chars.Length);
			byte[] array = new byte[byteCount];
			this.GetBytes(chars, 0, chars.Length, array, 0);
			return array;
		}

		// Token: 0x06001BCA RID: 7114
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x06001BCB RID: 7115
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x06001BCC RID: 7116 RVA: 0x0006809C File Offset: 0x0006629C
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			int charCount = this.GetCharCount(bytes, index, count);
			char[] array = new char[charCount];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000680C8 File Offset: 0x000662C8
		public virtual Decoder GetDecoder()
		{
			return new Encoding.ForwardingDecoder(this);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000680D0 File Offset: 0x000662D0
		private static object InvokeI18N(string name, params object[] args)
		{
			object obj = Encoding.lockobj;
			object result;
			lock (obj)
			{
				if (Encoding.i18nDisabled)
				{
					result = null;
				}
				else
				{
					if (Encoding.i18nAssembly == null)
					{
						try
						{
							try
							{
								Encoding.i18nAssembly = Assembly.Load("I18N, Version=2.0.5.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
							}
							catch (NotImplementedException)
							{
								Encoding.i18nDisabled = true;
								return null;
							}
							if (Encoding.i18nAssembly == null)
							{
								return null;
							}
						}
						catch (SystemException)
						{
							return null;
						}
					}
					Type type;
					try
					{
						type = Encoding.i18nAssembly.GetType("I18N.Common.Manager");
					}
					catch (NotImplementedException)
					{
						Encoding.i18nDisabled = true;
						return null;
					}
					if (type == null)
					{
						result = null;
					}
					else
					{
						object obj2;
						try
						{
							obj2 = type.InvokeMember("PrimaryManager", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null, null, null, null);
							if (obj2 == null)
							{
								return null;
							}
						}
						catch (MissingMethodException)
						{
							return null;
						}
						catch (SecurityException)
						{
							return null;
						}
						catch (NotImplementedException)
						{
							Encoding.i18nDisabled = true;
							return null;
						}
						try
						{
							result = type.InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, obj2, args, null, null, null);
						}
						catch (MissingMethodException)
						{
							result = null;
						}
						catch (SecurityException)
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000682FC File Offset: 0x000664FC
		public static Encoding GetEncoding(int codepage)
		{
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", "Valid values are between 0 and 65535, inclusive.");
			}
			int num = codepage;
			if (num == 1200)
			{
				return Encoding.Unicode;
			}
			if (num == 1201)
			{
				return Encoding.BigEndianUnicode;
			}
			if (num == 12000)
			{
				return Encoding.UTF32;
			}
			if (num == 12001)
			{
				return Encoding.BigEndianUTF32;
			}
			if (num == 65000)
			{
				return Encoding.UTF7;
			}
			if (num == 65001)
			{
				return Encoding.UTF8;
			}
			if (num == 0)
			{
				return Encoding.Default;
			}
			if (num == 20127)
			{
				return Encoding.ASCII;
			}
			if (num == 28591)
			{
				return Encoding.ISOLatin1;
			}
			Encoding encoding = (Encoding)Encoding.InvokeI18N("GetEncoding", new object[]
			{
				codepage
			});
			if (encoding != null)
			{
				encoding.is_readonly = true;
				return encoding;
			}
			string text = "System.Text.CP" + codepage.ToString();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type type = executingAssembly.GetType(text);
			if (type != null)
			{
				encoding = (Encoding)Activator.CreateInstance(type);
				encoding.is_readonly = true;
				return encoding;
			}
			type = Type.GetType(text);
			if (type != null)
			{
				encoding = (Encoding)Activator.CreateInstance(type);
				encoding.is_readonly = true;
				return encoding;
			}
			throw new NotSupportedException(string.Format("CodePage {0} not supported", codepage.ToString()));
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00068478 File Offset: 0x00066678
		[ComVisible(false)]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.is_readonly = false;
			return encoding;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0006849C File Offset: 0x0006669C
		public static Encoding GetEncoding(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			string text = name.ToLowerInvariant().Replace('-', '_');
			int codepage = 0;
			for (int i = 0; i < Encoding.encodings.Length; i++)
			{
				object obj = Encoding.encodings[i];
				if (obj is int)
				{
					codepage = (int)obj;
				}
				else if (text == (string)Encoding.encodings[i])
				{
					return Encoding.GetEncoding(codepage);
				}
			}
			Encoding encoding = (Encoding)Encoding.InvokeI18N("GetEncoding", new object[]
			{
				name
			});
			if (encoding != null)
			{
				return encoding;
			}
			string text2 = "System.Text.ENC" + text;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type type = executingAssembly.GetType(text2);
			if (type != null)
			{
				return (Encoding)Activator.CreateInstance(type);
			}
			type = Type.GetType(text2);
			if (type != null)
			{
				return (Encoding)Activator.CreateInstance(type);
			}
			throw new ArgumentException(string.Format("Encoding name '{0}' not supported", name), "name");
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000685AC File Offset: 0x000667AC
		public override int GetHashCode()
		{
			return this.DecoderFallback.GetHashCode() << 24 + this.EncoderFallback.GetHashCode() << 16 + this.codePage;
		}

		// Token: 0x06001BD3 RID: 7123
		public abstract int GetMaxByteCount(int charCount);

		// Token: 0x06001BD4 RID: 7124
		public abstract int GetMaxCharCount(int byteCount);

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000685D8 File Offset: 0x000667D8
		public virtual byte[] GetPreamble()
		{
			return new byte[0];
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000685E0 File Offset: 0x000667E0
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000685F0 File Offset: 0x000667F0
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00068610 File Offset: 0x00066810
		public virtual string HeaderName
		{
			get
			{
				return this.header_name;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00068618 File Offset: 0x00066818
		public static Encoding ASCII
		{
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.asciiEncoding == null)
						{
							Encoding.asciiEncoding = new ASCIIEncoding();
						}
					}
				}
				return Encoding.asciiEncoding;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00068678 File Offset: 0x00066878
		public static Encoding BigEndianUnicode
		{
			get
			{
				if (Encoding.bigEndianEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.bigEndianEncoding == null)
						{
							Encoding.bigEndianEncoding = new UnicodeEncoding(true, true);
						}
					}
				}
				return Encoding.bigEndianEncoding;
			}
		}

		// Token: 0x06001BDB RID: 7131
		[MethodImpl(4096)]
		internal static extern string InternalCodePage(ref int code_page);

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x000686DC File Offset: 0x000668DC
		public static Encoding Default
		{
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.defaultEncoding == null)
						{
							int num = 1;
							string name = Encoding.InternalCodePage(ref num);
							try
							{
								if (num == -1)
								{
									Encoding.defaultEncoding = Encoding.GetEncoding(name);
								}
								else
								{
									num &= 268435455;
									switch (num)
									{
									case 1:
										num = 20127;
										break;
									case 2:
										num = 65000;
										break;
									case 3:
										num = 65001;
										break;
									case 4:
										num = 1200;
										break;
									case 5:
										num = 1201;
										break;
									case 6:
										num = 28591;
										break;
									}
									Encoding.defaultEncoding = Encoding.GetEncoding(num);
								}
							}
							catch (NotSupportedException)
							{
								Encoding.defaultEncoding = Encoding.UTF8Unmarked;
							}
							catch (ArgumentException)
							{
								Encoding.defaultEncoding = Encoding.UTF8Unmarked;
							}
							Encoding.defaultEncoding.is_readonly = true;
						}
					}
				}
				return Encoding.defaultEncoding;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00068820 File Offset: 0x00066A20
		private static Encoding ISOLatin1
		{
			get
			{
				if (Encoding.isoLatin1Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.isoLatin1Encoding == null)
						{
							Encoding.isoLatin1Encoding = new Latin1Encoding();
						}
					}
				}
				return Encoding.isoLatin1Encoding;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00068880 File Offset: 0x00066A80
		public static Encoding UTF7
		{
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf7Encoding == null)
						{
							Encoding.utf7Encoding = new UTF7Encoding();
						}
					}
				}
				return Encoding.utf7Encoding;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x000688E0 File Offset: 0x00066AE0
		public static Encoding UTF8
		{
			get
			{
				if (Encoding.utf8EncodingWithMarkers == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf8EncodingWithMarkers == null)
						{
							Encoding.utf8EncodingWithMarkers = new UTF8Encoding(true);
						}
					}
				}
				return Encoding.utf8EncodingWithMarkers;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00068944 File Offset: 0x00066B44
		internal static Encoding UTF8Unmarked
		{
			get
			{
				if (Encoding.utf8EncodingWithoutMarkers == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf8EncodingWithoutMarkers == null)
						{
							Encoding.utf8EncodingWithoutMarkers = new UTF8Encoding(false, false);
						}
					}
				}
				return Encoding.utf8EncodingWithoutMarkers;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000689A8 File Offset: 0x00066BA8
		internal static Encoding UTF8UnmarkedUnsafe
		{
			get
			{
				if (Encoding.utf8EncodingUnsafe == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf8EncodingUnsafe == null)
						{
							Encoding.utf8EncodingUnsafe = new UTF8Encoding(false, false);
							Encoding.utf8EncodingUnsafe.is_readonly = false;
							Encoding.utf8EncodingUnsafe.DecoderFallback = new DecoderReplacementFallback(string.Empty);
							Encoding.utf8EncodingUnsafe.is_readonly = true;
						}
					}
				}
				return Encoding.utf8EncodingUnsafe;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00068A3C File Offset: 0x00066C3C
		public static Encoding Unicode
		{
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.unicodeEncoding == null)
						{
							Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
						}
					}
				}
				return Encoding.unicodeEncoding;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00068AA0 File Offset: 0x00066CA0
		public static Encoding UTF32
		{
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.utf32Encoding == null)
						{
							Encoding.utf32Encoding = new UTF32Encoding(false, true);
						}
					}
				}
				return Encoding.utf32Encoding;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00068B04 File Offset: 0x00066D04
		internal static Encoding BigEndianUTF32
		{
			get
			{
				if (Encoding.bigEndianUTF32Encoding == null)
				{
					object obj = Encoding.lockobj;
					lock (obj)
					{
						if (Encoding.bigEndianUTF32Encoding == null)
						{
							Encoding.bigEndianUTF32Encoding = new UTF32Encoding(true, true);
						}
					}
				}
				return Encoding.bigEndianUTF32Encoding;
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00068B68 File Offset: 0x00066D68
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00068BC4 File Offset: 0x00066DC4
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] bytes2 = this.GetBytes(array, 0, charCount);
			int num = bytes2.Length;
			if (num > byteCount)
			{
				throw new ArgumentException("byteCount is less that the number of bytes produced", "byteCount");
			}
			for (int j = 0; j < num; j++)
			{
				bytes[j] = bytes2[j];
			}
			return bytes2.Length;
		}

		// Token: 0x04000ECD RID: 3789
		internal int codePage;

		// Token: 0x04000ECE RID: 3790
		internal int windows_code_page;

		// Token: 0x04000ECF RID: 3791
		private bool is_readonly = true;

		// Token: 0x04000ED0 RID: 3792
		private DecoderFallback decoder_fallback;

		// Token: 0x04000ED1 RID: 3793
		private EncoderFallback encoder_fallback;

		// Token: 0x04000ED2 RID: 3794
		private static Assembly i18nAssembly;

		// Token: 0x04000ED3 RID: 3795
		private static bool i18nDisabled;

		// Token: 0x04000ED4 RID: 3796
		private static EncodingInfo[] encoding_infos;

		// Token: 0x04000ED5 RID: 3797
		private static readonly object[] encodings = new object[]
		{
			20127,
			"ascii",
			"us_ascii",
			"us",
			"ansi_x3.4_1968",
			"ansi_x3.4_1986",
			"cp367",
			"csascii",
			"ibm367",
			"iso_ir_6",
			"iso646_us",
			"iso_646.irv:1991",
			65000,
			"utf_7",
			"csunicode11utf7",
			"unicode_1_1_utf_7",
			"unicode_2_0_utf_7",
			"x_unicode_1_1_utf_7",
			"x_unicode_2_0_utf_7",
			65001,
			"utf_8",
			"unicode_1_1_utf_8",
			"unicode_2_0_utf_8",
			"x_unicode_1_1_utf_8",
			"x_unicode_2_0_utf_8",
			1200,
			"utf_16",
			"UTF_16LE",
			"ucs_2",
			"unicode",
			"iso_10646_ucs2",
			1201,
			"unicodefffe",
			"utf_16be",
			12000,
			"utf_32",
			"UTF_32LE",
			"ucs_4",
			12001,
			"UTF_32BE",
			28591,
			"iso_8859_1",
			"latin1"
		};

		// Token: 0x04000ED6 RID: 3798
		internal string body_name;

		// Token: 0x04000ED7 RID: 3799
		internal string encoding_name;

		// Token: 0x04000ED8 RID: 3800
		internal string header_name;

		// Token: 0x04000ED9 RID: 3801
		internal bool is_mail_news_display;

		// Token: 0x04000EDA RID: 3802
		internal bool is_mail_news_save;

		// Token: 0x04000EDB RID: 3803
		internal bool is_browser_save;

		// Token: 0x04000EDC RID: 3804
		internal bool is_browser_display;

		// Token: 0x04000EDD RID: 3805
		internal string web_name;

		// Token: 0x04000EDE RID: 3806
		private static volatile Encoding asciiEncoding;

		// Token: 0x04000EDF RID: 3807
		private static volatile Encoding bigEndianEncoding;

		// Token: 0x04000EE0 RID: 3808
		private static volatile Encoding defaultEncoding;

		// Token: 0x04000EE1 RID: 3809
		private static volatile Encoding utf7Encoding;

		// Token: 0x04000EE2 RID: 3810
		private static volatile Encoding utf8EncodingWithMarkers;

		// Token: 0x04000EE3 RID: 3811
		private static volatile Encoding utf8EncodingWithoutMarkers;

		// Token: 0x04000EE4 RID: 3812
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04000EE5 RID: 3813
		private static volatile Encoding isoLatin1Encoding;

		// Token: 0x04000EE6 RID: 3814
		private static volatile Encoding utf8EncodingUnsafe;

		// Token: 0x04000EE7 RID: 3815
		private static volatile Encoding utf32Encoding;

		// Token: 0x04000EE8 RID: 3816
		private static volatile Encoding bigEndianUTF32Encoding;

		// Token: 0x04000EE9 RID: 3817
		private static readonly object lockobj = new object();

		// Token: 0x020003A1 RID: 929
		private sealed class ForwardingDecoder : Decoder
		{
			// Token: 0x06001BE7 RID: 7143 RVA: 0x00068C84 File Offset: 0x00066E84
			public ForwardingDecoder(Encoding enc)
			{
				this.encoding = enc;
				DecoderFallback decoderFallback = this.encoding.DecoderFallback;
				if (decoderFallback != null)
				{
					base.Fallback = decoderFallback;
				}
			}

			// Token: 0x06001BE8 RID: 7144 RVA: 0x00068CB8 File Offset: 0x00066EB8
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x04000EEA RID: 3818
			private Encoding encoding;
		}
	}
}
