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
    `Categories` (`Id`, `Name`, `ParentId`, `Path`)
VALUES
    -- Categorías principales
    (1, 'Analíticas y reportes', NULL, '/monitoring'),
    (2, 'LMS', NULL, '/lms'),
    (3, 'Calendario', NULL, '/calendar'),
    (4, 'Pagos', NULL, '/payment'),
    (5, 'Calificaciones', NULL, '/grades'),
    (6, 'Mensajería', NULL, '/inbox'),
    (7, 'Usuarios', NULL, '/users'),
    (8, 'Cursos', NULL, '/courses'),
    (9, 'Reuniones', NULL, '/meetings'),
    -- Subcategorías de Analíticas y reportes
    (10, 'Monitoreo', 1, '/analytics'),
    (11, 'Reportes', 1, '/reports'),
    (12, 'Asistencia', 1, '/attendance'),
    -- Subcategorías de LMS
    (13, 'Cursos', 2, '/courses'),
    (14, 'Usuarios', 2, '/users'),
    (15, 'Tareas', 2, '/assignments'),
    -- Subcategorías de Calendario
    (16, 'Actividades', 3, '/activities'),
    (17, 'Horarios', 3, '/schedules'),
    -- Subcategorías de Pagos
    (18, 'Administración', 4, '/admin'),
    (19, 'General', 4, '/general'),
    -- Subcategorías de Calificaciones
    (20, 'Docentes', 5, '/teacher'),
    (21, 'Estudiante', 5, '/grades'),
    (22, 'Padres', 5, '/grades'),
    -- Subcategorías de Usuarios
    (23, 'Administrar usuarios', 7, '/management'),
    (24, 'Administrar roles', 7, '/roles/management');

INSERT INTO
    `Permissions` (
        `Name`,
        `Description`,
        `Path`,
        `Code`,
        `CategoryId`
    )
VALUES
    -- Permisos para Monitoreo (CategoryId 10)
    (
        'Ver análisis general',
        'Ver las analíticas generales de la institución',
        '/general',
        'VIEW_GENERAL_ANALYSIS',
        10
    ), -- 1
    (
        'Ver análisis categorizado',
        'Ver las analíticas según categorías',
        '/category',
        'VIEW_CATEGORIZED_ANALYSIS',
        10
    ), -- 2
    (
        'Ver análisis individual',
        'Ver análisis individual de un usuario',
        '/individual/:userId',
        'VIEW_INDIVIDUAL_ANALYSIS',
        10
    ), -- 3
    -- Permisos para Reportes (CategoryId 11)
    (
        'Ver reportes general',
        'Ver reportes generales de la institución',
        '/general',
        'VIEW_GENERAL_REPORTS',
        11
    ), -- 4
    (
        'Ver reportes categorizado',
        'Ver reportes según categorías',
        '/category',
        'VIEW_CATEGORIZED_REPORTS',
        11
    ), -- 5
    (
        'Ver reportes individual',
        'Ver reportes individuales de un usuario',
        '/individual/:userId',
        'VIEW_INDIVIDUAL_REPORTS',
        11
    ), -- 6
    (
        'Crear reportes',
        'Crear nuevos reportes',
        '/create',
        'CREATE_REPORTS',
        11
    ), -- 7
    (
        'Actualizar reportes',
        'Actualizar reportes existentes',
        '/update',
        'UPDATE_REPORTS',
        11
    ), -- 8
    (
        'Eliminar reportes',
        'Eliminar reportes',
        '/delete',
        'DELETE_REPORTS',
        11
    ), -- 9
    -- Permisos para Asistencia (CategoryId 12)
    (
        'Ver asistencia',
        'Ver asistencia de usuario',
        '/user/:userId',
        'VIEW_ATTENDANCE',
        12
    ), -- 10
    (
        'Actualizar asistencia',
        'Actualizar registros de asistencia',
        '/update',
        'UPDATE_ATTENDANCE',
        12
    ), -- 11
    -- Permisos para Cursos (CategoryId 13)
    (
        'Crear cursos',
        'Crear nuevos cursos',
        '/create',
        'CREATE_COURSES',
        13
    ), -- 12
    (
        'Eliminar cursos',
        'Eliminar cursos existentes',
        '/delete',
        'DELETE_COURSES',
        13
    ), -- 13
    (
        'Actualizar cursos',
        'Actualizar información de cursos',
        '/update',
        'UPDATE_COURSES',
        13
    ), -- 14
    (
        'Ver cursos',
        'Ver lista de cursos',
        '/',
        'VIEW_COURSES',
        13
    ), -- 15
    -- Permisos para Usuarios LMS (CategoryId 14)
    (
        'Añadir estudiantes',
        'Añadir estudiantes al LMS',
        '/add-student',
        'ADD_STUDENTS',
        14
    ), -- 16
    (
        'Eliminar estudiantes',
        'Eliminar estudiantes del LMS',
        '/remove-student',
        'REMOVE_STUDENTS',
        14
    ), -- 17
    (
        'Añadir profesores',
        'Añadir profesores al LMS',
        '/add-teacher',
        'ADD_TEACHERS',
        14
    ), -- 18
    (
        'Eliminar profesores',
        'Eliminar profesores del LMS',
        '/remove-teacher',
        'REMOVE_TEACHERS',
        14
    ), -- 19
    -- Permisos para Tareas (CategoryId 15)
    (
        'Asignar tareas',
        'Asignar tareas a estudiantes',
        '/assign',
        'ASSIGN_TASKS',
        15
    ), -- 20
    (
        'Visualizar tarea',
        'Visualizar tarea específica',
        '/:assignmentId',
        'VIEW_ASSIGNMENT',
        15
    ), -- 21
    (
        'Revisar tareas de estudiantes',
        'Revisar tareas entregadas por estudiantes',
        '/review/:assignmentId/student/:studentId',
        'REVIEW_ASSIGNMENTS',
        15
    ), -- 22
    (
        'Subir tareas',
        'Subir tareas entregadas por estudiantes',
        '/submit',
        'SUBMIT_ASSIGNMENTS',
        15
    ), -- 23
    (
        'Dejar comentarios',
        'Dejar comentarios en tareas',
        '/comments',
        'COMMENT_ASSIGNMENTS',
        15
    ), -- 24
    -- Permisos para Actividades (CategoryId 16)
    (
        'Crear actividades',
        'Crear nuevas actividades en el calendario',
        '/create',
        'CREATE_ACTIVITIES',
        16
    ), -- 25
    (
        'Eliminar actividades',
        'Eliminar actividades del calendario',
        '/delete',
        'DELETE_ACTIVITIES',
        16
    ), -- 26
    (
        'Actualizar actividades',
        'Actualizar actividades existentes',
        '/update',
        'UPDATE_ACTIVITIES',
        16
    ), -- 27
    (
        'Ver actividades',
        'Ver todas las actividades',
        '/',
        'VIEW_ACTIVITIES',
        16
    ), -- 28
    -- Permisos para Horarios (CategoryId 17)
    (
        'Crear horarios',
        'Crear nuevos horarios',
        '/create',
        'CREATE_SCHEDULES',
        17
    ), -- 29
    (
        'Asignar horarios',
        'Asignar horarios a usuarios',
        '/assign',
        'ASSIGN_SCHEDULES',
        17
    ), -- 30
    (
        'Actualizar horarios',
        'Actualizar horarios existentes',
        '/update',
        'UPDATE_SCHEDULES',
        17
    ), -- 31
    (
        'Eliminar horarios',
        'Eliminar horarios',
        '/delete',
        'DELETE_SCHEDULES',
        17
    ), -- 32
    (
        'Ver mi horario',
        'Ver horario personal de usuario',
        '/:userId',
        'VIEW_MY_SCHEDULE',
        17
    ), -- 33
    -- Permisos para Administración de Pagos (CategoryId 18)
    (
        'Asignar mensualidad',
        'Asignar mensualidades a estudiantes',
        '/assign-monthly-fee',
        'ASSIGN_MONTHLY_FEE',
        18
    ), -- 34
    (
        'Añadir pagos extras',
        'Añadir pagos extras o adicionales',
        '/add-extra-payments',
        'ADD_EXTRA_PAYMENTS',
        18
    ), -- 35
    (
        'Ver historial de pagos general',
        'Ver historial general de pagos',
        '/history',
        'VIEW_PAYMENT_HISTORY_GENERAL',
        18
    ), -- 36
    -- Permisos para Pagos generales (CategoryId 19)
    (
        'Ver historial de pagos por alumno',
        'Ver historial de pagos por estudiante',
        '/student/:studentId',
        'VIEW_PAYMENT_HISTORY_STUDENT',
        19
    ), -- 37
    (
        'Realizar pagos',
        'Realizar pagos de mensualidades u otros',
        '/make-payment',
        'MAKE_PAYMENTS',
        19
    ), -- 38
    (
        'Consultar deudas',
        'Consultar deudas de padres o responsables',
        '/debts/:parentId',
        'VIEW_DEBTS',
        19
    ), -- 39
    -- Permisos para Docentes Calificaciones (CategoryId 20)
    (
        'Ver lista de alumnos',
        'Ver lista de alumnos asignados',
        '/',
        'VIEW_STUDENT_LIST',
        20
    ), -- 40
    (
        'Revisar notas de alumno',
        'Revisar las notas de un alumno',
        '/student/:studentId',
        'REVIEW_STUDENT_GRADES',
        20
    ), -- 41
    (
        'Reportar problemas de alumno',
        'Reportar problemas relacionados a un alumno',
        '/report',
        'REPORT_STUDENT_ISSUES',
        20
    ), -- 42
    -- Permisos para Estudiantes Calificaciones (CategoryId 21)
    (
        'Ver mis notas',
        'Estudiante puede ver sus propias notas',
        '/student',
        'VIEW_OWN_GRADES',
        21
    ), -- 43
    (
        'Reportar problemas',
        'Estudiante puede reportar problemas',
        '/report',
        'REPORT_OWN_ISSUES',
        21
    ), -- 44
    -- Permisos para Padres Calificaciones (CategoryId 22)
    (
        'Ver notas por estudiantes',
        'Padres pueden ver notas de sus hijos',
        '/student/:studentId',
        'VIEW_CHILD_GRADES',
        22
    ), -- 45
    -- Permisos para Mensajería (CategoryId 6)
    (
        'Mandar mensajes',
        'Enviar mensajes a otros usuarios',
        '/send',
        'SEND_MESSAGES',
        6
    ), -- 46
    (
        'Ver mensajes',
        'Ver mensajes recibidos',
        '/inbox',
        'VIEW_MESSAGES',
        6
    ), -- 47
    (
        'Eliminar mensajes',
        'Eliminar mensajes recibidos o enviados',
        '/delete',
        'DELETE_MESSAGES',
        6
    ), -- 48
    -- Permisos para Administración de Usuarios (CategoryId 23)
    (
        'Ver estudiantes',
        'Ver lista de estudiantes',
        '/students',
        'VIEW_STUDENTS',
        23
    ), -- 49
    (
        'Editar información de estudiantes',
        'Editar datos de estudiantes',
        '/edit-student',
        'EDIT_STUDENTS',
        23
    ), -- 50
    (
        'Eliminar estudiantes',
        'Eliminar estudiantes del sistema',
        '/delete-student',
        'DELETE_STUDENTS',
        23
    ), -- 51
    (
        'Ver información de usuario',
        'Ver perfil de usuario',
        '/user/:userId',
        'VIEW_USER_INFO',
        23
    ), -- 52
    -- Permisos para Administración de Roles (CategoryId 24)
    (
        'Ver roles',
        'Ver lista de roles',
        '/',
        'VIEW_ROLES',
        24
    ), -- 53
    (
        'Crear roles',
        'Crear nuevos roles',
        '/create',
        'CREATE_ROLES',
        24
    ); -- 54
