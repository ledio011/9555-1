using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200031C RID: 796
	public class battle_info : SprotoTypeBase
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x0008AA50 File Offset: 0x00088C50
		public battle_info() : base(battle_info.max_field_count)
		{
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0008AA60 File Offset: 0x00088C60
		public battle_info(byte[] buffer) : base(battle_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0008AA7C File Offset: 0x00088C7C
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0008AA84 File Offset: 0x00088C84
		public List<damage_list> damage_list
		{
			get
			{
				return this._damage_list;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._damage_list = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0008AA9C File Offset: 0x00088C9C
		public bool HasDamage_list
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0008AAAC File Offset: 0x00088CAC
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0008AAB4 File Offset: 0x00088CB4
		public long my_rank
		{
			get
			{
				return this._my_rank;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._my_rank = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0008AACC File Offset: 0x00088CCC
		public bool HasMy_rank
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0008AADC File Offset: 0x00088CDC
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x0008AAE4 File Offset: 0x00088CE4
		public long my_damage
		{
			get
			{
				return this._my_damage;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._my_damage = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0008AAFC File Offset: 0x00088CFC
		public bool HasMy_damage
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0008AB0C File Offset: 0x00088D0C
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x0008AB14 File Offset: 0x00088D14
		public long all_damage
		{
			get
			{
				return this._all_damage;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._all_damage = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0008AB2C File Offset: 0x00088D2C
		public bool HasAll_damage
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0008AB3C File Offset: 0x00088D3C
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x0008AB44 File Offset: 0x00088D44
		public string lastKill
		{
			get
			{
				return this._lastKill;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._lastKill = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0008AB5C File Offset: 0x00088D5C
		public bool HasLastKill
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0008AB6C File Offset: 0x00088D6C
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0008AB74 File Offset: 0x00088D74
		public long end_time
		{
			get
			{
				return this._end_time;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._end_time = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0008AB8C File Offset: 0x00088D8C
		public bool HasEnd_time
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0008AB9C File Offset: 0x00088D9C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.damage_list = this.deserialize.read_obj_list<damage_list>();
					break;
				case 1:
					this.my_rank = this.deserialize.read_integer();
					break;
				case 2:
					this.my_damage = this.deserialize.read_integer();
					break;
				case 3:
					this.all_damage = this.deserialize.read_integer();
					break;
				case 4:
					this.lastKill = this.deserialize.read_string();
					break;
				case 5:
					this.end_time = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0008AC7C File Offset: 0x00088E7C
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_obj<damage_list>(this.damage_list, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.my_rank, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.my_damage, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.all_damage, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.lastKill, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.end_time, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x040018C2 RID: 6338
		private static int max_field_count = 6;

		// Token: 0x040018C3 RID: 6339
		private List<damage_list> _damage_list;

		// Token: 0x040018C4 RID: 6340
		private long _my_rank;

		// Token: 0x040018C5 RID: 6341
		private long _my_damage;

		// Token: 0x040018C6 RID: 6342
		private long _all_damage;

		// Token: 0x040018C7 RID: 6343
		private string _lastKill;

		// Token: 0x040018C8 RID: 6344
		private long _end_time;
	}
}
