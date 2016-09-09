using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;

namespace HtmlObjects.BusinessOperations.DAO
{
    public class MailAdresDAO
    {
        DataOperations.DbOperations.OsbDBOperations.VFirmaEmailler dbAccessObj;


        public MailAdresDAO()
        {
            dbAccessObj = new DataOperations.DbOperations.OsbDBOperations.VFirmaEmailler();
        }


        public List<Email> GetAllMails()
        {
            return dbAccessObj.getAllInfos();
        }


        public List<Email> GetMailsByMusteriKod(Decimal MusteriKod)
        {
            if (MusteriKod != -1)
            {
                return dbAccessObj.GetEmailByMusteriKod(MusteriKod);
            }else
            {
                return null;
            }
        }

        
        
        /// <summary>
        /// Eksik....
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public string GetMusteriKodByMail(string Mail)
        {
            if (!String.IsNullOrEmpty(Mail))
            {
                return null;
            }else
            {
                return null;
            }
        }
    }
}
