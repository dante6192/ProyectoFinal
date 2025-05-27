<?php
require 'config.php';
require 'utils.php';

$dbConn = connect($db);

$sql = "SELECT * FROM diagnosticos";
$stmt = $dbConn->prepare($sql);
$stmt->execute();
$diagnosticos = $stmt->fetchAll(PDO::FETCH_ASSOC);

header("Content-Type: application/json");
echo json_encode($diagnosticos);
?>
