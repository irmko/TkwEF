using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TkwEF.Core
{
    public static partial class UtilityCore
    {
        public static class ByFilter
        {
            const char bracketLeft = '(', bracketRight = ')';
            /// <summary>
            /// Разделитель
            /// </summary>
            public enum Separator { AND, OR }
            /// <summary>
            /// Тип фильтрации
            /// </summary>
            public enum FilterMode
            {
                None, Like, Not_Like, Equals, Not_Equals, Bolshe, Menshe, BolsheRavno, MensheRavno
            }

            /// <summary>
            /// Парсит строку фильтра
            /// </summary>
            /// <param name="filter">Строка фильтра</param>
            /// <param name="getOneResult">Если True, то возврашаем после первого вхождения</param>
            /// <returns></returns>
            public static List<string> GetArrayBySeparator(string filter, Separator separator, bool getOneResult = false)
            {
                const string vOR = " OR ", vAND = " AND ";
                //int iLeft = 0, iRight = 0;//Количество левых и правых скобок
                int curPosOR = 0, curPosBracketLeft = 0, curPosBracketRight = 0; // Текущая позиция OR и скобкок
                int posOR = 0, posBracket = 0; // Последняя позиция OR и скобки
                int posLeft = -1, posRigth = -1; // Текущая проверяемая позиция

                string vSeparator = vOR;
                if (separator == Separator.AND)
                    vSeparator = vAND;

                List<string> result = new List<string>();
                while (posLeft < filter.Length && ((getOneResult && result.Count == 0) || !getOneResult))
                {
                    if (posLeft == -1) posLeft = 0;
                    curPosOR = filter.IndexOf(vSeparator, posLeft);
                    curPosBracketLeft = filter.IndexOf(bracketLeft, posLeft);

                    if (curPosBracketLeft != -1 && ((curPosOR != -1 && curPosBracketLeft < curPosOR) || (curPosOR == -1)))
                        curPosOR = -1;
                    else if (curPosOR != -1 && ((curPosBracketLeft != -1 && curPosOR < curPosBracketLeft) || (curPosBracketLeft == -1)))
                        curPosBracketLeft = -1;

                    if (curPosOR != -1) // OR
                    {
                        if (curPosOR == 0 && !getOneResult)
                        {
                            posLeft += vSeparator.Length;
                            curPosOR = filter.IndexOf(vSeparator, posLeft);
                        }
                        posRigth = curPosOR;
                        _addInResult(filter, posLeft, posRigth, result);

                        if (curPosOR != -1 && !getOneResult)
                        {
                            posOR = curPosOR; // Зафиксировали позицию OR
                            posLeft = curPosOR + vSeparator.Length;
                        }
                    }
                    else if (curPosBracketLeft != -1) // Скобка
                    {
                        curPosBracketRight = GetPositionBracket(filter, curPosBracketLeft);
                        if (curPosBracketRight == -1) return null;
                        posRigth = curPosBracketRight + 1;
                        List<string> toNextOR = GetArrayBySeparator(filter.Substring(posRigth), separator, true);
                        posRigth += toNextOR.Count == 0 ? 0 : toNextOR[0].Length; //Добавляем количество знаков
                        posBracket = curPosBracketLeft;

                        //result.Add(filter.Substring(posLeft == -1 ? 0 : posLeft, posRigth - posLeft));
                        if (curPosBracketLeft == 0 && curPosBracketRight == filter.Length - 1)
                            result = GetArrayBySeparator(filter.Substring(1, filter.Length - 2), separator, getOneResult);
                        else if (posLeft == curPosBracketLeft && posRigth - 1 == curPosBracketRight)
                            _addInResult(filter, posLeft + 1, curPosBracketRight, result);
                        else
                            _addInResult(filter, posLeft, posRigth, result);
                        //if (toNextOR.Count != 0 && toNextOR[0].Trim().Length > 0)
                        //    result.Add(toNextOR[0]);
                        posLeft = posRigth + vSeparator.Length;
                    }
                    else if (curPosBracketLeft == -1 && curPosOR == -1)
                    {
                        if (posLeft == -1)
                        {
                            result.Add(filter);
                            return result;
                        }
                        //else if (curPosBracketRight != -1)
                        //{
                        //    result.Add(filter.Substring(posBracket + 1, filter.Length - posBracket - 2));
                        //    curPosBracketRight = -1;
                        //}
                        else
                        {
                            result.Add(filter.Substring(posLeft, filter.Length - posLeft));
                            posLeft += filter.Length - posLeft;
                        }
                    }
                }

                return result;
            }
            /// <summary>
            /// Добавление к результату
            /// </summary>
            /// <param name="filter"></param>
            /// <param name="posLeft"></param>
            /// <param name="posRigth"></param>
            /// <param name="result"></param>
            private static void _addInResult(string filter, int posLeft, int posRigth, List<string> result)
            {
                result.Add(filter.Substring(posLeft == -1 ? 0 : posLeft, posRigth - (posLeft == -1 ? 0 : posLeft)));
            }
            /// <summary>
            /// Возвращает позицию парной скобки
            /// </summary>
            /// <param name="filter"></param>
            /// <param name="posBegin"></param>
            /// <param name="isLeftBracket"></param>
            /// <returns></returns>
            public static int GetPositionBracket(string filter, int posBegin, bool isLeftBracket = true)
            {
                int iLeft = 0, iRight = 0;//Количество левых и правых скобок
                int curPosLeft = 0, curPosRight = 0; // Текущая позиция левой и правой скобки
                int posLeft = -1; // Позиция левой и правой скобки
                int pos = -1; // Текущая проверяемая позиция

                if (isLeftBracket)
                {
                    //Проверим что на данной позиции находится левая скобка
                    pos = filter.IndexOf(bracketLeft, posBegin);
                    if (pos == -1)
                        return -1;
                    iLeft = 1;
                    while (!(curPosLeft == -1 && curPosRight == -1) && pos + 1 < filter.Length)
                    {
                        curPosLeft = filter.IndexOf(bracketLeft, pos + 1);
                        curPosRight = filter.IndexOf(bracketRight, pos + 1);
                        if (curPosLeft != -1 && curPosLeft < (curPosRight == -1 ? filter.Length : curPosRight))
                        {
                            iLeft++;
                            pos = curPosLeft;
                            if (posLeft == -1)
                                posLeft = pos;
                        }
                        else if (curPosRight != -1)
                        {
                            iRight++;
                            pos = curPosRight;
                        }
                        if (iLeft == iRight)
                        {
                            //posRight = curPosRight;
                            return pos;
                            //posLeft = posRight = -1;
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                return -1;
            }
            /// <summary>
            /// Возвращает логический результат по куску фильтра OR
            /// </summary>
            /// <param name="filterOR"></param>
            /// <returns></returns>
            public static bool GetResultByOR(string filterOR)
            {
                //bool result = false;
                List<string> arr = GetArrayBySeparator(filterOR, Separator.OR);
                foreach (string a in arr)
                {
                    List<string> inArr = GetArrayBySeparator(a, Separator.OR);
                    if (inArr.Count > 1)
                    {
                        foreach (string inA in inArr)
                        {
                            if (GetResultByOR(inA))
                            {
                                inArr = null;
                                return true;
                            }
                        }
                    }
                    inArr = null;
                }
                return false;
            }
            /// <summary>
            /// Возвращает режим сравнивания
            /// </summary>
            /// <param name="filterString"></param>
            /// <param name="fMode"></param>
            /// <param name="equalBegin"></param>
            /// <param name="equalsPos"></param>
            /// <param name="equalZnak"></param>
            public static void GetFilterItem(string filterString, ref FilterMode fMode, ref int equalBegin, ref int equalsPos, ref int equalZnak)
            {
                if (filterString.IndexOf('=', equalBegin) > -1)
                {
                    fMode = FilterMode.Equals;
                    //equalsPos = filterString.Substring(0, filterString.IndexOf('=')).Trim().Length + "=".Length;
                    equalsPos = filterString.IndexOf('=', equalBegin);
                    equalZnak = "=".Length;
                }
                else if (filterString.IndexOf("NOT LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    fMode = FilterMode.Not_Like;
                    equalsPos = filterString.IndexOf("NOT LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase);
                    equalZnak = "NOT LIKE".Length;
                }
                else if (filterString.IndexOf("LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    fMode = FilterMode.Like;
                    equalsPos = filterString.IndexOf("LIKE", equalBegin, StringComparison.CurrentCultureIgnoreCase);
                    equalZnak = "Like".Length;
                }
                else if (filterString.IndexOf("<>", equalBegin) > -1)
                {
                    fMode = FilterMode.Not_Equals;
                    equalsPos = filterString.Substring(0, filterString.IndexOf("<>", equalBegin)).Length;
                    equalZnak = "<>".Length;
                }
                else if (filterString.IndexOf("<=", equalBegin) > -1)
                {
                    fMode = FilterMode.MensheRavno;
                    equalsPos = filterString.Substring(0, filterString.IndexOf("<=", equalBegin)).Length;
                    equalZnak = "<=".Length;
                }
                else if (filterString.IndexOf(">=", equalBegin) > -1)
                {
                    fMode = FilterMode.BolsheRavno;
                    equalsPos = filterString.Substring(0, filterString.IndexOf(">=", equalBegin)).Length;
                    equalZnak = ">=".Length;
                }
                else if (filterString.IndexOf("<", equalBegin) > -1)
                {
                    fMode = FilterMode.Menshe;
                    equalsPos = filterString.Substring(0, filterString.IndexOf("<", equalBegin)).Length;
                    equalZnak = "<".Length;
                }
                else if (filterString.IndexOf(">", equalBegin) > -1)
                {
                    fMode = FilterMode.Bolshe;
                    equalsPos = filterString.Substring(0, filterString.IndexOf(">", equalBegin)).Length;
                    equalZnak = ">".Length;
                }
                else
                {
                    fMode = FilterMode.Equals;
                    equalsPos = 0;
                    equalZnak = 0;
                    equalBegin = 0;
                }
                //equalEnd = filterString.ToUpper().IndexOf("OR");
                //if (equalEnd == -1)
                //    equalEnd = filterString.ToUpper().IndexOf("AND");
            }
        }
    }
}
