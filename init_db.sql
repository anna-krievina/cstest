CREATE DATABASE cstestdb;
GO

CREATE LOGIN csTest WITH PASSWORD = 'testdb1';  
GO

Use [cstestdb]

CREATE USER csTestUser FOR LOGIN csTest
GO

grant ALL to csTestUser
GO

grant SELECT, INSERT, UPDATE, DELETE, VIEW DEFINITION, ALTER to csTestUser
GO

CREATE TABLE users (
    id INT IDENTITY(1,1),
    username VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL,
	permission VARCHAR(100)
    PRIMARY KEY (id)
);

CREATE TABLE product (
    id INT IDENTITY(1,1),
    product_name VARCHAR(100) NOT NULL,
    amount INT NOT NULL,
    price FLOAT NOT NULL
    PRIMARY KEY (id)
);

CREATE TABLE audit(
	id INT IDENTITY(1,1),
	username VARCHAR(100),
	action VARCHAR(200),
	date DATE,
    PRIMARY KEY (id)
);

GO

INSERT INTO users (username, password, permission)
VALUES ('admin', 'admin', 'admin');
INSERT INTO users (username, password, permission)
VALUES ('test', 'test', 'user');


INSERT INTO product (product_name, amount, price)
VALUES ('HDD 1TB', '55', '74.09');
INSERT INTO product (product_name, amount, price)
VALUES ('HDD SSD 512GB', '102', '190.99');
INSERT INTO product (product_name, amount, price)
VALUES ('RAM DDR4 16GB', '47', '80.32');


INSERT INTO audit (username, action, date)
VALUES ('test', 'create', '2023-10-10 08:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'delete', '2023-10-11 09:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'edit', '2023-10-14 09:35:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'create', '2023-10-12 08:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'edit', '2023-10-14 10:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'create', '2023-10-06 11:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'edit', '2023-10-07 18:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'delete', '2023-10-05 12:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'edit', '2023-10-06 12:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'delete', '2023-10-14 08:05:00');
INSERT INTO audit (username, action, date)
VALUES ('test', 'create', '2023-10-05 08:05:00');
