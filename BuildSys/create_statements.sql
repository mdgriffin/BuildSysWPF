-- Software Engineering project
-- Author: Michael Griffin
-- Date: 27/03/2017

DROP TABLE QUOTE_MATERIALS;
DROP TABLE Materials;
DROP TABLE QUOTES;
--DROP TABLE UNITS;
DROP TABLE CUSTOMERS;
DROP TABLE SETTINGS;

CREATE TABLE Customers (
  customer_id int NOT NULL,
  company_name varchar(64),
  title varchar(8) NOT NULL, -- changed !!!
  firstname varchar(64) NOT NULL,
  surname varchar(64) NOT NULL,
  street varchar(64) NOT NULL,
  town varchar(64) NOT NULL,
  county varchar(64) NOT NULL,
  telephone varchar(42) NOT NULL,
  email varchar(42) NOT NULL,
  vat_no varchar(24),
  account_type char(1) NOT NULL,
  status char(1) DEFAULT 'A',
  CONSTRAINT pk_customers PRIMARY KEY (customer_id)
);

CREATE TABLE Quotes (
 quote_id int NOT NULL,
 date_issued TIMESTAMP NOT NULL,
 customer_id int NOT NULL,
 description VARCHAR2(224) NOT NULL, 
 date_amended TIMESTAMP, 
 status char(1) DEFAULT 'A' NOT NULL,
 subtotal number(10, 2) NOT NULL,
 vat number(10, 2) NOT NULL,
 CONSTRAINT pk_quotes PRIMARY KEY (quote_id),
 CONSTRAINT customer_quotes FOREIGN KEY (customer_id) REFERENCES Customers (customer_id)
);

/*
CREATE TABLE Units (
  unit_id int NOT NULL,
  name varchar(64) NOT NULL,
  CONSTRAINT pk_units PRIMARY KEY (unit_id)
);
*/

CREATE TABLE Materials (
  material_id int NOT NULL,
  name varchar(64) NOT NULL,
  unit varchar(32) NOT NULL,
  price_per_unit decimal(19, 4) NOT NULL,
  status char(1) DEFAULT 'A',
  is_service char(1) CHECK  (is_service in (0,1)),
  CONSTRAINT pk_materials PRIMARY KEY (material_id)
);


CREATE TABLE Quote_Materials (
  quote_material_id int NOT NULL,
  quote_id int NOT NULL,
  material_id int NOT NULL,
  description varchar(128) NOT NULL,
  price_per_unit decimal(19, 4),
  num_units number,
  is_service char(1) CHECK  (is_service in (0,1)),
  CONSTRAINT pk_quote_materials PRIMARY KEY (quote_material_id),
  CONSTRAINT fk_quote_materials_quote FOREIGN KEY (quote_id) REFERENCES Quotes (quote_id),
  CONSTRAINT fk_quote_materials_material FOREIGN KEY (material_id) REFERENCES Materials (material_id)
);

CREATE TABLE Settings (
  setting_id int NOT NULL,
  company_name varchar(64),
  street varchar(64) NOT NULL,
  town varchar(64) NOT NULL,
  county varchar(64) NOT NULL,
  telephone varchar(42) NOT NULL,
  email varchar(42) NOT NULL,
  vat_no varchar(24),
  CONSTRAINT pk_settings PRIMARY KEY (setting_id)
);

COMMIT;