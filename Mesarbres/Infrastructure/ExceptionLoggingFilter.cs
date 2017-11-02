﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mesarbres.Models;
using System.Net.Mail;
using System.Configuration;
namespace Mesarbres.Infrastructure
{
    public class ExceptionLoggingFilter : FilterAttribute, IExceptionFilter
    {
        private arbredb _db = new arbredb();

        public void OnException(ExceptionContext filterContext)
        {
            // send ajax response
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        Message = " Une erreur est survenu. Veuillez essayer plus tard.",
                    }
                };
            }
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.ExceptionHandled = true;


            // Enregistrer erreur dans BD table ErrorLog
            var _Context = DependencyResolver.Current.GetService<arbredb>();
            var error = new ErrorLog()
            {
                message = filterContext.Exception.Message,
                stacktrace = filterContext.Exception.StackTrace,
                controllername = filterContext.Controller.GetType().Name,
                targetresult = filterContext.Result.GetType().Name,
                sessionud = (string)filterContext.HttpContext.Request["LoanId"],
                useragent = filterContext.HttpContext.Request.UserAgent,
                datetime = DateTime.Now
            };
             _db.Errors.Add(error);
             _db.SaveChanges();
           
            // send an email notification
            //MailMessage email = new MailMessage();
            //email.From = new MailAddress("simojc@gmil.com");
            //email.To.Add(new MailAddress(ConfigurationManager.AppSettings["ErrorMail"]));
            //email.Subject = "Une erreur est survenue lors de l'exécution de l'application";
            //email.Body = filterContext.Exception.Message + Environment.NewLine + filterContext.Exception.StackTrace;
            //SmtpClient client = new SmtpClient("localhost");
            //client.Send(email);

            //throw new NotImplementedException(); 


        }
    }
}