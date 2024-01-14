CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

create table users
(
    id       bigserial    not null primary key,
    pid      uuid         not null unique,
    email    varchar(64)  not null unique,
    password varchar(255) not null
);

create table user_roles
(
    user_id bigint      not null references users (id) on delete cascade,
    "role"  varchar(64) not null,
    primary key (user_id, "role")
);

create table events
(
    id          bigserial                not null primary key,
    pid         uuid                     not null unique,
    creator_id  bigint                   not null references users (id) on delete no action,
    "name"      varchar(64)              not null,
    "start"     timestamp with time zone not null,
    "end"       timestamp with time zone not null,
    description varchar(1024) null,
    country     char(2) null,
    city        varchar(32) null,
    postcode    varchar(32) null,
    streethouse varchar(128) null
);

create table registrations
(
    id            bigserial                not null primary key,
    pid           uuid                     not null unique,
    event_id      bigint                   not null references events (id) on delete cascade,
    registered_at timestamp with time zone not null,
    "name"        varchar(64)              not null,
    phone         varchar(32) null,
    email         varchar(32) null
);

CREATE UNIQUE INDEX ix__registrations__name_phone ON registrations ("name", phone) WHERE phone is not null;
CREATE UNIQUE INDEX ix__registrations__name_email ON registrations ("name", email) WHERE email is not null;