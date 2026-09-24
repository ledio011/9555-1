using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000577 RID: 1399
	public class ret_request_random_rank_pvp_opponent
	{
		// Token: 0x02000578 RID: 1400
		public class request : SprotoTypeBase
		{
			// Token: 0x0600289C RID: 10396 RVA: 0x000AE32C File Offset: 0x000AC52C
			public request() : base(ret_request_random_rank_pvp_opponent.request.max_field_count)
			{
			}

			// Token: 0x0600289D RID: 10397 RVA: 0x000AE33C File Offset: 0x000AC53C
			public request(byte[] buffer) : base(ret_request_random_rank_pvp_opponent.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B81 RID: 2945
			// (get) Token: 0x0600289F RID: 10399 RVA: 0x000AE358 File Offset: 0x000AC558
			// (set) Token: 0x060028A0 RID: 10400 RVA: 0x000AE360 File Offset: 0x000AC560
			public long opponentNum
			{
				get
				{
					return this._opponentNum;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._opponentNum = value;
				}
			}

			// Token: 0x17000B82 RID: 2946
			// (get) Token: 0x060028A1 RID: 10401 RVA: 0x000AE378 File Offset: 0x000AC578
			public bool HasOpponentNum
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B83 RID: 2947
			// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000AE388 File Offset: 0x000AC588
			// (set) Token: 0x060028A3 RID: 10403 RVA: 0x000AE390 File Offset: 0x000AC590
			public List<character_look> characters
			{
				get
				{
					return this._characters;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._characters = value;
				}
			}

			// Token: 0x17000B84 RID: 2948
			// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000AE3A8 File Offset: 0x000AC5A8
			public bool HasCharacters
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B85 RID: 2949
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000AE3B8 File Offset: 0x000AC5B8
			// (set) Token: 0x060028A6 RID: 10406 RVA: 0x000AE3C0 File Offset: 0x000AC5C0
			public List<long> rankPos
			{
				get
				{
					return this._rankPos;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._rankPos = value;
				}
			}

			// Token: 0x17000B86 RID: 2950
			// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000AE3D8 File Offset: 0x000AC5D8
			public bool HasRankPos
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060028A8 RID: 10408 RVA: 0x000AE3E8 File Offset: 0x000AC5E8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.opponentNum = this.deserialize.read_integer();
						break;
					case 1:
						this.characters = this.deserialize.read_obj_list<character_look>();
						break;
					case 2:
						this.rankPos = this.deserialize.read_integer_list();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060028A9 RID: 10409 RVA: 0x000AE47C File Offset: 0x000AC67C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.opponentNum, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<character_look>(this.characters, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.rankPos, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D92 RID: 7570
			private static int max_field_count = 3;

			// Token: 0x04001D93 RID: 7571
			private long _opponentNum;

			// Token: 0x04001D94 RID: 7572
			private List<character_look> _characters;

			// Token: 0x04001D95 RID: 7573
			private List<long> _rankPos;
		}
	}
}
