using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AndrK.ZavPostav.DomainModel;
using AndrK.ZavPostav.BusinessLogic2;
using AndrK.ZavPostav.REST.Models;
using System.Web;

namespace AndrK.ZavPostav.REST.Controllers
{
    public class SubjectsController : ApiController
    {
        #region поля
        HttpResponseMessage response;
        SubjectProcess sp
        {
            get
            {
                if (_sp == null)
                {
                    var ctx = HttpContext.Current;
                    if (ctx != null && ctx.Cache["SubjRep"] != null)
                    {
                        _sp = ctx.Cache["SubjRep"] as SubjectProcess;
                        if (_sp == null)
                        {
                            _sp = new SubjectProcess(new MsSqlRepository.Repository());
                            //_sp = new SubjectProcess(new MokeRepository.Repository());
                        }
                    }
                    else
                    {
                        _sp = new SubjectProcess(new MsSqlRepository.Repository());
                        //_sp = new SubjectProcess(new MokeRepository.Repository());
                    }
                    ctx.Cache["SubjRep"] = _sp;
                }
                return _sp;
            }
        }
        SubjectProcess _sp;
        #endregion

        #region GET Get(mode,id)
        /// <summary>
        /// Получить список субъектов
        /// </summary>
        /// <param name="mode">Тип субъекта</param>
        /// <param name="id">Id</param>
        /// <returns>Результат запроса</returns>
        [HttpGet]
        public HttpResponseMessage Get(string mode = "", string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(mode))
                    response = "mode={zakazchik|seller}, id={null|guid}".ErrResponse(this);
                else
                {
                    if (mode.ToLower() == "zakazchik")
                    {
                        IList<Zakazchik> ret;
                        if (string.IsNullOrEmpty(id))
                            ret = sp.GetZakazchikList();
                        else
                            ret = new List<Zakazchik> { sp.GetZakazchikById(new Guid(id)) };
                        if (!object.Equals(ret, null))
                            response = Request.CreateResponse<IList<Zakazchik>>(HttpStatusCode.OK, ret);
                    }
                    else if (mode.ToLower() == "seller")
                    {
                        IList<Seller> ret;
                        if (string.IsNullOrEmpty(id))
                            ret = sp.GetSellerList();
                        else
                            ret = new List<Seller> { sp.GetSellerById(new Guid(id)) };
                        if (!object.Equals(ret, null))
                            response = Request.CreateResponse<IList<Seller>>(HttpStatusCode.OK, ret);
                    }
                    else
                        response = (new NotImplementedException($"Для mode=\"{mode}\" нет реализации")).ErrResponse(this);
                }
            }
            catch (Exception ex)
            {
                response = ex.ErrResponse(this);
            }
            return response;
        }
        #endregion

        #region POST Add/Modify(mode,subject)
        /// <summary>
        /// Добавить изменить
        /// </summary>
        /// <param name="mode">Режим</param>
        /// <param name="subject">Субъект</param>
        /// <returns>Результат</returns>
        [HttpPost]
        public HttpResponseMessage Merge(string mode, SubjectContent subject)
        {
            try
            {
                string subjType = subject.SubjectType;
                string objType = "";
                if (subjType.ToLower() == "zakazchik")
                    objType = "заказчик";
                else if (subjType.ToLower() == "seller")
                    objType = "продавец";
                else
                    throw new NotImplementedException($"Для SubjectType=\"{subjType}\" нет реализации");
                if (subject.Subject == null)
                    throw new ArgumentException($"Не указан {objType}");
                if (mode.ToLower() == "add")
                {
                    Guid? ret = null;
                    if (objType == "заказчик") ret = sp.AddZakazchik(new Zakazchik { Id = subject.Subject.Id, Name = subject.Subject.Name });
                    else if (objType == "продавец") ret = sp.AddSeller(new Seller { Id = subject.Subject.Id, Name = subject.Subject.Name });
                    if (ret != null)
                        response = $"Успешно вставлено id=\"{ret}\"".SuccessResponse(this);
                    else
                        response = $"Запись с id=\"{subject.Subject.Id}\" уже присутствует".ErrResponse(this, 2);
                }
                else if (mode.ToLower() == "update" || mode.ToLower() == "modify" || mode.ToLower() == "edit")
                    return Modify(subject);
            }
            catch (Exception ex)
            {
                response = ex.ErrResponse(this);
            }
            return response;
        }
        #endregion

        #region PUT Modify(subject)
        /// <summary>
        /// Добавить изменить
        /// </summary>
        /// <param name="mode">Режим</param>
        /// <param name="subject">Субъект</param>
        /// <returns>Результат</returns>
        [HttpPut]
        public HttpResponseMessage Modify(SubjectContent subject)
        {
            try
            {
                string subjType = subject.SubjectType;
                string objType = "";
                if (subjType.ToLower() == "zakazchik")
                    objType = "заказчик";
                else if (subjType.ToLower() == "seller")
                    objType = "продавец";
                else
                    throw new NotImplementedException($"Для SubjectType=\"{subjType}\" нет реализации");
                if (subject.Subject == null)
                    throw new ArgumentException($"Не указан {objType}");
                Guid? ret = null;
                if (objType == "заказчик") ret = sp.ModifyZakazchik(new Zakazchik { Id = subject.Subject.Id, Name = subject.Subject.Name });
                else if (objType == "продавец") ret = sp.ModifySeller(new Seller { Id = subject.Subject.Id, Name = subject.Subject.Name });
                if (ret != null)
                    response = $"Успешно изменено".SuccessResponse(this);
                else
                    response = $"Запись с id=\"{subject.Subject.Id}\" не найдена".ErrResponse(this, 2);
            }
            catch (Exception ex)
            {
                response = ex.ErrResponse(this);
            }
            return response;
        }
        #endregion

        #region Delete Get(mode,id)
        /// <summary>
        /// Удалить субъект
        /// </summary>
        /// <param name="mode">Тип субъекта</param>
        /// <param name="id">Id</param>
        /// <returns>Результат запроса</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(string mode = "", string id = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("Не указан Id");
                if (string.IsNullOrWhiteSpace(mode))
                    throw new ArgumentException("Не указан тип удаляемого объекта");
                if (mode.ToLower() == "zakazchik")
                {
                    Zakazchik ret = sp.GetZakazchikById(new Guid(id));
                    if (ret == null)
                        response = $"Заказчик с Id=\"{id}\" не найден".ErrResponse(this, 2);
                    else
                    {
                        sp.RemoveZakazchik(ref ret);
                        response = $"Заказчик с Id=\"{id}\" удален".SuccessResponse(this);
                    }
                }
                else if (mode.ToLower() == "seller")
                {
                    Seller ret = sp.GetSellerById(new Guid(id));
                    if (ret == null)
                        response = $"Поставщик с Id=\"{id}\" не найден".ErrResponse(this, 2);
                    else
                    {
                        sp.RemoveSeller(ref ret);
                        response = $"Поставщик с Id=\"{id}\" удален".SuccessResponse(this);
                    }
                }
                else
                    response = (new NotImplementedException($"Для mode=\"{mode}\" нет реализации")).ErrResponse(this);
            }
            catch (Exception ex)
            {
                response = ex.ErrResponse(this);
            }
            return response;
        }
        #endregion
    }
}
