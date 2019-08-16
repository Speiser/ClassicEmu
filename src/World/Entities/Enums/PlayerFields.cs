namespace Classic.World.Entities.Enums
{
    public enum PlayerFields
    {
        PLAYER_DUEL_ARBITER = 0x00 + UnitFields.UNIT_END, // Size:2
        PLAYER_FLAGS = 0x02 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDID = 0x03 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDRANK = 0x04 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES = 0x05 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_2 = 0x06 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_3 = 0x07 + UnitFields.UNIT_END, // Size:1
        PLAYER_DUEL_TEAM = 0x08 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILD_TIMESTAMP = 0x09 + UnitFields.UNIT_END, // Size:1
        PLAYER_QUEST_LOG_1_1 = 0x0A + UnitFields.UNIT_END, // count = 20
        PLAYER_QUEST_LOG_1_2 = 0x0B + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_1_3 = 0x0C + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_1 = 0x43 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_2 = 0x44 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_3 = 0x45 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + UnitFields.UNIT_END, // Size:2, count = 19
        PLAYER_VISIBLE_ITEM_1_0 = 0x48 + UnitFields.UNIT_END, // Size:8
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + UnitFields.UNIT_END,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + UnitFields.UNIT_END, // Size:46
        PLAYER_FIELD_PACK_SLOT_1 = 0x158 + UnitFields.UNIT_END, // Size:32
        PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178 + UnitFields.UNIT_END, // Size:48
        PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + UnitFields.UNIT_END, // Size:12
        PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + UnitFields.UNIT_END,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + UnitFields.UNIT_END, // Size:24
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + UnitFields.UNIT_END,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + UnitFields.UNIT_END, // Size:64
        PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + UnitFields.UNIT_END,
        PLAYER_FARSIGHT = 0x20c + UnitFields.UNIT_END, // Size:2
        PLAYER_FIELD_COMBO_TARGET = 0x20e + UnitFields.UNIT_END, // Size:2
        PLAYER_XP = 0x210 + UnitFields.UNIT_END, // Size:1
        PLAYER_NEXT_LEVEL_XP = 0x211 + UnitFields.UNIT_END, // Size:1
        PLAYER_SKILL_INFO_1_1 = 0x212 + UnitFields.UNIT_END, // Size:384
        PLAYER_SKILL_PROP_1_1 = 0x213 + UnitFields.UNIT_END, // Size:384

        PLAYER_CHARACTER_POINTS1 = 0x392 + UnitFields.UNIT_END, // Size:1
        PLAYER_CHARACTER_POINTS2 = 0x393 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_CREATURES = 0x394 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_RESOURCES = 0x395 + UnitFields.UNIT_END, // Size:1
        PLAYER_BLOCK_PERCENTAGE = 0x396 + UnitFields.UNIT_END, // Size:1
        PLAYER_DODGE_PERCENTAGE = 0x397 + UnitFields.UNIT_END, // Size:1
        PLAYER_PARRY_PERCENTAGE = 0x398 + UnitFields.UNIT_END, // Size:1
        PLAYER_CRIT_PERCENTAGE = 0x399 + UnitFields.UNIT_END, // Size:1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + UnitFields.UNIT_END, // Size:1
        PLAYER_EXPLORED_ZONES_1 = 0x39b + UnitFields.UNIT_END, // Size:64
        PLAYER_REST_STATE_EXPERIENCE = 0x3db + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COINAGE = 0x3dc + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT0 = 0x3DD + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT1 = 0x3DE + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT2 = 0x3DF + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT3 = 0x3E0 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT4 = 0x3E1 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT0 = 0x3E2 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT1 = 0x3E3 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT2 = 0x3E4 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT3 = 0x3E5 + UnitFields.UNIT_END, // Size:1,
        PLAYER_FIELD_NEGSTAT4 = 0x3E6 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3E7 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3EE + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3F5 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3FC + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_BYTES = 0x40A + UnitFields.UNIT_END, // Size:1
        PLAYER_AMMO_ID = 0x40B + UnitFields.UNIT_END, // Size:1
        PLAYER_SELF_RES_SPELL = 0x40C + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_PVP_MEDALS = 0x40D + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x40E + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_PRICE_LAST = 0x419 + UnitFields.UNIT_END,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x41A + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = 0x425 + UnitFields.UNIT_END,
        PLAYER_FIELD_SESSION_KILLS = 0x426 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_KILLS = 0x427 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_KILLS = 0x428 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_KILLS = 0x429 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0x42a + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = 0x42b + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BYTES2 = 0x430 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + UnitFields.UNIT_END, // Size:20

        PLAYER_END = 0x446 + UnitFields.UNIT_END
    }
}