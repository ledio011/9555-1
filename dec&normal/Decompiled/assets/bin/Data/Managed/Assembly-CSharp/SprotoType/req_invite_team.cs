using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000480 RID: 1152
	public class req_invite_team
	{
		// Token: 0x02000481 RID: 1153
		public class request : SprotoTypeBase
		{
			// Token: 0x0600233E RID: 9022 RVA: 0x000A4050 File Offset: 0x000A2250
			public request() : base(req_invite_team.request.max_field_count)
			{
			}

			// Token: 0x0600233F RID: 9023 RVA: 0x000A4060 File Offset: 0x000A2260
			public request(byte[] buffer) : base(req_invite_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009EF RID: 2543
			// (get) Token: 0x06002341 RID: 9025 RVA: 0x000A407C File Offset: 0x000A227C
			// (set) Token: 0x06002342 RID: 9026 RVA: 0x000A4084 File Offset: 0x000A2284
			public long characterid
			{
				get
				{
					return this._characterid;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterid = value;
				}
			}

			// Token: 0x170009F0 RID: 2544
			// (get) Token: 0x06002343 RID: 9027 RVA: 0x000A409C File Offset: 0x000A229C
			public bool HasCharacterid
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009F1 RID: 2545
			// (get) Token: 0x06002344 RID: 9028 RVA: 0x000A40AC File Offset: 0x000A22AC
			// (set) Token: 0x06002345 RID: 9029 RVA: 0x000A40B4 File Offset: 0x000A22B4
			public string goalId
			{
				get
				{
					return this._goalId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._goalId = value;
				}
			}

			// Token: 0x170009F2 RID: 2546
			// (get) Token: 0x06002346 RID: 9030 RVA: 0x000A40CC File Offset: 0x000A22CC
			public bool HasGoalId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170009F3 RID: 2547
			// (get) Token: 0x06002347 RID: 9031 RVA: 0x000A40DC File Offset: 0x000A22DC
			// (set) Token: 0x06002348 RID: 9032 RVA: 0x000A40E4 File Offset: 0x000A22E4
			public long minLevel
			{
				get
				{
					return this._minLevel;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._minLevel = value;
				}
			}

			// Token: 0x170009F4 RID: 2548
			// (get) Token: 0x06002349 RID: 9033 RVA: 0x000A40FC File Offset: 0x000A22FC
			public bool HasMinLevel
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170009F5 RID: 2549
			// (get) Token: 0x0600234A RID: 9034 RVA: 0x000A410C File Offset: 0x000A230C
			// (set) Token: 0x0600234B RID: 9035 RVA: 0x000A4114 File Offset: 0x000A2314
			public long maxLevel
			{
				get
				{
					return this._maxLevel;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._maxLevel = value;
				}
			}

			// Token: 0x170009F6 RID: 2550
			// (get) Token: 0x0600234C RID: 9036 RVA: 0x000A412C File Offset: 0x000A232C
			public bool HasMaxLevel
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170009F7 RID: 2551
			// (get) Token: 0x0600234D RID: 9037 RVA: 0x000A413C File Offset: 0x000A233C
			// (set) Token: 0x0600234E RID: 9038 RVA: 0x000A4144 File Offset: 0x000A2344
			public long isVerfiy
			{
				get
				{
					return this._isVerfiy;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._isVerfiy = value;
				}
			}

			// Token: 0x170009F8 RID: 2552
			// (get) Token: 0x0600234F RID: 9039 RVA: 0x000A415C File Offset: 0x000A235C
			public bool HasIsVerfiy
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170009F9 RID: 2553
			// (get) Token: 0x06002350 RID: 9040 RVA: 0x000A416C File Offset: 0x000A236C
			// (set) Token: 0x06002351 RID: 9041 RVA: 0x000A4174 File Offset: 0x000A2374
			public long recruit
			{
				get
				{
					return this._recruit;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._recruit = value;
				}
			}

			// Token: 0x170009FA RID: 2554
			// (get) Token: 0x06002352 RID: 9042 RVA: 0x000A418C File Offset: 0x000A238C
			public bool HasRecruit
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x06002353 RID: 9043 RVA: 0x000A419C File Offset: 0x000A239C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.characterid = this.deserialize.read_integer();
						break;
					case 1:
						this.goalId = this.deserialize.read_string();
						break;
					case 2:
						this.minLevel = this.deserialize.read_integer();
						break;
					case 3:
						this.maxLevel = this.deserialize.read_integer();
						break;
					case 4:
						this.isVerfiy = this.deserialize.read_integer();
						break;
					case 5:
						this.recruit = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002354 RID: 9044 RVA: 0x000A427C File Offset: 0x000A247C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterid, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.goalId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.minLevel, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.maxLevel, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.isVerfiy, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.recruit, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C31 RID: 7217
			private static int max_field_count = 6;

			// Token: 0x04001C32 RID: 7218
			private long _characterid;

			// Token: 0x04001C33 RID: 7219
			private string _goalId;

			// Token: 0x04001C34 RID: 7220
			private long _minLevel;

			// Token: 0x04001C35 RID: 7221
			private long _maxLevel;

			// Token: 0x04001C36 RID: 7222
			private long _isVerfiy;

			// Token: 0x04001C37 RID: 7223
			private long _recruit;
		}
	}
}
