CREATE TABLE managers (
    	id UUID PRIMARY KEY,
	mail VARCHAR(255) NOT NULL,
	hashed_password VARCHAR(255) NOT NULL,
    	name VARCHAR(35) NOT NULL,
	surname VARCHAR(35) NOT NULL,
	patronymic VARCHAR(35) NOT NULL
);

CREATE TABLE sellers 
(
	id UUID PRIMARY KEY,
	mail VARCHAR(255) NOT NULL,
	hashed_password VARCHAR(255) NOT NULL,
	name VARCHAR(35) NOT NULL,
	surname VARCHAR(35) NOT NULL,
	patronymic VARCHAR(35) NOT NULL, 
	manager_id UUID REFERENCES managers(id)
); 

CREATE TABLE scripts
(
	id SERIAL PRIMARY KEY NOT NULL,
	title VARCHAR(50) NOT NULL,
	description TEXT,
	creator_id UUID REFERENCES managers(id) NOT NULL
);

CREATE TABLE blocks
(
	id SERIAL PRIMARY KEY NOT NULL,
	script_id SERIAL REFERENCES scripts(id) NOT NULL,
	content TEXT
);

CREATE TABLE block_connections
(
	id SERIAL PRIMARY KEY NOT NULL,
	previous_block_id SERIAL REFERENCES blocks(id) NOT NULL,
	next_block_id SERIAL REFERENCES blocks(id) NOT NULL
)
