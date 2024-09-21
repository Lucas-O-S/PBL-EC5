
--Criação DB
create database		

--Tabela de Empresa
create table Empresa(
	id int primary key not null,
	nome varchar(500) not null,
	logo varbinary(max) null,
	sede varchar(500) not null

)

--Tabela Sensor
create table Sensor(
	id int primary key not null,
	descricao varchar(500) null,
	fk_empresa_id int not null,
	foreign key (fk_empresa_id)  references Empresa(id) 

)


--Tabela de acesso
create table Acesso(
	id int primary key not null,
	senha varchar(500) not null,
	fk_empresa_id int not null,
	foreign key (fk_empresa_id)  references Empresa(id) 

)

--Tabela Temperatura
create table Temperatura(
	data_hora datetime not null,
	valor int not null,
	fk_sensor_id int not null,
	primary key(data_hora, fk_sensor_id ),
	foreign key (fk_sensor_id) references Sensor(id)
)

--Tabela de Funcionarios
create table Funcionario(
	id int primary key not null,
	nome varchar(500) not null,
	Cargo varchar(500) not null,
	Foto varbinary(max) null

)

--Tabela Manutenção
create table Manutencao(

	data_hora datetime not null,
	fk_sensor_id int not null,
	fk_funcionario_id int not null,
	estado varchar(100) not null,
	primary key(data_hora,fk_sensor_id,fk_funcionario_id),
	foreign key ( fk_sensor_id) references Sensor(id),
	foreign key (fk_funcionario_id) references Funcionario(id)

)
