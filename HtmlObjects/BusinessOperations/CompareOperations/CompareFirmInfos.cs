using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;

namespace HtmlObjects.BusinessOperations.CompareOperations
{
    public class CompareFirmInfos
    {

        /// <summary>
        ///  eşleşmezse -1 , eşleşirse müsteriKodu döner
        /// </summary>
        /// <param name="telNumber"></param>
        /// <param name="telefonList"></param>
        /// <returns></returns>
        public Decimal ComparePhoneNumber(string telNumber,ref List<TelNo> telefonList)
        {
             
            if(telNumber != null && telefonList != null)
            {
               
                foreach (var item in telefonList)
                {
                    if(item.TelefonNumarasi != null)
                    {

                        if (item.TelefonNumarasi.Equals(telNumber))
                        {
                            return item.MusteriKod;
                        }
                    } 
                }
            }


            return -1;
        }


    }
}
