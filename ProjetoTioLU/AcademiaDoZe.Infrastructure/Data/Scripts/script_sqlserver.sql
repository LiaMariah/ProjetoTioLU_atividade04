IF OBJECT_ID(N'dbo.tb_logradouro', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tb_logradouro
    (
        id_logradouro INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        cep VARCHAR(8) NOT NULL,
        nome VARCHAR(150) NOT NULL,
        bairro VARCHAR(150) NOT NULL,
        cidade VARCHAR(150) NOT NULL,
        estado VARCHAR(2) NOT NULL,
        pais VARCHAR(100) NOT NULL
    );
END;

IF OBJECT_ID(N'dbo.tb_colaborador', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tb_colaborador
    (
        id_colaborador INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        cpf VARCHAR(11) NOT NULL UNIQUE,
        nome VARCHAR(150) NOT NULL,
        nascimento DATE NOT NULL,
        telefone VARCHAR(11) NOT NULL,
        email VARCHAR(254) NOT NULL UNIQUE,
        logradouro_id INT NOT NULL REFERENCES dbo.tb_logradouro(id_logradouro),
        numero VARCHAR(20) NOT NULL,
        complemento VARCHAR(150) NULL,
        senha VARCHAR(255) NOT NULL,
        foto VARBINARY(MAX) NULL,
        admissao DATE NOT NULL,
        tipo INT NOT NULL,
        vinculo INT NOT NULL
    );
END;

IF OBJECT_ID(N'dbo.tb_aluno', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tb_aluno
    (
        id_aluno INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        cpf VARCHAR(11) NOT NULL UNIQUE,
        nome VARCHAR(150) NOT NULL,
        nascimento DATE NOT NULL,
        telefone VARCHAR(11) NOT NULL,
        email VARCHAR(254) NOT NULL UNIQUE,
        logradouro_id INT NOT NULL REFERENCES dbo.tb_logradouro(id_logradouro),
        numero VARCHAR(20) NOT NULL,
        complemento VARCHAR(150) NULL,
        senha VARCHAR(255) NOT NULL,
        foto VARBINARY(MAX) NULL
    );
END;
