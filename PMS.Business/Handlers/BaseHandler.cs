using PMS.DataAccess.Db;

namespace PMS.Business.Handlers
{
    public abstract class BaseHandler
    {
        internal PmsContext DbContext { get; set; }

        public BaseHandler(PmsContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
