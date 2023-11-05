CREATE TABLE IF NOT EXISTS characters
(
    id SERIAL PRIMARY KEY,
    account_id INT NOT NULL,
    name VARCHAR(32) UNIQUE NOT NULL,
    level SMALLINT DEFAULT 1,
    xp INT DEFAULT 0,
    race SMALLINT NOT NULL,
    class SMALLINT NOT NULL,
    gender SMALLINT NOT NULL,
    skin SMALLINT NOT NULL,
    face SMALLINT NOT NULL,
    hair_style SMALLINT NOT NULL,
    hair_color SMALLINT NOT NULL,
    facial_hair SMALLINT NOT NULL,
    outfit_id SMALLINT NOT NULL,
    created TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    position_x FLOAT NOT NULL,
    position_y FLOAT NOT NULL,
    position_z FLOAT NOT NULL,
    position_o FLOAT NOT NULL,
    map_id INT NOT NULL,
    zone_id INT NOT NULL,
    flag INT NOT NULL,
    watch_faction INT DEFAULT 255,
    rested_state SMALLINT DEFAULT 1,
    stand_state SMALLINT NOT NULL
);

CREATE INDEX IF NOT EXISTS characters_name_idx ON characters (name);