using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E6 RID: 1510
	public class slot_item : SprotoTypeBase
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x000B464C File Offset: 0x000B284C
		public slot_item() : base(slot_item.max_field_count)
		{
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000B465C File Offset: 0x000B285C
		public slot_item(byte[] buffer) : base(slot_item.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000B4678 File Offset: 0x000B2878
		// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x000B4680 File Offset: 0x000B2880
		public string uuid
		{
			get
			{
				return this._uuid;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._uuid = value;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000B4698 File Offset: 0x000B2898
		public bool HasUuid
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000B46A8 File Offset: 0x000B28A8
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x000B46B0 File Offset: 0x000B28B0
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._ID = value;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000B46C8 File Offset: 0x000B28C8
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000B46D8 File Offset: 0x000B28D8
		// (set) Token: 0x06002BBC RID: 11196 RVA: 0x000B46E0 File Offset: 0x000B28E0
		public List<item> items
		{
			get
			{
				return this._items;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._items = value;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000B46F8 File Offset: 0x000B28F8
		public bool HasItems
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000B4708 File Offset: 0x000B2908
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.uuid = this.deserialize.read_string();
					break;
				case 1:
					this.ID = this.deserialize.read_string();
					break;
				case 2:
					this.items = this.deserialize.read_obj_list<item>();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000B479C File Offset: 0x000B299C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.uuid, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.ID, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj<item>(this.items, 2);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E67 RID: 7783
		private static int max_field_count = 3;

		// Token: 0x04001E68 RID: 7784
		private string _uuid;

		// Token: 0x04001E69 RID: 7785
		private string _ID;

		// Token: 0x04001E6A RID: 7786
		private List<item> _items;
	}
}
