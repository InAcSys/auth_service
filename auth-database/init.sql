CREATE TABLE
    IF NOT EXISTS `Roles` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL UNIQUE,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
    );

CREATE TABLE
    IF NOT EXISTS `Categories` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL UNIQUE,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
    );

CREATE TABLE
    IF NOT EXISTS `Permissions` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL UNIQUE,
        `Description` VARCHAR(255) NOT NULL,
        `Path` VARCHAR(255) NOT NULL,
        `CategoryId` INT NULL,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
        FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`) ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS `RolePermissions` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `RoleId` INT NOT NULL,
        `PermissionId` INT NOT NULL,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
        FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE,
        FOREIGN KEY (`PermissionId`) REFERENCES `Permissions` (`Id`) ON DELETE CASCADE
    );

INSERT INTO
    `Roles` (`Name`)
VALUES
    ('Estudiante'),
    ('Docente'),
    ('Admin'),
    ('Director'),
    ('Padre de familia');

INSERT INTO
    `Categories` (`Name`)
VALUES
    ('Usuarios'),
    ('Cursos');

INSERT INTO
    `Permissions` (`Name`, `Description`, `Path`, `CategoryId`)
VALUES
    (
        'Crear usuario',
        'Permite crear un nuevo usuario',
        '/',
        1
    ),
    (
        'Actualizar usuario',
        'Permite actualizar un usuario existente',
        '/',
        1
    ),
    (
        'Eliminar usuario',
        'Permite eliminar un usuario',
        '/',
        1
    ),
    (
        'Leer usuario',
        'Permite ver los detalles de un usuario',
        '/',
        1
    ),
    ('Crear rol', 'Permite crear un nuevo rol', '/', 1),
    (
        'Actualizar rol',
        'Permite actualizar un rol existente',
        '/',
        1
    ),
    ('Eliminar rol', 'Permite eliminar un rol', '/', 1),
    (
        'Leer rol',
        'Permite ver los detalles de un rol',
        '/',
        1
    ),
    (
        'Asignar rol',
        'Permite asignar un rol a un usuario',
        '/',
        1
    );