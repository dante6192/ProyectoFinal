<?php
require 'config.php';
require 'utils.php';

header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json");

$dbConn = connect($db);
$sql = "SELECT * FROM diagnosticos";
$stmt = $dbConn->prepare($sql);
$stmt->execute();
$resultado = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode($resultado);
?>
