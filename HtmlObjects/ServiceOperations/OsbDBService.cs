using HtmlObjects.BusinessOperations.DAO;
using HtmlObjects.DataOperations.DbOperations.Entities;

namespace HtmlObjects.ServiceOperations
{
    public class OsbDBService
    {

        /// <summary>
        /// Verilen UnmatchedFirm nesnesini OSBDB.UnmatchedFirm tablosuna kayıt eder
        /// </summary>
        /// <param name="firm"></param>
        public void SaveData(UnmatchedFirm firm)
        {
            UnmatchedFirmDAO dao = new UnmatchedFirmDAO();
            dao.Add(firm);
        }

        /// <summary>
        /// Verilen MatchedFirm nesnesini OSBDB.MatchedFirm tablosuna kayıt eder
        /// </summary>
        /// <param name="firm"></param>
        public void SaveData(MatchedFirm firm)
        {
            MatchedFirmDAO dao = new MatchedFirmDAO();
            dao.Add(firm);
        }

    }
}
