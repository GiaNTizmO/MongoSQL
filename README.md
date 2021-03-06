# MongoSQL 
![MongoSQL-LOGO](https://i.imgur.com/frhyKff.png) 

MongoSQL - BS (Bridge Server) - Bridge MongoDB and SQL Command Interfave (Imitate MySQL MariaDB Server with SQL)

![MongoSQL](https://i.imgur.com/mAC4RTN.png)
![MongoSQL Databases list](https://i.imgur.com/PwYsXc1.png)

## Our Goal
Our goal: To combine the familiar MySQL system with SQL queries with an ultra-fast, flexible and efficient No-SQL MongoDB database.
Combine the two technologies to produce an ultra fast SQL-like database.
## How does it work?
We have a MongoDB server and a client that works with MySQL-type databases.
Mongosql simulates the operation of a MySQL server with identical responses to queries with support for SQL commands, but it already works directly with the MongoDB Database.

### Simply put:
Mongosql is a bridge between MongoDB and the MySQL client

## What is implemented?
Right now - NOTHING!
My goal - Simulate mysql server (MySQL 5.7 MariaDB 10.3.25) packets.
Second goal - Write SQL Processor with basic SQL commands support (SELECT, INSERT, UPDATE, CREATE, ALTER TABLE)

## Stage of development:
Packet stage:
- [x] Greeting Packet
![Greeting Packet image](https://i.imgur.com/IlOlUGb.png)
- [x] Auth Packet
- [x] Version Packet
- [ ] Response Packet
- [ ] Command Packet
- [ ] MongoDB Integration
- [x] Config system
- [ ] mysql_native_password encrypting
- [ ] OpenSSL Encryption feature
- [x] CLI Interface
