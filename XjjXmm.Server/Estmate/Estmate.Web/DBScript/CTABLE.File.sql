
create table patient
(
  id							VARCHAR(36) not null,
  name							VARCHAR(50),
  gender						VARCHAR(1), /* 1 男 2 女 */
  birthday						DATETIME,
  idcard_no						VARCHAR(18),
  phone_no						VARCHAR(20)
);


create table ESTIMATE_ITEM
(
  id               VARCHAR(36) not null,
  rfid				     VARCHAR(36) not null,
  patient_id       VARCHAR(36) not null,
  code             VARCHAR(36), /* 量表唯一code */
  period            VARCHAR(10),  /* 评分阶段，入院 1， 住院 2， 出院 3， 随访 4 */
  period_desc       VARCHAR(20),  /* 评分阶段，入院 1， 住院 2， 出院 3， 随访 4 */
  estimate_score   int,    /* 总分， */
  is_delete        int default 0,
  module_type      VARCHAR(20),/* 模块， 卒中， 血透。。。*/
  estimate_value   CLOB,

)




prompt
prompt Creating table MED_FILE_INFO
prompt ===================================
prompt
create table MED_FILE_INFO
(
  ID                            VARCHAR2(36) not null,
  MD5				        VARCHAR2(36) not null,
  FILE_NAME	                    VARCHAR2(50),
  FILE_PATH                       VARCHAR2(1000),
  Root_PATH                       VARCHAR2(500),
  FILE_TYPE			            VARCHAR2(20),
  FILE_SIZE                      int,
  CREATE_DATE		  	        DATE,
 
  constraint MED_FILE_INFO_PK primary key (ID)
)
;
grant select, insert, update, delete on MED_FILE_INFO to ROLE_DOCARE;



prompt
prompt Creating table MED_ECG_FILE
prompt ===================================
prompt
create table MED_ECG_FILE
(
  ID                            VARCHAR2(36) not null,
  HOSPITAL_ID                   VARCHAR2(36) not null,
  PATIENT_ID                    VARCHAR2(36) not null,
  VISIT_ID                      VARCHAR2(36) not null,
  ECG_NO                        VARCHAR2(36) not null, 
  FILE_ID                       VARCHAR2(36) not null, /* 文件服务器ID */
  constraint MED_ECG_FILE_PK primary key (ID)
);
grant select, insert, update, delete on MED_ECG_FILE to ROLE_DOCARE;


