﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.Core.Helper
{
    /*
        https://stud-work.ru/maska-fajla-s-pomoshchyu-regulyarnykh-vyrazhenij

        System.Text.RegularExpresion - пространство имен, где хранятся нужные методы для работы с Regex'ами.

        МЕТАСИМВОЛЫ - это символы для составления шаблона поиска.

        \d - определяет символы цифр.
        \D - определяет любой символ, который не является цифрой.
        \w - определяет любой символ цифры, буквы или подчеркивания.
        \W - определяет любой символ, который не является цифрой, буквой или подчеркиванием.
        \s - определяет любой непечатный символ, включая пробел.
        \S - определяет любой символ, кроме символов табуляции, новой строки и возврата каретки.
         . - определяет любой символ, кроме символа новой строки.
        \. - определяет символ точки.

        КВАНТИФИКАТОРЫ - это символы, которые определяют где и сколько раз необходимое вхождение символов может встречаться.

        ^ - с начала строки.
        $ - с конца строки.
        + - одно или более вхождений подшаблона в строке.
        | - символ для указания вариантов шаблона (ИЛИ)
        * - ноль или более вхождений подшаблона в строке.
        {} - количество элементов.
        ?<переменная> - переменная в регулярном выражении

        ПРИМЕРЫ:

        var regex = new Regex(@"^\d*\D+\d+$);
        bool res = regex.IsMatch("123"); ( false )
        bool res = regex.IsMatch("abc123"); ( true )

        Regex.Replace("test123aaa4x5x6bbb789ccc",   // исходная строка
            @"\d+",                                 // шаблон (одна или более цифр
            " ");                                   // символ для замены ( пробел )

        Regex.Replace("02/05/1982",                                     // исходная строка
            @"(?<месяц>\d{1,2})\/(?<день>\d{1,2})\/(?<год>\d{2,4})"     // шаблон ( в скобках: в переменную месяц вставляется цифры (1 или 2), в переменную день - тоже самое, в переменную год - цифры 2 или 4 )
            "${день}-${месяц}-${год}");                                 // собираем переменные через дефис ( получается 02-05-1982 )

        Regex.Replace("123456",                             // исходная строка
            @"\d",                                          // шаблон
            m => (int.Parse(m.Value) + 1).ToString());      // функция изменения совпадения ( каждую цифру увеличивает на 1 )

        ПРИМЕР КОЛЛЕКЦИИ

        string input = "";
        input += "<a href='http://site111.com'>Home-page</a>";
        input += "<a href='http://site222.com'>Другая страница</a>";
        input += "<a href='https://site333.com'>Еще одна другая страница</a>";
        input += "<a href='https://site444.com'>Совсем другая страница</a>";
        input += "<a href='http://site555.com'>Немного другая страница</a>";

        regex = new Regex(@"href='(?<link>\S+)'>(?<text>\S+)</a>");

    
    */

    public class RegexEx
    {

    }
}