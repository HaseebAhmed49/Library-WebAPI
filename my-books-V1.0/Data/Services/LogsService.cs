using System;
using System.Collections.Generic;
using System.Linq;
using my_books_V1._0.Data.Models;

namespace my_books_V1._0.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsfromDB() => _context.Logs.ToList(); 
    }
}
