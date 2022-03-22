using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace SimpleBackend.WebApi.Models.Tokens
{
    /// <summary>
    /// Хранилище токенов
    /// </summary>
    public sealed class TokenStorageService
    {
        private readonly Dictionary<int, TokenPair> _storage;
        private readonly ILogger<TokenStorageService> _logger;
        private readonly object _lockObject;
        private readonly bool _isThreadSafe;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="threadSafe">Флаг безопасности для потоков</param>
        /// <param name="logger">Журнал логирования ошибок</param>
        public TokenStorageService(bool threadSafe = false, ILogger<TokenStorageService> logger = null)
        {
            _storage = new Dictionary<int, TokenPair>();
            _logger = logger;
            _lockObject = new object();
            _isThreadSafe = threadSafe;
        }

        /// <summary>
        /// Добавление токена в хранилище
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Токен</param>
        /// <param name="endTime">Дата окончания действия токена</param>
        /// <exception cref="ArgumentNullException">Отсутвует токен</exception>
        /// <exception cref="Exception">Не удалось записать токен в хранилище</exception>
        public void AddToken(int userId, string token, DateTime endTime)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "Отсутвует токен");

            var tokenData = new TokenPair() { Token = token, EndTime = endTime };
            var result = _isThreadSafe
                ? ThreadSafeAddToken(_storage, userId, tokenData)
                : SimpleAddToken(_storage, userId, tokenData);
            if (result)
                return;
            var exception = new Exception("Не удалось добавить данные токена");
            _logger?.LogError(exception, "Не удалось добавить данные токена");
            throw exception;
        }

        /// <summary>
        /// Простое добавление токена
        /// </summary>
        /// <param name="storage">хранилище токенов</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="tokenData">Данные токена</param>
        /// <returns>Результат выполнения</returns>
        private bool SimpleAddToken(Dictionary<int, TokenPair> storage, int userId, TokenPair tokenData)
        {
            _storage.Add(userId, tokenData);
            return true;
        }

        /// <summary>
        /// Безопасное добавление токена
        /// </summary>
        /// <param name="storage">хранилище токенов</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="tokenData">Данные токена</param>
        /// <returns>Результат выполнения</returns>
        private bool ThreadSafeAddToken(Dictionary<int, TokenPair> storage, int userId, TokenPair tokenData)
        {
            var tryCounter = 0;
            do
            {
                if (Monitor.TryEnter(_lockObject, 10))
                {
                    SimpleAddToken(storage, userId, tokenData);
                    Monitor.Exit(_lockObject);
                    break;
                }

                tryCounter++;
            } while (tryCounter < 15);

            return tryCounter < 15;
        }

        /// <summary>
        /// Валидация токена
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Токен пользователя</param>
        /// <returns>Результат валидации токена</returns>
        public bool ValidateToken(int userId, string token)
            => _isThreadSafe
                ? ThreadSaveValidateToken(_storage, userId, token)
                : SimpleValidateToken(_storage, userId, token);


        /// <summary>
        /// Простая валидация токена
        /// </summary>
        /// <param name="storage">Хранилище токенов</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Токен пользователя</param>
        /// <returns>Результат валидации.</returns>
        private bool SimpleValidateToken(Dictionary<int, TokenPair> storage, int userId, string token)
        {
            if (!storage.ContainsKey(userId))
                return false;
            var savedToken = storage[userId];
            return savedToken.Token.Equals(token) & (savedToken.EndTime > DateTime.Now);
        }

        /// <summary>
        /// Потокобезопасная валидация токена
        /// </summary>
        /// <param name="storage">Хранилище токенов</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Токен пользователя</param>
        /// <returns>Результат валидации.</returns>
        private bool ThreadSaveValidateToken(Dictionary<int, TokenPair> storage, int userId, string token)
        {
            var tryCounter = 0;
            var validationResult = false;
            do
            {
                if (Monitor.TryEnter(_lockObject, 10))
                {
                    validationResult = SimpleValidateToken(storage, userId, token);
                    Monitor.Exit(_lockObject);
                    break;
                }
                tryCounter++;
            } while (tryCounter < 15);

            return validationResult && tryCounter < 15;
        }

        /// <summary>
        /// Удаление токена из списка
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <exception cref="Exception">Не удалось удалить токен пользователя</exception>
        public void DeleteToken(int userId)
        {
            var result = _isThreadSafe
                ? ThreadSafeDeleteToken(_storage, userId)
                : SimpleDeleteToken(_storage, userId);
            if (result) return;
            var exception = new Exception($"Не удалось удалить токен пользователя {userId}");
            _logger?.LogError(exception, "Не удалось удалить токен пользователя");
            throw exception;
        }

        /// <summary>
        /// Простое удаление токена
        /// </summary>
        /// <param name="storage">Хранилище токенов</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результат выполнения операции</returns>
        private bool SimpleDeleteToken(Dictionary<int, TokenPair> storage, int userId)
        {
            if (!storage.ContainsKey(userId))
                return true;
            storage.Remove(userId);
            return true;
        }

        /// <summary>
        /// Потокобезопаное удаление токена
        /// </summary>
        /// <param name="storage">Хранилище токенов</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результат выполнения операции</returns>
        private bool ThreadSafeDeleteToken(Dictionary<int, TokenPair> storage, int userId)
        {
            var tryCounter = 0;
            do
            {
                if (Monitor.TryEnter(_lockObject, 10))
                {
                    SimpleDeleteToken(storage, userId);
                    Monitor.Exit(_lockObject);
                    break;
                }

                tryCounter++;
            } while (tryCounter < 15);

            return tryCounter < 15;
        }
    }
}