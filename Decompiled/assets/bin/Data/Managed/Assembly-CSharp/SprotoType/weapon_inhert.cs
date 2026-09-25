using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200066A RID: 1642
	public class weapon_inhert
	{
		// Token: 0x0200066B RID: 1643
		public class request : SprotoTypeBase
		{
			// Token: 0x06002F80 RID: 12160 RVA: 0x000BC00C File Offset: 0x000BA20C
			public request() : base(weapon_inhert.request.max_field_count)
			{
			}

			// Token: 0x06002F81 RID: 12161 RVA: 0x000BC01C File Offset: 0x000BA21C
			public request(byte[] buffer) : base(weapon_inhert.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000E09 RID: 3593
			// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000BC038 File Offset: 0x000BA238
			// (set) Token: 0x06002F84 RID: 12164 RVA: 0x000BC040 File Offset: 0x000BA240
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

			// Token: 0x17000E0A RID: 3594
			// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000BC058 File Offset: 0x000BA258
			public bool HasIndex1
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000E0B RID: 3595
			// (get) Token: 0x06002F86 RID: 12166 RVA: 0x000BC068 File Offset: 0x000BA268
			// (set) Token: 0x06002F87 RID: 12167 RVA: 0x000BC070 File Offset: 0x000BA270
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

			// Token: 0x17000E0C RID: 3596
			// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000BC088 File Offset: 0x000BA288
			public bool HasIndex2
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000E0D RID: 3597
			// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000BC098 File Offset: 0x000BA298
			// (set) Token: 0x06002F8A RID: 12170 RVA: 0x000BC0A0 File Offset: 0x000BA2A0
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

			// Token: 0x17000E0E RID: 3598
			// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000BC0B8 File Offset: 0x000BA2B8
			public bool HasAttribute_index1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000E0F RID: 3599
			// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000BC0C8 File Offset: 0x000BA2C8
			// (set) Token: 0x06002F8D RID: 12173 RVA: 0x000BC0D0 File Offset: 0x000BA2D0
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

			// Token: 0x17000E10 RID: 3600
			// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000BC0E8 File Offset: 0x000BA2E8
			public bool HasAttribute_index2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06002F8F RID: 12175 RVA: 0x000BC0F8 File Offset: 0x000BA2F8
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

			// Token: 0x06002F90 RID: 12176 RVA: 0x000BC1A4 File Offset: 0x000BA3A4
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

			// Token: 0x04001F6F RID: 8047
			private static int max_field_count = 4;

			// Token: 0x04001F70 RID: 8048
			private long _index1;

			// Token: 0x04001F71 RID: 8049
			private long _index2;

			// Token: 0x04001F72 RID: 8050
			private long _attribute_index1;

			// Token: 0x04001F73 RID: 8051
			private long _attribute_index2;
		}
	}
}
