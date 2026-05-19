CREATE DATABASE KNS_Database;
USE KNS_Database;

CREATE USER if NOT EXISTS 'job_app_user'@'localhost'
IDENTIFIED BY 'Simon25';

GRANT INSERT, UPDATE, DELETE, SELECT
ON KNS_Database.*
TO 'job_app_user'@'localhost';

FLUSH PRIVILEGES;
SHOW GRANTS FOR 'job_app_user'@'localhost';
