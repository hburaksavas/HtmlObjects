using HtmlObjects.DataOperations.DbOperations.Entities;
using System.Collections.Generic;

namespace HtmlObjects.BusinessOperations.DAO
{
    public class UnmatchedFirmDAO
    {
        DataOperations.DbOperations.OsbDBOperations.UnmatchedFirmOperations operation;

        public UnmatchedFirmDAO()
        {
            operation = new DataOperations.DbOperations.OsbDBOperations.UnmatchedFirmOperations(); 
        }

        public List<UnmatchedFirm> getList()
        {
            return operation.getList();
        }

        public void Add(UnmatchedFirm firm )
        {
            if(firm != null)
            {
                if (!operation.isExists(firm))
                {
                    operation.insert(firm);
                }                                                   
            }
        }


        /// Update-Delete metotları..

    }
}
