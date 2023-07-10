CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS cults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500),
        tags VARCHAR(255),
        leaderId VARCHAR(255) NOT NULL,
        FOREIGN KEY (leaderId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

ALTER TABLE cults ADD COLUMN popularity INT NOT NULL DEFAULT 0;

INSERT INTO
    cults(
        name,
        description,
        tags,
        leaderId
    )
VALUES (
        "Curious George's Silly Monkeys",
        "All hail the yellow hat",
        "bananas, monkey biz, ooh ooh ah ah",
        "632cc248c1fe0f9df71b9d4d"
    );

DELETE FROM cults WHERE id = 3;

CREATE TABLE
    IF NOT EXISTS cult_members(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        cultId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY (cultId) REFERENCES cults(id) ON DELETE CASCADE,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        UNIQUE(cultId, accountId)
    ) default charset utf8 COMMENT '';

SELECT cm.id, act.*
FROM cult_members cm
    JOIN accounts act ON act.id = cm.accountId;

INSERT INTO
    cult_members (cultId, accountId)
VALUES (1, "632cc248c1fe0f9df71b9d4d");