using System;
using System.Collections.Generic;

namespace Sproto
{
	// Token: 0x0200080D RID: 2061
	public class ProtocolFunctionDictionary
	{
		// Token: 0x0600317B RID: 12667 RVA: 0x000C154C File Offset: 0x000BF74C
		public ProtocolFunctionDictionary()
		{
			this.MetaDictionary = new Dictionary<int, ProtocolFunctionDictionary.MetaInfo>();
			this.ProtocolDictionary = new Dictionary<Type, int>();
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000C156C File Offset: 0x000BF76C
		private ProtocolFunctionDictionary.MetaInfo _getMeta(int tag)
		{
			ProtocolFunctionDictionary.MetaInfo metaInfo;
			if (!this.MetaDictionary.TryGetValue(tag, ref metaInfo))
			{
				metaInfo = new ProtocolFunctionDictionary.MetaInfo();
				this.MetaDictionary.Add(tag, metaInfo);
			}
			return metaInfo;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000C15A0 File Offset: 0x000BF7A0
		public void SetProtocol<ProtocolType>(int tag)
		{
			ProtocolFunctionDictionary.MetaInfo metaInfo = this._getMeta(tag);
			metaInfo.ProtocolType = typeof(ProtocolType);
			this.ProtocolDictionary.Add(metaInfo.ProtocolType, tag);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000C15D8 File Offset: 0x000BF7D8
		public void SetRequest<T>(int tag) where T : SprotoTypeBase, new()
		{
			ProtocolFunctionDictionary.MetaInfo metaInfo = this._getMeta(tag);
			this._set<T>(tag, out metaInfo.Request);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000C15FC File Offset: 0x000BF7FC
		public void SetResponse<T>(int tag) where T : SprotoTypeBase, new()
		{
			ProtocolFunctionDictionary.MetaInfo metaInfo = this._getMeta(tag);
			this._set<T>(tag, out metaInfo.Response);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000C1620 File Offset: 0x000BF820
		private void _set<T>(int tag, out KeyValuePair<Type, ProtocolFunctionDictionary.typeFunc> field) where T : SprotoTypeBase, new()
		{
			ProtocolFunctionDictionary.typeFunc typeFunc = delegate(byte[] buffer, int offset, int len)
			{
				T t = Activator.CreateInstance<T>();
				t.init(buffer, offset, len);
				return t;
			};
			field = new KeyValuePair<Type, ProtocolFunctionDictionary.typeFunc>(typeof(T), typeFunc);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000C164C File Offset: 0x000BF84C
		private SprotoTypeBase _gen(KeyValuePair<Type, ProtocolFunctionDictionary.typeFunc> field, int tag, byte[] buffer, int offset = 0, int len = 0)
		{
			if (field.Value != null)
			{
				return field.Value(buffer, offset, len);
			}
			return null;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000C167C File Offset: 0x000BF87C
		public SprotoTypeBase GenResponse(int tag, byte[] buffer, int offset = 0, int len = 0)
		{
			ProtocolFunctionDictionary.MetaInfo metaInfo = this.MetaDictionary[tag];
			return this._gen(metaInfo.Response, tag, buffer, offset, len);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000C16A8 File Offset: 0x000BF8A8
		public SprotoTypeBase GenRequest(int tag, byte[] buffer, int offset = 0, int len = 0)
		{
			ProtocolFunctionDictionary.MetaInfo metaInfo = this.MetaDictionary[tag];
			return this._gen(metaInfo.Request, tag, buffer, offset, len);
		}

		// Token: 0x17000E2A RID: 3626
		public ProtocolFunctionDictionary.MetaInfo this[int tag]
		{
			get
			{
				return this.MetaDictionary[tag];
			}
		}

		// Token: 0x17000E2B RID: 3627
		public int this[Type protocolType]
		{
			get
			{
				return this.ProtocolDictionary[protocolType];
			}
		}

		// Token: 0x04002133 RID: 8499
		private Dictionary<int, ProtocolFunctionDictionary.MetaInfo> MetaDictionary;

		// Token: 0x04002134 RID: 8500
		private Dictionary<Type, int> ProtocolDictionary;

		// Token: 0x0200080E RID: 2062
		public class MetaInfo
		{
			// Token: 0x04002135 RID: 8501
			public Type ProtocolType;

			// Token: 0x04002136 RID: 8502
			public KeyValuePair<Type, ProtocolFunctionDictionary.typeFunc> Request;

			// Token: 0x04002137 RID: 8503
			public KeyValuePair<Type, ProtocolFunctionDictionary.typeFunc> Response;
		}

		// Token: 0x02000ADA RID: 2778
		// (Invoke) Token: 0x06004FF1 RID: 20465
		public delegate SprotoTypeBase typeFunc(byte[] buffer, int offset, int len);
	}
}
