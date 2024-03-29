namespace Classic.World.Entities.Enums;

public enum PlayerFields_Vanilla
{
    PLAYER_DUEL_ARBITER = 0x00 + UnitFields_Vanilla.UNIT_END, // Size:2
    PLAYER_FLAGS = 0x02 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_GUILDID = 0x03 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_GUILDRANK = 0x04 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_BYTES = 0x05 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_BYTES_2 = 0x06 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_BYTES_3 = 0x07 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_DUEL_TEAM = 0x08 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_GUILD_TIMESTAMP = 0x09 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_QUEST_LOG_1_1 = 0x0A + UnitFields_Vanilla.UNIT_END, // count = 20
    PLAYER_QUEST_LOG_1_2 = 0x0B + UnitFields_Vanilla.UNIT_END,
    PLAYER_QUEST_LOG_1_3 = 0x0C + UnitFields_Vanilla.UNIT_END,
    PLAYER_QUEST_LOG_LAST_1 = 0x43 + UnitFields_Vanilla.UNIT_END,
    PLAYER_QUEST_LOG_LAST_2 = 0x44 + UnitFields_Vanilla.UNIT_END,
    PLAYER_QUEST_LOG_LAST_3 = 0x45 + UnitFields_Vanilla.UNIT_END,
    PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + UnitFields_Vanilla.UNIT_END, // Size:2, count = 19
    PLAYER_VISIBLE_ITEM_1_0 = 0x48 + UnitFields_Vanilla.UNIT_END, // Size:8
    PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + UnitFields_Vanilla.UNIT_END,
    PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + UnitFields_Vanilla.UNIT_END,
    PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + UnitFields_Vanilla.UNIT_END,
    PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + UnitFields_Vanilla.UNIT_END, // Size:46
    PLAYER_FIELD_PACK_SLOT_1 = 0x158 + UnitFields_Vanilla.UNIT_END, // Size:32
    PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_BANK_SLOT_1 = 0x178 + UnitFields_Vanilla.UNIT_END, // Size:48
    PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + UnitFields_Vanilla.UNIT_END, // Size:12
    PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + UnitFields_Vanilla.UNIT_END, // Size:24
    PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + UnitFields_Vanilla.UNIT_END, // Size:64
    PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + UnitFields_Vanilla.UNIT_END,
    PLAYER_FARSIGHT = 0x20c + UnitFields_Vanilla.UNIT_END, // Size:2
    PLAYER_FIELD_COMBO_TARGET = 0x20e + UnitFields_Vanilla.UNIT_END, // Size:2
    PLAYER_XP = 0x210 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_NEXT_LEVEL_XP = 0x211 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_SKILL_INFO_1_1 = 0x212 + UnitFields_Vanilla.UNIT_END, // Size:384
    PLAYER_SKILL_PROP_1_1 = 0x213 + UnitFields_Vanilla.UNIT_END, // Size:384

    PLAYER_CHARACTER_POINTS1 = 0x392 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_CHARACTER_POINTS2 = 0x393 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_TRACK_CREATURES = 0x394 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_TRACK_RESOURCES = 0x395 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_BLOCK_PERCENTAGE = 0x396 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_DODGE_PERCENTAGE = 0x397 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_PARRY_PERCENTAGE = 0x398 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_CRIT_PERCENTAGE = 0x399 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_EXPLORED_ZONES_1 = 0x39b + UnitFields_Vanilla.UNIT_END, // Size:64
    PLAYER_REST_STATE_EXPERIENCE = 0x3db + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_COINAGE = 0x3dc + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_POSSTAT0 = 0x3DD + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_POSSTAT1 = 0x3DE + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_POSSTAT2 = 0x3DF + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_POSSTAT3 = 0x3E0 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_POSSTAT4 = 0x3E1 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_NEGSTAT0 = 0x3E2 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_NEGSTAT1 = 0x3E3 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_NEGSTAT2 = 0x3E4 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_NEGSTAT3 = 0x3E5 + UnitFields_Vanilla.UNIT_END, // Size:1,
    PLAYER_FIELD_NEGSTAT4 = 0x3E6 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3E7 + UnitFields_Vanilla.UNIT_END, // Size:7
    PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3EE + UnitFields_Vanilla.UNIT_END, // Size:7
    PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3F5 + UnitFields_Vanilla.UNIT_END, // Size:7
    PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3FC + UnitFields_Vanilla.UNIT_END, // Size:7
    PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + UnitFields_Vanilla.UNIT_END, // Size:7
    PLAYER_FIELD_BYTES = 0x40A + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_AMMO_ID = 0x40B + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_SELF_RES_SPELL = 0x40C + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_PVP_MEDALS = 0x40D + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_BUYBACK_PRICE_1 = 0x40E + UnitFields_Vanilla.UNIT_END, // count=12
    PLAYER_FIELD_BUYBACK_PRICE_LAST = 0x419 + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x41A + UnitFields_Vanilla.UNIT_END, // count=12
    PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = 0x425 + UnitFields_Vanilla.UNIT_END,
    PLAYER_FIELD_SESSION_KILLS = 0x426 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_YESTERDAY_KILLS = 0x427 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_LAST_WEEK_KILLS = 0x428 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_THIS_WEEK_KILLS = 0x429 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0x42a + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = 0x42b + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_BYTES2 = 0x430 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + UnitFields_Vanilla.UNIT_END, // Size:1
    PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + UnitFields_Vanilla.UNIT_END, // Size:20

    PLAYER_END = 0x446 + UnitFields_Vanilla.UNIT_END
}

public enum PlayerFields_TBC
{
    PLAYER_DUEL_ARBITER = UnitFields_TBC.UNIT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_FLAGS = UnitFields_TBC.UNIT_END + 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_GUILDID = UnitFields_TBC.UNIT_END + 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_GUILDRANK = UnitFields_TBC.UNIT_END + 0x0004, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_BYTES = UnitFields_TBC.UNIT_END + 0x0005, // Size: 1, Type: BYTES, Flags: PUBLIC
    PLAYER_BYTES_2 = UnitFields_TBC.UNIT_END + 0x0006, // Size: 1, Type: BYTES, Flags: PUBLIC
    PLAYER_BYTES_3 = UnitFields_TBC.UNIT_END + 0x0007, // Size: 1, Type: BYTES, Flags: PUBLIC
    PLAYER_DUEL_TEAM = UnitFields_TBC.UNIT_END + 0x0008, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_GUILD_TIMESTAMP = UnitFields_TBC.UNIT_END + 0x0009, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_QUEST_LOG_1_1 = UnitFields_TBC.UNIT_END + 0x000A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_1_2 = UnitFields_TBC.UNIT_END + 0x000B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_1_3 = UnitFields_TBC.UNIT_END + 0x000C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_1_4 = UnitFields_TBC.UNIT_END + 0x000D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_2_1 = UnitFields_TBC.UNIT_END + 0x000E, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_2_2 = UnitFields_TBC.UNIT_END + 0x000F, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_2_3 = UnitFields_TBC.UNIT_END + 0x0010, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_2_4 = UnitFields_TBC.UNIT_END + 0x0011, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_3_1 = UnitFields_TBC.UNIT_END + 0x0012, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_3_2 = UnitFields_TBC.UNIT_END + 0x0013, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_3_3 = UnitFields_TBC.UNIT_END + 0x0014, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_3_4 = UnitFields_TBC.UNIT_END + 0x0015, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_4_1 = UnitFields_TBC.UNIT_END + 0x0016, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_4_2 = UnitFields_TBC.UNIT_END + 0x0017, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_4_3 = UnitFields_TBC.UNIT_END + 0x0018, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_4_4 = UnitFields_TBC.UNIT_END + 0x0019, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_5_1 = UnitFields_TBC.UNIT_END + 0x001A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_5_2 = UnitFields_TBC.UNIT_END + 0x001B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_5_3 = UnitFields_TBC.UNIT_END + 0x001C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_5_4 = UnitFields_TBC.UNIT_END + 0x001D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_6_1 = UnitFields_TBC.UNIT_END + 0x001E, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_6_2 = UnitFields_TBC.UNIT_END + 0x001F, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_6_3 = UnitFields_TBC.UNIT_END + 0x0020, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_6_4 = UnitFields_TBC.UNIT_END + 0x0021, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_7_1 = UnitFields_TBC.UNIT_END + 0x0022, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_7_2 = UnitFields_TBC.UNIT_END + 0x0023, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_7_3 = UnitFields_TBC.UNIT_END + 0x0024, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_7_4 = UnitFields_TBC.UNIT_END + 0x0025, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_8_1 = UnitFields_TBC.UNIT_END + 0x0026, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_8_2 = UnitFields_TBC.UNIT_END + 0x0027, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_8_3 = UnitFields_TBC.UNIT_END + 0x0028, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_8_4 = UnitFields_TBC.UNIT_END + 0x0029, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_9_1 = UnitFields_TBC.UNIT_END + 0x002A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_9_2 = UnitFields_TBC.UNIT_END + 0x002B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_9_3 = UnitFields_TBC.UNIT_END + 0x002C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_9_4 = UnitFields_TBC.UNIT_END + 0x002D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_10_1 = UnitFields_TBC.UNIT_END + 0x002E, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_10_2 = UnitFields_TBC.UNIT_END + 0x002F, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_10_3 = UnitFields_TBC.UNIT_END + 0x0030, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_10_4 = UnitFields_TBC.UNIT_END + 0x0031, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_11_1 = UnitFields_TBC.UNIT_END + 0x0032, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_11_2 = UnitFields_TBC.UNIT_END + 0x0033, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_11_3 = UnitFields_TBC.UNIT_END + 0x0034, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_11_4 = UnitFields_TBC.UNIT_END + 0x0035, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_12_1 = UnitFields_TBC.UNIT_END + 0x0036, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_12_2 = UnitFields_TBC.UNIT_END + 0x0037, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_12_3 = UnitFields_TBC.UNIT_END + 0x0038, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_12_4 = UnitFields_TBC.UNIT_END + 0x0039, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_13_1 = UnitFields_TBC.UNIT_END + 0x003A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_13_2 = UnitFields_TBC.UNIT_END + 0x003B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_13_3 = UnitFields_TBC.UNIT_END + 0x003C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_13_4 = UnitFields_TBC.UNIT_END + 0x003D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_14_1 = UnitFields_TBC.UNIT_END + 0x003E, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_14_2 = UnitFields_TBC.UNIT_END + 0x003F, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_14_3 = UnitFields_TBC.UNIT_END + 0x0040, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_14_4 = UnitFields_TBC.UNIT_END + 0x0041, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_15_1 = UnitFields_TBC.UNIT_END + 0x0042, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_15_2 = UnitFields_TBC.UNIT_END + 0x0043, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_15_3 = UnitFields_TBC.UNIT_END + 0x0044, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_15_4 = UnitFields_TBC.UNIT_END + 0x0045, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_16_1 = UnitFields_TBC.UNIT_END + 0x0046, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_16_2 = UnitFields_TBC.UNIT_END + 0x0047, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_16_3 = UnitFields_TBC.UNIT_END + 0x0048, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_16_4 = UnitFields_TBC.UNIT_END + 0x0049, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_17_1 = UnitFields_TBC.UNIT_END + 0x004A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_17_2 = UnitFields_TBC.UNIT_END + 0x004B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_17_3 = UnitFields_TBC.UNIT_END + 0x004C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_17_4 = UnitFields_TBC.UNIT_END + 0x004D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_18_1 = UnitFields_TBC.UNIT_END + 0x004E, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_18_2 = UnitFields_TBC.UNIT_END + 0x004F, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_18_3 = UnitFields_TBC.UNIT_END + 0x0050, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_18_4 = UnitFields_TBC.UNIT_END + 0x0051, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_19_1 = UnitFields_TBC.UNIT_END + 0x0052, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_19_2 = UnitFields_TBC.UNIT_END + 0x0053, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_19_3 = UnitFields_TBC.UNIT_END + 0x0054, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_19_4 = UnitFields_TBC.UNIT_END + 0x0055, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_20_1 = UnitFields_TBC.UNIT_END + 0x0056, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_20_2 = UnitFields_TBC.UNIT_END + 0x0057, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_20_3 = UnitFields_TBC.UNIT_END + 0x0058, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_20_4 = UnitFields_TBC.UNIT_END + 0x0059, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_21_1 = UnitFields_TBC.UNIT_END + 0x005A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_21_2 = UnitFields_TBC.UNIT_END + 0x005B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_21_3 = UnitFields_TBC.UNIT_END + 0x005C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_21_4 = UnitFields_TBC.UNIT_END + 0x005D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_22_1 = UnitFields_TBC.UNIT_END + 0x005E, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_22_2 = UnitFields_TBC.UNIT_END + 0x005F, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_22_3 = UnitFields_TBC.UNIT_END + 0x0060, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_22_4 = UnitFields_TBC.UNIT_END + 0x0061, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_23_1 = UnitFields_TBC.UNIT_END + 0x0062, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_23_2 = UnitFields_TBC.UNIT_END + 0x0063, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_23_3 = UnitFields_TBC.UNIT_END + 0x0064, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_23_4 = UnitFields_TBC.UNIT_END + 0x0065, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_24_1 = UnitFields_TBC.UNIT_END + 0x0066, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_24_2 = UnitFields_TBC.UNIT_END + 0x0067, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_24_3 = UnitFields_TBC.UNIT_END + 0x0068, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_24_4 = UnitFields_TBC.UNIT_END + 0x0069, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_25_1 = UnitFields_TBC.UNIT_END + 0x006A, // Size: 1, Type: INT, Flags: GROUP_ONLY
    PLAYER_QUEST_LOG_25_2 = UnitFields_TBC.UNIT_END + 0x006B, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_QUEST_LOG_25_3 = UnitFields_TBC.UNIT_END + 0x006C, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_QUEST_LOG_25_4 = UnitFields_TBC.UNIT_END + 0x006D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_VISIBLE_ITEM_1_CREATOR = UnitFields_TBC.UNIT_END + 0x006E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_1_0 = UnitFields_TBC.UNIT_END + 0x0070, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_1_PROPERTIES = UnitFields_TBC.UNIT_END + 0x007C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_1_PAD = UnitFields_TBC.UNIT_END + 0x007D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_2_CREATOR = UnitFields_TBC.UNIT_END + 0x007E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_2_0 = UnitFields_TBC.UNIT_END + 0x0080, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_2_PROPERTIES = UnitFields_TBC.UNIT_END + 0x008C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_2_PAD = UnitFields_TBC.UNIT_END + 0x008D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_3_CREATOR = UnitFields_TBC.UNIT_END + 0x008E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_3_0 = UnitFields_TBC.UNIT_END + 0x0090, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_3_PROPERTIES = UnitFields_TBC.UNIT_END + 0x009C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_3_PAD = UnitFields_TBC.UNIT_END + 0x009D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_4_CREATOR = UnitFields_TBC.UNIT_END + 0x009E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_4_0 = UnitFields_TBC.UNIT_END + 0x00A0, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_4_PROPERTIES = UnitFields_TBC.UNIT_END + 0x00AC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_4_PAD = UnitFields_TBC.UNIT_END + 0x00AD, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_5_CREATOR = UnitFields_TBC.UNIT_END + 0x00AE, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_5_0 = UnitFields_TBC.UNIT_END + 0x00B0, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_5_PROPERTIES = UnitFields_TBC.UNIT_END + 0x00BC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_5_PAD = UnitFields_TBC.UNIT_END + 0x00BD, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_6_CREATOR = UnitFields_TBC.UNIT_END + 0x00BE, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_6_0 = UnitFields_TBC.UNIT_END + 0x00C0, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_6_PROPERTIES = UnitFields_TBC.UNIT_END + 0x00CC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_6_PAD = UnitFields_TBC.UNIT_END + 0x00CD, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_7_CREATOR = UnitFields_TBC.UNIT_END + 0x00CE, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_7_0 = UnitFields_TBC.UNIT_END + 0x00D0, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_7_PROPERTIES = UnitFields_TBC.UNIT_END + 0x00DC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_7_PAD = UnitFields_TBC.UNIT_END + 0x00DD, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_8_CREATOR = UnitFields_TBC.UNIT_END + 0x00DE, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_8_0 = UnitFields_TBC.UNIT_END + 0x00E0, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_8_PROPERTIES = UnitFields_TBC.UNIT_END + 0x00EC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_8_PAD = UnitFields_TBC.UNIT_END + 0x00ED, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_9_CREATOR = UnitFields_TBC.UNIT_END + 0x00EE, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_9_0 = UnitFields_TBC.UNIT_END + 0x00F0, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_9_PROPERTIES = UnitFields_TBC.UNIT_END + 0x00FC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_9_PAD = UnitFields_TBC.UNIT_END + 0x00FD, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_10_CREATOR = UnitFields_TBC.UNIT_END + 0x00FE, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_10_0 = UnitFields_TBC.UNIT_END + 0x0100, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_10_PROPERTIES = UnitFields_TBC.UNIT_END + 0x010C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_10_PAD = UnitFields_TBC.UNIT_END + 0x010D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_11_CREATOR = UnitFields_TBC.UNIT_END + 0x010E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_11_0 = UnitFields_TBC.UNIT_END + 0x0110, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_11_PROPERTIES = UnitFields_TBC.UNIT_END + 0x011C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_11_PAD = UnitFields_TBC.UNIT_END + 0x011D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_12_CREATOR = UnitFields_TBC.UNIT_END + 0x011E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_12_0 = UnitFields_TBC.UNIT_END + 0x0120, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_12_PROPERTIES = UnitFields_TBC.UNIT_END + 0x012C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_12_PAD = UnitFields_TBC.UNIT_END + 0x012D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_13_CREATOR = UnitFields_TBC.UNIT_END + 0x012E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_13_0 = UnitFields_TBC.UNIT_END + 0x0130, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_13_PROPERTIES = UnitFields_TBC.UNIT_END + 0x013C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_13_PAD = UnitFields_TBC.UNIT_END + 0x013D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_14_CREATOR = UnitFields_TBC.UNIT_END + 0x013E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_14_0 = UnitFields_TBC.UNIT_END + 0x0140, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_14_PROPERTIES = UnitFields_TBC.UNIT_END + 0x014C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_14_PAD = UnitFields_TBC.UNIT_END + 0x014D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_15_CREATOR = UnitFields_TBC.UNIT_END + 0x014E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_15_0 = UnitFields_TBC.UNIT_END + 0x0150, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_15_PROPERTIES = UnitFields_TBC.UNIT_END + 0x015C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_15_PAD = UnitFields_TBC.UNIT_END + 0x015D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_16_CREATOR = UnitFields_TBC.UNIT_END + 0x015E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_16_0 = UnitFields_TBC.UNIT_END + 0x0160, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_16_PROPERTIES = UnitFields_TBC.UNIT_END + 0x016C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_16_PAD = UnitFields_TBC.UNIT_END + 0x016D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_17_CREATOR = UnitFields_TBC.UNIT_END + 0x016E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_17_0 = UnitFields_TBC.UNIT_END + 0x0170, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_17_PROPERTIES = UnitFields_TBC.UNIT_END + 0x017C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_17_PAD = UnitFields_TBC.UNIT_END + 0x017D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_18_CREATOR = UnitFields_TBC.UNIT_END + 0x017E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_18_0 = UnitFields_TBC.UNIT_END + 0x0180, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_18_PROPERTIES = UnitFields_TBC.UNIT_END + 0x018C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_18_PAD = UnitFields_TBC.UNIT_END + 0x018D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_19_CREATOR = UnitFields_TBC.UNIT_END + 0x018E, // Size: 2, Type: LONG, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_19_0 = UnitFields_TBC.UNIT_END + 0x0190, // Size: 12, Type: INT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_19_PROPERTIES = UnitFields_TBC.UNIT_END + 0x019C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
    PLAYER_VISIBLE_ITEM_19_PAD = UnitFields_TBC.UNIT_END + 0x019D, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_CHOSEN_TITLE = UnitFields_TBC.UNIT_END + 0x019E, // Size: 1, Type: INT, Flags: PUBLIC
    PLAYER_FIELD_PAD_0 = UnitFields_TBC.UNIT_END + 0x019F, // Size: 1, Type: INT, Flags: NONE
    PLAYER_FIELD_INV_SLOT_HEAD = UnitFields_TBC.UNIT_END + 0x01A0, // Size: 46, Type: LONG, Flags: PRIVATE
    PLAYER_FIELD_PACK_SLOT_1 = UnitFields_TBC.UNIT_END + 0x01CE, // Size: 32, Type: LONG, Flags: PRIVATE
    PLAYER_FIELD_BANK_SLOT_1 = UnitFields_TBC.UNIT_END + 0x01EE, // Size: 56, Type: LONG, Flags: PRIVATE
    PLAYER_FIELD_BANKBAG_SLOT_1 = UnitFields_TBC.UNIT_END + 0x0226, // Size: 14, Type: LONG, Flags: PRIVATE
    PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = UnitFields_TBC.UNIT_END + 0x0234, // Size: 24, Type: LONG, Flags: PRIVATE
    PLAYER_FIELD_KEYRING_SLOT_1 = UnitFields_TBC.UNIT_END + 0x024C, // Size: 64, Type: LONG, Flags: PRIVATE
    PLAYER_FIELD_VANITYPET_SLOT_1 = UnitFields_TBC.UNIT_END + 0x028C, // Size: 36, Type: LONG, Flags: PRIVATE
    PLAYER_FARSIGHT = UnitFields_TBC.UNIT_END + 0x02B0, // Size: 2, Type: LONG, Flags: PRIVATE
    PLAYER__FIELD_KNOWN_TITLES = UnitFields_TBC.UNIT_END + 0x02B2, // Size: 2, Type: LONG, Flags: PRIVATE
    PLAYER_XP = UnitFields_TBC.UNIT_END + 0x02B4, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_NEXT_LEVEL_XP = UnitFields_TBC.UNIT_END + 0x02B5, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_SKILL_INFO_1_1 = UnitFields_TBC.UNIT_END + 0x02B6, // Size: 384, Type: TWO_SHORT, Flags: PRIVATE
    PLAYER_CHARACTER_POINTS1 = UnitFields_TBC.UNIT_END + 0x0436, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_CHARACTER_POINTS2 = UnitFields_TBC.UNIT_END + 0x0437, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_TRACK_CREATURES = UnitFields_TBC.UNIT_END + 0x0438, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_TRACK_RESOURCES = UnitFields_TBC.UNIT_END + 0x0439, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_BLOCK_PERCENTAGE = UnitFields_TBC.UNIT_END + 0x043A, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_DODGE_PERCENTAGE = UnitFields_TBC.UNIT_END + 0x043B, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_PARRY_PERCENTAGE = UnitFields_TBC.UNIT_END + 0x043C, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_EXPERTISE = UnitFields_TBC.UNIT_END + 0x043D, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_OFFHAND_EXPERTISE = UnitFields_TBC.UNIT_END + 0x043E, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_CRIT_PERCENTAGE = UnitFields_TBC.UNIT_END + 0x043F, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_RANGED_CRIT_PERCENTAGE = UnitFields_TBC.UNIT_END + 0x0440, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_OFFHAND_CRIT_PERCENTAGE = UnitFields_TBC.UNIT_END + 0x0441, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_SPELL_CRIT_PERCENTAGE1 = UnitFields_TBC.UNIT_END + 0x0442, // Size: 7, Type: FLOAT, Flags: PRIVATE
    PLAYER_SHIELD_BLOCK = UnitFields_TBC.UNIT_END + 0x0449, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_EXPLORED_ZONES_1 = UnitFields_TBC.UNIT_END + 0x044A, // Size: 128, Type: BYTES, Flags: PRIVATE
    PLAYER_REST_STATE_EXPERIENCE = UnitFields_TBC.UNIT_END + 0x04CA, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_COINAGE = UnitFields_TBC.UNIT_END + 0x04CB, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_DAMAGE_DONE_POS = UnitFields_TBC.UNIT_END + 0x04CC, // Size: 7, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = UnitFields_TBC.UNIT_END + 0x04D3, // Size: 7, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = UnitFields_TBC.UNIT_END + 0x04DA, // Size: 7, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_HEALING_DONE_POS = UnitFields_TBC.UNIT_END + 0x04E1, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_TARGET_RESISTANCE = UnitFields_TBC.UNIT_END + 0x04E2, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE = UnitFields_TBC.UNIT_END + 0x04E3, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_BYTES = UnitFields_TBC.UNIT_END + 0x04E4, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_AMMO_ID = UnitFields_TBC.UNIT_END + 0x04E5, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_SELF_RES_SPELL = UnitFields_TBC.UNIT_END + 0x04E6, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_PVP_MEDALS = UnitFields_TBC.UNIT_END + 0x04E7, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_BUYBACK_PRICE_1 = UnitFields_TBC.UNIT_END + 0x04E8, // Size: 12, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = UnitFields_TBC.UNIT_END + 0x04F4, // Size: 12, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_KILLS = UnitFields_TBC.UNIT_END + 0x0500, // Size: 1, Type: TWO_SHORT, Flags: PRIVATE
    PLAYER_FIELD_TODAY_CONTRIBUTION = UnitFields_TBC.UNIT_END + 0x0501, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_YESTERDAY_CONTRIBUTION = UnitFields_TBC.UNIT_END + 0x0502, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_LIFETIME_HONORBALE_KILLS = UnitFields_TBC.UNIT_END + 0x0503, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_BYTES2 = UnitFields_TBC.UNIT_END + 0x0504, // Size: 1, Type: BYTES, Flags: PRIVATE
    PLAYER_FIELD_WATCHED_FACTION_INDEX = UnitFields_TBC.UNIT_END + 0x0505, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_COMBAT_RATING_1 = UnitFields_TBC.UNIT_END + 0x0506, // Size: 24, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = UnitFields_TBC.UNIT_END + 0x051E, // Size: 18, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_HONOR_CURRENCY = UnitFields_TBC.UNIT_END + 0x0530, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_ARENA_CURRENCY = UnitFields_TBC.UNIT_END + 0x0531, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_MOD_MANA_REGEN = UnitFields_TBC.UNIT_END + 0x0532, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_FIELD_MOD_MANA_REGEN_INTERRUPT = UnitFields_TBC.UNIT_END + 0x0533, // Size: 1, Type: FLOAT, Flags: PRIVATE
    PLAYER_FIELD_MAX_LEVEL = UnitFields_TBC.UNIT_END + 0x0534, // Size: 1, Type: INT, Flags: PRIVATE
    PLAYER_FIELD_DAILY_QUESTS_1 = UnitFields_TBC.UNIT_END + 0x0535, // Size: 25, Type: INT, Flags: PRIVATE
    PLAYER_END = UnitFields_TBC.UNIT_END + 0x054E,
}
