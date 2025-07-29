CREATE TABLE
    IF NOT EXISTS `Roles` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
    ) CHARACTER
SET
    utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE
    IF NOT EXISTS `Categories` (
        `Id` INT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL,
        `ParentId` INT NULL,
        `Path` VARCHAR(100) NOT NULL,
        `Code` VARCHAR(100) NOT NULL,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
        FOREIGN KEY (`ParentId`) REFERENCES `Categories` (`Id`) ON DELETE CASCADE
    ) CHARACTER
SET
    utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE
    IF NOT EXISTS `Permissions` (
        `Id` INT AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(100) NOT NULL,
        `Description` VARCHAR(255) NOT NULL,
        `Path` VARCHAR(255) NOT NULL,
        `Code` VARCHAR(255) NOT NULL,
        `CategoryId` INT NULL,
        `IsActive` BOOLEAN NOT NULL DEFAULT TRUE,
        `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `Updated` TIMESTAMP NULL,
        `Deleted` TIMESTAMP NULL,
        `TenantId` CHAR(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
        FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`) ON DELETE CASCADE
    ) CHARACTER
SET
    utf8mb4 COLLATE utf8mb4_unicode_ci;

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
    ) CHARACTER
SET
    utf8mb4 COLLATE utf8mb4_unicode_ci;

INSERT INTO
    `Roles` (`Name`)
VALUES
    ('Estudiante'),
    ('Docente'),
    ('Admin'),
    ('Director'),
    ('Padre de familia');

INSERT INTO
    `Categories` (`Id`, `Name`, `ParentId`, `Path`, `Code`)
VALUES
    -- Categorías principales
    (1, 'LMS', NULL, '/lms', 'LMS_PAGE'),
    (2, 'Usuarios', NULL, '/users', 'USERS_PAGE'),
    (
        3,
        'Institución',
        NULL,
        '/institute',
        'INSTITUTE_PAGE'
    ),
    -- Subcategorías de LMS
    (4, 'Materias', 1, '/subjects', 'SUBJECTS_PAGE'),
    -- Subcategorías de Usuarios
    (
        5,
        'Administrar usuarios',
        2,
        '/management',
        'USERS_MANAGEMENT_PAGE'
    ),
    -- Subcategorias de Instituto
    (
        6,
        'Instituto',
        3,
        '/',
        'INSTITUTE_MANAGEMENT_PAGE'
    ),
    (
        7,
        'Niveles academicos',
        3,
        '/academic-levels',
        'ACADEMIC_LEVELS_PAGE'
    );

INSERT INTO
    `Permissions` (
        `Name`,
        `Description`,
        `Path`,
        `Code`,
        `CategoryId`
    )
VALUES
    (
        'Crear materias',
        'Crear nuevos materias',
        '/',
        'CREATE_SUBJECTS',
        4
    ), -- 1
    (
        'Crear tareas',
        'Crear nuevas tareas',
        '/',
        'CREATE_ASSIGNMENTS',
        4
    ), -- 2
    (
        'Publicar anuncios',
        'Publicar anuncions de clase',
        '/',
        'PUBLISH_ANNOUNCEMENTS',
        4
    ), -- 3
    (
        'Matricular estudiantes',
        'Matricular a los estudiantes respectivos dentro de la materia',
        '/',
        'ENROLL_STUDENTS',
        4
    ), -- 4
    (
        'Visualizar mis materias',
        'Permite unicamente ver las materias donde el usuario esta matriculado/a o es docente.',
        '/',
        'VIEW_MY_SUBJECTS',
        4
    ), -- 5
    (
        'Visualizar las materias',
        'Permite visualizar las materias que fueron creadas.',
        '/',
        'VIEW_SUBJECTS',
        4
    ), -- 6
    (
        'Crear usuarios',
        'Crear nuevos usuarios en el sistema',
        '/',
        'CREATE_USERS',
        5
    ), -- 7
    (
        'Eliminar usarios',
        'Eliminar usuarios del sistema',
        '/',
        'DELETE_USERS',
        5
    ), -- 8
    (
        'Visualizar perfil del instituto',
        'Permite la visualizacion y edicion de la informacion del instituto',
        '/',
        'INSTITUTE_SHOW',
        6
    ), -- 9
    (
        'Crear niveles',
        'Crear nuevos niveles academicos en el sistema',
        '/',
        'CREATE_ACADEMIC_LEVELS',
        7
    ), -- 10
    (
        'SUBIR TAREAS',
        'Permite a los estudiantes poder subir la resolucion de sus tareas.',
        '/',
        'SUBMIT_ASSIGNMENT',
        4
    ), -- 11
    (
        'Revision de tareas',
        'Permite a los docentes revisar y asignar un puntaje a la tarea enviada por el estudiante.',
        '/',
        'REVIEW_ASSIGNMENT',
        4
    );

-- 12