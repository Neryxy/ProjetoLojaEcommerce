-- CRIANDO O BANCO DE DADOS
CREATE DATABASE dbProjetoLoja;
-- USANDO O BANCO DE DADOS
USE dbProjetoLoja;

-- CRIANDO AS TABELAS DO BANCO DE DADOS
CREATE TABLE tb_Usuario(
	Id int primary key auto_increment,
    Nome varchar(50) not null,
    Email varchar(50) not null,
    Senha varchar(250) not null,
    Nivel varchar(50) not null
);

CREATE TABLE tb_Produtos(
	Id int primary key auto_increment,
    Nome varchar(150) not null,
    Descricao varchar(250) not null,
    Preco decimal(12,2) not null,
    Estoque int not null,
    Imagem varchar(200) not null
);

-- CONSULTANDO A TABELA DO BANCO DE DADOS

SELECT * FROM tb_Usuario;

-- INSERINDO DADOS NA TABELA DO BANCO DE DADOS

INSERT INTO tb_Usuario(Nome, Email, Senha, Nivel)VALUES('Administrador','admin@email.com','123456','Admin');

INSERT INTO tb_Produtos(Nome, Descricao, Preco, Estoque, Imagem)VALUES('Mouse Gamer RGB', 'Mouse ergonômico com iluminação RGB e alta precisão para jogos',129.90,25,'mouse_gamer.jpg');

INSERT INTO tb_Produtos(Nome, Descricao, Preco, Estoque, Imagem)VALUES('Teclado Mecânico RGB', 'Teclado mecânico com switches azuis, iluminação RGB e alta durabilidade para jogos e digitação', 249.90, 18, 'teclado_mecanico.jpg');
SELECT * FROM tb_Produtos;

INSERT INTO tb_Produtos(Nome, Descricao, Preco, Estoque,Imagem)VALUES('Headset Gamer USB','Headset com som surround, microfone ajustável e almofadas confortáveis', 249.90, 8, 'headset-gamer.jpg');

SELECT * FROM tb_Usuario