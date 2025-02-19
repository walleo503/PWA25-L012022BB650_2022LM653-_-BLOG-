using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Laboratorio.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions <> options) : base (options)
        {
        
        }
    }
}
