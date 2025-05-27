-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 27-05-2025 a las 06:06:51
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_diagnostico`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `diagnosticos`
--

CREATE TABLE `diagnosticos` (
  `Id` int(11) NOT NULL,
  `NombrePaciente` varchar(100) NOT NULL,
  `Resultado` varchar(255) NOT NULL,
  `Fecha` date NOT NULL,
  `Imagen` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `diagnosticos`
--

INSERT INTO `diagnosticos` (`Id`, `NombrePaciente`, `Resultado`, `Fecha`, `Imagen`) VALUES
(2, 'Pablo', 'prueba1', '0000-00-00', 'imagenes/68320c7913fc5.jpg'),
(3, 'Pablo Monteros', 'Prueba2', '0000-00-00', 'imagenes/68322dd5b831d.jpg'),
(7, 'Juan Perez', '{\"resultado\":[{\"label\":\"seat belt, seatbelt\",\"score\":0.3552480936050415},{\"label\":\"Band Aid\",\"score\":0.3062096834182739},{\"label\":\"sunscreen, sunblock, sun blocker\",\"score\":0.04606546834111214},{\"label\":\"crutch\",\"score\":0.019495155662298203},{\"label\":\"nec', '2025-05-25', 'imagenes/68338a3ade59c.jpg'),
(8, 'Pablo Monteros ', '{\"resultado\":[{\"label\":\"rule, ruler\",\"score\":0.08509143441915512},{\"label\":\"quill, quill pen\",\"score\":0.07457597553730011},{\"label\":\"nematode, nematode worm, roundworm\",\"score\":0.05592126399278641},{\"label\":\"Band Aid\",\"score\":0.04036382585763931},{\"label\"', '2025-05-26', 'imagenes/6835358169c4f.jpg');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `diagnosticos`
--
ALTER TABLE `diagnosticos`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `diagnosticos`
--
ALTER TABLE `diagnosticos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
