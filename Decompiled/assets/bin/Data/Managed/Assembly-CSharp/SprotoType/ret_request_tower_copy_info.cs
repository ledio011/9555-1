using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000581 RID: 1409
	public class ret_request_tower_copy_info
	{
		// Token: 0x02000582 RID: 1410
		public class request : SprotoTypeBase
		{
			// Token: 0x060028E5 RID: 10469 RVA: 0x000AEC60 File Offset: 0x000ACE60
			public request() : base(ret_request_tower_copy_info.request.max_field_count)
			{
			}

			// Token: 0x060028E6 RID: 10470 RVA: 0x000AEC70 File Offset: 0x000ACE70
			public request(byte[] buffer) : base(ret_request_tower_copy_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B9D RID: 2973
			// (get) Token: 0x060028E8 RID: 10472 RVA: 0x000AEC8C File Offset: 0x000ACE8C
			// (set) Token: 0x060028E9 RID: 10473 RVA: 0x000AEC94 File Offset: 0x000ACE94
			public tower_info tower_info
			{
				get
				{
					return this._tower_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._tower_info = value;
				}
			}

			// Token: 0x17000B9E RID: 2974
			// (get) Token: 0x060028EA RID: 10474 RVA: 0x000AECAC File Offset: 0x000ACEAC
			public bool HasTower_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B9F RID: 2975
			// (get) Token: 0x060028EB RID: 10475 RVA: 0x000AECBC File Offset: 0x000ACEBC
			// (set) Token: 0x060028EC RID: 10476 RVA: 0x000AECC4 File Offset: 0x000ACEC4
			public List<tower_special_reward> tower_special_reward
			{
				get
				{
					return this._tower_special_reward;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._tower_special_reward = value;
				}
			}

			// Token: 0x17000BA0 RID: 2976
			// (get) Token: 0x060028ED RID: 10477 RVA: 0x000AECDC File Offset: 0x000ACEDC
			public bool HasTower_special_reward
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060028EE RID: 10478 RVA: 0x000AECEC File Offset: 0x000ACEEC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.tower_info = this.deserialize.read_obj<tower_info>();
						continue;
					case 2:
						this.tower_special_reward = this.deserialize.read_obj_list<tower_special_reward>();
						continue;
					}
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x060028EF RID: 10479 RVA: 0x000AED68 File Offset: 0x000ACF68
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.tower_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<tower_special_reward>(this.tower_special_reward, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DA6 RID: 7590
			private static int max_field_count = 3;

			// Token: 0x04001DA7 RID: 7591
			private tower_info _tower_info;

			// Token: 0x04001DA8 RID: 7592
			private List<tower_special_reward> _tower_special_reward;
		}
	}
}
