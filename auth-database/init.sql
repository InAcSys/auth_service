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
    IF NOT EXISTS `Permissions` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL UNIQUE,
        `Description` VARCHAR(255) NOT NULL,
        `Path` VARCHAR(255) NOT NULL,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
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
    `Permissions` (`Name`, `Description`, `Path`)
VALUES
    (
        'Crear usuario',
        'Permite crear un nuevo usuario',
        '/'
    ),
    (
        'Actualizar usuario',
        'Permite actualizar un usuario existente',
        '/'
    ),
    (
        'Eliminar usuario',
        'Permite eliminar un usuario',
        '/'
    ),
    (
        'Leer usuario',
        'Permite ver los detalles de un usuario',
        '/'
    ),
    ('Crear rol', 'Permite crear un nuevo rol', '/'),
    (
        'Actualizar rol',
        'Permite actualizar un rol existente',
        '/'
    ),
    ('Eliminar rol', 'Permite eliminar un rol', '/'),
    (
        'Leer rol',
        'Permite ver los detalles de un rol',
        '/'
    ),
    (
        'Asignar rol',
        'Permite asignar un rol a un usuario',
        '/'
    );