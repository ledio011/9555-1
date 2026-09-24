using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000082 RID: 130
	[DebuggerDisplay("{_value}", Name = "[{_key}]")]
	[ComVisible(true)]
	[Serializable]
	public struct DictionaryEntry
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00014748 File Offset: 0x00012948
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00014758 File Offset: 0x00012958
		public object Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00014760 File Offset: 0x00012960
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040001E6 RID: 486
		private object _key;

		// Token: 0x040001E7 RID: 487
		private object _value;
	}
}
