use arulWing

create Table Clients(
	ID INT IDENTITY(1,1),
	Profile VARCHAR(MAX),
	Name VARCHAR(255),
	Email VARCHAR(255),
	Phone INT,
	PRIMARY KEY(ID)
);

create Table Projects(
	ID INT,
	client VARCHAR(255),
	description VARCHAR(255),
	Status BIT DEFAULT '0',
	PRIMARY KEY(ID)
);

create Table Products(
	product VARCHAR(255),
	amount int
);

INSERT INTO Products VALUES
('Branding', 10000),
('Website', 23000),
('Animation', 50000),
('Typography', 5000),
('Illustration', 5000),
('Mobile App', 20000)

create Table Invoice(
	ID INT,
	amount int,
	PRIMARY KEY(ID)
);