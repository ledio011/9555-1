using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003A8 RID: 936
	public class equip_item
	{
		// Token: 0x020003A9 RID: 937
		public class request : SprotoTypeBase
		{
			// Token: 0x06001C0D RID: 7181 RVA: 0x00095600 File Offset: 0x00093800
			public request() : base(equip_item.request.max_field_count)
			{
			}

			// Token: 0x06001C0E RID: 7182 RVA: 0x00095610 File Offset: 0x00093810
			public request(byte[] buffer) : base(equip_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000705 RID: 1797
			// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0009562C File Offset: 0x0009382C
			// (set) Token: 0x06001C11 RID: 7185 RVA: 0x00095634 File Offset: 0x00093834
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId = value;
				}
			}

			// Token: 0x17000706 RID: 1798
			// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0009564C File Offset: 0x0009384C
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0009565C File Offset: 0x0009385C
			// (set) Token: 0x06001C14 RID: 7188 RVA: 0x00095664 File Offset: 0x00093864
			public bool inhert
			{
				get
				{
					return this._inhert;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._inhert = value;
				}
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0009567C File Offset: 0x0009387C
			public bool HasInhert
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001C16 RID: 7190 RVA: 0x0009568C File Offset: 0x0009388C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.inhert = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001C17 RID: 7191 RVA: 0x00095704 File Offset: 0x00093904
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.inhert, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A39 RID: 6713
			private static int max_field_count = 2;

			// Token: 0x04001A3A RID: 6714
			private long _indexId;

			// Token: 0x04001A3B RID: 6715
			private bool _inhert;
		}
	}
}
