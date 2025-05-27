<?php
// Incluir el archivo de configuración
require 'config.php';

// Crear la conexión usando los datos de config.php
$conexion = mysqli_connect($db['host'], $db['username'], $db['password'], $db['db']);

if (!$conexion) {
    die("Error al conectar a la base de datos: " . mysqli_connect_error());
}

// Verificar si se recibió el parámetro 'id'
if (isset($_GET['id'])) {
    $id = intval($_GET['id']); // Convertir a entero para evitar inyecciones SQL

    // Ejecutar el DELETE
    $query = "DELETE FROM diagnosticos WHERE Id = $id";

    if (mysqli_query($conexion, $query)) {
        echo "success";
    } else {
        echo "error: " . mysqli_error($conexion);
    }
} else {
    echo "error: id no recibido";
}
?>
