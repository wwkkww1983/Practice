using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Data.Repository
{
    public class RepositoryFactory
    {
        public Repository BaseRepository()
        {
            return new Repository(DbFactory.Base());
        }
    }
}
