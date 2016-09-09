using HtmlObjects.DataOperations.DbOperations.Entities;
using System.Collections.Generic;

namespace HtmlObjects.BusinessOperations.DAO
{
    public class MatchedFirmDAO
    {
        DataOperations.DbOperations.OsbDBOperations.MatchedFirmOperations operation;

        public MatchedFirmDAO()
        {
            operation = new DataOperations.DbOperations.OsbDBOperations.MatchedFirmOperations();
        }

        public List<MatchedFirm> getList()
        {

            return operation.getList();
            
        }

        public void Add( MatchedFirm firm )
        {
            if(firm != null)
            {
                if (!operation.isExists(firm))
                {
                    operation.insert(firm);

                }
            }           
        }

    }
}
