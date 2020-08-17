using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLibrary.Data.EF.EF
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OnlineLibraryContext>
    {
        private string _connectionString
        {
            get
            {
                return "data source=localhost;initial catalog=OnlineLibrary;Integrated Security=True;";
            }
        }
        public OnlineLibraryContext CreateDbContext(string[] args)
        {
            return new OnlineLibraryContext(_connectionString);
        }

        public OnlineLibraryContext CreateDbContext()
        {
            return new OnlineLibraryContext(_connectionString);
        }
    }
}
