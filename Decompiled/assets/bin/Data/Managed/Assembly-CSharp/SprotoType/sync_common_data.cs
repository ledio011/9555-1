using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000601 RID: 1537
	public class sync_common_data
	{
		// Token: 0x02000602 RID: 1538
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C74 RID: 11380 RVA: 0x000B5E38 File Offset: 0x000B4038
			public request() : base(sync_common_data.request.max_field_count)
			{
			}

			// Token: 0x06002C75 RID: 11381 RVA: 0x000B5E48 File Offset: 0x000B4048
			public request(byte[] buffer) : base(sync_common_data.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CE7 RID: 3303
			// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000B5E68 File Offset: 0x000B4068
			// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000B5E70 File Offset: 0x000B4070
			public long serverTime
			{
				get
				{
					return this._serverTime;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._serverTime = value;
				}
			}

			// Token: 0x17000CE8 RID: 3304
			// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000B5E88 File Offset: 0x000B4088
			public bool HasServerTime
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000CE9 RID: 3305
			// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000B5E98 File Offset: 0x000B4098
			// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000B5EA0 File Offset: 0x000B40A0
			public long time_offset
			{
				get
				{
					return this._time_offset;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._time_offset = value;
				}
			}

			// Token: 0x17000CEA RID: 3306
			// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000B5EB8 File Offset: 0x000B40B8
			public bool HasTime_offset
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000CEB RID: 3307
			// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000B5EC8 File Offset: 0x000B40C8
			// (set) Token: 0x06002C7E RID: 11390 RVA: 0x000B5ED0 File Offset: 0x000B40D0
			public long daily_mission_refresh_time
			{
				get
				{
					return this._daily_mission_refresh_time;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._daily_mission_refresh_time = value;
				}
			}

			// Token: 0x17000CEC RID: 3308
			// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000B5EE8 File Offset: 0x000B40E8
			public bool HasDaily_mission_refresh_time
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000CED RID: 3309
			// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000B5EF8 File Offset: 0x000B40F8
			// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000B5F00 File Offset: 0x000B4100
			public long pvp_scale
			{
				get
				{
					return this._pvp_scale;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._pvp_scale = value;
				}
			}

			// Token: 0x17000CEE RID: 3310
			// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000B5F18 File Offset: 0x000B4118
			public bool HasPvp_scale
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000CEF RID: 3311
			// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000B5F28 File Offset: 0x000B4128
			// (set) Token: 0x06002C84 RID: 11396 RVA: 0x000B5F30 File Offset: 0x000B4130
			public long first_buy
			{
				get
				{
					return this._first_buy;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._first_buy = value;
				}
			}

			// Token: 0x17000CF0 RID: 3312
			// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000B5F48 File Offset: 0x000B4148
			public bool HasFirst_buy
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000CF1 RID: 3313
			// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000B5F58 File Offset: 0x000B4158
			// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000B5F60 File Offset: 0x000B4160
			public long big_pack
			{
				get
				{
					return this._big_pack;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._big_pack = value;
				}
			}

			// Token: 0x17000CF2 RID: 3314
			// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000B5F78 File Offset: 0x000B4178
			public bool HasBig_pack
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000CF3 RID: 3315
			// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000B5F88 File Offset: 0x000B4188
			// (set) Token: 0x06002C8A RID: 11402 RVA: 0x000B5F90 File Offset: 0x000B4190
			public long adfree
			{
				get
				{
					return this._adfree;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._adfree = value;
				}
			}

			// Token: 0x17000CF4 RID: 3316
			// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000B5FA8 File Offset: 0x000B41A8
			public bool HasAdfree
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x17000CF5 RID: 3317
			// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000B5FB8 File Offset: 0x000B41B8
			// (set) Token: 0x06002C8D RID: 11405 RVA: 0x000B5FC0 File Offset: 0x000B41C0
			public long tips
			{
				get
				{
					return this._tips;
				}
				set
				{
					this.has_field.set_field(7, true);
					this._tips = value;
				}
			}

			// Token: 0x17000CF6 RID: 3318
			// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000B5FD8 File Offset: 0x000B41D8
			public bool HasTips
			{
				get
				{
					return this.has_field.has_field(7);
				}
			}

			// Token: 0x17000CF7 RID: 3319
			// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000B5FE8 File Offset: 0x000B41E8
			// (set) Token: 0x06002C90 RID: 11408 RVA: 0x000B5FF0 File Offset: 0x000B41F0
			public Dictionary<string, function_info> func_info
			{
				get
				{
					return this._func_info;
				}
				set
				{
					this.has_field.set_field(8, true);
					this._func_info = value;
				}
			}

			// Token: 0x17000CF8 RID: 3320
			// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000B6008 File Offset: 0x000B4208
			public bool HasFunc_info
			{
				get
				{
					return this.has_field.has_field(8);
				}
			}

			// Token: 0x17000CF9 RID: 3321
			// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000B6018 File Offset: 0x000B4218
			// (set) Token: 0x06002C93 RID: 11411 RVA: 0x000B6020 File Offset: 0x000B4220
			public long push
			{
				get
				{
					return this._push;
				}
				set
				{
					this.has_field.set_field(9, true);
					this._push = value;
				}
			}

			// Token: 0x17000CFA RID: 3322
			// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000B6038 File Offset: 0x000B4238
			public bool HasPush
			{
				get
				{
					return this.has_field.has_field(9);
				}
			}

			// Token: 0x17000CFB RID: 3323
			// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000B6048 File Offset: 0x000B4248
			// (set) Token: 0x06002C96 RID: 11414 RVA: 0x000B6050 File Offset: 0x000B4250
			public long guildId
			{
				get
				{
					return this._guildId;
				}
				set
				{
					this.has_field.set_field(10, true);
					this._guildId = value;
				}
			}

			// Token: 0x17000CFC RID: 3324
			// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000B6068 File Offset: 0x000B4268
			public bool HasGuildId
			{
				get
				{
					return this.has_field.has_field(10);
				}
			}

			// Token: 0x17000CFD RID: 3325
			// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000B6078 File Offset: 0x000B4278
			// (set) Token: 0x06002C99 RID: 11417 RVA: 0x000B6080 File Offset: 0x000B4280
			public long seed
			{
				get
				{
					return this._seed;
				}
				set
				{
					this.has_field.set_field(11, true);
					this._seed = value;
				}
			}

			// Token: 0x17000CFE RID: 3326
			// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000B6098 File Offset: 0x000B4298
			public bool HasSeed
			{
				get
				{
					return this.has_field.has_field(11);
				}
			}

			// Token: 0x17000CFF RID: 3327
			// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000B60A8 File Offset: 0x000B42A8
			// (set) Token: 0x06002C9C RID: 11420 RVA: 0x000B60B0 File Offset: 0x000B42B0
			public long server_level
			{
				get
				{
					return this._server_level;
				}
				set
				{
					this.has_field.set_field(12, true);
					this._server_level = value;
				}
			}

			// Token: 0x17000D00 RID: 3328
			// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000B60C8 File Offset: 0x000B42C8
			public bool HasServer_level
			{
				get
				{
					return this.has_field.has_field(12);
				}
			}

			// Token: 0x17000D01 RID: 3329
			// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000B60D8 File Offset: 0x000B42D8
			// (set) Token: 0x06002C9F RID: 11423 RVA: 0x000B60E0 File Offset: 0x000B42E0
			public long start_time
			{
				get
				{
					return this._start_time;
				}
				set
				{
					this.has_field.set_field(13, true);
					this._start_time = value;
				}
			}

			// Token: 0x17000D02 RID: 3330
			// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000B60F8 File Offset: 0x000B42F8
			public bool HasStart_time
			{
				get
				{
					return this.has_field.has_field(13);
				}
			}

			// Token: 0x06002CA1 RID: 11425 RVA: 0x000B6108 File Offset: 0x000B4308
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.serverTime = this.deserialize.read_integer();
						continue;
					case 2:
						this.time_offset = this.deserialize.read_integer();
						continue;
					case 3:
						this.daily_mission_refresh_time = this.deserialize.read_integer();
						continue;
					case 4:
						this.pvp_scale = this.deserialize.read_integer();
						continue;
					case 5:
						this.first_buy = this.deserialize.read_integer();
						continue;
					case 6:
						this.big_pack = this.deserialize.read_integer();
						continue;
					case 7:
						this.adfree = this.deserialize.read_integer();
						continue;
					case 8:
						this.tips = this.deserialize.read_integer();
						continue;
					case 9:
						this.func_info = this.deserialize.read_map<string, function_info>((function_info v) => v.ID);
						continue;
					case 10:
						this.push = this.deserialize.read_integer();
						continue;
					case 11:
						this.guildId = this.deserialize.read_integer();
						continue;
					case 12:
						this.seed = this.deserialize.read_integer();
						continue;
					case 13:
						this.server_level = this.deserialize.read_integer();
						continue;
					case 14:
						this.start_time = this.deserialize.read_integer();
						continue;
					}
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002CA2 RID: 11426 RVA: 0x000B62DC File Offset: 0x000B44DC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.serverTime, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.time_offset, 2);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.daily_mission_refresh_time, 3);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.pvp_scale, 4);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.first_buy, 5);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.big_pack, 6);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_integer(this.adfree, 7);
				}
				if (this.has_field.has_field(7))
				{
					this.serialize.write_integer(this.tips, 8);
				}
				if (this.has_field.has_field(8))
				{
					this.serialize.write_obj<string, function_info>(this.func_info, 9);
				}
				if (this.has_field.has_field(9))
				{
					this.serialize.write_integer(this.push, 10);
				}
				if (this.has_field.has_field(10))
				{
					this.serialize.write_integer(this.guildId, 11);
				}
				if (this.has_field.has_field(11))
				{
					this.serialize.write_integer(this.seed, 12);
				}
				if (this.has_field.has_field(12))
				{
					this.serialize.write_integer(this.server_level, 13);
				}
				if (this.has_field.has_field(13))
				{
					this.serialize.write_integer(this.start_time, 14);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E9C RID: 7836
			private static int max_field_count = 15;

			// Token: 0x04001E9D RID: 7837
			private long _serverTime;

			// Token: 0x04001E9E RID: 7838
			private long _time_offset;

			// Token: 0x04001E9F RID: 7839
			private long _daily_mission_refresh_time;

			// Token: 0x04001EA0 RID: 7840
			private long _pvp_scale;

			// Token: 0x04001EA1 RID: 7841
			private long _first_buy;

			// Token: 0x04001EA2 RID: 7842
			private long _big_pack;

			// Token: 0x04001EA3 RID: 7843
			private long _adfree;

			// Token: 0x04001EA4 RID: 7844
			private long _tips;

			// Token: 0x04001EA5 RID: 7845
			private Dictionary<string, function_info> _func_info;

			// Token: 0x04001EA6 RID: 7846
			private long _push;

			// Token: 0x04001EA7 RID: 7847
			private long _guildId;

			// Token: 0x04001EA8 RID: 7848
			private long _seed;

			// Token: 0x04001EA9 RID: 7849
			private long _server_level;

			// Token: 0x04001EAA RID: 7850
			private long _start_time;
		}
	}
}
