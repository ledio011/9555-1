using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005DF RID: 1503
	public class skill_info : SprotoTypeBase
	{
		// Token: 0x06002B4F RID: 11087 RVA: 0x000B3914 File Offset: 0x000B1B14
		public skill_info() : base(skill_info.max_field_count)
		{
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000B3924 File Offset: 0x000B1B24
		public skill_info(byte[] buffer) : base(skill_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000B3940 File Offset: 0x000B1B40
		// (set) Token: 0x06002B53 RID: 11091 RVA: 0x000B3948 File Offset: 0x000B1B48
		public string skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._skillId = value;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000B3960 File Offset: 0x000B1B60
		public bool HasSkillId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000B3970 File Offset: 0x000B1B70
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x000B3978 File Offset: 0x000B1B78
		public long skillLevel
		{
			get
			{
				return this._skillLevel;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._skillLevel = value;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000B3990 File Offset: 0x000B1B90
		public bool HasSkillLevel
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000B39A0 File Offset: 0x000B1BA0
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x000B39A8 File Offset: 0x000B1BA8
		public long indexPos
		{
			get
			{
				return this._indexPos;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._indexPos = value;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000B39C0 File Offset: 0x000B1BC0
		public bool HasIndexPos
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x000B39D0 File Offset: 0x000B1BD0
		// (set) Token: 0x06002B5C RID: 11100 RVA: 0x000B39D8 File Offset: 0x000B1BD8
		public long unlockLevel
		{
			get
			{
				return this._unlockLevel;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._unlockLevel = value;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000B39F0 File Offset: 0x000B1BF0
		public bool HasUnlockLevel
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000B3A00 File Offset: 0x000B1C00
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x000B3A08 File Offset: 0x000B1C08
		public long indexPos2
		{
			get
			{
				return this._indexPos2;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._indexPos2 = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000B3A20 File Offset: 0x000B1C20
		public bool HasIndexPos2
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000B3A30 File Offset: 0x000B1C30
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x000B3A38 File Offset: 0x000B1C38
		public bool disable
		{
			get
			{
				return this._disable;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._disable = value;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000B3A50 File Offset: 0x000B1C50
		public bool HasDisable
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000B3A60 File Offset: 0x000B1C60
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.skillId = this.deserialize.read_string();
					break;
				case 1:
					this.skillLevel = this.deserialize.read_integer();
					break;
				case 2:
					this.indexPos = this.deserialize.read_integer();
					break;
				case 3:
					this.unlockLevel = this.deserialize.read_integer();
					break;
				case 4:
					this.indexPos2 = this.deserialize.read_integer();
					break;
				case 5:
					this.disable = this.deserialize.read_boolean();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000B3B40 File Offset: 0x000B1D40
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.skillId, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.skillLevel, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.indexPos, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.unlockLevel, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.indexPos2, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_boolean(this.disable, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E4A RID: 7754
		private static int max_field_count = 6;

		// Token: 0x04001E4B RID: 7755
		private string _skillId;

		// Token: 0x04001E4C RID: 7756
		private long _skillLevel;

		// Token: 0x04001E4D RID: 7757
		private long _indexPos;

		// Token: 0x04001E4E RID: 7758
		private long _unlockLevel;

		// Token: 0x04001E4F RID: 7759
		private long _indexPos2;

		// Token: 0x04001E50 RID: 7760
		private bool _disable;
	}
}
