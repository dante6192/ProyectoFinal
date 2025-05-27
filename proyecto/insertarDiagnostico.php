<?php
require 'config.php';
require 'utils.php';

header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Content-Type: application/json");

// Verifica si se recibieron los datos
if (isset($_POST['nombre']) && isset($_POST['resultado']) && isset($_POST['fecha']) && isset($_POST['imagen'])) {

    $nombre = $_POST['nombre'];
    $resultado = $_POST['resultado'];
    $fecha = $_POST['fecha'];
    $imagenBase64 = $_POST['imagen'];

    // Decodifica la imagen base64
    $nombreImagen = uniqid() . ".jpg";
    $rutaDestino = "imagenes/" . $nombreImagen;

    if (file_put_contents($rutaDestino, base64_decode($imagenBase64))) {
        $dbConn = connect($db);

        $sql = "INSERT INTO diagnosticos (NombrePaciente, Resultado, Imagen, Fecha)
                VALUES (:nombre, :resultado, :imagen, :fecha)";

        $stmt = $dbConn->prepare($sql);
        $stmt->bindParam(':nombre', $nombre);
        $stmt->bindParam(':resultado', $resultado);
        $stmt->bindParam(':imagen', $rutaDestino);
        $stmt->bindParam(':fecha', $fecha);

        if ($stmt->execute()) {
            echo json_encode(["mensaje" => "Diagnóstico guardado con éxito."]);
        } else {
            echo json_encode(["error" => "No se pudo guardar en la base de datos."]);
        }
    } else {
        echo json_encode(["error" => "Error al guardar la imagen base64."]);
    }
} else {
    echo json_encode(["error" => "Datos incompletos"]);
}
