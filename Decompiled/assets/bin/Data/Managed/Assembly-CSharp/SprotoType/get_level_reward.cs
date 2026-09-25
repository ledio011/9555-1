using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003BE RID: 958
	public class get_level_reward
	{
		// Token: 0x020003BF RID: 959
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D39 RID: 7481 RVA: 0x00097DF4 File Offset: 0x00095FF4
			public request() : base(SprotoType.get_level_reward.request.max_field_count)
			{
			}

			// Token: 0x06001D3A RID: 7482 RVA: 0x00097E04 File Offset: 0x00096004
			public request(byte[] buffer) : base(SprotoType.get_level_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000795 RID: 1941
			// (get) Token: 0x06001D3C RID: 7484 RVA: 0x00097E20 File Offset: 0x00096020
			// (set) Token: 0x06001D3D RID: 7485 RVA: 0x00097E28 File Offset: 0x00096028
			public Dictionary<string, level_reward> level_reward
			{
				get
				{
					return this._level_reward;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._level_reward = value;
				}
			}

			// Token: 0x17000796 RID: 1942
			// (get) Token: 0x06001D3E RID: 7486 RVA: 0x00097E40 File Offset: 0x00096040
			public bool HasLevel_reward
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00097E50 File Offset: 0x00096050
			// (set) Token: 0x06001D40 RID: 7488 RVA: 0x00097E58 File Offset: 0x00096058
			public List<item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._items = value;
				}
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00097E70 File Offset: 0x00096070
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001D42 RID: 7490 RVA: 0x00097E80 File Offset: 0x00096080
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
							this.items = this.deserialize.read_obj_list<item>();
						}
					}
					else
					{
						this.level_reward = this.deserialize.read_map<string, level_reward>((level_reward v) => v.ID);
					}
				}
			}

			// Token: 0x06001D43 RID: 7491 RVA: 0x00097F14 File Offset: 0x00096114
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, level_reward>(this.level_reward, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<item>(this.items, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A92 RID: 6802
			private static int max_field_count = 2;

			// Token: 0x04001A93 RID: 6803
			private Dictionary<string, level_reward> _level_reward;

			// Token: 0x04001A94 RID: 6804
			private List<item> _items;
		}
	}
}
