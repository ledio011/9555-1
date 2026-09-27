using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003C2 RID: 962
	public class grant_activity_reward
	{
		// Token: 0x020003C3 RID: 963
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D4F RID: 7503 RVA: 0x00098098 File Offset: 0x00096298
			public request() : base(grant_activity_reward.request.max_field_count)
			{
			}

			// Token: 0x06001D50 RID: 7504 RVA: 0x000980A8 File Offset: 0x000962A8
			public request(byte[] buffer) : base(grant_activity_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700079B RID: 1947
			// (get) Token: 0x06001D52 RID: 7506 RVA: 0x000980C4 File Offset: 0x000962C4
			// (set) Token: 0x06001D53 RID: 7507 RVA: 0x000980CC File Offset: 0x000962CC
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

			// Token: 0x1700079C RID: 1948
			// (get) Token: 0x06001D54 RID: 7508 RVA: 0x000980E4 File Offset: 0x000962E4
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700079D RID: 1949
			// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000980F4 File Offset: 0x000962F4
			// (set) Token: 0x06001D56 RID: 7510 RVA: 0x000980FC File Offset: 0x000962FC
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x1700079E RID: 1950
			// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00098114 File Offset: 0x00096314
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700079F RID: 1951
			// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00098124 File Offset: 0x00096324
			// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0009812C File Offset: 0x0009632C
			public bool win
			{
				get
				{
					return this._win;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._win = value;
				}
			}

			// Token: 0x170007A0 RID: 1952
			// (get) Token: 0x06001D5A RID: 7514 RVA: 0x00098144 File Offset: 0x00096344
			public bool HasWin
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170007A1 RID: 1953
			// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00098154 File Offset: 0x00096354
			// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0009815C File Offset: 0x0009635C
			public List<item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._items = value;
				}
			}

			// Token: 0x170007A2 RID: 1954
			// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00098174 File Offset: 0x00096374
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00098184 File Offset: 0x00096384
			// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0009818C File Offset: 0x0009638C
			public battle_info battle_info
			{
				get
				{
					return this._battle_info;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._battle_info = value;
				}
			}

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x06001D60 RID: 7520 RVA: 0x000981A4 File Offset: 0x000963A4
			public bool HasBattle_info
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x06001D61 RID: 7521 RVA: 0x000981B4 File Offset: 0x000963B4
			// (set) Token: 0x06001D62 RID: 7522 RVA: 0x000981BC File Offset: 0x000963BC
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._ID = value;
				}
			}

			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x06001D63 RID: 7523 RVA: 0x000981D4 File Offset: 0x000963D4
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x170007A7 RID: 1959
			// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000981E4 File Offset: 0x000963E4
			// (set) Token: 0x06001D65 RID: 7525 RVA: 0x000981EC File Offset: 0x000963EC
			public List<item> items2
			{
				get
				{
					return this._items2;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._items2 = value;
				}
			}

			// Token: 0x170007A8 RID: 1960
			// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00098204 File Offset: 0x00096404
			public bool HasItems2
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x06001D67 RID: 7527 RVA: 0x00098214 File Offset: 0x00096414
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
						this.state = this.deserialize.read_integer();
						break;
					case 2:
						this.win = this.deserialize.read_boolean();
						break;
					case 3:
						this.items = this.deserialize.read_obj_list<item>();
						break;
					case 4:
						this.battle_info = this.deserialize.read_obj<battle_info>();
						break;
					case 5:
						this.ID = this.deserialize.read_string();
						break;
					case 6:
						this.items2 = this.deserialize.read_obj_list<item>();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001D68 RID: 7528 RVA: 0x00098310 File Offset: 0x00096510
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.win, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj<item>(this.items, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_obj(this.battle_info, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_string(this.ID, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_obj<item>(this.items2, 6);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A98 RID: 6808
			private static int max_field_count = 7;

			// Token: 0x04001A99 RID: 6809
			private long _type;

			// Token: 0x04001A9A RID: 6810
			private long _state;

			// Token: 0x04001A9B RID: 6811
			private bool _win;

			// Token: 0x04001A9C RID: 6812
			private List<item> _items;

			// Token: 0x04001A9D RID: 6813
			private battle_info _battle_info;

			// Token: 0x04001A9E RID: 6814
			private string _ID;

			// Token: 0x04001A9F RID: 6815
			private List<item> _items2;
		}
	}
}
