CREATE TABLE IF NOT EXISTS accounts
(
    id SERIAL PRIMARY KEY,
    username VARCHAR(32) UNIQUE NOT NULL,
    session_key BYTEA
    -- TODO: verifier
);

CREATE INDEX IF NOT EXISTS accounts_username_idx ON accounts (username);