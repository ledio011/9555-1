using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000315 RID: 789
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		// Token: 0x0600180C RID: 6156 RVA: 0x00057A9C File Offset: 0x00055C9C
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", "Null argument");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter", "Null argument");
			}
			this.converter = converter;
			this.assemblyName = type.Assembly.FullName;
			this.fullTypeName = type.FullName;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00057B18 File Offset: 0x00055D18
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00057B20 File Offset: 0x00055D20
		public string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00057B28 File Offset: 0x00055D28
		public int MemberCount
		{
			get
			{
				return this.serialized.Count;
			}
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00057B38 File Offset: 0x00055D38
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name is null");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type is null");
			}
			if (this.serialized.ContainsKey(name))
			{
				throw new SerializationException("Value has been serialized already.");
			}
			SerializationEntry serializationEntry = new SerializationEntry(name, type, value);
			this.serialized.Add(name, serializationEntry);
			this.values.Add(serializationEntry);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00057BB4 File Offset: 0x00055DB4
		public object GetValue(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name is null.");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.serialized.ContainsKey(name))
			{
				throw new SerializationException("No element named " + name + " could be found.");
			}
			SerializationEntry serializationEntry = (SerializationEntry)this.serialized[name];
			if (serializationEntry.Value != null && !type.IsInstanceOfType(serializationEntry.Value))
			{
				return this.converter.Convert(serializationEntry.Value, type);
			}
			return serializationEntry.Value;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00057C58 File Offset: 0x00055E58
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type is null.");
			}
			this.fullTypeName = type.FullName;
			this.assemblyName = type.Assembly.FullName;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00057C88 File Offset: 0x00055E88
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.values);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00057C98 File Offset: 0x00055E98
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00057CB4 File Offset: 0x00055EB4
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00057CD0 File Offset: 0x00055ED0
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00057CEC File Offset: 0x00055EEC
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00057D08 File Offset: 0x00055F08
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00057D24 File Offset: 0x00055F24
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00057D40 File Offset: 0x00055F40
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00057D5C File Offset: 0x00055F5C
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
			}
			else
			{
				this.AddValue(name, value, value.GetType());
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00057D8C File Offset: 0x00055F8C
		public bool GetBoolean(string name)
		{
			object value = this.GetValue(name, typeof(bool));
			return this.converter.ToBoolean(value);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00057DB8 File Offset: 0x00055FB8
		public short GetInt16(string name)
		{
			object value = this.GetValue(name, typeof(short));
			return this.converter.ToInt16(value);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00057DE4 File Offset: 0x00055FE4
		public int GetInt32(string name)
		{
			object value = this.GetValue(name, typeof(int));
			return this.converter.ToInt32(value);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00057E10 File Offset: 0x00056010
		public long GetInt64(string name)
		{
			object value = this.GetValue(name, typeof(long));
			return this.converter.ToInt64(value);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00057E3C File Offset: 0x0005603C
		public string GetString(string name)
		{
			object value = this.GetValue(name, typeof(string));
			if (value == null)
			{
				return null;
			}
			return this.converter.ToString(value);
		}

		// Token: 0x04000C85 RID: 3205
		private Hashtable serialized = new Hashtable();

		// Token: 0x04000C86 RID: 3206
		private ArrayList values = new ArrayList();

		// Token: 0x04000C87 RID: 3207
		private string assemblyName;

		// Token: 0x04000C88 RID: 3208
		private string fullTypeName;

		// Token: 0x04000C89 RID: 3209
		private IFormatterConverter converter;
	}
}
