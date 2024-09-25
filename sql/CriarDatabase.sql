--Criação DB
create database Termo_Light
go

use [Termo_Light]
go

--Tabela de Empresa
create table empresa(
	id int primary key not null,
	nome varchar(500) not null,
	logo varbinary(max) null,
	sede varchar(500) not null

)
go
insert into Empresa(id, nome, logo, sede) values (1,'Termo-Light', null, 'SP')
go

--Tabela Sensor
create table sensor(
	id int primary key not null,
	descricao varchar(500) null,
	fk_empresa_id int null,
	foreign key (fk_empresa_id)  references Empresa(id) 
	on delete set null
	

)
go


--Tabela de acesso
create table acesso(
	id int primary key not null,
	senha varchar(500) not null,
	fk_empresa_id int not null,
	foreign key (fk_empresa_id)  references Empresa(id)
	---quando excluir uma empresa o acesso é deletado
	on delete cascade

)
go
insert into acesso (id, senha, fk_empresa_id) values(1,'123456',1)
go

--Tabela Temperatura
create table temperatura(
	data_hora datetime not null,
	valor int not null,
	fk_sensor_id int not null,
	primary key(data_hora, fk_sensor_id ),
	foreign key (fk_sensor_id) references Sensor(id)
	--Quando deletar o sensor seus dados são excluidos
	on delete cascade
)
go

--Tabela de Funcionarios
create table funcionario(
	id int primary key not null,
	nome varchar(500) not null,
	cargo varchar(500) not null,
	foto varbinary(max) null

)
go
--Tabela Manutenção
create table manutencao(

	data_hora datetime not null,
	fk_sensor_id int not null,
	fk_funcionario_id int not null,
	estado varchar(100) not null,
	primary key(data_hora,fk_sensor_id,fk_funcionario_id),
	foreign key ( fk_sensor_id) references Sensor(id),
	foreign key (fk_funcionario_id) references Funcionario(id)


)
go
