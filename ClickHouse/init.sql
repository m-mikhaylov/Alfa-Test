CREATE USER IF NOT EXISTS 'alfn' IDENTIFIED BY 'alfn';

GRANT ALL PRIVILEGES ON alfn.* TO 'alfn';

CREATE DATABASE IF NOT EXISTS alfn;

USE alfn;

CREATE TABLE IF NOT EXISTS primes (
    timestamp_generated DateTime,
    timestamp_received DateTime,
    nickname String,
    number UInt32
) ENGINE = MergeTree()
ORDER BY timestamp_generated;

CREATE TABLE IF NOT EXISTS alfn.kafka_primes (
    timestamp_generated DateTime,
    number UInt32
) ENGINE = Kafka() 
SETTINGS kafka_broker_list = 'kafka:9092',
         kafka_topic_list = 'primes',
         kafka_group_name = 'primes_group',
         kafka_format = 'JSONEachRow';

CREATE MATERIALIZED VIEW IF NOT EXISTS alfn.kafka_primes_to_primes TO alfn.primes 
AS SELECT 
    now() as timestamp_received,
    timestamp_generated,
    'omaxmix' as nickname, 
    number 
FROM alfn.kafka_primes;


