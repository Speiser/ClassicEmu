CREATE TABLE IF NOT EXISTS accounts
(
    id SERIAL PRIMARY KEY,
    username VARCHAR(32) UNIQUE NOT NULL,
    session_key BYTEA
    -- TODO: verifier
);

CREATE INDEX IF NOT EXISTS accounts_username_idx ON accounts (username);

CREATE TABLE IF NOT EXISTS realms
(
    id SERIAL PRIMARY KEY,
    name VARCHAR(32) UNIQUE NOT NULL,
    address VARCHAR(255) DEFAULT '127.0.0.1',
    port_vanilla SMALLINT DEFAULT 13250,
    port_tbc SMALLINT DEFAULT 13251,
    port_wotlk SMALLINT DEFAULT 13252,
    type SMALLINT DEFAULT 0,
    flags SMALLINT DEFAULT 2,
    population INT DEFAULT 0,
    timezone SMALLINT DEFAULT 1
);

CREATE TABLE IF NOT EXISTS realmcharacters
(
    realm_id INT NOT NULL,
    account_id INT NOT NULL,
    amount SMALLINT DEFAULT 0,
    PRIMARY KEY (realm_id, account_id)
);