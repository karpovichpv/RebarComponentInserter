using System;
using System.Collections.Generic;
using Tekla.Structures.Datatype;
using Tekla.Structures.Geometry3d;
using Distance = Tekla.Structures.Datatype.Distance;

namespace RebarComponentInserter.Extensions
{
    internal static class InserterHelpers
    {
        /// <summary>
        /// Проверяет требуется ли создавать каркас
        /// </summary>
        /// <param name="spacingType">Тип шага</param>
        /// <param name="totalCount">Количество каркасов</param>
        /// <param name="currentNumber">Текущий номер каркаса</param>
        /// <returns>Необходимо ли создавать каркас в этой точке</returns>
        public static bool CheckIfCreationNeeded(SpacingType spacingType, int totalCount, int currentNumber)
        {
            bool isFirstExcluded = spacingType == SpacingType.FirstFlexibleExclude && currentNumber == 0;
            bool isLastExcluded = spacingType == SpacingType.LastFlexibleExclude && currentNumber == totalCount;

            return !isFirstExcluded && !isLastExcluded;
        }

        /// <summary>
        /// Список шагов вставки элементов-каркасов
        /// </summary>
        /// <param name="data">Данные для построения плагинов</param>
        /// <param name="aStartPoint">Точка началао распределения диапазона</param>
        /// <param name="aEndPoint">Точка окончания распределения диапазона</param>
        /// <returns>Массив шагов</returns>
        public static Distance[] ConvertToDistanceList(
            PluginData data,
            Point aStartPoint,
            Point aEndPoint)
        {
            SpacingType spacingType = ConvertSpacingType(data.SpacingType);
            Distance[] rawDistanceList = [.. DistanceList.Parse(data.Spacings)];

            if (spacingType != SpacingType.DistanceList)
            {
                List<Distance> listResult = [];
                double distance = aStartPoint.GetDistance(aEndPoint) - (data.OffsetFromEnd * 2);
                double firstSpacing = rawDistanceList[0].Value;
                int count = (int)Math.Ceiling(distance / firstSpacing);
                double flexibleSpacing = distance - (firstSpacing * (count - 1));

                for (int i = 0; i <= count; i++)
                {
                    if (i == 0)
                        continue;
                    else if (CheckIfNeedToAddFlexibleSpacing(spacingType, count, i))
                        listResult.Add(new Distance(flexibleSpacing));
                    else
                        listResult.Add(new Distance(firstSpacing));
                }

                return [.. listResult];
            }

            return rawDistanceList;
        }

        /// <summary>
        /// Отдаёт тип шага по int из формы
        /// </summary>
        /// <param name="spacingTypeInt">int из формы</param>
        /// <returns>Тип шага</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SpacingType ConvertSpacingType(int spacingTypeInt)
        {
            return spacingTypeInt switch
            {
                0 => SpacingType.FirstFlexibleExist,
                1 => SpacingType.LastFlexibleExist,
                2 => SpacingType.FirstFlexibleExclude,
                3 => SpacingType.LastFlexibleExclude,
                4 => SpacingType.DistanceList,
                _ => throw new NotImplementedException("Тип раскладки шага не поддерживается"),
            };
        }

        private static bool CheckIfNeedToAddFlexibleSpacing(SpacingType spacingType, int count, int i)
        {
            bool isFirstFlexible =
                i == 1
                && (
                    spacingType == SpacingType.FirstFlexibleExist
                    || spacingType == SpacingType.FirstFlexibleExclude
                );
            bool isLastFlexible =
                i == count
                && (
                    spacingType == SpacingType.LastFlexibleExclude
                    || spacingType == SpacingType.LastFlexibleExist
                );
            return isFirstFlexible || isLastFlexible;
        }
    }
}