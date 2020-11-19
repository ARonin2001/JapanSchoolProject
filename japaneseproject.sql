-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3307
-- Время создания: Ноя 09 2020 г., 22:02
-- Версия сервера: 10.3.22-MariaDB
-- Версия PHP: 7.1.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `japaneseproject`
--

-- --------------------------------------------------------

--
-- Структура таблицы `admin`
--

CREATE TABLE `admin` (
  `idAdmin` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `surName` varchar(30) NOT NULL,
  `fatherName` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `admin`
--

INSERT INTO `admin` (`idAdmin`, `name`, `surName`, `fatherName`) VALUES
(1, 'Alex', 'Lesly', 'Olegovich'),
(2, 'Uri', 'Puygin', 'Alexandrovich');

-- --------------------------------------------------------

--
-- Структура таблицы `autoriz_adm`
--

CREATE TABLE `autoriz_adm` (
  `idAutoriz_adm` int(11) NOT NULL,
  `log` varchar(45) NOT NULL,
  `par` int(11) NOT NULL,
  `Admin_idAdmin` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `autoriz_adm`
--

INSERT INTO `autoriz_adm` (`idAutoriz_adm`, `log`, `par`, `Admin_idAdmin`) VALUES
(1, '123123', 123123, 1),
(2, 'Hi', 123123, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `autoriz_kl`
--

CREATE TABLE `autoriz_kl` (
  `idAutoriz_kl` int(11) NOT NULL,
  `log` varchar(45) NOT NULL,
  `par` int(11) NOT NULL,
  `customer_idcustomer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `autoriz_men`
--

CREATE TABLE `autoriz_men` (
  `idAutoriz_men` int(11) NOT NULL,
  `log` varchar(45) NOT NULL,
  `par` int(11) NOT NULL,
  `meneger_idmeneger` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `autoriz_men`
--

INSERT INTO `autoriz_men` (`idAutoriz_men`, `log`, `par`, `meneger_idmeneger`) VALUES
(1, '542', 542, 1),
(2, '789', 789, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `autoriz_op`
--

CREATE TABLE `autoriz_op` (
  `idAutoriz_op` int(11) NOT NULL,
  `log` varchar(45) NOT NULL,
  `par` int(11) NOT NULL,
  `operator_idoperator` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `customer`
--

CREATE TABLE `customer` (
  `idcustomer` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `surName` varchar(30) NOT NULL,
  `fatherName` varchar(30) DEFAULT NULL,
  `mail` varchar(30) NOT NULL,
  `phone` varchar(15) NOT NULL,
  `country` varchar(20) DEFAULT NULL,
  `gender` char(1) DEFAULT NULL,
  `city` tinytext DEFAULT NULL,
  `education` tinytext DEFAULT NULL,
  `levelLng` char(1) DEFAULT NULL,
  `service` tinytext DEFAULT NULL,
  `periodJapanStudy` varchar(30) DEFAULT NULL,
  `startStudy` date DEFAULT NULL,
  `dateBirth` date DEFAULT NULL,
  `school_id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `customer`
--

INSERT INTO `customer` (`idcustomer`, `name`, `surName`, `fatherName`, `mail`, `phone`, `country`, `gender`, `city`, `education`, `levelLng`, `service`, `periodJapanStudy`, `startStudy`, `dateBirth`, `school_id`, `order_id`) VALUES
(1, 'Александра', 'Пронина', 'Олеговна', 'kjkl', '56165', 'kjhk', 'w', 'Tokyou', 'lkjl', '5', 'jklj', '4564', '2022-05-20', '2001-11-25', 1, 1),
(2, 'Сергей', 'Куваев', 'Алексеевич', 'mail.com', '234234', 'Russia', 'm', 'Nara', 'sdfsdfdsf', '1', 'Premium', '2', '2020-10-02', '2002-10-23', 1, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `dormitory`
--

CREATE TABLE `dormitory` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `address` varchar(45) NOT NULL,
  `price` int(11) NOT NULL,
  `places` int(11) NOT NULL,
  `img` varchar(45) DEFAULT NULL,
  `school_id` int(11) UNSIGNED ZEROFILL NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `longprogram`
--

CREATE TABLE `longprogram` (
  `id` int(11) NOT NULL,
  `period` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `examinationDocuments` int(11) NOT NULL,
  `entranceFee` int(11) NOT NULL,
  `education` int(11) NOT NULL,
  `culturalProgram` int(11) NOT NULL,
  `InsuranceTrainingMaterials` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `longprogram`
--

INSERT INTO `longprogram` (`id`, `period`, `examinationDocuments`, `entranceFee`, `education`, `culturalProgram`, `InsuranceTrainingMaterials`) VALUES
(1, 'от 1 года', 30000, 70000, 600000, 80000, 60000);

-- --------------------------------------------------------

--
-- Структура таблицы `meneger`
--

CREATE TABLE `meneger` (
  `idmeneger` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `surName` varchar(30) NOT NULL,
  `fatherName` varchar(30) DEFAULT NULL,
  `mail` varchar(45) NOT NULL,
  `phone` varchar(15) NOT NULL,
  `datePost` date NOT NULL,
  `dateOtstavki` date DEFAULT NULL,
  `status` tinytext NOT NULL,
  `polojenie` tinytext NOT NULL,
  `gender` char(1) NOT NULL,
  `dateBirth` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `meneger`
--

INSERT INTO `meneger` (`idmeneger`, `name`, `surName`, `fatherName`, `mail`, `phone`, `datePost`, `dateOtstavki`, `status`, `polojenie`, `gender`, `dateBirth`) VALUES
(1, 'Sanya', 'Who', 'Girls', 'sdfs@gmailc.om', '2342342', '2020-11-01', NULL, 'dfgdf', 'dfgdgf', 'a', '1995-11-17'),
(2, 'Alexandr', 'Pronin', 'Olegovich', 'sdffd@gamil.com', '2423432423', '2020-11-03', NULL, 'Активен', 'Работает', 'm', '1999-01-12');

-- --------------------------------------------------------

--
-- Структура таблицы `operator`
--

CREATE TABLE `operator` (
  `idoperator` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `surName` varchar(30) NOT NULL,
  `fatherName` varchar(30) DEFAULT NULL,
  `mail` varchar(45) NOT NULL,
  `phone` varchar(15) NOT NULL,
  `gender` char(1) NOT NULL,
  `datePost` date NOT NULL,
  `dateOtstavki` date DEFAULT NULL,
  `dateBirth` date NOT NULL,
  `status` tinytext NOT NULL,
  `polojenie` tinytext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `operator`
--

INSERT INTO `operator` (`idoperator`, `name`, `surName`, `fatherName`, `mail`, `phone`, `gender`, `datePost`, `dateOtstavki`, `dateBirth`, `status`, `polojenie`) VALUES
(1, 'Lex', 'Man', 'Jefirson', 'Lex.sdf@mail.ru', '23423423', 'm', '2020-11-03', NULL, '2000-01-01', 'ggff', 'ggff'),
(2, 'Hey', 'Men', 'WhatAreYouDoing', 'sdfds@gmail.com', '32434234', 'm', '2020-11-01', '2020-11-02', '1999-01-14', 'sdfsdf', 'sdfsdf'),
(3, 'i', 'f', 'd', 'sdf', '468485', 'm', '2020-11-01', NULL, '2003-04-05', 'sdf', 'sdf'),
(4, 'dfds', 'erf', 'wfe', 'sdfdsf', '124234', 'w', '2020-11-03', NULL, '2005-03-04', 'sdf', 'sdfd');

-- --------------------------------------------------------

--
-- Структура таблицы `schools`
--

CREATE TABLE `schools` (
  `id` int(11) NOT NULL,
  `name` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `city` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `id_shortprogram` int(11) NOT NULL,
  `id_longprogram` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `schools`
--

INSERT INTO `schools` (`id`, `name`, `city`, `id_shortprogram`, `id_longprogram`) VALUES
(1, 'Аояма', 'Токио', 1, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `shortprogram`
--

CREATE TABLE `shortprogram` (
  `id` int(11) NOT NULL,
  `period` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `examinationDocuments` int(11) NOT NULL,
  `entranceFee` int(11) NOT NULL,
  `education` int(11) NOT NULL,
  `culturalProgram` int(11) NOT NULL,
  `InsuranceTrainingMaterials` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `shortprogram`
--

INSERT INTO `shortprogram` (`id`, `period`, `examinationDocuments`, `entranceFee`, `education`, `culturalProgram`, `InsuranceTrainingMaterials`) VALUES
(1, '3 месяца', 0, 0, 150000, 20000, 15000);

-- --------------------------------------------------------

--
-- Структура таблицы `typeservice`
--

CREATE TABLE `typeservice` (
  `id` int(11) NOT NULL,
  `type` varchar(45) NOT NULL,
  `price` int(11) NOT NULL,
  `description` varchar(3000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `typeservice`
--

INSERT INTO `typeservice` (`id`, `type`, `price`, `description`) VALUES
(1, 'Базовый', 0, 'Последний этап оформления — подача документов на визу в Посольстве или Консульстве Японии в России. Студенты долгосрочной программы подают заполненные анкеты и сертификат на визу от Иммиграции Японии, а краткосрочной — приглашение от школы. MANABO помогают заполнить анкеты и подать документы на визу.'),
(2, 'Стандартный', 30000, '・Услуги базового пакета\r\n・Встреча в аэропорту\r\n・Сопровождение до места проживания и школы\r\n・Оформление договора аренды жилья\r\n・Переходник, карта Suica или Pasmo и карта поездов\r\n\r\nРусскоговорящий сотрудник MANABO встретит вас в аэропорту Японии с табличкой и поможет сориентироваться. Проезд из аэропорта включен в стоимость, поэтому наличные деньги не понадобятся. Сопровождающий расскажет о жизни в Японии и ответит на интересующие вопросы об учебе в школе японского языка.\r\n\r\n*В стоимость пакета включен проезд на поездах и автобусах, цену индивидуального такси уточняйте у менеджера MANABO. \r\n\r\nРусскоговорящий гид проводит вас до места заключения договора на жилье и поможет понять содержание разговора. После заселения студента провожают до школы и показывают дорогу.\r\n\r\nЯпонский переходник, проездной с остатком 1000 иен и карта поездов с отмеченным и местами учебы и проживания передаст сопровождающий во время встречи. Проездной действует на всех линиях поездов, метро и на автобусах. Помимо этого, картой можно оплачивать покупки в магазинах и автоматах с едой и напитками. Пополнить проездной можно на любой станции Японии.'),
(3, 'Премиум', 50000, '・Услуги базового и стандартного пакетов\r\n・Регистрация в мэрии и оформление японской медицинской страховки\r\n・Покупка японской сим-карты\r\n・Открытие счета в японском банке\r\n・Именная печать и 2000 иен наличными\r\n\r\nВсе жители Японии, включая иностранных студентов, регистрируются по месту жительства. Регистрация проходит на японском языке, бланки заполняются тоже на японском. Сотрудники администрации редко говорят по-английски, поэтому русскоговорящий сопровождающий поможет с переводом и заполнением. После регистрации в мэрии студент получает японскую медицинскую страховку, которая покрывает 70% затрат на лечение и лекарства. Стоимость страховки для студента составит около 1500 иен в месяц.\r\n\r\nСопровождающий поможет выбрать оператора, тариф и переведет слова сотрудника магазина во время заключения договора. Для заключения договора необходимо знать японский или английский язык, либо прийти с переводчиком.\r\n\r\nБанковский счет нужен для получения зарплаты с подработки, на него же можно принимать переводы из России. Открыть счет в японском банке без знания языка сложно, к тому же, не все банки принимают иностранных студентов. Сопровождающий поможет обратиться в нужный банк и заполнить все документы.\r\n\r\nПомимо переходника, проездного с остатком 1000 иен и карты поездов, во время встречи в аэропорту сопровождающий передаст именную печать и 2000 иен наличными. Печать в Японии используют вместо подписи, поэтому ее делают даже иностранцы.');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`idAdmin`);

--
-- Индексы таблицы `autoriz_adm`
--
ALTER TABLE `autoriz_adm`
  ADD PRIMARY KEY (`idAutoriz_adm`,`Admin_idAdmin`),
  ADD KEY `fk_Autoriz_adm_Admin1_idx` (`Admin_idAdmin`);

--
-- Индексы таблицы `autoriz_kl`
--
ALTER TABLE `autoriz_kl`
  ADD PRIMARY KEY (`idAutoriz_kl`,`customer_idcustomer`),
  ADD KEY `fk_Autoriz_kl_customer1_idx` (`customer_idcustomer`);

--
-- Индексы таблицы `autoriz_men`
--
ALTER TABLE `autoriz_men`
  ADD PRIMARY KEY (`idAutoriz_men`,`meneger_idmeneger`),
  ADD KEY `fk_Autoriz_men_meneger1_idx` (`meneger_idmeneger`);

--
-- Индексы таблицы `autoriz_op`
--
ALTER TABLE `autoriz_op`
  ADD PRIMARY KEY (`idAutoriz_op`,`operator_idoperator`),
  ADD KEY `fk_Autoriz_op_operator1_idx` (`operator_idoperator`);

--
-- Индексы таблицы `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`idcustomer`,`school_id`,`order_id`),
  ADD KEY `fk_customer_school1_idx` (`school_id`),
  ADD KEY `fk_customer_order1_idx` (`order_id`);

--
-- Индексы таблицы `dormitory`
--
ALTER TABLE `dormitory`
  ADD PRIMARY KEY (`id`,`school_id`),
  ADD KEY `fk_dormitory_school1_idx` (`school_id`);

--
-- Индексы таблицы `longprogram`
--
ALTER TABLE `longprogram`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `meneger`
--
ALTER TABLE `meneger`
  ADD PRIMARY KEY (`idmeneger`);

--
-- Индексы таблицы `operator`
--
ALTER TABLE `operator`
  ADD PRIMARY KEY (`idoperator`);

--
-- Индексы таблицы `schools`
--
ALTER TABLE `schools`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `shortprogram`
--
ALTER TABLE `shortprogram`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `typeservice`
--
ALTER TABLE `typeservice`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `autoriz_adm`
--
ALTER TABLE `autoriz_adm`
  MODIFY `idAutoriz_adm` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `autoriz_kl`
--
ALTER TABLE `autoriz_kl`
  MODIFY `idAutoriz_kl` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `autoriz_men`
--
ALTER TABLE `autoriz_men`
  MODIFY `idAutoriz_men` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `autoriz_op`
--
ALTER TABLE `autoriz_op`
  MODIFY `idAutoriz_op` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `customer`
--
ALTER TABLE `customer`
  MODIFY `idcustomer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `dormitory`
--
ALTER TABLE `dormitory`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `longprogram`
--
ALTER TABLE `longprogram`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `meneger`
--
ALTER TABLE `meneger`
  MODIFY `idmeneger` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `operator`
--
ALTER TABLE `operator`
  MODIFY `idoperator` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `schools`
--
ALTER TABLE `schools`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `shortprogram`
--
ALTER TABLE `shortprogram`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `typeservice`
--
ALTER TABLE `typeservice`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `autoriz_adm`
--
ALTER TABLE `autoriz_adm`
  ADD CONSTRAINT `fk_Autoriz_adm_Admin1` FOREIGN KEY (`Admin_idAdmin`) REFERENCES `admin` (`idAdmin`);

--
-- Ограничения внешнего ключа таблицы `autoriz_kl`
--
ALTER TABLE `autoriz_kl`
  ADD CONSTRAINT `fk_Autoriz_kl_customer1` FOREIGN KEY (`customer_idcustomer`) REFERENCES `customer` (`idcustomer`);

--
-- Ограничения внешнего ключа таблицы `autoriz_men`
--
ALTER TABLE `autoriz_men`
  ADD CONSTRAINT `fk_Autoriz_men_meneger1` FOREIGN KEY (`meneger_idmeneger`) REFERENCES `meneger` (`idmeneger`);

--
-- Ограничения внешнего ключа таблицы `autoriz_op`
--
ALTER TABLE `autoriz_op`
  ADD CONSTRAINT `fk_Autoriz_op_operator1` FOREIGN KEY (`operator_idoperator`) REFERENCES `operator` (`idoperator`);

--
-- Ограничения внешнего ключа таблицы `customer`
--
ALTER TABLE `customer`
  ADD CONSTRAINT `fk_customer_order1` FOREIGN KEY (`order_id`) REFERENCES `typeservice` (`id`);

--
-- Ограничения внешнего ключа таблицы `dormitory`
--
ALTER TABLE `dormitory`
  ADD CONSTRAINT `fk_dormitory_school1` FOREIGN KEY (`school_id`) REFERENCES `school` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
