using System;
using System.Linq;
using System.Reflection;

namespace SimpleBackend.WebApi.Helpers.Extensions
{
    /// <summary>
    /// Расширение для перечисления
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Расширения для получения атрибутов перечислений
        /// </summary>
        /// <param name="value">Перечисление у которого необходимо взять атрибут</param>
        /// <typeparam name="TAttribute">Необходимый тип атрибута</typeparam>
        /// <returns>Запрашиваем атрибут</returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
            => value.GetType().GetMember(value.ToString()).FirstOrDefault()?.GetCustomAttribute<TAttribute>();
    }
}