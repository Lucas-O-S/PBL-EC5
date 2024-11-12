--Criação DB
create database Termo_Light
go

use [Termo_Light]
go

--Tabela de Empresa
create table empresa(
	id int identity(1,1) primary key not null,
	nome varchar(500) not null,
	logo varbinary(max) null,
	sede varchar(500) not null

)
go
insert into Empresa( nome, logo, sede) values ('Termo-Light', null, 'SP')
go

--Tabela Sensor
create table sensor(
	id int identity(1,1) primary key not null,
	descricao varchar(500) null,
	fk_empresa_id int null,
	foreign key (fk_empresa_id)  references Empresa(id) 
	on delete set null
	

)
go
--Tabela de acesso
create table acesso(
	id int identity(1,1) primary key not null,
	Nome_Usuario varchar(500) not null,
	login_Usuario varchar(500) not null,
	Senha varchar(500) not null,
	fk_empresa_id int not null,
	foreign key (fk_empresa_id)  references Empresa(id)
	---quando excluir uma empresa o acesso é deletado
	on delete cascade

)
go
insert into acesso (Nome_Usuario, login_usuario, senha, fk_empresa_id) values('SA','SA','123456',1)
go


--Tabela de Funcionarios
create table funcionario(
	id int identity(1,1) primary key not null,
	nome varchar(500) not null,
	cargo varchar(500) not null,
	foto varbinary(max) null

)
go

--Tabela Manutenção
create table manutencao(

	id int identity(1,1) primary key not null,
	data_hora datetime not null,
	fk_sensor_id int not null,
	fk_funcionario_id int not null,
	estado int not null,
	foreign key ( fk_sensor_id) references Sensor(id),
	foreign key (fk_funcionario_id) references Funcionario(id)


)
go

