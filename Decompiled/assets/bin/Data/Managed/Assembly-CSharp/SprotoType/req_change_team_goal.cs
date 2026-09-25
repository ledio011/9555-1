using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200046C RID: 1132
	public class req_change_team_goal
	{
		// Token: 0x0200046D RID: 1133
		public class request : SprotoTypeBase
		{
			// Token: 0x060022ED RID: 8941 RVA: 0x000A37C8 File Offset: 0x000A19C8
			public request() : base(req_change_team_goal.request.max_field_count)
			{
			}

			// Token: 0x060022EE RID: 8942 RVA: 0x000A37D8 File Offset: 0x000A19D8
			public request(byte[] buffer) : base(req_change_team_goal.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009E1 RID: 2529
			// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000A37F4 File Offset: 0x000A19F4
			// (set) Token: 0x060022F1 RID: 8945 RVA: 0x000A37FC File Offset: 0x000A19FC
			public string goalId
			{
				get
				{
					return this._goalId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._goalId = value;
				}
			}

			// Token: 0x170009E2 RID: 2530
			// (get) Token: 0x060022F2 RID: 8946 RVA: 0x000A3814 File Offset: 0x000A1A14
			public bool HasGoalId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170009E3 RID: 2531
			// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000A3824 File Offset: 0x000A1A24
			// (set) Token: 0x060022F4 RID: 8948 RVA: 0x000A382C File Offset: 0x000A1A2C
			public long minLevel
			{
				get
				{
					return this._minLevel;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._minLevel = value;
				}
			}

			// Token: 0x170009E4 RID: 2532
			// (get) Token: 0x060022F5 RID: 8949 RVA: 0x000A3844 File Offset: 0x000A1A44
			public bool HasMinLevel
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170009E5 RID: 2533
			// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000A3854 File Offset: 0x000A1A54
			// (set) Token: 0x060022F7 RID: 8951 RVA: 0x000A385C File Offset: 0x000A1A5C
			public long maxLevel
			{
				get
				{
					return this._maxLevel;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._maxLevel = value;
				}
			}

			// Token: 0x170009E6 RID: 2534
			// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000A3874 File Offset: 0x000A1A74
			public bool HasMaxLevel
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170009E7 RID: 2535
			// (get) Token: 0x060022F9 RID: 8953 RVA: 0x000A3884 File Offset: 0x000A1A84
			// (set) Token: 0x060022FA RID: 8954 RVA: 0x000A388C File Offset: 0x000A1A8C
			public long isVerfiy
			{
				get
				{
					return this._isVerfiy;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._isVerfiy = value;
				}
			}

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x060022FB RID: 8955 RVA: 0x000A38A4 File Offset: 0x000A1AA4
			public bool HasIsVerfiy
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x060022FC RID: 8956 RVA: 0x000A38B4 File Offset: 0x000A1AB4
			// (set) Token: 0x060022FD RID: 8957 RVA: 0x000A38BC File Offset: 0x000A1ABC
			public long recruit
			{
				get
				{
					return this._recruit;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._recruit = value;
				}
			}

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x060022FE RID: 8958 RVA: 0x000A38D4 File Offset: 0x000A1AD4
			public bool HasRecruit
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x060022FF RID: 8959 RVA: 0x000A38E4 File Offset: 0x000A1AE4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.goalId = this.deserialize.read_string();
						break;
					case 1:
						this.minLevel = this.deserialize.read_integer();
						break;
					case 2:
						this.maxLevel = this.deserialize.read_integer();
						break;
					case 3:
						this.isVerfiy = this.deserialize.read_integer();
						break;
					case 4:
						this.recruit = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002300 RID: 8960 RVA: 0x000A39AC File Offset: 0x000A1BAC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.goalId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.minLevel, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.maxLevel, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.isVerfiy, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.recruit, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C20 RID: 7200
			private static int max_field_count = 5;

			// Token: 0x04001C21 RID: 7201
			private string _goalId;

			// Token: 0x04001C22 RID: 7202
			private long _minLevel;

			// Token: 0x04001C23 RID: 7203
			private long _maxLevel;

			// Token: 0x04001C24 RID: 7204
			private long _isVerfiy;

			// Token: 0x04001C25 RID: 7205
			private long _recruit;
		}
	}
}
