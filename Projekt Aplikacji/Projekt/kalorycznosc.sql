-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 30 Sty 2020, 18:23
-- Wersja serwera: 10.1.26-MariaDB
-- Wersja PHP: 7.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `kalorycznosc`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `kalorie`
--

CREATE TABLE `kalorie` (
  `nazwa` varchar(40) NOT NULL,
  `kcal` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Zrzut danych tabeli `kalorie`
--

INSERT INTO `kalorie` (`nazwa`, `kcal`) VALUES
('Agrest', 41),
('Agrest', 41),
('Ananas', 41),
('Any?', 337),
('Baleron', 244),
('Banan', 95),
('Boczek w?dzony', 477),
('Chleb pszenny', 257),
('Chrzan', 67),
('Czekolada gorzka', 554),
('Dynia', 28),
('Grahamka', 252),
('Kalafior', 22),
('chleb', 120),
('Mieso', 50),
('Kalafior', 250);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
