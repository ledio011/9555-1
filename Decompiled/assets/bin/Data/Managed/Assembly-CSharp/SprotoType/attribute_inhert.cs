using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000313 RID: 787
	public class attribute_inhert
	{
		// Token: 0x02000314 RID: 788
		public class request : SprotoTypeBase
		{
			// Token: 0x06001663 RID: 5731 RVA: 0x000899E0 File Offset: 0x00087BE0
			public request() : base(attribute_inhert.request.max_field_count)
			{
			}

			// Token: 0x06001664 RID: 5732 RVA: 0x000899F0 File Offset: 0x00087BF0
			public request(byte[] buffer) : base(attribute_inhert.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x06001666 RID: 5734 RVA: 0x00089A0C File Offset: 0x00087C0C
			// (set) Token: 0x06001667 RID: 5735 RVA: 0x00089A14 File Offset: 0x00087C14
			public long index1
			{
				get
				{
					return this._index1;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._index1 = value;
				}
			}

			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x06001668 RID: 5736 RVA: 0x00089A2C File Offset: 0x00087C2C
			public bool HasIndex1
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x06001669 RID: 5737 RVA: 0x00089A3C File Offset: 0x00087C3C
			// (set) Token: 0x0600166A RID: 5738 RVA: 0x00089A44 File Offset: 0x00087C44
			public long index2
			{
				get
				{
					return this._index2;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._index2 = value;
				}
			}

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x0600166B RID: 5739 RVA: 0x00089A5C File Offset: 0x00087C5C
			public bool HasIndex2
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x0600166C RID: 5740 RVA: 0x00089A6C File Offset: 0x00087C6C
			// (set) Token: 0x0600166D RID: 5741 RVA: 0x00089A74 File Offset: 0x00087C74
			public long attribute_index1
			{
				get
				{
					return this._attribute_index1;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._attribute_index1 = value;
				}
			}

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x0600166E RID: 5742 RVA: 0x00089A8C File Offset: 0x00087C8C
			public bool HasAttribute_index1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x0600166F RID: 5743 RVA: 0x00089A9C File Offset: 0x00087C9C
			// (set) Token: 0x06001670 RID: 5744 RVA: 0x00089AA4 File Offset: 0x00087CA4
			public long attribute_index2
			{
				get
				{
					return this._attribute_index2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._attribute_index2 = value;
				}
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x06001671 RID: 5745 RVA: 0x00089ABC File Offset: 0x00087CBC
			public bool HasAttribute_index2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001672 RID: 5746 RVA: 0x00089ACC File Offset: 0x00087CCC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.index1 = this.deserialize.read_integer();
						break;
					case 1:
						this.index2 = this.deserialize.read_integer();
						break;
					case 2:
						this.attribute_index1 = this.deserialize.read_integer();
						break;
					case 3:
						this.attribute_index2 = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001673 RID: 5747 RVA: 0x00089B78 File Offset: 0x00087D78
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.index1, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.index2, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.attribute_index1, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.attribute_index2, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x0400189E RID: 6302
			private static int max_field_count = 4;

			// Token: 0x0400189F RID: 6303
			private long _index1;

			// Token: 0x040018A0 RID: 6304
			private long _index2;

			// Token: 0x040018A1 RID: 6305
			private long _attribute_index1;

			// Token: 0x040018A2 RID: 6306
			private long _attribute_index2;
		}
	}
}
