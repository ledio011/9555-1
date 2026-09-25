using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003FE RID: 1022
	public class item : SprotoTypeBase
	{
		// Token: 0x06001FA5 RID: 8101 RVA: 0x0009CE68 File Offset: 0x0009B068
		public item() : base(item.max_field_count)
		{
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0009CE78 File Offset: 0x0009B078
		public item(byte[] buffer) : base(item.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x0009CE94 File Offset: 0x0009B094
		// (set) Token: 0x06001FA9 RID: 8105 RVA: 0x0009CE9C File Offset: 0x0009B09C
		public string itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._itemId = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x0009CEB4 File Offset: 0x0009B0B4
		public bool HasItemId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x0009CEC4 File Offset: 0x0009B0C4
		// (set) Token: 0x06001FAC RID: 8108 RVA: 0x0009CECC File Offset: 0x0009B0CC
		public long itemCount
		{
			get
			{
				return this._itemCount;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._itemCount = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0009CEE4 File Offset: 0x0009B0E4
		public bool HasItemCount
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x0009CEF4 File Offset: 0x0009B0F4
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x0009CEFC File Offset: 0x0009B0FC
		public long quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._quality = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0009CF14 File Offset: 0x0009B114
		public bool HasQuality
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0009CF24 File Offset: 0x0009B124
		// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x0009CF2C File Offset: 0x0009B12C
		public string id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._id = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0009CF44 File Offset: 0x0009B144
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0009CF54 File Offset: 0x0009B154
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x0009CF5C File Offset: 0x0009B15C
		public long count2
		{
			get
			{
				return this._count2;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._count2 = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0009CF74 File Offset: 0x0009B174
		public bool HasCount2
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0009CF84 File Offset: 0x0009B184
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.itemId = this.deserialize.read_string();
					continue;
				case 1:
					this.itemCount = this.deserialize.read_integer();
					continue;
				case 3:
					this.quality = this.deserialize.read_integer();
					continue;
				case 4:
					this.id = this.deserialize.read_string();
					continue;
				case 5:
					this.count2 = this.deserialize.read_integer();
					continue;
				}
				this.deserialize.read_unknow_data();
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0009D050 File Offset: 0x0009B250
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.itemId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.itemCount, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.quality, 3);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.id, 4);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.count2, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B40 RID: 6976
		private static int max_field_count = 6;

		// Token: 0x04001B41 RID: 6977
		private string _itemId;

		// Token: 0x04001B42 RID: 6978
		private long _itemCount;

		// Token: 0x04001B43 RID: 6979
		private long _quality;

		// Token: 0x04001B44 RID: 6980
		private string _id;

		// Token: 0x04001B45 RID: 6981
		private long _count2;
	}
}
