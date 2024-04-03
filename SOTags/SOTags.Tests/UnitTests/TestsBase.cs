using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOTags.DataAccess.Entities;

namespace SOTags.Tests.UnitTests
{
    public class TestsBase
    {
        public void ClearDatabase(DbContext dbContext)
        {
            var allEntities = dbContext.Set<Tag>().ToList();
            dbContext.Set<Tag>().RemoveRange(allEntities);
            dbContext.SaveChanges();
        }
    }
}
