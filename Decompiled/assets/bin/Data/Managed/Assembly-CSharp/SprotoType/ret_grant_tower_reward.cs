using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200051D RID: 1309
	public class ret_grant_tower_reward
	{
		// Token: 0x0200051E RID: 1310
		public class request : SprotoTypeBase
		{
			// Token: 0x06002640 RID: 9792 RVA: 0x000A982C File Offset: 0x000A7A2C
			public request() : base(ret_grant_tower_reward.request.max_field_count)
			{
			}

			// Token: 0x06002641 RID: 9793 RVA: 0x000A983C File Offset: 0x000A7A3C
			public request(byte[] buffer) : base(ret_grant_tower_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AB1 RID: 2737
			// (get) Token: 0x06002643 RID: 9795 RVA: 0x000A9858 File Offset: 0x000A7A58
			// (set) Token: 0x06002644 RID: 9796 RVA: 0x000A9860 File Offset: 0x000A7A60
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000AB2 RID: 2738
			// (get) Token: 0x06002645 RID: 9797 RVA: 0x000A9878 File Offset: 0x000A7A78
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AB3 RID: 2739
			// (get) Token: 0x06002646 RID: 9798 RVA: 0x000A9888 File Offset: 0x000A7A88
			// (set) Token: 0x06002647 RID: 9799 RVA: 0x000A9890 File Offset: 0x000A7A90
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06002648 RID: 9800 RVA: 0x000A98A8 File Offset: 0x000A7AA8
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06002649 RID: 9801 RVA: 0x000A98B8 File Offset: 0x000A7AB8
			// (set) Token: 0x0600264A RID: 9802 RVA: 0x000A98C0 File Offset: 0x000A7AC0
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

			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x0600264B RID: 9803 RVA: 0x000A98D8 File Offset: 0x000A7AD8
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000AB7 RID: 2743
			// (get) Token: 0x0600264C RID: 9804 RVA: 0x000A98E8 File Offset: 0x000A7AE8
			// (set) Token: 0x0600264D RID: 9805 RVA: 0x000A98F0 File Offset: 0x000A7AF0
			public tower_info tower_info
			{
				get
				{
					return this._tower_info;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._tower_info = value;
				}
			}

			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x0600264E RID: 9806 RVA: 0x000A9908 File Offset: 0x000A7B08
			public bool HasTower_info
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x0600264F RID: 9807 RVA: 0x000A9918 File Offset: 0x000A7B18
			// (set) Token: 0x06002650 RID: 9808 RVA: 0x000A9920 File Offset: 0x000A7B20
			public List<tower_special_reward> tower_special_reward
			{
				get
				{
					return this._tower_special_reward;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._tower_special_reward = value;
				}
			}

			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x06002651 RID: 9809 RVA: 0x000A9938 File Offset: 0x000A7B38
			public bool HasTower_special_reward
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002652 RID: 9810 RVA: 0x000A9948 File Offset: 0x000A7B48
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.type = this.deserialize.read_integer();
						break;
					case 1:
						this.id = this.deserialize.read_integer();
						break;
					case 2:
						this.items = this.deserialize.read_obj_list<item>();
						break;
					case 3:
						this.tower_info = this.deserialize.read_obj<tower_info>();
						break;
					case 4:
						this.tower_special_reward = this.deserialize.read_obj_list<tower_special_reward>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002653 RID: 9811 RVA: 0x000A9A10 File Offset: 0x000A7C10
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj<item>(this.items, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj(this.tower_info, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_obj<tower_special_reward>(this.tower_special_reward, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CE7 RID: 7399
			private static int max_field_count = 5;

			// Token: 0x04001CE8 RID: 7400
			private long _type;

			// Token: 0x04001CE9 RID: 7401
			private long _id;

			// Token: 0x04001CEA RID: 7402
			private List<item> _items;

			// Token: 0x04001CEB RID: 7403
			private tower_info _tower_info;

			// Token: 0x04001CEC RID: 7404
			private List<tower_special_reward> _tower_special_reward;
		}
	}
}
