﻿--------------------------------------------------------
--  File created - Sunday-April-23-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table CUSTOMERS
--------------------------------------------------------

  CREATE TABLE "T00119683"."CUSTOMERS" 
   (	"CUSTOMER_ID" NUMBER(*,0), 
	"COMPANY_NAME" VARCHAR2(64 BYTE), 
	"TITLE" VARCHAR2(8 BYTE), 
	"FIRSTNAME" VARCHAR2(64 BYTE), 
	"SURNAME" VARCHAR2(64 BYTE), 
	"STREET" VARCHAR2(64 BYTE), 
	"TOWN" VARCHAR2(64 BYTE), 
	"COUNTY" VARCHAR2(64 BYTE), 
	"TELEPHONE" VARCHAR2(42 BYTE), 
	"EMAIL" VARCHAR2(42 BYTE), 
	"VAT_NO" VARCHAR2(24 BYTE), 
	"ACCOUNT_TYPE" CHAR(1 BYTE), 
	"STATUS" CHAR(1 BYTE) DEFAULT 'A', 
	"REGISTERED_ON" DATE DEFAULT CURRENT_DATE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table MATERIALS
--------------------------------------------------------

  CREATE TABLE "T00119683"."MATERIALS" 
   (	"MATERIAL_ID" NUMBER(*,0), 
	"NAME" VARCHAR2(64 BYTE), 
	"UNIT" VARCHAR2(32 BYTE), 
	"PRICE_PER_UNIT" NUMBER(19,4), 
	"STATUS" CHAR(1 BYTE) DEFAULT 'A', 
	"IS_SERVICE" CHAR(1 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table QUOTES
--------------------------------------------------------

  CREATE TABLE "T00119683"."QUOTES" 
   (	"QUOTE_ID" NUMBER(*,0), 
	"DATE_ISSUED" TIMESTAMP (6), 
	"CUSTOMER_ID" NUMBER(*,0), 
	"DESCRIPTION" VARCHAR2(224 BYTE), 
	"DATE_AMENDED" TIMESTAMP (6), 
	"STATUS" CHAR(1 BYTE) DEFAULT 'A', 
	"SUBTOTAL" NUMBER(10,2), 
	"VAT" NUMBER(10,2)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table QUOTE_MATERIALS
--------------------------------------------------------

  CREATE TABLE "T00119683"."QUOTE_MATERIALS" 
   (	"QUOTE_MATERIAL_ID" NUMBER(*,0), 
	"QUOTE_ID" NUMBER(*,0), 
	"MATERIAL_ID" NUMBER(*,0), 
	"DESCRIPTION" VARCHAR2(128 BYTE), 
	"PRICE_PER_UNIT" NUMBER(19,4), 
	"NUM_UNITS" NUMBER, 
	"IS_SERVICE" CHAR(1 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SETTINGS
--------------------------------------------------------

  CREATE TABLE "T00119683"."SETTINGS" 
   (	"SETTING_ID" NUMBER(*,0), 
	"COMPANY_NAME" VARCHAR2(64 BYTE), 
	"STREET" VARCHAR2(64 BYTE), 
	"TOWN" VARCHAR2(64 BYTE), 
	"COUNTY" VARCHAR2(64 BYTE), 
	"TELEPHONE" VARCHAR2(42 BYTE), 
	"EMAIL" VARCHAR2(42 BYTE), 
	"VAT_NO" VARCHAR2(24 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into T00119683.CUSTOMERS
SET DEFINE OFF;
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (1,null,'Mr','Michael','Griffin','Flintfield Faha','Killarney','Kerry','064312456','mdgriffin064@gmail.com',null,'P','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (2,null,'Mrs','Magdalena','Griffin','Flintfield Faha','Killarney','Kerry','0868875317','maga@example.com',null,'P','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (3,'ABC Ltd','Mr','Michael','Guinan','12 Example Street','Listowel','Kerry','067123456','me@example.com','IE2034508345','B','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (4,null,'Mr','Theresa','Henry','34 Oakland Lodge','Ballybunion','Kerry','0495922323','theresa@example.com',null,'P','I',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (5,'DCE ltd','Mr','Joe','Reilly','12 Watercress Street','Castleisland','Kerry','066123456','joe@example.com','IE293847383','B','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (6,'Fitzgibbon Sisters ltd','Mrs','Emma','Fitzgibbon','23 Strand hill','Dooks Glenbeigh','Kerry','063123456','emma@example.com','IE34545534','B','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (7,'Appleton Interiors','Mr','Joe','Reilly','12 Watercress Street','Castleisland','Kerry','066123456','joe@example.com','IE293847383','B','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (8,'John Twomey Construction Ltd.','Mr','John','Towomey','Orchard House Main Street','Castlegregory','Kerry','0666543311','twomey.construction@example.com','IE567474357','B','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (9,'The Sports Bar','Mr','Harry','Counihan','43 Castleford Avenue','Newcastle West','Limerick','0962384839','harry54@example.com','IE5676577663','B','A',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (10,'The Grand Hotel','Mrs','Alana','Turner','High Street','Kenmare','Kerry','06134534545','alana@example.com','IE4564564567','B','I',to_date('21-APR-17','DD-MON-RR'));
Insert into T00119683.CUSTOMERS (CUSTOMER_ID,COMPANY_NAME,TITLE,FIRSTNAME,SURNAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO,ACCOUNT_TYPE,STATUS,REGISTERED_ON) values (11,'Templeton Nursing Home','Ms','Geraldine','Phillips','Tralee Road','Milltown','Kerry','0665612789','templeton@example.com','IE456456654','B','A',to_date('21-APR-17','DD-MON-RR'));
REM INSERTING into T00119683.MATERIALS
SET DEFINE OFF;
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (1,'Roof Slate','Units',2.33,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (2,'Concrete Slab','Metres Squared',3.56,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (3,'Steel Nails','Units',0.03,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (4,'50mm By 100mm Oak Board','Metres',8.7,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (5,'Carpentry','Hours',17,'A','1');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (6,'Labour','Hours',14.5,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (7,'Concrete','Metres Cubed',10.23,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (8,'24mm White Marble Tile','Metres Squared',102.34,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (9,'Skim Coat Plaster','Kilogram',1.68,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (10,'Roof Felt','Metres Squared',4.58,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (11,'Eletrician','Hours',24,'A','1');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (12,'12mm Gypsum Slab','Metres Squared',3.21,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (13,'Porcelain Floor Tile','Metres Squared',23.5,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (14,'Plant Hire','Hours',54.25,'A','1');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (15,'Cooper Wiring','Metres',18.54,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (16,'Loft Insulation Roll','Units',39.75,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (17,'6 inch concrete block','Units',0.26,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (18,'4 inch concrete block','Units',0.21,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (19,'Prefinished Four Panel Pine Door','Units',120,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (20,'Prefinished Oak Four Panel Door','Units',109,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (21,'Prefinished Six Panel Oak Door','Units',109,'I','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (22,'8mm Oak Effect Laminate Flooring','Metres Squared',8.99,'A','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (23,'22mm Solid Nordic Oak Flooring','Metres Squared',79.99,'I','0');
Insert into T00119683.MATERIALS (MATERIAL_ID,NAME,UNIT,PRICE_PER_UNIT,STATUS,IS_SERVICE) values (24,'3mm Insulating Underlay','Metres Squared',27,'A','0');
REM INSERTING into T00119683.QUOTES
SET DEFINE OFF;
Insert into T00119683.QUOTES (QUOTE_ID,DATE_ISSUED,CUSTOMER_ID,DESCRIPTION,DATE_AMENDED,STATUS,SUBTOTAL,VAT) values (1,to_timestamp('18-APR-17 04.27.55.625000000 PM','DD-MON-RR HH.MI.SSXFF AM'),1,'Dormer Bungalow Kilcummin Killarney',to_timestamp('18-APR-17 04.27.55.625000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'A',13541,2550.36);
Insert into T00119683.QUOTES (QUOTE_ID,DATE_ISSUED,CUSTOMER_ID,DESCRIPTION,DATE_AMENDED,STATUS,SUBTOTAL,VAT) values (2,to_timestamp('21-APR-17 02.05.27.716000000 PM','DD-MON-RR HH.MI.SSXFF AM'),2,'Extension to Cottage Main Street Lixnaw',to_timestamp('23-APR-17 08.44.13.694000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'I',11089.29,2097.75);
Insert into T00119683.QUOTES (QUOTE_ID,DATE_ISSUED,CUSTOMER_ID,DESCRIPTION,DATE_AMENDED,STATUS,SUBTOTAL,VAT) values (3,to_timestamp('23-APR-17 11.43.25.248000000 AM','DD-MON-RR HH.MI.SSXFF AM'),3,'4 Bedroom Bungalow Lackabane Fossa Killarney',to_timestamp('23-APR-17 03.24.21.279000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'A',7112,1405.55);
REM INSERTING into T00119683.QUOTE_MATERIALS
SET DEFINE OFF;
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (1,1,1,'Roof Slate for Dormerr',3200,3200,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (2,1,6,'lllLabour for clearing site',150,150,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (3,1,5,'Carpentry Roofing Dormer',17,230,'1');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (4,2,1,'Roof Slate',2.33,3000,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (5,2,15,'Copper Wires',18.54,20,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (6,2,14,'Ground Clearing',54.25,32,'1');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (7,2,13,'Living Room Floor Tile',23.5,25,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (8,2,12,'Ceiling Slab',3.21,19,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (9,2,11,'Electrical Services',24,56,'1');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (10,3,1,'Roof Slateee',2300,2300,'0');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (11,3,5,'Roofing Carpentry',69,69,'1');
Insert into T00119683.QUOTE_MATERIALS (QUOTE_MATERIAL_ID,QUOTE_ID,MATERIAL_ID,DESCRIPTION,PRICE_PER_UNIT,NUM_UNITS,IS_SERVICE) values (12,3,6,'Site Clearance Labout',40,40,'0');
REM INSERTING into T00119683.SETTINGS
SET DEFINE OFF;
Insert into T00119683.SETTINGS (SETTING_ID,COMPANY_NAME,STREET,TOWN,COUNTY,TELEPHONE,EMAIL,VAT_NO) values (1,'ABCD Ltd','Main Street','Ballintubbert','Roscommon','056129394858','abc@example.com','IE349305858');
--------------------------------------------------------
--  DDL for Index PK_SETTINGS
--------------------------------------------------------

  CREATE UNIQUE INDEX "T00119683"."PK_SETTINGS" ON "T00119683"."SETTINGS" ("SETTING_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PK_QUOTES
--------------------------------------------------------

  CREATE UNIQUE INDEX "T00119683"."PK_QUOTES" ON "T00119683"."QUOTES" ("QUOTE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PK_MATERIALS
--------------------------------------------------------

  CREATE UNIQUE INDEX "T00119683"."PK_MATERIALS" ON "T00119683"."MATERIALS" ("MATERIAL_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PK_CUSTOMERS
--------------------------------------------------------

  CREATE UNIQUE INDEX "T00119683"."PK_CUSTOMERS" ON "T00119683"."CUSTOMERS" ("CUSTOMER_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PK_QUOTE_MATERIALS
--------------------------------------------------------

  CREATE UNIQUE INDEX "T00119683"."PK_QUOTE_MATERIALS" ON "T00119683"."QUOTE_MATERIALS" ("QUOTE_MATERIAL_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table SETTINGS
--------------------------------------------------------

  ALTER TABLE "T00119683"."SETTINGS" ADD CONSTRAINT "PK_SETTINGS" PRIMARY KEY ("SETTING_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "T00119683"."SETTINGS" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."SETTINGS" MODIFY ("TELEPHONE" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."SETTINGS" MODIFY ("COUNTY" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."SETTINGS" MODIFY ("TOWN" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."SETTINGS" MODIFY ("STREET" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."SETTINGS" MODIFY ("SETTING_ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table CUSTOMERS
--------------------------------------------------------

  ALTER TABLE "T00119683"."CUSTOMERS" ADD CONSTRAINT "PK_CUSTOMERS" PRIMARY KEY ("CUSTOMER_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("ACCOUNT_TYPE" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("TELEPHONE" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("COUNTY" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("TOWN" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("STREET" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("SURNAME" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("FIRSTNAME" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("TITLE" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."CUSTOMERS" MODIFY ("CUSTOMER_ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table QUOTES
--------------------------------------------------------

  ALTER TABLE "T00119683"."QUOTES" ADD CONSTRAINT "PK_QUOTES" PRIMARY KEY ("QUOTE_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("VAT" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("SUBTOTAL" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("STATUS" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("DESCRIPTION" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("CUSTOMER_ID" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("DATE_ISSUED" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTES" MODIFY ("QUOTE_ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table MATERIALS
--------------------------------------------------------

  ALTER TABLE "T00119683"."MATERIALS" ADD CONSTRAINT "PK_MATERIALS" PRIMARY KEY ("MATERIAL_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "T00119683"."MATERIALS" ADD CHECK (is_service in (0,1)) ENABLE;
  ALTER TABLE "T00119683"."MATERIALS" MODIFY ("PRICE_PER_UNIT" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."MATERIALS" MODIFY ("UNIT" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."MATERIALS" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."MATERIALS" MODIFY ("MATERIAL_ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table QUOTE_MATERIALS
--------------------------------------------------------

  ALTER TABLE "T00119683"."QUOTE_MATERIALS" ADD CONSTRAINT "PK_QUOTE_MATERIALS" PRIMARY KEY ("QUOTE_MATERIAL_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "T00119683"."QUOTE_MATERIALS" ADD CHECK (is_service in (0,1)) ENABLE;
  ALTER TABLE "T00119683"."QUOTE_MATERIALS" MODIFY ("DESCRIPTION" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTE_MATERIALS" MODIFY ("MATERIAL_ID" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTE_MATERIALS" MODIFY ("QUOTE_ID" NOT NULL ENABLE);
  ALTER TABLE "T00119683"."QUOTE_MATERIALS" MODIFY ("QUOTE_MATERIAL_ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table QUOTES
--------------------------------------------------------

  ALTER TABLE "T00119683"."QUOTES" ADD CONSTRAINT "CUSTOMER_QUOTES" FOREIGN KEY ("CUSTOMER_ID")
	  REFERENCES "T00119683"."CUSTOMERS" ("CUSTOMER_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table QUOTE_MATERIALS
--------------------------------------------------------

  ALTER TABLE "T00119683"."QUOTE_MATERIALS" ADD CONSTRAINT "FK_QUOTE_MATERIALS_MATERIAL" FOREIGN KEY ("MATERIAL_ID")
	  REFERENCES "T00119683"."MATERIALS" ("MATERIAL_ID") ENABLE;
  ALTER TABLE "T00119683"."QUOTE_MATERIALS" ADD CONSTRAINT "FK_QUOTE_MATERIALS_QUOTE" FOREIGN KEY ("QUOTE_ID")
	  REFERENCES "T00119683"."QUOTES" ("QUOTE_ID") ENABLE;