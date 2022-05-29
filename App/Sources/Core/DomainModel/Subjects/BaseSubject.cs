using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AndrK.ZavPostav.DomainModel
{
    /// <summary>
    /// Базовый класс для субъектов
    /// </summary>
    public class BaseSubject : ISubject, IStorable
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public BaseSubject()
        {
            this._id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
        protected Guid _id;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        protected string _name;

        /// <summary>
        /// Строковое представление
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Subject: Type={MethodBase.GetCurrentMethod().DeclaringType}: Name={Name}";
        }

    }
}
