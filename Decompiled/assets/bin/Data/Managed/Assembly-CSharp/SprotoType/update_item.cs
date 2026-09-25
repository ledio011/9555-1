using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000640 RID: 1600
	public class update_item
	{
		// Token: 0x02000641 RID: 1601
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E5E RID: 11870 RVA: 0x000B9C08 File Offset: 0x000B7E08
			public request() : base(update_item.request.max_field_count)
			{
			}

			// Token: 0x06002E5F RID: 11871 RVA: 0x000B9C18 File Offset: 0x000B7E18
			public request(byte[] buffer) : base(update_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DA1 RID: 3489
			// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000B9C34 File Offset: 0x000B7E34
			// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000B9C3C File Offset: 0x000B7E3C
			public long containertype
			{
				get
				{
					return this._containertype;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._containertype = value;
				}
			}

			// Token: 0x17000DA2 RID: 3490
			// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000B9C54 File Offset: 0x000B7E54
			public bool HasContainertype
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DA3 RID: 3491
			// (get) Token: 0x06002E64 RID: 11876 RVA: 0x000B9C64 File Offset: 0x000B7E64
			// (set) Token: 0x06002E65 RID: 11877 RVA: 0x000B9C6C File Offset: 0x000B7E6C
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._indexId = value;
				}
			}

			// Token: 0x17000DA4 RID: 3492
			// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000B9C84 File Offset: 0x000B7E84
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DA5 RID: 3493
			// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000B9C94 File Offset: 0x000B7E94
			// (set) Token: 0x06002E68 RID: 11880 RVA: 0x000B9C9C File Offset: 0x000B7E9C
			public gameitem gameitem
			{
				get
				{
					return this._gameitem;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._gameitem = value;
				}
			}

			// Token: 0x17000DA6 RID: 3494
			// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000B9CB4 File Offset: 0x000B7EB4
			public bool HasGameitem
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002E6A RID: 11882 RVA: 0x000B9CC4 File Offset: 0x000B7EC4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.containertype = this.deserialize.read_integer();
						break;
					case 1:
						this.indexId = this.deserialize.read_integer();
						break;
					case 2:
						this.gameitem = this.deserialize.read_obj<gameitem>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002E6B RID: 11883 RVA: 0x000B9D58 File Offset: 0x000B7F58
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.containertype, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.indexId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj(this.gameitem, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F24 RID: 7972
			private static int max_field_count = 3;

			// Token: 0x04001F25 RID: 7973
			private long _containertype;

			// Token: 0x04001F26 RID: 7974
			private long _indexId;

			// Token: 0x04001F27 RID: 7975
			private gameitem _gameitem;
		}
	}
}
