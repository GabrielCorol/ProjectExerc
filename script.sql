create database dbProjeto;

use dbProjeto;

create table Usuario (Id int primary key AUTO_INCREMENT,
Nome varchar(50),
Email varchar(50),
Senha varchar(50));

create table Produto (Id int primary key AUTO_INCREMENT,
Nome varchar(50),
Descricao varchar(200),
Preco decimal,
Quantidade varchar(50));

select * from Usuario;
select * from Produto;