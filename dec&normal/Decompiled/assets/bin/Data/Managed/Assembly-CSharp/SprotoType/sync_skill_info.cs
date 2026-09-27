using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000611 RID: 1553
	public class sync_skill_info
	{
		// Token: 0x02000612 RID: 1554
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CFB RID: 11515 RVA: 0x000B6F94 File Offset: 0x000B5194
			public request() : base(sync_skill_info.request.max_field_count)
			{
			}

			// Token: 0x06002CFC RID: 11516 RVA: 0x000B6FA4 File Offset: 0x000B51A4
			public request(byte[] buffer) : base(sync_skill_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D1D RID: 3357
			// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000B6FC0 File Offset: 0x000B51C0
			// (set) Token: 0x06002CFF RID: 11519 RVA: 0x000B6FC8 File Offset: 0x000B51C8
			public Dictionary<string, skill_info> skill_dict
			{
				get
				{
					return this._skill_dict;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._skill_dict = value;
				}
			}

			// Token: 0x17000D1E RID: 3358
			// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000B6FE0 File Offset: 0x000B51E0
			public bool HasSkill_dict
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D1F RID: 3359
			// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000B6FF0 File Offset: 0x000B51F0
			// (set) Token: 0x06002D02 RID: 11522 RVA: 0x000B6FF8 File Offset: 0x000B51F8
			public bool isLevelUp
			{
				get
				{
					return this._isLevelUp;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isLevelUp = value;
				}
			}

			// Token: 0x17000D20 RID: 3360
			// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000B7010 File Offset: 0x000B5210
			public bool HasIsLevelUp
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002D04 RID: 11524 RVA: 0x000B7020 File Offset: 0x000B5220
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
							this.isLevelUp = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.skill_dict = this.deserialize.read_map<string, skill_info>((skill_info v) => v.skillId);
					}
				}
			}

			// Token: 0x06002D05 RID: 11525 RVA: 0x000B70B4 File Offset: 0x000B52B4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, skill_info>(this.skill_dict, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.isLevelUp, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EC5 RID: 7877
			private static int max_field_count = 2;

			// Token: 0x04001EC6 RID: 7878
			private Dictionary<string, skill_info> _skill_dict;

			// Token: 0x04001EC7 RID: 7879
			private bool _isLevelUp;
		}

		// Token: 0x02000613 RID: 1555
		public class response : SprotoTypeBase
		{
			// Token: 0x06002D07 RID: 11527 RVA: 0x000B7128 File Offset: 0x000B5328
			public response() : base(sync_skill_info.response.max_field_count)
			{
			}

			// Token: 0x06002D08 RID: 11528 RVA: 0x000B7138 File Offset: 0x000B5338
			public response(byte[] buffer) : base(sync_skill_info.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D21 RID: 3361
			// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000B7154 File Offset: 0x000B5354
			// (set) Token: 0x06002D0B RID: 11531 RVA: 0x000B715C File Offset: 0x000B535C
			public bool isLevelUp
			{
				get
				{
					return this._isLevelUp;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._isLevelUp = value;
				}
			}

			// Token: 0x17000D22 RID: 3362
			// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000B7174 File Offset: 0x000B5374
			public bool HasIsLevelUp
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002D0D RID: 11533 RVA: 0x000B7184 File Offset: 0x000B5384
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.isLevelUp = this.deserialize.read_boolean();
					}
				}
			}

			// Token: 0x06002D0E RID: 11534 RVA: 0x000B71E0 File Offset: 0x000B53E0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.isLevelUp, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EC9 RID: 7881
			private static int max_field_count = 1;

			// Token: 0x04001ECA RID: 7882
			private bool _isLevelUp;
		}
	}
}
